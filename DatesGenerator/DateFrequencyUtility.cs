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

using System;

namespace DatesGenerator
{
    /// <summary>
    /// Utility methods for the DateFrequency enumerator.
    /// </summary>
    public static class DateFrequencyUtility
    {
        /// <summary>
        /// Gets the string representation of the specified DateFrequency value.
        /// </summary>
        /// <param name="frequency">The DateFrequency to use.</param>
        /// <returns>The string representation of the specified DateFrequency object.</returns>
        public static string StringRepresentation(this DateFrequency frequency)
        {
            switch (frequency)
            {
                case DateFrequency.BiWeekly:
                    return "Bi-weekly";
                case DateFrequency.Semiannual:
                    return "Semi-annual";
                default:
                    return frequency.ToString();
            }
        }

        /// <summary>
        /// Parses the given string into a DateFrequency value.
        /// </summary>
        /// <param name="value">Thee string to parse.</param>
        /// <returns>A DateFrequency value that has the given string as representation.</returns>
        /// <exception cref="System.ArgumentException">The argument cannot be parsed.</exception>
        /// <exception cref="System.ArgumentNullException">The argument is null.</exception>
        /// <exception cref="System.OverflowException">The argument is outside the range of the
        /// DateFrequency type.</exception>
        public static DateFrequency ParseDateFrequency(string value)
        {
            switch (value)
            {
                case "Bi-weekly":
                    return DateFrequency.BiWeekly;
                case "Semi-annual":
                    return DateFrequency.Semiannual;
                default:
                    return (DateFrequency)Enum.Parse(typeof(DateFrequency), value);
            }
        }
    }
}
