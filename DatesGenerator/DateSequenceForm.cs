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
using System.Windows.Forms;
using DVPLDOM;
using DVPLI;
using Mono.Addins;

namespace DatesGenerator
{
    /// <summary>
    /// The form used for creating the vector of dates based on the start and end dates and the
    /// frequency to use between each date.
    /// </summary>
    [Extension("/Fairmat/Editor")]
    public partial class DateSequenceForm : Form, IEditorEx
    {
        #region Fields
        /// <summary>
        /// The start date.
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// A flag that indicates whether or not the start date has to be excluded.
        /// </summary>
        private bool excludeStartDate;

        /// <summary>
        /// The end date.
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// The frequency of the dates generated between the start and end dates.
        /// </summary>
        private DateFrequency frequency;

        /// <summary>
        /// True if the object has already been initialized, false otherwise.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// The project associated to the model parameter being edited.
        /// </summary>
        private Project project;

        /// <summary>
        /// The model parameter being edited.
        /// </summary>
        private ModelParameterArray editedObject;
        #endregion // Fields

        #region Constructors
        /// <summary>
        /// Initializes the form.
        /// </summary>
        public DateSequenceForm()
        {
            InitializeComponent();
            FinalizeInitialization();
            InitializeHandlers();
        }
        #endregion // Constructors

        #region Initialization
        /// <summary>
        /// Finalizes the initialization of the form.
        /// </summary>
        private void FinalizeInitialization()
        {
            // ComboBox initialization
            string[] names = Enum.GetNames(typeof(DateFrequency));

            // Adjust the names
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = ((DateFrequency)Enum.Parse(typeof(DateFrequency), names[i])).StringRepresentation();
            }

            // Finish the initialization
            this.comboBoxFrequency.Items.AddRange(names);
        }

        /// <summary>
        /// Initializes the handlers of the form.
        /// </summary>
        private void InitializeHandlers()
        {
            this.buttonOk.Click += new EventHandler(buttonOk_Click);
            this.buttonCancel.Click += new EventHandler(buttonCancel_Click);
            this.buttonUpdate.Click += new EventHandler(buttonUpdate_Click);
        }

        #endregion // Initialization

        #region Handlers
        /// <summary>
        /// Tries to validate the data and if the validation is successful
        /// initializes the ModelParameter object and closes the form.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (Validation(false))
            {
                InitializeModelParameter();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Calculates and shows the dates to be generated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            InitializeDatesPreview();
        }
        #endregion // Handlers

        #region IEditorEx implementation
        /// <summary>
        /// Gets an array containing the types supported by this object.
        /// </summary>
        public Type[] ProvidesTo
        {
            get
            {
                return new Type[] { typeof(ModelParameterDateSequence) };
            }
        }

        /// <summary>
        /// Checks if the given instance can be edited using this form.
        /// </summary>
        /// <param name="instance">The object to check.</param>
        /// <returns>True if the object can be edited with the current form,
        /// false otherwise.</returns>
        public bool CheckInstance(object instance)
        {
            return (instance as ModelParameterDateSequence) != null;
        }

