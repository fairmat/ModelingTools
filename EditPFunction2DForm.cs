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

using System;
using System.Data;
using System.Windows.Forms;
using DVPForms;
using DVPLDOM;
using DVPLI;
using Mono.Addins;

namespace PFunction2D
{
    /// <summary>
    /// Extends the EditFunctionsForm in order to allow it to handle also PFunction2D,
    /// and so 2d interpolation defined functions.
    /// </summary>
    [Extension("/Fairmat/Editor")]
    public class EditPFunction2DForm : EditFunctionsForm
    {
        /// <summary>
        /// Gets the data requested by the ProvidesTo interface member which
        /// tells Fairmat that this class will handle PFunction2D objects.
        /// </summary>
        /// <remarks>
        /// As in this class only PFunction2D are handled the other
        /// types commonly handled by <see cref="EditFunctionsForm"/> are discarded.
        /// </remarks>
        public override Type[] ProvidesTo
        {
            get
            {
                return new Type[] { typeof(PFunction2D) };
            }
        }

        /// <summary>
        /// Handles the objects which will be displayed through this form
        /// for editing and previewing.
        /// </summary>
        /// <param name="obj">The object to edit with this form, must be a PFunction2D.</param>
        public override void Bind(IEditable obj)
        {
            base.m_Function = (Function)obj;

            // Remove some tabs which have no meaning for this use.
            base.tabControlEditFunctions.Controls.Remove(tabPageAnaliticFunctionData);
            base.tabControlEditFunctions.Controls.Remove(tabPageEditZRCalibrator);
            base.tabControlEditFunctions.SelectedIndex = 0;

            // Load the data from the function on the grid.
            PointFunctionDataToDataGrid();
        }

        /// <summary>
        /// Handles the graphical plotting of the 2D function.
        /// </summary>
        protected override void PlotFunction()
        {
            // Currently just skip it.
        }

        /// <summary>
        /// Loads the data from the function on the grid
        /// so it's possible to view and edit it.
        /// </summary>
        private void PointFunctionDataToDataGrid()
        {
            // Take a reference to the function casted to PFunction2D
            // so we can access its members, it's assured to be one
            // from checks in fairmat (due to the ProvidesTo implementation).
            PFunction2D function = (PFunction2D)base.m_Function;

            // Clear all the columns so it's possible to populate them from clean.
            base.fairmatDataGridViewPointData.Columns.Clear();

            // As we use our numbers disable the functionality to
            // number the rows from the fairmatDataGridView control.
            base.fairmatDataGridViewPointData.ShowRowNumber = false;

            base.fairmatDataGridViewPointData.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // Creates the columns for each given x cordinate point stored in the function.
            for (int i = 0; i < function.XCordinates.Count; i++)
            {
                // The column name will have the cordinate associated to the specific x entry.
                base.fairmatDataGridViewPointData.ColumnAdd(function.XCordinates[i].ToString());
            }

            // Populates all the rows of the matrix which defines the function.
            for (int y = 0; y < function.YCordinates.Count; y++)
            {
                // Creates a row for each y cordinate present in the function definition.
                DataGridViewRow row = new DataGridViewRow();

                // Sets the header cell for the row to contain the value of the cordinate.
                row.HeaderCell.Value = function.YCordinates[y];

                // Finally fill each cell of the current row with the respective value.
                for (int x = 0; x < function.XCordinates.Count; x++)
                {
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    cell.Value = function.PointValue(x, y);
                    row.Cells.Add(cell);
                }

                // Finally add the row which was just constructed to the grind.
                base.fairmatDataGridViewPointData.Rows.Add(row);
            }
        }

        /// <summary>
        /// Handles the ok button click action by closing the window and saving the
        /// changes applied to the data.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        protected override void buttonOk_Click(object sender, System.EventArgs e)
        {
            // If this is a new PFunction2D we need to check if the symbol is a duplicate, in that
            // case we warn the user. This isn't needed when modifying the data as the symbol name
            // is fixed afterward.
            if (base.m_ModifyOnly == false)
            {
                // Check if the name is already present
                if (base.m_Project.ExistSymbol(textBoxFunctionName.Text) == true ||
                    !base.m_Project.IsValidSymbolName(textBoxFunctionName.Text))
                {
                    base.form_errors = true;
                }
            }

            // Sets the global data like the function description and name.
            if (DoDataExchangeGeneral(false) == false)
            {
                base.form_errors = true;
            }

            /*if (DoDataExchangePointData(false) == false)
            {
                base.form_errors = true;
            }*/

            // Workaround for bug https://bugzilla.novell.com/show_bug.cgi?id=631810.
            // Do not set the property DialogResult.OK on the button, but set it manually.
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
