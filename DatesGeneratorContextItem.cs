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
using System.Collections.Generic;
using DVPLDOM;
using DVPLI;
using Mono.Addins;

namespace DatesGenerator
{
    /// <summary>
    /// Represents the context menu item for the dates vector generator.
    /// </summary>
    [Extension("/Fairmat/SymbolToolUI")]
    public class DatesGeneratorContextItem : IToolUI, IProvider
    {
        #region Fields
        /// <summary>
        /// Sets the ModelParameterArray object being created with the array of dates.
        /// </summary>
        private ModelParameterArray datesVector;
        #endregion // Fields

        #region IToolUI implementation
        /// <summary>
        /// Sets the ModelParameterArray object being created.
        /// </summary>
        public object Object
        {
            set
            {
                this.datesVector = value as ModelParameterArray;
            }
        }

        /// <summary>
        /// Gets the category of the context menu item.
        /// </summary>
        public string Category
        {
            get
            {
                return "Vector generator";
            }
        }

        /// <summary>
        /// Gets the tooltip to show for the context menu item.
        /// </summary>
        public string ToolTipText
        {
            get
            {
                return "Creates the vector as a series of dates.";
            }
        }

        /// <summary>
        /// Gets the description to show in the context menu.
        /// </summary>
        public string Description
        {
            get
            {
                return "Dates generator";
            }
        }

        /// <summary>
        /// Executes the form for creating the ModelParameterArray with the array of dates.
        /// </summary>
        public void Execute()
        {
            if (this.datesVector != null)
            {
                DateSequenceForm f = new DateSequenceForm();
                List<object> parameterList = new List<object>();
                parameterList.Add(Document.ActiveDocument.Part.ElementAt(0));
                parameterList.Add(true);

                f.Bind(this.datesVector);
                f.BindInfo(parameterList);
                f.ShowDialog();
            }
        }
        #endregion // IToolUI implementation

        #region IProvider implementation
        /// <summary>
        /// Gets an array containing the types supported by this object.
        /// </summary>
        public Type[] ProvidesTo
        {
            get
            {
                return new Type[] { typeof(ModelParameterArray) };
            }
        }
        #endregion // IProvider implementation
    }
}
