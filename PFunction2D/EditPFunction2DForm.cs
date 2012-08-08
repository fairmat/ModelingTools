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
using System.Windows.Forms;
using DVPForms;
using DVPLDOM;
using DVPLI;
using Mono.Addins;

namespace PFunction2D
{
    /// <summary>
    /// Extends the <see cref="EditFunctionsForm"/> in order to allow it to handle also <see cref="PFunction2D"/>,
    /// and so 2d interpolation defined functions.
    /// </summary>
    [Extension("/Fairmat/Editor")]
    public class EditPFunction2DForm : EditFunctionsForm
    {
        #region Properties

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

        #endregion Properties

        #region Public interface methods

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

            // Temporarily remove the data sources as it's not supported yet.
            base.tabControlEditFunctions.Controls.Remove(tabPageDataSource);
            base.buttonImport.Hide();

            // Sets the selected tab to be the first.
            base.tabControlEditFunctions.SelectedIndex = 0;

            // Set the interpolation types.
            this.interpolationExtrapolationControlPF.SetInterpolationType(((PFunction2D)m_Function).Interpolation);
            this.interpolationExtrapolationControlPF.LeastSquareCoefficients = ((PFunction2D)m_Function).LeastSquaresCoefficients;
            this.interpolationExtrapolationControlPF.SetExtrapolationType(((PFunction2D)m_Function).Extrapolation);

            // Load the data from the function on the grid.
            PointFunctionDataToDataGrid();

            // Setup additional operations on the Datagrid to allow editing columns/rows.
            base.fairmatDataGridViewPointData.EnableColumnRename = true;
            base.fairmatDataGridViewPointData.EnableRowRename = true;
            base.fairmatDataGridViewPointData.EnableColumnEditing = true;
            base.fairmatDataGridViewPointData.ColumnRenameOperationName = "Change x cordinate";
            base.fairmatDataGridViewPointData.RowRenameOperationName = "Change y cordinate";
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// Handles the graphical plotting of the 2D function.
        /// </summary>
        protected override void PlotFunction()
        {
            // Create a temporary PFunction2D which will be used
            // in order to preview current data.
            PFunction2D tempFunction = new PFunction2D();
            DataGridToPointFunctionData(tempFunction);

            // The coordinates are always 2 here as it's a 2D function.
            base.OnPlotGenericFunction(2, tempFunction);
        }

        /// <summary>
        /// Saves the changes applied to the data.
        /// </summary>
        protected override void SaveDataChanges()
        {
            if (!DataGridToPointFunctionData((PFunction2D)base.m_Function))
            {
                base.form_errors = true;
            }
        }

        /// <summary>
        /// Does additional operations on the OnShown event.
        /// </summary>
        /// <remarks>
        /// This is needed because the AutoResizeRowHeaderWidth method
        /// cannot be called when filling the table, else it won't be effective.
        /// </remarks>
        /// <param name="e">The parameter is not used but passed to the base class.</param>
        protected override void OnShown(EventArgs e)
        {
            base.fairmatDataGridViewPointData.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            base.OnShown(e);
        }

        #endregion Overriden methods

        #region Private methods

        /// <summary>
        /// Loads the data from the function on the grid
        /// so it's possible to view and edit it.
        /// </summary>
        private void PointFunctionDataToDataGrid()
        {
            // Take a reference to the function casted to PFunction2D
            // so we can access its members, it's assured to be one
            // from checks in Fairmat (due to the ProvidesTo implementation).
            PFunction2D function = (PFunction2D)base.m_Function;

            // Clear all the columns so it's possible to populate them from clean.
            base.fairmatDataGridViewPointData.Columns.Clear();

            // As we use our numbers disable the functionality to
            // number the rows from the fairmatDataGridView control.
            base.fairmatDataGridViewPointData.ShowRowNumber = false;

            // Creates the columns for each given x coordinate point stored in the function.
            for (int i = 0; i < function.XCordinates.Length; i++)
            {
                // The column name will have the coordinate associated to the specific x entry.
                base.fairmatDataGridViewPointData.ColumnAdd(function.XCordinates[i].Expression);
            }

            // Populates all the rows of the matrix which defines the function.
            for (int y = 0; y < function.YCordinates.Length; y++)
            {
                // Creates a row for each y coordinate present in the function definition.
                DataGridViewRow row = new DataGridViewRow();

                // Sets the header cell for the row to contain the value of the coordinate.
                row.HeaderCell.Value = function.YCordinates[y].Expression;

                // Finally fill each cell of the current row with the respective value.
                for (int x = 0; x < function.XCordinates.Length; x++)
                {
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    cell.Value = function[x, y].Expression;
                    row.Cells.Add(cell);
                }

                // Finally add the row which was just constructed to the grind.
                base.fairmatDataGridViewPointData.Rows.Add(row);
            }
        }

