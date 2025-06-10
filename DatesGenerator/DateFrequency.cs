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

namespace DatesGenerator
{
    /// <summary>
    /// The frequency between each date.
    /// </summary>
    public enum DateFrequency
    {
        /// <summary>
        /// One day.
        /// </summary>
        Daily = 0,

        /// <summary>
        /// One week.
        /// </summary>
        Weekly = 1,

        /// <summary>
        /// Two weeks.
        /// </summary>
        BiWeekly = 2,

        /// <summary>
        /// One month.
        /// </summary>
        Monthly = 3,

        /// <summary>
        /// Three months.
        /// </summary>
        Quarterly = 4,

        /// <summary>
        /// Six months.
        /// </summary>
        Semiannual = 5,

        /// <summary>
        /// One year.
        /// </summary>
        Annual = 6,

        /// <summary>
        /// Four months.
        /// </summary>
        ThreePerAnnum = 7,

        /// <summary>
        /// Every Two Years.
        /// </summary>
        EveryTwoYears = 8,

        /// <summary>
        /// Every Three Years.
        /// </summary>
        EveryThreeYears = 9,

        /// <summary>
        /// Two months periods.
        /// </summary>
        EveryTwoMonths = 10,

        /// <summary>
        /// No Frequency
        /// </summary>
        NoFrequency = 99

    }
}
