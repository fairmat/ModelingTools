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
using DVPLI;
using Mono.Addins;

namespace DatesGenerator
{
    /// <summary>
    /// The symbol choice to be shown in the "Parameters &amp; Functions" list of choices.
    /// </summary>
    [Extension("/Fairmat/SymbolChoice")]
    public class DatesSequenceSymbolChoice : IEditableChoice
    {
        #region IEditableChoice implementation
        /// <summary>
        /// Gets the description to show in the "Parameters &amp; Functions" list of choices.
        /// </summary>
        public string Description
        {
            get { return "Dates sequence"; }
        }

        /// <summary>
        /// Returns the ModelParameterDateSequence representing the array of dates.
        /// </summary>
        /// <returns>The ModelParameterDateSequence representing the array of dates.</returns>
        public IEditable CreateInstance()
        {
            return new ModelParameterDateSequence(DateTime.Now.Date,
                                                  DateTime.Now.Date,
                                                  DateFrequency.Daily);
        }
        #endregion // IEditableChoice implementation
    }
}
