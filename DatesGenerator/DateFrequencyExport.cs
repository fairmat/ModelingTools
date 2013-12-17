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

using DVPLI;
using Mono.Addins;
using System;

namespace DatesGenerator
{
    /// <summary>
    /// Exports the date frequency enumerator into the Fairmat UI.
    /// </summary>
    [Extension("/Fairmat/ChoicesListType")]
    public class DateFrequencyExport : ITypedScalarType
    {

        #region ITypedScalarType Members

        /// <summary>
        /// Gets the available enumerators that can be used for the symbols representing
        /// an enumerator.
        /// </summary>
        /// <param name="forceDiscovery">
        /// true if the discovery process has to be forced; otherwise false.
        /// </param>
        /// <returns>
        /// A list containing the types of available enumerators with the corresponding description.
        /// </returns>
        public Tuple<Type, string>[] GetTypes()
        {
            return new Tuple<Type, string>[] { new Tuple<Type, string>(typeof(DateFrequency), "Dates' Sequence Frequency") };
        }

        #endregion
    }
}
