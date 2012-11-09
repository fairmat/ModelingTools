﻿/* Copyright (C) 2012 Fairmat SRL (info@fairmat.com, http://www.fairmat.com/)
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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DVPLDOM;
using DVPLI;

namespace DatesGenerator
{
    /// <summary>
    /// Implements a model parameter that represents a sequence of dates.
    /// </summary>
    [Serializable]
    public class ModelParameterDateSequence : ModelParameterArray
    {
        #region Fields
        /// <summary>
        /// The version of the ModelParameterDateSequence object.
        /// </summary>
        [NonSerialized]
        private int version = 1;
        #endregion // Fields

        #region Properties
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the start date has to be excluded.
        /// </summary>
        public bool ExcludeStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the frequency of the dates generated between the start and end dates.
        /// </summary>
        public DateFrequency Frequency { get; set; }

        /// <summary>
        /// Gets or sets the expression that generates the object.
        /// </summary>
        public override Array Expr
        {
            get
            {
                object[,] retValue = new object[3, 1];
                retValue[0, 0] = StartDate;
                retValue[1, 0] = EndDate;
                retValue[2, 0] = (int)this.Frequency;
                return retValue;
            }

            set
            {
                try
                {
                    StartDate = (DateTime)value.GetValue(0, 0);
                    EndDate = (DateTime)value.GetValue(1, 0);
                    Frequency = (DateFrequency)value.GetValue(2, 0);
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

            info.AddValue("_StartDate", this.StartDate.Ticks);
            info.AddValue("_EndDate", this.EndDate.Ticks);
            info.AddValue("_Frequency", (int)this.Frequency);
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
                case DateFrequency.ThreeMonths:
                    return startDate.AddMonths(3 * periods);
                case DateFrequency.SixMonths:
                    return startDate.AddMonths(6 * periods);
                case DateFrequency.Year:
                    return startDate.AddYears(periods);
                default:
                    return startDate;
            }
        }
        #endregion // Helper methods
    }
}
