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
using System.Threading;
using System.Collections.Generic;

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

        /// <summary>
        /// THe thread used for the preview of the items
        /// </summary>
        private Thread previewThread;

        private bool previewAborted;

        #endregion // Fields

        #region Constructors
        /// <summary>
        /// Initializes the form.
        /// </summary>
        public DateSequenceForm()
        {
            InitializeComponent();
            FinalizeInitialization();
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
            this.buttonOk.Click += buttonOk_Click;
            this.buttonCancel.Click += buttonCancel_Click;
            this.buttonUpdate.Click += buttonUpdate_Click;
            this.comboBoxDatesGeneration.SelectedIndexChanged += comboBoxDatesGeneration_SelectedIndexChanged;
            this.comboBoxFrequency.SelectedIndexChanged += comboBoxFrequency_SelectedIndexChanged;
            this.expressionSkipPeriods.TextChanged += checkBoxExclude_CheckedChanged;
            this.checkBoxFollowFrequency.CheckedChanged += (obj, args) => HandleAutomaticPreview();
            this.expressionEndDate.TextChanged += expressionEndDate_TextChanged;
            this.expressionStartDate.TextChanged += expressionStartDate_TextChanged;
            this.expressionVectorRef.TextChanged += expressionVectorRef_TextChanged;
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
            var backup = StoreValues(this.editedObject);

            if (Validation(false))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                RestoreValues(this.editedObject, backup);
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
        /// in the <see cref="comboBoxDatesGeneration"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        void comboBoxDatesGeneration_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAutomaticPreview();
        }

        /// <summary>
        /// Automatically updates the dates to be shown each time there is change
        /// in the <see cref="expressionVectorRef"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void expressionVectorRef_TextChanged(object sender, EventArgs e)
        {
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
                    object startDateExpression = ((ModelParameterDateSequence)this.editedObject).StartDateExpression.Expr.GetValue(0, 0);
                    if (startDateExpression is DateTime)
                        this.expressionStartDate.Text = ((DateTime)startDateExpression).ToShortDateString();
                    else
                        this.expressionStartDate.Text = startDateExpression.ToString();

                    object endDateExpression = ((ModelParameterDateSequence)this.editedObject).EndDateExpression.Expr.GetValue(0, 0);
                    if (endDateExpression is DateTime)
                        this.expressionEndDate.Text = ((DateTime)endDateExpression).ToShortDateString();
                    else
                        this.expressionEndDate.Text = endDateExpression.ToString();

                    this.comboBoxFrequency.Text = ((ModelParameterDateSequence)this.editedObject).FrequencyExpression;
                    this.expressionSkipPeriods.Text = ((ModelParameterDateSequence)this.editedObject).SkipPeriods.Expr.GetValue(0, 0).ToString();
                    this.checkBoxFollowFrequency.Checked = ((ModelParameterDateSequence)this.editedObject).FollowFrequency;
                    this.comboBoxDatesGeneration.SelectedIndex = ((ModelParameterDateSequence)this.editedObject).GenerateSequenceFromStartDate ? 0 : 1;
                    this.expressionVectorRef.Text = ((ModelParameterDateSequence)this.editedObject).VectorReferenceExpr;
                }
                else
                {
                    // Initialize the other elements of the GUI to their default
                    this.expressionStartDate.Text = DateTime.Now.Date.ToShortDateString();
                    this.expressionEndDate.Text = DateTime.Now.Date.ToShortDateString();
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

                // From now initialize the handlers in order to avoid unnecessary preview attempt
                InitializeHandlers();
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

                InitializeModelParameter();
                this.project.m_ErrorList.Clear();
                this.project.m_RuntimeErrorList.Clear();
                if (this.editedObject.Parse(project))
                {
                    // Error list
                    foreach (var error in this.project.m_ErrorList)
                    {
                        validationErrors += error.Message + "\n\r";
                    }

                    // Runtime error list
                    foreach (var error in this.project.m_RuntimeErrorList)
                    {
                        validationErrors += error.Message + "\n\r";
                    }

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
                modelParameterDateSequence.SkipPeriods = this.expressionSkipPeriods.Text;
                modelParameterDateSequence.FollowFrequency = this.checkBoxFollowFrequency.Checked;
                modelParameterDateSequence.EndDateExpression = this.expressionEndDate.Text;
                modelParameterDateSequence.FrequencyExpression = this.comboBoxFrequency.Text;
                modelParameterDateSequence.GenerateSequenceFromStartDate = this.comboBoxDatesGeneration.SelectedIndex == 0;
                modelParameterDateSequence.VarName = this.textBoxName.Text;
                modelParameterDateSequence.Tag = null;
                modelParameterDateSequence.VectorReferenceExpr = this.expressionVectorRef.Text;

                // Parse the object to generate the array of dates
                modelParameterDateSequence.Parse(this.project);
            }
            else
            {
                // Initialize the array to the sequence of dates
                ModelParameterDateSequence modelParameterDateSequence = new ModelParameterDateSequence(this.expressionStartDate.Text,
                                                                                                       this.expressionEndDate.Text,
                                                                                                       this.comboBoxFrequency.Text);

                modelParameterDateSequence.SkipPeriods = this.expressionSkipPeriods.Text;
                modelParameterDateSequence.VectorReferenceExpr = this.expressionVectorRef.Text;
                modelParameterDateSequence.GenerateSequenceFromStartDate = this.comboBoxDatesGeneration.SelectedIndex == 0;
                modelParameterDateSequence.Parse(this.project);
                this.editedObject.Values = modelParameterDateSequence.Values;
            }
        }

        /// <summary>
        /// Initializes the edited object in order to show its preview.
        /// </summary>
        private void InitializeDatesPreview()
        {
            // Check if the thread is running and in case abort it and wait for it to stop
            if (previewThread != null &&
                previewThread.ThreadState == ThreadState.Running)
            {
                this.previewAborted = true;
                previewThread.Abort();
                previewThread.Join();
            }

            // Start the thread
            string startDate = this.expressionStartDate.Text;
            string endDate = this.expressionEndDate.Text;
            string frequency = this.comboBoxFrequency.Text;
            string skipPeriods = this.expressionSkipPeriods.Text;
            bool followFrequency = this.checkBoxFollowFrequency.Checked;
            bool generateSequenceFromStartDate = this.comboBoxDatesGeneration.SelectedIndex == 0;
            string vectorRefExpr = this.expressionVectorRef.Text;
            previewThread = new Thread(() => InitializeDatesPreviewThreadWorker(startDate,
                                                                                endDate,
                                                                                frequency,
                                                                                skipPeriods,
                                                                                followFrequency,
                                                                                generateSequenceFromStartDate,
                                                                                vectorRefExpr));

            previewThread.IsBackground = true;
            previewThread.Start();
        }

        private void InitializeDatesPreviewThreadWorker(string startDate,
                                                        string endDate,
                                                        string frequency,
                                                        string skipPeriods,
                                                        bool followFrequency,
                                                        bool generateSequenceFromStartDate,
                                                        string vectorRefExpr)
        {
            if (Validation(true))
            {
                ModelParameterDateSequence preview;
                try
                {
                    preview = new ModelParameterDateSequence(startDate,
                                                             endDate,
                                                             frequency);
                }
                catch
                {
                    // Error, don't show the preview
                    InitializeUiEmptyPreview();
                    return;
                }

                preview.SkipPeriods = skipPeriods;
                preview.FollowFrequency = followFrequency;
                preview.GenerateSequenceFromStartDate = generateSequenceFromStartDate;
                preview.VectorReferenceExpr = vectorRefExpr;

                DateTime minDate = new DateTime(1900, 1, 1);
                DateTime maxDate = new DateTime(2100, 12, 31);
                try
                {
                    project.Initialize(true);
                    Engine.Parser.Parse("EffectiveDate");
                }
                catch
                {
                    // Ignore project initialization error during the preview
                }

                preview.ParsePreview(project, minDate, maxDate);

                int elements = Math.Min(50, preview.Values.Count);
                InitializeUiPreview(preview, minDate, maxDate, elements);
            }
        }

        private void InitializeUiPreview(ModelParameterDateSequence preview, DateTime minDate, DateTime maxDate, int elements)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => InitializeUiPreview(preview, minDate, maxDate, elements)));
                return;
            }

            // Update the label
            if (preview.StartDate < minDate || preview.EndDate > maxDate)
            {
                this.labelElementsCount.Text = string.Format("Preview generated only between {0} and {1}",
                                                             minDate.ToShortDateString(),
                                                             maxDate.ToShortDateString());
            }
            else
            {
                this.labelElementsCount.Text = string.Format("{0} Elements", preview.Values.Count);
                if (elements != preview.Values.Count)
                {
                    this.labelElementsCount.Text += string.Format(", First {0} Shown", elements);
                }
            }

            if (previewAborted)
                return;

            this.dataGridViewDates.Rows.Clear();
            for (int i = 0; i < elements; i++)
            {
                if (previewAborted)
                    return;

                this.dataGridViewDates.Rows.Add(preview.Values[i]);
            }
        }

        private void InitializeUiEmptyPreview()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(InitializeUiEmptyPreview));
                return;
            }

            this.labelElementsCount.Text = string.Empty;
            this.dataGridViewDates.Rows.Clear();
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

        /// <summary>
        /// Saves the values of the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>A dictionary containing the values of the parameter.</returns>
        private IDictionary<string, object> StoreValues(ModelParameterArray parameter)
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();

            if (parameter is ModelParameterDateSequence)
            {
                var dateSequence = (ModelParameterDateSequence)parameter;
                dictionary.Add("StartDateExpression", dateSequence.StartDateExpression);
                dictionary.Add("SkipPeriods", dateSequence.SkipPeriods);
                dictionary.Add("FollowFrequency", dateSequence.FollowFrequency);
                dictionary.Add("EndDateExpression", dateSequence.EndDateExpression);
                dictionary.Add("FrequencyExpression", dateSequence.FrequencyExpression);
                dictionary.Add("GenerateSequenceFromStartDate", dateSequence.GenerateSequenceFromStartDate);
                dictionary.Add("VectorReferenceExpr", dateSequence.VectorReferenceExpr);
            }
            else
            {
                dictionary.Add("Expr", parameter.Expr);
            }

            return dictionary;
        }

        /// <summary>
        /// Restores the values of the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="dictionary">A dictionary containing the values of the parameter.</param>
        private void RestoreValues(ModelParameterArray parameter, IDictionary<string, object> dictionary)
        {
            if (parameter is ModelParameterDateSequence)
            {
                var dateSequence = (ModelParameterDateSequence)parameter;
                dateSequence.StartDateExpression = (ModelParameter)dictionary["StartDateExpression"];
                dateSequence.SkipPeriods = (ModelParameter)dictionary["SkipPeriods"];
                dateSequence.FollowFrequency = (bool)dictionary["FollowFrequency"];
                dateSequence.EndDateExpression = (ModelParameter)dictionary["EndDateExpression"];
                dateSequence.FrequencyExpression = (string)dictionary["FrequencyExpression"];
                dateSequence.GenerateSequenceFromStartDate = (bool)dictionary["GenerateSequenceFromStartDate"];
                dateSequence.VectorReferenceExpr = (string)dictionary["VectorReferenceExpr"];
            }
            else
            {
                parameter.Expr = (Array)dictionary["Expr"];
            }
        }

        #endregion // Helper methods
    }
}
