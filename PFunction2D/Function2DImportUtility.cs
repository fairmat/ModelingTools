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
using System.Collections.Generic;
using System.Windows.Forms;

namespace PFunction2D
{
    /// <summary>
    /// Extension methods for the import of a 2D function.
    /// </summary>
    public static class Function2DImportUtility
    {
        /// <summary>
        /// Initializes the DataGridView with the matrix contained in the clipboard using the
        /// first row and column as headers values in order to represent a 2D function.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to use.</param>
        /// <param name="errorMessage">The resulting error message (if any error happens).</param>
        /// <returns>true if the import was successfull; otherwise false.</returns>
        public static bool Import2DFunctionFromClipboard(this DataGridView dataGridView, out string errorMessage)
        {
            // Set the error message to the empty string
            errorMessage = string.Empty;

            IDataObject clipboard = Clipboard.GetDataObject();
            if (clipboard != null)
            {
                string clipboardString = (string)clipboard.GetData("System.String", true);
                if (!string.IsNullOrEmpty(clipboardString))
                {
                    clipboardString = NormalizeClipboardString(clipboardString);

                    string[] lines = clipboardString.Split('\n', '\r');
                    int columns = -1;

                    // Check if the matrix dimensions are consistent
                    bool consistent = true;
                    foreach (string line in lines)
                    {
                        // Use tab as separator for the elements
                        string[] elements = line.Split('\t');

                        if (columns == -1)
                            columns = elements.Length;
                        else if (columns != elements.Length)
                            consistent = false;
                    }

                    if (consistent)
                    {
                        // Determine the matrix
                        string[,] matrixString = new string[lines.Length, columns];
                        for (int i = 0; i < matrixString.GetLength(0); i++)
                        {
                            string[] elements = lines[i].Split('\t');
                            for (int j = 0; j < matrixString.GetLength(1); j++)
                            {
                                matrixString[i, j] = elements[j];
                            }
                        }

                        return dataGridView.Import2DFunctionFromMatrix(matrixString, out errorMessage);
                    }
                    else
                        errorMessage = "The matrix dimensions are not consistent.";
                }
                else
                    errorMessage = "The clipboard is empty.";
            }
            else
                errorMessage = "The clipboard is empty.";

            return errorMessage == string.Empty;
        }

        /// <summary>
        /// Initializes the DataGridView with the given matrix using the first row and column as
        /// headers values in order to represent a 2D function.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to use.</param>
        /// <param name="matrix">The matrix containing the 2D function.</param>
        /// <param name="errorMessage">The resulting error message (if any error happens).</param>
        /// <returns>true if the import was successfull; otherwise false.</returns>
        public static bool Import2DFunctionFromMatrix(this DataGridView dataGridView, string[,] matrix, out string errorMessage)
        {
            if (IsMatrixValid(matrix, out errorMessage))
            {
                // Set the column headers
                List<string> columnHeaders = new List<string>();
                for (int i = 1; i < matrix.GetLength(1); i++)
                {
                    columnHeaders.Add(matrix[0, i]);
                }

                dataGridView.SetColumnHeaders(columnHeaders);

                // Set the row headers
                List<string> rowHeaders = new List<string>();
                for (int i = 1; i < matrix.GetLength(0); i++)
                {
                    rowHeaders.Add(matrix[i, 0]);
                }

                dataGridView.SetRowHeaders(rowHeaders);

                // Set the matrix content
                for (int i = 1; i < matrix.GetLength(0); i++)
                {
                    for (int j = 1; j < matrix.GetLength(1); j++)
                    {
                        dataGridView[j - 1, i - 1].Value = matrix[i, j];
                    }
                }
            }

            return errorMessage == string.Empty;
        }

        /// <summary>
        /// Sets the text of the column headers based on the given list.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to use.</param>
        /// <param name="headers">The list of string to use as column header text.</param>
        private static void SetColumnHeaders(this DataGridView dataGridView, List<string> headers)
        {
            // Adjust the column count
            if (dataGridView.Columns.Count != headers.Count)
            {
                if (dataGridView.Columns.Count > headers.Count)
                {
                    for (int i = dataGridView.Columns.Count - 1; i >= headers.Count; i--)
                        dataGridView.Columns.RemoveAt(i);
                }
                else
                {
                    for (int i = dataGridView.Columns.Count; i < headers.Count; i++)
                        dataGridView.Columns.Add(headers[i], headers[i]);
                }
            }

            // Set the column headers
            for (int i = 0; i < headers.Count; i++)
                dataGridView.Columns[i].HeaderText = headers[i];
        }

        /// <summary>
        /// Sets the text of the row headers based on the given list.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to use.</param>
        /// <param name="headers">The list of string to use as row header text.</param>
        private static void SetRowHeaders(this DataGridView dataGridView, List<string> headers)
        {
            // Adjust the row count
            if (dataGridView.Rows.Count - 1 != headers.Count)
            {
                if (dataGridView.Rows.Count - 1 > headers.Count)
                {
                    for (int i = dataGridView.Rows.Count - 2; i >= headers.Count; i--)
                        dataGridView.Rows.RemoveAt(i);
                }
                else
                {
                    for (int i = dataGridView.Rows.Count - 1; i < headers.Count; i++)
                        dataGridView.Rows.Add(headers[i], headers[i]);
                }
            }

            // Set the row headers
            for (int i = 0; i < headers.Count; i++)
                dataGridView.Rows[i].HeaderCell.Value = headers[i];
        }

        /// <summary>
        /// Normalizes the string obtained from the clipboard.
        /// </summary>
        /// <param name="clipboardString">The string obtained from the clipboard.</param>
        /// <returns>The normalized string.</returns>
        private static string NormalizeClipboardString(string clipboardString)
        {
            clipboardString = clipboardString.Replace("\r\n", "\n");
            clipboardString = clipboardString.Replace("\n\r", "\n");

            while (clipboardString.EndsWith("\n") || clipboardString.EndsWith("\r"))
            {
                clipboardString = clipboardString.Substring(0, clipboardString.Length - 1);
            }

            return clipboardString;
        }

        /// <summary>
        /// Checks whether the given matrix is valid or not for import.
        /// </summary>
        /// <param name="matrix">The matrix to check.</param>
        /// <param name="errorMessage">The resulting error message (if any error happens).</param>
        /// <returns>true if the matrix is valid for import; otherwise false.</returns>
        private static bool IsMatrixValid(string[,] matrix, out string errorMessage)
        {
            // Null reference
            if (matrix == null)
            {
                errorMessage = "Invalid matrix reference.";
                return false;
            }

            // Headers
            if (matrix.GetLength(0) < 2 || matrix.GetLength(1) < 2)
            {
                errorMessage = "The matrix must have the row and column header values.";
                return false;
            }

            // First element blank
            if (!string.IsNullOrWhiteSpace(matrix[0, 0]))
            {
                errorMessage = "The first element of the matrix must be blank or empty.";
                return false;
            }

            List<string> header = new List<string>();

            // Row header duplicates
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (header.Contains(matrix[i, 0]))
                {
                    errorMessage = "The row headers can't contain duplicate values.";
                    return false;
                }

                header.Add(matrix[i, 0]);
            }

            // Column header duplicates
            header.Clear();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (header.Contains(matrix[0, i]))
                {
                    errorMessage = "The column headers can't contain duplicate values.";
                    return false;
                }

                header.Add(matrix[0, i]);
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
