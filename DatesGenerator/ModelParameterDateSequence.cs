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
        private int version = 2;

        /// <summary>
        /// Backing field for the EndDate property.
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// Backing field for the EndDate property.
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// Backing field for the StartDateExpression property.
        /// </summary>
        private string startDateExpression;

        /// <summary>
        /// Backing field for the StartDateExpression property.
        /// </summary>
        private string endDateExpression;

        /// <summary>
        /// Backing field for the Frequency property.
        /// </summary>
        private DateFrequency frequency;

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
        public string StartDateExpression
        {
            get
            {
                DateTime tmp;
                if (DateTime.TryParseExact(startDateExpression, "yyyy-MM-dd", null, DateTimeStyles.None, out tmp))

                    return tmp.ToShortDateString();

                return startDateExpression;
            }
            set
            {
                DateTime tmp;
                if (DateTime.TryParse(value, out tmp))
                    startDateExpression = tmp.ToString("yyyy-MM-dd");
                else
                    startDateExpression = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the start date has to be excluded.
        /// </summary>
        public bool ExcludeStartDate { get; set; }

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
        public string EndDateExpression
        {
            get
            {
                DateTime tmp;
                if (DateTime.TryParseExact(endDateExpression, "yyyy-MM-dd", null, DateTimeStyles.None, out tmp))
                    return tmp.ToShortDateString();

                return endDateExpression;
            }
            set
            {
                DateTime tmp;
                if (DateTime.TryParse(value, out tmp))
                    endDateExpression = tmp.ToString("yyyy-MM-dd");
                else
                    endDateExpression = value;
            }
        }

        /// <summary>
        /// Gets or sets the frequency of the dates generated between the start and end dates.
        /// </summary>
        public DateFrequency Frequency {
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
        /// Gets or sets the expression representing the date frequency.
        /// </summary>
        public string FrequencyExpression { get; set; }

        /// <summary>
        /// Gets or sets the expression that generates the object.
        /// </summary>
        public override Array Expr
        {
            get
            {
                object[,] retValue = new object[3, 1];
                retValue[0, 0] = StartDateExpression;
                retValue[1, 0] = EndDateExpression;
                retValue[2, 0] = FrequencyExpression;
                return retValue;
            }

            set
            {
                try
                {
                    StartDateExpression = (string)value.GetValue(0, 0);
                    EndDateExpression = (string)value.GetValue(1, 0);
                    FrequencyExpression = (string)value.GetValue(2, 0);
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
                    // Introduction of ExludeStartDate
                    ExcludeStartDate = info.GetBoolean("_ExcludeStartDate");
                }
                else
                {
                    ExcludeStartDate = false;
                }

                #endregion // No expressions
            }
            else
            {
                DateTime tmp;

                startDateExpression = info.GetString("_StartDateExpression");
                if (DateTime.TryParse(startDateExpression, out tmp))
                    StartDateExpression = tmp.ToShortDateString();

                endDateExpression = info.GetString("_EndDateExpression");
                if (DateTime.TryParse(endDateExpression, out tmp))
                    EndDateExpression = tmp.ToShortDateString();

                FrequencyExpression = info.GetString("_FrequencyExpression");
                ExcludeStartDate = info.GetBoolean("_ExcludeStartDate");
            }
        }

        #endregion // Constructor

        #region Overrided methods
        /// <summary>
        /// Parses the object.
        /// </summary>
        /// <param name="p_Context">The project in which to parse the object.</param>
        /// <returns>true if an error occurred during the parsing, false otherwise.</returns>
        public override bool Parse(IProject p_Context)
        {
            if (InitializeObject(p_Context as Project))
                return true;

            if (Validation())
            {
                List<RightValue> dates = new List<RightValue>();
                RightValue rv;
                int i = 0;

                if (StartDate != EndDate)
                {
                    // Add the dates from the designated start date adding the interval each time
                    DateTime tempDate = StartDate;
                    if (ExcludeStartDate)
                        tempDate = AddPeriod(Frequency, StartDate, ++i);

                    while (tempDate.CompareTo(EndDate) < 0)
                    {
                        rv = RightValue.ConvertFrom(tempDate, true);

                        dates.Add(rv);
                        tempDate = AddPeriod(Frequency, StartDate, ++i);
                    }
                }

                // Add the last date
                rv = RightValue.ConvertFrom(EndDate, true);

                dates.Add(rv);

                // Set the model parameter array values
                this.Values = dates;
                return base.Parse(p_Context);
            }
            else
                return true;
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

            info.AddValue("_StartDateExpression", this.startDateExpression);
            info.AddValue("_EndDateExpression", this.endDateExpression);
            info.AddValue("_FrequencyExpression", this.FrequencyExpression);
            info.AddValue("_ExcludeStartDate", ExcludeStartDate);
            info.AddValue("_VersionDateSequence", version);
        }

        /// <summary>
        /// Gets the kind of data represented by this object.
        /// </summary>
        /// <returns>The kind of data represented.</returns>
        public override string GetStringKind()
        {
            return "Date Sequence";
        }
        #endregion // Overridden methods

        #region Helper methods
        /// <summary>
        /// Validates the data of the object.
        /// </summary>
        /// <returns>true if the data has been successfully validated, false otherwise.</returns>
        private bool Validation()
        {
            // The start date has to be smaller than the end date
            return this.StartDate.CompareTo(EndDate) <= 0;
        }

        /// <summary>
        /// Adds a certain number of time to the specified date. The amount of time to be
        /// added is based on the value of the frequency and the number of periods to
        /// add to the start date.
        /// </summary>
        /// <param name="frequency">The frequency of the dates generation.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="periods">The number of periods to add to the start date.</param>
        /// <returns>The DateTime where the specified number of periods has been added to
        /// the start date.</returns>
        private DateTime AddPeriod(DateFrequency frequency, DateTime startDate, int periods)
        {
            switch (frequency)
            {
                case DateFrequency.Daily:
                    return startDate.AddDays(periods);
                case DateFrequency.Weekly:
                    return startDate.AddDays(7 * periods);
                case DateFrequency.BiWeekly:
                    return startDate.AddDays(14 * periods);
                case DateFrequency.Monthly:
                    return startDate.AddMonths(periods);
                case DateFrequency.Quarterly:
                    return startDate.AddMonths(3 * periods);
                case DateFrequency.Semiannual:
                    return startDate.AddMonths(6 * periods);
                case DateFrequency.Annual:
                    return startDate.AddYears(periods);
                default:
                    return startDate;
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
            catch(Exception ex)
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
                startDate = GetDate(context as Project, StartDateExpression);
            }
            catch (Exception ex)
            {
                context.AddError("Start date is not valid. Details: " + ex.Message);
                return true;
            }

            try
            {
                endDate = GetDate(context as Project, EndDateExpression);
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

            return false;
        }

        #endregion // Helper methods

        #region IExportableContainer Members

        /// <summary>
        /// Gets the objects exported from this parameter.
        /// </summary>
        /// <param name="recursive">True if export has to be recursive; otherwise false.</param>
        /// <returns>A list of the objects exported from this parameter.</returns>
        public List<IExportable> ExportObjects(bool recursive)
        {
            List<IExportable> retVal = new List<IExportable>();
            ExportablePropertyAssociator<string> startDate = new ExportablePropertyAssociator<string>("StartDateExpression", this, VarName + " Start Date");
            ExportablePropertyAssociator<string> endDate = new ExportablePropertyAssociator<string>("EndDateExpression", this, VarName + " End Date");
            ExportablePropertyAssociator<string> frequency = new ExportablePropertyAssociator<string>("FrequencyExpression", this, VarName + " Frequency", typeof(DateFrequency));
            retVal.AddRange(new IExportable[] { this, startDate, endDate, frequency });

            return retVal;
        }

        #endregion
    }
}