        /// <summary>
        /// Binds the vector to edit/create to this form.
        /// </summary>
        /// <param name="editedObject">The sequence of dates to edit/create.</param>
        public void Bind(IEditable editedObject)
        {
            this.editedObject = editedObject as ModelParameterArray;
            if (editedObject != null)
            {
                // Bind the ModelParameterArray information to the GUI
                this.textBoxName.Text = string.IsNullOrEmpty(this.editedObject.Name) ?
                                        string.Empty : this.editedObject.Name;
                this.publishingInfoControl.DataExchange(true, this.editedObject);

                if (this.editedObject is ModelParameterDateSequence)
                {
                    // Binds the date sequence specific information to the GUI
                    this.dateTimePickerStartDate.Value = ((ModelParameterDateSequence)this.editedObject).StartDate;
                    this.dateTimePickerEndDate.Value = ((ModelParameterDateSequence)this.editedObject).EndDate;
                    this.comboBoxFrequency.Text = ((ModelParameterDateSequence)this.editedObject).Frequency.StringRepresentation();
                    this.checkBoxExclude.Checked = ((ModelParameterDateSequence)this.editedObject).ExcludeStartDate;
                }
                else
                {
                    // Initialize the other elements of the GUI to their default
                    this.dateTimePickerStartDate.Value = DateTime.Now.Date;
                    this.dateTimePickerEndDate.Value = DateTime.Now.Date;
                    this.comboBoxFrequency.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Binds other information. to this form (like the project in which the vector is defined).
        /// </summary>
        /// <param name="info">The other information to bind to this form.</param>
        public void BindInfo(object info)
        {
            System.Collections.IList parameters = info as System.Collections.IList;
            if (parameters != null)
            {
                this.project = (Project)parameters[0];
                try
                {
                    this.initialized = (bool)parameters[1];
                }
                catch
                {
                    this.initialized = false;
                }

                this.textBoxName.Enabled = !initialized;
                if (initialized)
                {
                    InitializeDatesPreview();
                }
            }
        }
        #endregion // IEditorEx implementation

        #region Helper methods
        /// <summary>
        /// Sets and validates the values specified in the form.
        /// </summary>
        /// <param name="preview">
        /// True if the validation is caused by a preview; otherwise false.
        /// </param>
        /// <returns>True if the values have been successfully set and validated,
        /// false otherwise.</returns>
        private bool Validation(bool preview)
        {
            bool errors = false;
            string validationErrors = string.Empty;

            if (!preview)
            {
                // Name of the dates vector
                if (!this.initialized && (this.project.ExistSymbol(this.textBoxName.Text) ||
                                          !this.project.IsValidSymbolName(this.textBoxName.Text)))
                {
                    validationErrors += "The name used is either already in use or invalid.\n\r";
                    errors = true;
                }
            }

            // Date
            this.startDate = this.dateTimePickerStartDate.Value;
            this.excludeStartDate = this.checkBoxExclude.Checked;
            this.endDate = this.dateTimePickerEndDate.Value;
            if (this.startDate.CompareTo(this.endDate) > 0)
            {
                validationErrors += "The end date must be greater or equal than the start date.\n\r";
                errors = true;
            }

            // Frequency
            bool frequencyValid = true;
            try
            {
                this.frequency = DateFrequencyUtility.ParseDateFrequency(this.comboBoxFrequency.Text);
            }
            catch
            {
                frequencyValid = false;
            }

            if (!frequencyValid)
            {
                validationErrors += "The frequency is not a valid value.\n\r";
                errors = true;
            }

            // Check for errors
            if (errors)
            {
                MessageBox.Show(validationErrors,
                                DVPForms.MainFormSingleton.Instance.ApplicationName);
            }

            return !errors;
        }

        /// <summary>
        /// Based on the values of the form initializes the array of dates.
        /// </summary>
        private void InitializeModelParameter()
        {
            this.publishingInfoControl.DataExchange(false, this.editedObject);

            if (this.editedObject is ModelParameterDateSequence)
            {
                // Initialize the model parameter representing the sequence of dates
                ModelParameterDateSequence modelParameterDateSequence = (ModelParameterDateSequence)this.editedObject;
                modelParameterDateSequence.StartDate = this.startDate;
                modelParameterDateSequence.ExcludeStartDate = this.excludeStartDate;
                modelParameterDateSequence.EndDate = this.endDate;
                modelParameterDateSequence.Frequency = this.frequency;
                modelParameterDateSequence.VarName = this.textBoxName.Text;
                modelParameterDateSequence.Tag = null;

                // Parse the object to generate the array of dates
                modelParameterDateSequence.Parse(this.project);
            }
            else
            {
                // Initialize the array to the sequence of dates
                ModelParameterDateSequence modelParameterDateSequence = new ModelParameterDateSequence(this.startDate, this.endDate, this.frequency);
                modelParameterDateSequence.ExcludeStartDate = this.excludeStartDate;
                modelParameterDateSequence.Parse(this.project);
                this.editedObject.Values = modelParameterDateSequence.Values;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Initializes the edited object in order to show its preview.
        /// </summary>
        private void InitializeDatesPreview()
        {
            if (Validation(true))
            {
                ModelParameterDateSequence preview = new ModelParameterDateSequence(this.startDate, this.endDate, this.frequency);
                preview.ExcludeStartDate = this.excludeStartDate;
                preview.Parse(project);
                this.dataGridViewDates.Rows.Clear();

                this.labelElementsCount.Text = preview.Values.Count + " Elements";
                for (int i = 0; i < preview.Values.Count; i++)
                {
                    this.dataGridViewDates.Rows.Add(preview.Values[i]);
                }
            }
        }
        #endregion // Helper methods
    }
}
