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
            this.checkBoxGenerateFromStart.CheckedChanged += checkBoxGenerateFromStart_CheckedChanged;
            this.checkBoxFollowFrequency.CheckedChanged += (obj, args) => HandleAutomaticPreview();
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

        /// <summary>
        /// Automatically updates the dates to be shown each time there is change
        /// in the <see cref="expressionStartDate"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void expressionStartDate_TextChanged(object sender, EventArgs e)
        {
            HandleAutomaticPreview();
        }

        /// <summary>
        /// Automatically updates the dates to be shown each time there is change
        /// in the <see cref="checkBoxExcludeStartDate"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void checkBoxExclude_CheckedChanged(object sender, EventArgs e)
        {
            HandleAutomaticPreview();
        }

        /// <summary>
        /// Automatically updates the dates to be shown each time there is change
        /// in the <see cref="expressionEndDate"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void expressionEndDate_TextChanged(object sender, EventArgs e)
        {
            HandleAutomaticPreview();
        }

        /// <summary>
        /// Automatically updates the dates to be shown each time there is change
        /// in the <see cref="comboBoxFrequency"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void comboBoxFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAutomaticPreview();
        }

        /// <summary>
        /// Automatically updates the dates to be shown each time there is change
        /// in the <see cref="comboBoxFrequency"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        void checkBoxGenerateFromStart_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxGenerateFromStart.Checked)
                this.checkBoxExcludeStartDate.Enabled = true;
            else
            {
                this.checkBoxExcludeStartDate.Checked = false;
                this.checkBoxExcludeStartDate.Enabled = false;
            }

            HandleAutomaticPreview();
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
                    this.expressionStartDate.Text = ((ModelParameterDateSequence)this.editedObject).StartDateExpression;
                    this.expressionEndDate.Text = ((ModelParameterDateSequence)this.editedObject).EndDateExpression;
                    this.comboBoxFrequency.Text = ((ModelParameterDateSequence)this.editedObject).FrequencyExpression;
                    this.checkBoxExcludeStartDate.Checked = ((ModelParameterDateSequence)this.editedObject).ExcludeStartDate;
                    this.checkBoxFollowFrequency.Checked = ((ModelParameterDateSequence)this.editedObject).FollowFrequency;
                    this.checkBoxGenerateFromStart.Checked = ((ModelParameterDateSequence)this.editedObject).GenerateSequenceFromStartDate;
                }
                else
                {
                    // Initialize the other elements of the GUI to their default
                    this.expressionStartDate.Text = DateTime.Now.Date.ToShortDateString();
                    this.expressionEndDate.Text = DateTime.Now.Date.ToShortDateString();
                    this.comboBoxFrequency.SelectedIndex = 0;
                }

                // Disable the possibility of excluding the start date when generated backward
                if (!this.checkBoxGenerateFromStart.Checked)
                {
                    this.checkBoxExcludeStartDate.Checked = false;
                    this.checkBoxExcludeStartDate.Enabled = false;
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
                    this.project.Initialize(true);
                }
                catch
                {
                    //ignore errors at this level
                }

                try
                {
                    this.initialized = (bool)parameters[1];
                }
                catch
                {
                    this.initialized = false;
                }

                this.textBoxName.Enabled = !initialized;
                HandleAutomaticPreview();
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
                modelParameterDateSequence.StartDateExpression = this.expressionStartDate.Text;
                modelParameterDateSequence.ExcludeStartDate = this.checkBoxExcludeStartDate.Checked;
                modelParameterDateSequence.FollowFrequency = this.checkBoxFollowFrequency.Checked;
                modelParameterDateSequence.EndDateExpression = this.expressionEndDate.Text;
                modelParameterDateSequence.FrequencyExpression = this.comboBoxFrequency.Text;
                modelParameterDateSequence.GenerateSequenceFromStartDate = this.checkBoxGenerateFromStart.Checked;
                modelParameterDateSequence.VarName = this.textBoxName.Text;
                modelParameterDateSequence.Tag = null;

                // Parse the object to generate the array of dates
                modelParameterDateSequence.Parse(this.project);
            }
            else
            {
                // Initialize the array to the sequence of dates
                ModelParameterDateSequence modelParameterDateSequence = new ModelParameterDateSequence(this.expressionStartDate.Text,
                                                                                                       this.expressionEndDate.Text,
                                                                                                       this.comboBoxFrequency.Text);

                modelParameterDateSequence.ExcludeStartDate = this.checkBoxExcludeStartDate.Checked;
                modelParameterDateSequence.Parse(this.project);
                modelParameterDateSequence.GenerateSequenceFromStartDate = this.checkBoxGenerateFromStart.Checked;
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
                ModelParameterDateSequence preview = new ModelParameterDateSequence(this.expressionStartDate.Text,
                                                                                    this.expressionEndDate.Text,
                                                                                    this.comboBoxFrequency.Text);

                preview.ExcludeStartDate = this.checkBoxExcludeStartDate.Checked;
                preview.FollowFrequency = this.checkBoxFollowFrequency.Checked;
                preview.GenerateSequenceFromStartDate = this.checkBoxGenerateFromStart.Checked;
                preview.Parse(project);
                this.dataGridViewDates.Rows.Clear();

                this.labelElementsCount.Text = preview.Values.Count + " Elements";
                for (int i = 0; i < preview.Values.Count; i++)
                {
                    this.dataGridViewDates.Rows.Add(preview.Values[i]);
                }
            }
        }

        /// <summary>
        /// Handles the automatic preview by ignoring all errors.
        /// </summary>
        private void HandleAutomaticPreview()
        {
            try
            {
                InitializeDatesPreview();
            }
            catch (Exception)
            { }
        }

        #endregion // Helper methods
    }
}
