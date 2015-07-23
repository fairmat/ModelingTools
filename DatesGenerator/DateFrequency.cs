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
        Daily,

        /// <summary>
        /// One week.
        /// </summary>
        Weekly,

        /// <summary>
        /// Two weeks.
        /// </summary>
        BiWeekly,

        /// <summary>
        /// One month.
        /// </summary>
        Monthly,

        /// <summary>
        /// Three months.
        /// </summary>
        Quarterly,

        /// <summary>
        /// Six months.
        /// </summary>
        Semiannual,

        /// <summary>
        /// One year.
        /// </summary>
        Annual,

        /// <summary>
        /// Four months.
        /// </summary>
        ThreePerAnnum,

        /// <summary>
        /// Every Two Years.
        /// </summary>
        EveryTwoYears,

        /// <summary>
        /// Every Three Years.
        /// </summary>
        EveryThreeYears

    }
}
