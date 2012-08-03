namespace DatesGenerator
{
    partial class DateSequenceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Frees up used resources.
        /// </summary>
        /// <param name="disposing">
        /// True if the managed resources must be freed up, false otherwise.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code generated from Windows Form Designer

        /// <summary>
        /// This method is required to support the designer window. Don't modify
        /// the method content with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxFrequency = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDatesSequence = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelDatesSequence = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxParameterInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelParameterInfo = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxExport = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.groupBoxDatesSequence.SuspendLayout();
            this.tableLayoutPanelDatesSequence.SuspendLayout();
            this.groupBoxParameterInfo.SuspendLayout();
            this.tableLayoutPanelParameterInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelButtons.AutoSize = true;
            this.tableLayoutPanelButtons.ColumnCount = 2;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelButtons.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.buttonOk, 0, 0);
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(153, 244);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(178, 29);
            this.tableLayoutPanelButtons.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(98, 3);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(10, 3, 5, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(3, 3);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelStartDate
            // 
            this.labelStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartDate.Location = new System.Drawing.Point(3, 6);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(53, 13);
            this.labelStartDate.TabIndex = 1;
            this.labelStartDate.Text = "Start date";
            // 
            // labelEndDate
            // 
            this.labelEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEndDate.Location = new System.Drawing.Point(3, 32);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(50, 13);
            this.labelEndDate.TabIndex = 2;
            this.labelEndDate.Text = "End date";
            // 
            // labelFrequency
            // 
            this.labelFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrequency.Location = new System.Drawing.Point(3, 59);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(57, 13);
            this.labelFrequency.TabIndex = 3;
            this.labelFrequency.Text = "Frequency";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(66, 3);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(125, 20);
            this.dateTimePickerStartDate.TabIndex = 4;
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(66, 29);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(125, 20);
            this.dateTimePickerEndDate.TabIndex = 5;
            // 
            // comboBoxFrequency
            // 
            this.comboBoxFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFrequency.FormattingEnabled = true;
            this.comboBoxFrequency.Location = new System.Drawing.Point(66, 55);
            this.comboBoxFrequency.Name = "comboBoxFrequency";
            this.comboBoxFrequency.Size = new System.Drawing.Size(125, 21);
            this.comboBoxFrequency.TabIndex = 6;
            // 
            // labelName
            // 
            this.labelName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(3, 6);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Name";
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(3, 32);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 8;
            this.labelDescription.Text = "Description";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(69, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(125, 20);
            this.textBoxName.TabIndex = 9;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.Location = new System.Drawing.Point(69, 29);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(250, 20);
            this.textBoxDescription.TabIndex = 10;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSize = true;
            this.tableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxDatesSequence, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelButtons, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxParameterInfo, 0, 0);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(334, 276);
            this.tableLayoutPanelMain.TabIndex = 11;
            // 
            // groupBoxDatesSequence
            // 
            this.groupBoxDatesSequence.AutoSize = true;
            this.groupBoxDatesSequence.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxDatesSequence.Controls.Add(this.tableLayoutPanelDatesSequence);
            this.groupBoxDatesSequence.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxDatesSequence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDatesSequence.Location = new System.Drawing.Point(3, 126);
            this.groupBoxDatesSequence.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.groupBoxDatesSequence.Name = "groupBoxDatesSequence";
            this.groupBoxDatesSequence.Size = new System.Drawing.Size(328, 100);
            this.groupBoxDatesSequence.TabIndex = 12;
            this.groupBoxDatesSequence.TabStop = false;
            this.groupBoxDatesSequence.Text = "Dates Sequence";
            // 
            // tableLayoutPanelDatesSequence
            // 
            this.tableLayoutPanelDatesSequence.AutoSize = true;
            this.tableLayoutPanelDatesSequence.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelDatesSequence.ColumnCount = 2;
            this.tableLayoutPanelDatesSequence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDatesSequence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDatesSequence.Controls.Add(this.labelStartDate, 0, 0);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.labelEndDate, 0, 1);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.labelFrequency, 0, 2);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.comboBoxFrequency, 1, 2);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.dateTimePickerStartDate, 1, 0);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.dateTimePickerEndDate, 1, 1);
            this.tableLayoutPanelDatesSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDatesSequence.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelDatesSequence.Name = "tableLayoutPanelDatesSequence";
            this.tableLayoutPanelDatesSequence.RowCount = 3;
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.Size = new System.Drawing.Size(322, 79);
            this.tableLayoutPanelDatesSequence.TabIndex = 13;
            // 
            // groupBoxParameterInfo
            // 
            this.groupBoxParameterInfo.AutoSize = true;
            this.groupBoxParameterInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxParameterInfo.Controls.Add(this.tableLayoutPanelParameterInfo);
            this.groupBoxParameterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxParameterInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxParameterInfo.Location = new System.Drawing.Point(3, 5);
            this.groupBoxParameterInfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.groupBoxParameterInfo.Name = "groupBoxParameterInfo";
            this.groupBoxParameterInfo.Size = new System.Drawing.Size(328, 103);
            this.groupBoxParameterInfo.TabIndex = 12;
            this.groupBoxParameterInfo.TabStop = false;
            this.groupBoxParameterInfo.Text = "Parameter informations";
            // 
            // tableLayoutPanelParameterInfo
            // 
            this.tableLayoutPanelParameterInfo.AutoSize = true;
            this.tableLayoutPanelParameterInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelParameterInfo.ColumnCount = 2;
            this.tableLayoutPanelParameterInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelParameterInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelParameterInfo.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelParameterInfo.Controls.Add(this.labelDescription, 0, 1);
            this.tableLayoutPanelParameterInfo.Controls.Add(this.textBoxDescription, 1, 1);
            this.tableLayoutPanelParameterInfo.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanelParameterInfo.Controls.Add(this.checkBoxExport, 0, 2);
            this.tableLayoutPanelParameterInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelParameterInfo.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelParameterInfo.Name = "tableLayoutPanelParameterInfo";
            this.tableLayoutPanelParameterInfo.RowCount = 3;
            this.tableLayoutPanelParameterInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelParameterInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelParameterInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelParameterInfo.Size = new System.Drawing.Size(322, 82);
            this.tableLayoutPanelParameterInfo.TabIndex = 0;
            // 
            // checkBoxExport
            // 
            this.checkBoxExport.AutoSize = true;
            this.tableLayoutPanelParameterInfo.SetColumnSpan(this.checkBoxExport, 2);
            this.checkBoxExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxExport.Location = new System.Drawing.Point(6, 62);
            this.checkBoxExport.Margin = new System.Windows.Forms.Padding(6, 10, 3, 3);
            this.checkBoxExport.Name = "checkBoxExport";
            this.checkBoxExport.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExport.TabIndex = 11;
            this.checkBoxExport.Text = "Export";
            this.checkBoxExport.UseVisualStyleBackColor = true;
            // 
            // DateSequenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(339, 284);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateSequenceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit dates sequence";
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.groupBoxDatesSequence.ResumeLayout(false);
            this.groupBoxDatesSequence.PerformLayout();
            this.tableLayoutPanelDatesSequence.ResumeLayout(false);
            this.tableLayoutPanelDatesSequence.PerformLayout();
            this.groupBoxParameterInfo.ResumeLayout(false);
            this.groupBoxParameterInfo.PerformLayout();
            this.tableLayoutPanelParameterInfo.ResumeLayout(false);
            this.tableLayoutPanelParameterInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.ComboBox comboBoxFrequency;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxDatesSequence;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDatesSequence;
        private System.Windows.Forms.GroupBox groupBoxParameterInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelParameterInfo;
        private System.Windows.Forms.CheckBox checkBoxExport;
    }
}