        /// <summary>
        /// Gets the data from the Data Grid and puts it back in the function.
        /// </summary>
        /// <param name="destination">
        /// The <see cref="PFunction2D"/> object where
        /// the data will be stored if it succeeds validation.
        /// </param>
        /// <returns>True if the operation was successful, False otherwise.</returns>
        private bool DataGridToPointFunctionData(PFunction2D destination)
        {
            PFunction2D tempFunction = new PFunction2D();

            // Check if bind was executed. Is it still needed?
            if (m_Project != null)
            {
                m_Project.ResetExpressionParser();
                m_Project.CreateSymbols();

#if MONO
                // If it's running on Mono the event EditingControlShowing doesn't work, so
                // convert the decimal separators in the data grid before calculating the points.
                if (Engine.RunningOnMono)
                    fairmatDataGridViewPointData.ConvertDecimalSeparators();
#endif

                DataGridViewRowCollection rows = fairmatDataGridViewPointData.Rows;
                DataGridViewColumnCollection columns = fairmatDataGridViewPointData.Columns;

                // Sets the sizes of the function. Additionally check if the row.Count is zero,
                // in that case it means there are no columns and rows and so must be handled
                // separately
                tempFunction.SetSizes(columns.Count, rows.Count > 0 ? rows.Count - 1 : 0);

                // First load the column headers values
                for (int x = columns.Count - 1; x >= 0; x--)
                {
                    // DataGridView in case the header cell is edited to an empty string replaces
                    // its value with DBNull (non existent value).
                    // So, substitute the cell value explicitly with the empty string.
                    if (columns[x].HeaderCell.Value is DBNull ||
                        columns[x].HeaderCell.Value == null)
                    {
                        columns[x].HeaderCell.Value = string.Empty;
                    }

                    try
                    {
                        tempFunction[x, -1] = RightValue.ConvertFrom(columns[x].HeaderText, true);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("The string " + columns[x].HeaderText + " (column position " +
                                        (x + 1) + ") is invalid due to: " + e.Message,
                                        DataExchange.ApplicationName);
                        return false;
                    }
                }

                // Then the rows and the cells.
                for (int y = rows.Count - 2; y >= 0; y--)
                {
                    // DataGridView in case the header cell is edited to an empty string replaces
                    // its value with DBNull (non existent value).
                    // So, substitute the cell value explicitly with the empty string.
                    if (rows[y].HeaderCell.Value is DBNull || rows[y].HeaderCell.Value == null)
                    {
                        rows[y].HeaderCell.Value = string.Empty;
                    }

                    try
                    {
                        tempFunction[-1, y] = RightValue.ConvertFrom(rows[y].HeaderCell.Value, true);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("The string " + rows[y].HeaderCell.Value + " (row position " +
                                        (y + 1) + ") is invalid due to: " + e.Message,
                                        DataExchange.ApplicationName);
                        return false;
                    }

                    for (int x = 0; x < columns.Count; x++)
                    {
                        // DataGridView in case the cell is edited to an empty string replaces
                        // its value with DBNull (non existent value).
                        // So, substitute the cell value explicitly with the empty string.
                        if (rows[y].Cells[x].Value is DBNull)
                        {
                            rows[y].Cells[x].Value = string.Empty;
                        }

                        try
                        {
                            tempFunction[x, y] = RightValue.ConvertFrom(rows[y].Cells[x].Value, true);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("The string " + rows[y].Cells[x].Value +
                                            " (position " + (x + 1) + ", " + (y + 1) + ") " +
                                            "is invalid due to: " + e.Message,
                                            DataExchange.ApplicationName);
                            return false;
                        }
                    }
                }
            }

            try
            {
                // Save the information about the interpolation/extrapolation
                tempFunction.Interpolation = this.interpolationExtrapolationControlPF.GetInterpolationType();
                tempFunction.LeastSquaresCoefficients = this.interpolationExtrapolationControlPF.LeastSquareCoefficients;
                tempFunction.Extrapolation = this.interpolationExtrapolationControlPF.GetExtrapolationType();
            }
            catch (Exception e)
            {
                MessageBox.Show("The selected functionality is currently not usable: " + e.Message,
                                DataExchange.ApplicationName);
                return false;
            }

            tempFunction.CopyTo(destination);
            return true;
        }

        #endregion Private methods

    }
}
