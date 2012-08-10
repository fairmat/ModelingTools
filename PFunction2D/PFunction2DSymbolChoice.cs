/* Copyright (C) 2012 Fairmat SRL (info@fairmat.com, http://www.fairmat.com/)
 * Author(s): Stefano Angeleri (stefano.angeleri@fairmat.com)
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

namespace PFunction2D
{
    /// <summary>
    /// The symbol choice to be shown in the "Functions" list of choices.
    /// </summary>
    [Extension("/Fairmat/SymbolChoice")]
    public class PFunction2DSymbolChoice : IEditableChoice
    {
        #region IEditableChoice implementation

        /// <summary>
        /// Gets the description to show in the "Functions" list of choices.
        /// </summary>
        public string Description
        {
            get
            {
                return "Functions/2D Function defined by value interpolation";
            }
        }

        /// <summary>
        /// Returns the <see cref="Pfunction2D"/> representing a 2D function
        /// defined by value interpolation.
        /// </summary>
        /// <returns>
        /// The <see cref="Pfunction2D"/> representing the 2D
        /// function defined by value interpolation.
        /// </returns>
        public IEditable CreateInstance()
        {
            return new PFunction2D();
        }

        #endregion IEditableChoice implementation
    }
}
