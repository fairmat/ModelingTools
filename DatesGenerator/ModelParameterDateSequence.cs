/* Copyright (C) 2012 Fairmat SRL (info@fairmat.com, http://www.fairmat.com/)
 * Author(s): Francesco Biondi (francesco.biondi@fairmat.com)
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using DVPLDOM;
using DVPLI;
using DVPLUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace DatesGenerator
{
    /// <summary>
    /// Implements a model parameter that represents a sequence of dates.
    /// </summary>
    [Serializable]
    public class ModelParameterDateSequence : ModelParameterArray, IExportableContainer
    {
        #region Fields

        /// <summary>
        /// The version of the ModelParameterDateSequence object.
        /// </summary>
        [NonSerialized]
        private int version = 7;

        /// <summary>
        /// Backing field for the EndDate property.
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// Backing field for the EndDate property.
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// Backing field for the Frequency property.
        /// </summary>
        private DateFrequency frequency;

        /// <summary>
        /// The numerical value of the number of periods to skip (truncated to int)
        /// </summary>
        private int skipPeriodsParsed;

        /// <summary>
        /// The numerical value of the number of periods to skip from the vector reference (truncated to int)
        /// </summary>
        private int skipPeriodsArrayParsed;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
                StartDateExpression = value.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets or sets the expression of the start date.
        /// </summary>
        public ModelParameter StartDateExpression { get; set; }

        /// <summary>
        /// Gets or sets the number of periods to skip during the generation (as an expression).
        /// </summary>
        public ModelParameter SkipPeriods { get; set; }

        /// <summary>
        /// Gets or sets the vector reference (if date sequence is not uniform).
        /// </summary>
        public string VectorReferenceExpr { get; set; }

        /// <summary>
        /// Gets or sets the number of periods to skip from the vector reference (as an expression).
        /// </summary>
        public ModelParameter SkipPeriodsArray { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
                EndDateExpression = value.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets or sets the expression of the end date.
        /// </summary>
        public ModelParameter EndDateExpression { get; set; }

        /// <summary>
        /// Gets or sets the frequency of the dates generated between the start and end dates.
        /// </summary>
        public DateFrequency Frequency
        {
            get
            {
                return this.frequency;
            }
            set
            {
                this.frequency = value;
                FrequencyExpression = DateFrequencyUtility.StringRepresentation(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the frequency has to be followed strictly.
        /// </summary>
        public bool FollowFrequency { get; set; }

        /// <summary>
        /// Gets or sets the expression representing the date frequency.
        /// </summary>
        public string FrequencyExpression { get; set; }

        /// <summary>
        /// Gets or sets the expression representing the date frequency during the export.
        /// </summary>
        public string FrequencyExpressionExport
        {
            get
            {
                try
                {
                    DateFrequency frequency = DateFrequencyUtility.ParseDateFrequency(FrequencyExpression);
                    return frequency.ToString();
                }
                catch
                {
                    return FrequencyExpression;
                }
            }
            set
            {
                try
                {
                    DateFrequency frequency = DateFrequencyUtility.ParseDateFrequency(value);
                    FrequencyExpression = frequency.StringRepresentation();
                }
                catch
                {
                    FrequencyExpression = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the sequence has to be generated from
        /// the start date (otherwise the sequence will be generated from the end date).
        /// </summary>
        public bool GenerateSequenceFromStartDate { get; set; }

        /// <summary>
        /// Gets or sets the expression that generates the object.
        /// </summary>
        public override Array Expr
        {
            get
            {
                object[,] retValue = new object[4, 1];
                retValue[0, 0] = StartDateExpression.Expr.GetValue(0, 0);
                retValue[1, 0] = EndDateExpression.Expr.GetValue(0, 0);
                retValue[2, 0] = FrequencyExpression;
                retValue[3, 0] = SkipPeriods.Expr.GetValue(0, 0);
                return retValue;
            }

            set
            {
                try
                {
                    StartDateExpression = (ModelParameter)value.GetValue(0, 0).ToString();
                    EndDateExpression = (ModelParameter)value.GetValue(1, 0).ToString();
                    FrequencyExpression = (string)value.GetValue(2, 0);
                    SkipPeriods = (ModelParameter)value.GetValue(3, 0).ToString();
                }
                catch
                {
                    StartDate = DateTime.Now;
                    EndDate = DateTime.Now;
                    Frequency = DateFrequency.Daily;
                }
            }
        }

        /// <summary>
        /// Gets the array of dates generated in the sequence.
        /// </summary>
        public override Array Val
        {
            get
            {
                return Values == null ? null : Values.ToArray();
            }
        }

        #endregion // Properties

        #region Constructor

        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="frequency">The frequency of the dates generated.</param>
        public ModelParameterDateSequence(DateTime startDate, DateTime endDate, DateFrequency frequency)
        {
            StartDate = startDate;
            EndDate = endDate;
            Frequency = frequency;
            GenerateSequenceFromStartDate = true;
            FollowFrequency = true;
            SkipPeriods = new ModelParameter((double)0);
            SkipPeriodsArray = new ModelParameter((double)0);
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="startDateExpression">The expression for the start date.</param>
        /// <param name="endDateExpression">The expression for the end date.</param>
        /// <param name="frequencyExpression">The expression for the frequency.</param>
        public ModelParameterDateSequence(string startDateExpression, string endDateExpression, string frequencyExpression)
        {
            StartDateExpression = startDateExpression;
            EndDateExpression = endDateExpression;
            FrequencyExpression = frequencyExpression;
            GenerateSequenceFromStartDate = true;
            FollowFrequency = true;
            SkipPeriods = new ModelParameter((double)0);
            SkipPeriodsArray = new ModelParameter((double)0);
        }

        /// <summary>
        /// Initializes the object based on the serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data.</param>
        /// <param name="context">The StreamingContext that contains contextual
        /// information about the source.</param>
        public ModelParameterDateSequence(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Set the default for GenerateSequenceFromStartDate and FollowFrequency
            GenerateSequenceFromStartDate = true;
            FollowFrequency = true;

            int serialializedVersion;
            try
            {
                serialializedVersion = info.GetInt32("_VersionDateSequence");
            }
            catch
            {
                serialializedVersion = 0;
            }

            if (serialializedVersion < 2)
            {
                #region No expressions
                try
                {
                    this.StartDate = new DateTime(info.GetInt64("_StartDate"));
                    this.EndDate = new DateTime(info.GetInt64("_EndDate"));
                }
                catch (Exception)
                {
                    // In case the calibration time was serialized in a different way.
                    this.StartDate = info.GetDateTime("_StartDate");
                    this.EndDate = info.GetDateTime("_EndDate");
                }

                int frequency = info.GetInt32("_Frequency");
                Array enumValues = Enum.GetValues(typeof(DateFrequency));
                foreach (DateFrequency value in enumValues)
                {
                    if ((int)value == frequency)
                    {
                        Frequency = value;
                        break;
                    }
                }

                if (serialializedVersion >= 1)
                {
                    // Introduction of ExcludeStartDate
                    bool exclude = info.GetBoolean("_ExcludeStartDate");
                    SkipPeriods = exclude ? new ModelParameter(1) : new ModelParameter((double)0);
                }
                else
                {
                    SkipPeriods = new ModelParameter((double)0);
                }

                #endregion // No expressions
            }
            else
            {
                if (serialializedVersion < 4)
                {
                    string tmpS = info.GetString("_StartDateExpression");
                    DateTime tmpD;
                    if (DateTime.TryParseExact(tmpS, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out tmpD))
                        StartDate = tmpD;
                    else
                        StartDateExpression = new ModelParameter(tmpS);

                    tmpS = info.GetString("_EndDateExpression");
                    if (DateTime.TryParseExact(tmpS, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out tmpD))
                        EndDate = tmpD;
                    else
                        EndDateExpression = new ModelParameter(tmpS);
                }
                else
                {
                    StartDateExpression = (ModelParameter)info.GetValue("_StartDateExpression", typeof(ModelParameter));
                    EndDateExpression = (ModelParameter)info.GetValue("_EndDateExpression", typeof(ModelParameter));
                    SetupExportedIDs();
                }

                FrequencyExpression = info.GetString("_FrequencyExpression");

                if (serialializedVersion >= 3)
                {
                    // FollowFrequency and GenerateSequenceFromStartDate in version 3
                    FollowFrequency = info.GetBoolean("_FollowFrequency");
                    GenerateSequenceFromStartDate = info.GetBoolean("_GenerateSequenceFromStartDate");
                }

                // Skip Periods introduced in version 5
                if (serialializedVersion < 5)
                {
                    bool exclude = info.GetBoolean("_ExcludeStartDate");
                    SkipPeriods = exclude ? new ModelParameter(1) : new ModelParameter((double)0);
                }
                else
                {
                    SkipPeriods = (ModelParameter)info.GetValue("_SkipPeriods", typeof(ModelParameter));
                }

                if (serialializedVersion < 6)
                {
                    VectorReferenceExpr = string.Empty;
                }
                else
                {
                    VectorReferenceExpr = info.GetString("_VectorReferenceExpr");
                    SkipPeriodsArray = ObjectSerialization.GetValue<ModelParameter>(info, "_SkipPeriodsArray", new ModelParameter(0.0));
                }
            }
        }

        #endregion // Constructor

        #region Overrided methods

        /// <summary>
        /// Parse the Vector References and checks data consistency.
        /// </summary>
        /// <param name="p_Context"></param>
        /// <returns>True if there are errors, false otherwise.</returns>
        private bool ParseVectorReferences(IProject p_Context)
        {
            ModelParameterArray[] references = GetVectorRef();
            for (int i = 0; i < references.Length; i++)
            {
                var reference = references[i];
                if (i == 0)
                {
                    this.Values.AddRange(reference.Values.Skip(skipPeriodsArrayParsed));
                }
                else
                {
                    this.Values.AddRange(reference.Values);
                }
            }

            var datesArray = this.Values.Select(x => x as RightValueDate).ToArray();
            for (int index = 1; index < datesArray.Length; index++)
            {
                var previousDate = datesArray[index - 1]?.m_Date;
                var date = datesArray[index]?.m_Date;

                if (date == null || previousDate == null)
                {
                    p_Context.AddError($"{VectorReferenceExpr} some values are not dates.");
                    return true;
                }

                bool areDuplicatesOrNotSorted = previousDate.Value >= date.Value;
                if (areDuplicatesOrNotSorted)
                {
                    string errorMessage = $"{VectorReferenceExpr} has duplicated or unordered dates.";
                    p_Context.AddError(errorMessage);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Parses the object.
        /// </summary>
        /// <param name="p_Context">The project in which to parse the object.</param>
        /// <returns>true if an error occurred during the parsing, false otherwise.</returns>
        public override bool Parse(IProject p_Context)
        {
            if (StartDateExpression.Parse(p_Context))
                return true;

            if (EndDateExpression.Parse(p_Context))
                return true;

            if (SkipPeriods.Parse(p_Context))
                return true;

            if (InitializeObject(p_Context as Project))
                return true;

            if (Validation(p_Context))
            {
                if (ValidVectorRef())
                {
                    if (SkipPeriodsArray.Parse(p_Context))
                    {
                        return true;
                    }
                    this.Values = new List<RightValue>();
                    return ParseVectorReferences(p_Context);
                }
                else
                {
                    List<RightValue> dates;
                    if (GenerateSequenceFromStartDate)
                        dates = GenerateForward();
                    else
                        dates = GenerateBackward();

                    if (dates.Count == 0)
                        return true;

                    // Set the model parameter array values
                    this.Values = dates;
                    return base.Parse(p_Context);
                }
            }

            return true;
        }

        /// <summary>
        /// Generates the list of dates from the start date.
        /// </summary>
        /// <returns>The generated dates list.</returns>
        private List<RightValue> GenerateForward()
        {
            List<RightValue> dates = new List<RightValue>();
            int i = 0;
            RightValue rv;

            // Add the dates from the designated start date adding the interval each time
            DateTime tempDate = StartDate;
            int datesSkipped = 0;

            while (tempDate.CompareTo(EndDate) < 0)
            {
                if (datesSkipped < skipPeriodsParsed)
                {
                    datesSkipped++;
                }
                else
                {
                    rv = RightValue.ConvertFrom(tempDate, true);
                    dates.Add(rv);
                }

                tempDate = AddPeriod(Frequency, StartDate, ++i, GenerateSequenceFromStartDate);
            }

            if (tempDate.Equals(EndDate))
            {
                // The end date follow the frequency scheme so no need to control it
                dates.Add(RightValue.ConvertFrom(EndDate, true));
            }
            else
            {
                if (FollowFrequency)
                {
                    // Follow the frequency schema strictly
                    dates.Add(RightValue.ConvertFrom(EndDate, true));
                }
                else
                {
                    // The end date doesn't follow the frequency schema so replace the
                    // last current date genrated with the last date of the sequence
                    DateTime startMinPeriod = new DateTime(tempDate.Year, tempDate.Month, 1);
                    DateTime endMinPeriod = AddPeriod(Frequency, startMinPeriod, 1, true);
                    if (EndDate.CompareTo(startMinPeriod) >= 0 &&
                        EndDate.CompareTo(endMinPeriod) < 0)
                    {
                        // Add the date since there is no date for this period in the sequence
                        dates.Add(RightValue.ConvertFrom(EndDate, true));
                    }
                    else
                    {
                        // A date is already present for the period so replace the last date
                        dates[dates.Count - 1] = RightValue.ConvertFrom(EndDate, true);
                    }
                }
            }

            return dates;
        }

        /// <summary>
        /// Generates the list of dates from the end date.
        /// </summary>
        /// <returns>The generated dates list.</returns>
        private List<RightValue> GenerateBackward()
        {
            List<RightValue> dates = new List<RightValue>();
            RightValue rv;
            int i = 0;

            // Add the dates from the designated end date adding the interval each time
            DateTime tempDate = EndDate;
            while (tempDate.CompareTo(StartDate) > 0)
            {
                rv = RightValue.ConvertFrom(tempDate, true);

                dates.Add(rv);
                tempDate = AddPeriod(Frequency, EndDate, ++i, GenerateSequenceFromStartDate);
            }

            if (tempDate.Equals(StartDate))
            {
                // The end date follow the frequency scheme so no need to control it
                dates.Add(RightValue.ConvertFrom(StartDate, true));
            }
            else
            {
                // The end date doesn't follow the frequency schema so replace the
                // last current date generated with the last date of the sequence
                DateTime startMinPeriod = new DateTime(tempDate.Year, tempDate.Month, 1);
                DateTime endMinPeriod = AddPeriod(Frequency, startMinPeriod, 1, true);
                if (StartDate.CompareTo(startMinPeriod) >= 0 &&
                    StartDate.CompareTo(endMinPeriod) < 0)
                {
                    if (FollowFrequency)
                    {
                        // Follow the frequency schema strictly
                        dates.Add(RightValue.ConvertFrom(tempDate, true));
                    }
                    else
                    {
                        // Add the start date since the sequence can be generated loosely
                        dates.Add(RightValue.ConvertFrom(StartDate, true));
                    }
                }
                else
                {
                    if (!FollowFrequency)
                    {
                        // Follow the frequency schema strictly
                        dates.Add(RightValue.ConvertFrom(StartDate, true));
                    }
                }
            }

            dates.Reverse();
            if (dates.Count > skipPeriodsParsed)
                dates = dates.Skip(skipPeriodsParsed).ToList();

            return dates;
        }

        /// <summary>
        /// Initializes the serialization information of the current object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The StreamingContext that contains contextual information
        /// about the destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_StartDateExpression", StartDateExpression);
            info.AddValue("_EndDateExpression", EndDateExpression);
            info.AddValue("_FrequencyExpression", this.FrequencyExpression);
            info.AddValue("_SkipPeriods", SkipPeriods);
            info.AddValue("_FollowFrequency", FollowFrequency);
            info.AddValue("_GenerateSequenceFromStartDate", GenerateSequenceFromStartDate);
            info.AddValue("_VectorReferenceExpr", VectorReferenceExpr);
            info.AddValue("_SkipPeriodsArray", SkipPeriodsArray);
            info.AddValue("_VersionDateSequence", version);
        }

        /// <summary>
        /// Gets the kind of data represented by this object.
        /// </summary>
        /// <returns>The kind of data represented.</returns>
        public override string GetStringKind()
        {
            return "Dates Sequence";
        }

        #endregion // Overridden methods

        #region Helper methods

        private ModelParameterArray[] GetVectorRef()
        {
            var arrayReference = Engine.Parser.EvaluateAsReference(this.VectorReferenceExpr);
            var multipleReference = arrayReference as object[];
            var singleReference = arrayReference as ModelParameterArray;
            ModelParameterArray[] toReturn = null;
            if (multipleReference != null)
            {
                toReturn = multipleReference.Select(x => x as ModelParameterArray).ToArray();
            }
            else
            {
                toReturn = new ModelParameterArray[] { singleReference };
            }

            return toReturn;
        }

        private bool ValidVectorRef()
        {
            bool existVectorReference = !string.IsNullOrEmpty(this.VectorReferenceExpr);
            if (!existVectorReference)
            {
                return false;
            }

            ModelParameterArray[] vectorRef = GetVectorRef();
            bool areReferencesOk = !Engine.Parser.GetParserError() &&
                                   !vectorRef.Any(x => x as ModelParameterArray == null || x.Values.Count == 0);
            return areReferencesOk;
        }

        /// <summary>
        /// Validates the data of the object.
        /// </summary>
        /// <returns>true if the data has been successfully validated, false otherwise.</returns>
        private bool Validation(IProject p_Context)
        {
            if (ValidVectorRef())
            {
                return true;
            }
            else
            {
                // The start date must antecede than the end date
                bool valid = this.StartDate.CompareTo(EndDate) <= 0;
                if (!valid && p_Context != null)
                {
                    p_Context.AddError("Date Sequence " + VarName + " is not valid: 'Start Date' must antecede 'End Date'");
                }

                return valid;
            }
        }

        /// <summary>
        /// Adds a certain number of time to the specified date. The amount of time to be
        /// added is based on the value of the frequency and the number of periods to
        /// add to the start date.
        /// </summary>
        /// <param name="frequency">The frequency of the dates generation.</param>
        /// <param name="date">The reference date.</param>
        /// <param name="periods">The number of periods to add to the start date.</param>
        /// <param name="add">A value indicating if the period has to be added.</param>
        /// <returns>The DateTime where the specified number of periods has been added to
        /// the start date.</returns>
        public DateTime AddPeriod(DateFrequency frequency, DateTime date, int periods, bool add)
        {
            periods = add ? periods : -periods;
            switch (frequency)
            {
                case DateFrequency.Daily:
                    return date.AddDays(periods);
                case DateFrequency.Weekly:
                    return date.AddDays(7 * periods);
                case DateFrequency.BiWeekly:
                    return date.AddDays(14 * periods);
                case DateFrequency.Monthly:
                    return date.AddMonths(periods);
                case DateFrequency.EveryTwoMonths:
                    return date.AddMonths(2 * periods);
                case DateFrequency.Quarterly:
                    return date.AddMonths(3 * periods);
                case DateFrequency.ThreePerAnnum:
                    return date.AddMonths(4 * periods);
                case DateFrequency.Semiannual:
                    return date.AddMonths(6 * periods);
                case DateFrequency.Annual:
                    return date.AddYears(periods);
                case DateFrequency.EveryTwoYears:
                    return date.AddYears(2 * periods);
                case DateFrequency.EveryThreeYears:
                    return date.AddYears(3 * periods);
                default:
                    return date;
            }
        }

        /// <summary>
        /// Parses the given expression in order to find the date represented by the expression.
        /// </summary>
        /// <param name="project">The object to use as context for the expression.</param>
        /// <param name="expression">The expression to parse.</param>
        /// <returns>The date represented by the given expression.</returns>
        private DateTime GetDate(Project project, string expression)
        {
            if (project == null)
                throw new Exception("Invalid context");

            DateTime retVal;
            if (DateTime.TryParse(expression, out retVal))
                return retVal;

            try
            {
                int expId = Engine.Parser.Parse(expression);
                double dateAsDouble = Engine.Parser.Evaluate(expId);
                return project.GetDate(dateAsDouble);
            }
            catch (Exception ex)
            {
                throw new Exception(expression + " is not valid expression.", ex);
            }
        }

        /// <summary>
        /// Parses the given expression in order to find the frequency represented by the expression.
        /// </summary>
        /// <param name="project">The object to use as context for the expression.</param>
        /// <param name="expression">The expression to parse.</param>
        /// <returns>The frequency represented by the given expression.</returns>
        private DateFrequency GetDateFrequency(Project project, string expression)
        {
            if (project == null)
                throw new Exception("Invalid context");

            try
            {
                return DateFrequencyUtility.ParseDateFrequency(expression);
            }
            catch
            {
                int expId = Engine.Parser.Parse(expression);
                double frequencyAsDouble = Engine.Parser.Evaluate(expId);
                if ((int)frequencyAsDouble == frequencyAsDouble)
                {
                    int enumAsInt = (int)frequencyAsDouble;
                    int[] enumValues = (int[])Enum.GetValues(typeof(DateFrequency));
                    for (int i = 0; i < enumValues.Length; i++)
                    {
                        if ((int)enumValues[i] == enumAsInt)
                            return (DateFrequency)enumValues[i];
                    }
                }
            }

            throw new Exception(expression + " is not a valid frequency");
        }

        /// <summary>
        /// Initializes the object based on the current expressions using the given object as
        /// context.
        /// </summary>
        /// <param name="context">The context for the expressions.</param>
        /// <returns>false if the initialization is unsuccessfull; otherwise false.</returns>
        private bool InitializeObject(Project context)
        {
            try
            {
                startDate = GetDate(context as Project, StartDateExpression.Expr.GetValue(0, 0).ToString()).Date;
            }
            catch (Exception ex)
            {
                context.AddError("Start date is not valid. Details: " + ex.Message);
                return true;
            }

            try
            {
                endDate = GetDate(context as Project, EndDateExpression.Expr.GetValue(0, 0).ToString()).Date;
            }
            catch (Exception ex)
            {
                context.AddError("End date is not valid. Details: " + ex.Message);
                return true;
            }

            try
            {
                frequency = GetDateFrequency((Project)context, FrequencyExpression);
            }
            catch (Exception ex)
            {
                context.AddError("Frequency is not valid. Details: " + ex.Message);
                return true;
            }

            try
            {
                skipPeriodsParsed = (int)SkipPeriods.V();
            }
            catch (Exception ex)
            {
                context.AddError("The number of periods to skip is not valid. Details: " + ex.Message);
                return true;
            }

            try
            {
                double v = SkipPeriodsArray?.V() ?? 0.0f;
                skipPeriodsArrayParsed = (int)v;
            }
            catch (Exception ex)
            {
                context.AddError("The number of periods to skip is not valid. Details: " + ex.Message);
                return true;
            }

            return false;
        }

        #endregion // Helper methods

        #region IExportableContainer Members

        /// <summary>
        /// Gets the objects exported from this parameter.
        /// </summary>
        /// <param name="recursive">True if export has to be recursive; otherwise false.</param>
        /// <returns>A list of the objects exported from this parameter.</returns>
        public override List<IExportable> ExportObjects(bool recursive)
        {
            List<IExportable> retVal = new List<IExportable>();
            ExportablePropertyAssociator<string> frequency = new ExportablePropertyAssociator<string>("FrequencyExpressionExport", this, "Frequency", typeof(DateFrequency));
            StartDateExpression.Description = "Start Date";
            EndDateExpression.Description = "End Date";
            SetupExportedIDs();
            retVal.AddRange(new IExportable[] { this, StartDateExpression, EndDateExpression, frequency });
            return retVal;
        }
        #endregion

        /// <summary>
        /// Ids for exported objects must be univoque
        /// </summary>
        void SetupExportedIDs()
        {
            // ToDo: set an object name only if the object is going to be published, otherwise set to empty string.
            StartDateExpression.Name = VarName + "StartDate";
            EndDateExpression.Name = VarName + "EndDate";
        }

        /// <summary>
        /// Parses the object for preview purposes.
        /// </summary>
        /// <param name="p_Context">The project in which to parse the object.</param>
        /// <param name="minDate">The minimum date of the preview.</param>
        /// <param name="maxDate">The maximum date of the preview.</param>
        /// <returns>true if an error occurred during the parsing, false otherwise.</returns>
        public bool ParsePreview(IProject p_Context, DateTime minDate, DateTime maxDate)
        {
            if (InitializeObject(p_Context as Project))
                return true;

            if (Validation(p_Context))
            {
                if (ValidVectorRef())
                {
                    this.Values = new List<RightValue>();
                    return ParseVectorReferences(p_Context);
                }
                else
                {
                    List<RightValue> dates;

                    if (StartDate >= minDate &&
                        EndDate <= maxDate)
                    {

                        if (GenerateSequenceFromStartDate)
                            dates = GenerateForward();
                        else
                            dates = GenerateBackward();

                        if (dates.Count == 0)
                            return true;
                    }
                    else
                    {
                        dates = new List<RightValue>();
                    }

                    // Set the model parameter array values
                    this.Values = dates;
                    return base.Parse(p_Context);
                }
            }

            return true;
        }
    }
}
