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
            this.components = new System.ComponentModel.Container();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelDefinition = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewDates = new System.Windows.Forms.DataGridView();
            this.columnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxDatesSequence = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelDatesSequence = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxExcludeStartDate = new System.Windows.Forms.CheckBox();
            this.expressionEndDate = new ExpressionTypyingHelper.OneLineExpressionControl();
            this.comboBoxFrequency = new System.Windows.Forms.ComboBox();
            this.expressionStartDate = new ExpressionTypyingHelper.OneLineExpressionControl();
            this.checkBoxGenerateFromStart = new System.Windows.Forms.CheckBox();
            this.groupBoxParameterInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelParameterInfo = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDefinition = new System.Windows.Forms.TabPage();
            this.tabPagePublishing = new System.Windows.Forms.TabPage();
            this.publishingInfoControl = new DVPForms.Controls.PublishingInfoControl();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelStatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelElementsCount = new System.Windows.Forms.Label();
            this.checkBoxFollowFrequency = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelDefinition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDates)).BeginInit();
            this.groupBoxDatesSequence.SuspendLayout();
            this.tableLayoutPanelDatesSequence.SuspendLayout();
            this.groupBoxParameterInfo.SuspendLayout();
            this.tableLayoutPanelParameterInfo.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageDefinition.SuspendLayout();
            this.tabPagePublishing.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelStatusBar.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStartDate
            // 
            this.labelStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartDate.Location = new System.Drawing.Point(3, 7);
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
            this.labelEndDate.Location = new System.Drawing.Point(3, 34);
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
            this.labelFrequency.Location = new System.Drawing.Point(3, 62);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(57, 13);
            this.labelFrequency.TabIndex = 3;
            this.labelFrequency.Text = "Frequency";
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
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(44, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(125, 20);
            this.textBoxName.TabIndex = 9;
            // 
            // tableLayoutPanelDefinition
            // 
            this.tableLayoutPanelDefinition.AutoSize = true;
            this.tableLayoutPanelDefinition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelDefinition.ColumnCount = 3;
            this.tableLayoutPanelDefinition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDefinition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDefinition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDefinition.Controls.Add(this.dataGridViewDates, 1, 0);
            this.tableLayoutPanelDefinition.Controls.Add(this.groupBoxDatesSequence, 0, 1);
            this.tableLayoutPanelDefinition.Controls.Add(this.groupBoxParameterInfo, 0, 0);
            this.tableLayoutPanelDefinition.Controls.Add(this.flowLayoutPanelButtons, 2, 0);
            this.tableLayoutPanelDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDefinition.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelDefinition.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelDefinition.Name = "tableLayoutPanelDefinition";
            this.tableLayoutPanelDefinition.RowCount = 3;
            this.tableLayoutPanelDefinition.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDefinition.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDefinition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDefinition.Size = new System.Drawing.Size(532, 262);
            this.tableLayoutPanelDefinition.TabIndex = 11;
            // 
            // dataGridViewDates
            // 
            this.dataGridViewDates.AllowUserToAddRows = false;
            this.dataGridViewDates.AllowUserToDeleteRows = false;
            this.dataGridViewDates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDate});
            this.dataGridViewDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDates.Location = new System.Drawing.Point(275, 3);
            this.dataGridViewDates.Name = "dataGridViewDates";
            this.dataGridViewDates.ReadOnly = true;
            this.tableLayoutPanelDefinition.SetRowSpan(this.dataGridViewDates, 3);
            this.dataGridViewDates.Size = new System.Drawing.Size(167, 262);
            this.dataGridViewDates.TabIndex = 12;
            // 
            // columnDate
            // 
            this.columnDate.HeaderText = "Dates";
            this.columnDate.Name = "columnDate";
            this.columnDate.ReadOnly = true;
            // 
            // groupBoxDatesSequence
            // 
            this.groupBoxDatesSequence.AutoSize = true;
            this.groupBoxDatesSequence.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxDatesSequence.Controls.Add(this.tableLayoutPanelDatesSequence);
            this.groupBoxDatesSequence.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxDatesSequence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDatesSequence.Location = new System.Drawing.Point(3, 70);
            this.groupBoxDatesSequence.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.groupBoxDatesSequence.Name = "groupBoxDatesSequence";
            this.groupBoxDatesSequence.Size = new System.Drawing.Size(266, 157);
            this.groupBoxDatesSequence.TabIndex = 12;
            this.groupBoxDatesSequence.TabStop = false;
            this.groupBoxDatesSequence.Text = "Dates Sequence";
            // 
            // tableLayoutPanelDatesSequence
            // 
            this.tableLayoutPanelDatesSequence.AutoSize = true;
            this.tableLayoutPanelDatesSequence.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelDatesSequence.ColumnCount = 3;
            this.tableLayoutPanelDatesSequence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDatesSequence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDatesSequence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDatesSequence.Controls.Add(this.checkBoxExcludeStartDate, 2, 0);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.labelStartDate, 0, 0);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.labelEndDate, 0, 1);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.labelFrequency, 0, 2);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.expressionEndDate, 1, 1);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.comboBoxFrequency, 1, 2);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.expressionStartDate, 1, 0);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.checkBoxGenerateFromStart, 0, 4);
            this.tableLayoutPanelDatesSequence.Controls.Add(this.checkBoxFollowFrequency, 0, 3);
            this.tableLayoutPanelDatesSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDatesSequence.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelDatesSequence.Name = "tableLayoutPanelDatesSequence";
            this.tableLayoutPanelDatesSequence.RowCount = 5;
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDatesSequence.Size = new System.Drawing.Size(260, 136);
            this.tableLayoutPanelDatesSequence.TabIndex = 13;
            // 
            // checkBoxExcludeStartDate
            // 
            this.checkBoxExcludeStartDate.AutoSize = true;
            this.checkBoxExcludeStartDate.Location = new System.Drawing.Point(182, 3);
            this.checkBoxExcludeStartDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.checkBoxExcludeStartDate.Name = "checkBoxExcludeStartDate";
            this.checkBoxExcludeStartDate.Size = new System.Drawing.Size(75, 20);
            this.checkBoxExcludeStartDate.TabIndex = 5;
            this.checkBoxExcludeStartDate.Text = "Exclude";
            this.checkBoxExcludeStartDate.UseVisualStyleBackColor = true;
            this.checkBoxExcludeStartDate.CheckedChanged += new System.EventHandler(this.checkBoxExclude_CheckedChanged);
            // 
            // expressionEndDate
            // 
            this.expressionEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.expressionEndDate.KeyboardEventsEnabled = true;
            this.expressionEndDate.Location = new System.Drawing.Point(66, 30);
            this.expressionEndDate.Multiline = false;
            this.expressionEndDate.Name = "expressionEndDate";
            this.expressionEndDate.Size = new System.Drawing.Size(103, 21);
            this.expressionEndDate.TabIndex = 14;
            this.expressionEndDate.Text = "";
            this.expressionEndDate.TextAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.expressionEndDate.TextChanged += new System.EventHandler(this.expressionEndDate_TextChanged);
            // 
            // comboBoxFrequency
            // 
            this.comboBoxFrequency.FormattingEnabled = true;
            this.comboBoxFrequency.Location = new System.Drawing.Point(66, 57);
            this.comboBoxFrequency.Name = "comboBoxFrequency";
            this.comboBoxFrequency.Size = new System.Drawing.Size(103, 24);
            this.comboBoxFrequency.TabIndex = 15;
            this.comboBoxFrequency.SelectedIndexChanged += new System.EventHandler(this.comboBoxFrequency_SelectedIndexChanged);
            // 
            // expressionStartDate
            // 
            this.expressionStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.expressionStartDate.KeyboardEventsEnabled = true;
            this.expressionStartDate.Location = new System.Drawing.Point(66, 3);
            this.expressionStartDate.Multiline = false;
            this.expressionStartDate.Name = "expressionStartDate";
            this.expressionStartDate.Size = new System.Drawing.Size(103, 21);
            this.expressionStartDate.TabIndex = 6;
            this.expressionStartDate.Text = "";
            this.expressionStartDate.TextAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.expressionStartDate.TextChanged += new System.EventHandler(this.expressionStartDate_TextChanged);
            // 
            // checkBoxGenerateFromStart
            // 
            this.checkBoxGenerateFromStart.AutoSize = true;
            this.tableLayoutPanelDatesSequence.SetColumnSpan(this.checkBoxGenerateFromStart, 3);
            this.checkBoxGenerateFromStart.Location = new System.Drawing.Point(3, 113);
            this.checkBoxGenerateFromStart.Name = "checkBoxGenerateFromStart";
            this.checkBoxGenerateFromStart.Size = new System.Drawing.Size(244, 20);
            this.checkBoxGenerateFromStart.TabIndex = 17;
            this.checkBoxGenerateFromStart.Text = "Generate Sequence From Start Date";
            this.checkBoxGenerateFromStart.UseVisualStyleBackColor = true;
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
            this.groupBoxParameterInfo.Size = new System.Drawing.Size(266, 47);
            this.groupBoxParameterInfo.TabIndex = 12;
            this.groupBoxParameterInfo.TabStop = false;
            this.groupBoxParameterInfo.Text = "Parameter information";
            // 
            // tableLayoutPanelParameterInfo
            // 
            this.tableLayoutPanelParameterInfo.AutoSize = true;
            this.tableLayoutPanelParameterInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelParameterInfo.ColumnCount = 2;
            this.tableLayoutPanelParameterInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelParameterInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelParameterInfo.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelParameterInfo.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanelParameterInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelParameterInfo.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelParameterInfo.Name = "tableLayoutPanelParameterInfo";
            this.tableLayoutPanelParameterInfo.RowCount = 1;
            this.tableLayoutPanelParameterInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelParameterInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelParameterInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelParameterInfo.Size = new System.Drawing.Size(260, 26);
            this.tableLayoutPanelParameterInfo.TabIndex = 0;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.AutoSize = true;
            this.flowLayoutPanelButtons.Controls.Add(this.buttonUpdate);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(448, 3);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.tableLayoutPanelDefinition.SetRowSpan(this.flowLayoutPanelButtons, 3);
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(81, 262);
            this.flowLayoutPanelButtons.TabIndex = 13;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(3, 3);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 12;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Visible = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageDefinition);
            this.tabControlMain.Controls.Add(this.tabPagePublishing);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(3, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(546, 294);
            this.tabControlMain.TabIndex = 12;
            // 
            // tabPageDefinition
            // 
            this.tabPageDefinition.Controls.Add(this.tableLayoutPanelDefinition);
            this.tabPageDefinition.Location = new System.Drawing.Point(4, 22);
            this.tabPageDefinition.Name = "tabPageDefinition";
            this.tabPageDefinition.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefinition.Size = new System.Drawing.Size(538, 268);
            this.tabPageDefinition.TabIndex = 0;
            this.tabPageDefinition.Text = "Definition";
            this.tabPageDefinition.UseVisualStyleBackColor = true;
            // 
            // tabPagePublishing
            // 
            this.tabPagePublishing.Controls.Add(this.publishingInfoControl);
            this.tabPagePublishing.Location = new System.Drawing.Point(4, 22);
            this.tabPagePublishing.Name = "tabPagePublishing";
            this.tabPagePublishing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePublishing.Size = new System.Drawing.Size(538, 268);
            this.tabPagePublishing.TabIndex = 2;
            this.tabPagePublishing.Text = "Publishing";
            this.tabPagePublishing.UseVisualStyleBackColor = true;
            // 
            // publishingInfoControl
            // 
            this.publishingInfoControl.Category = "";
            this.publishingInfoControl.Comments = "";
            this.publishingInfoControl.Description = "";
            this.publishingInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publishingInfoControl.Export = false;
            this.publishingInfoControl.Location = new System.Drawing.Point(3, 3);
            this.publishingInfoControl.Name = "publishingInfoControl";
            this.publishingInfoControl.Size = new System.Drawing.Size(532, 262);
            this.publishingInfoControl.TabIndex = 0;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tabControlMain, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelStatusBar, 0, 1);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(552, 353);
            this.tableLayoutPanelMain.TabIndex = 14;
            // 
            // tableLayoutPanelStatusBar
            // 
            this.tableLayoutPanelStatusBar.AutoSize = true;
            this.tableLayoutPanelStatusBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelStatusBar.ColumnCount = 2;
            this.tableLayoutPanelStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelStatusBar.Controls.Add(this.tableLayoutPanelButtons, 1, 0);
            this.tableLayoutPanelStatusBar.Controls.Add(this.labelElementsCount, 0, 0);
            this.tableLayoutPanelStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelStatusBar.Location = new System.Drawing.Point(3, 303);
            this.tableLayoutPanelStatusBar.Name = "tableLayoutPanelStatusBar";
            this.tableLayoutPanelStatusBar.RowCount = 1;
            this.tableLayoutPanelStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanelStatusBar.Size = new System.Drawing.Size(546, 47);
            this.tableLayoutPanelStatusBar.TabIndex = 13;
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
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(365, 15);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(178, 29);
            this.tableLayoutPanelButtons.TabIndex = 14;
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
            // labelElementsCount
            // 
            this.labelElementsCount.AutoSize = true;
            this.labelElementsCount.Location = new System.Drawing.Point(8, 0);
            this.labelElementsCount.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labelElementsCount.Name = "labelElementsCount";
            this.labelElementsCount.Size = new System.Drawing.Size(59, 13);
            this.labelElementsCount.TabIndex = 15;
            this.labelElementsCount.Text = "0 Elements";
            // 
            // checkBoxFollowFrequency
            // 
            this.checkBoxFollowFrequency.AutoSize = true;
            this.tableLayoutPanelDatesSequence.SetColumnSpan(this.checkBoxFollowFrequency, 3);
            this.checkBoxFollowFrequency.Location = new System.Drawing.Point(3, 87);
            this.checkBoxFollowFrequency.Name = "checkBoxFollowFrequency";
            this.checkBoxFollowFrequency.Size = new System.Drawing.Size(133, 20);
            this.checkBoxFollowFrequency.TabIndex = 18;
            this.checkBoxFollowFrequency.Text = "Follow Frequency";
            this.checkBoxFollowFrequency.UseVisualStyleBackColor = true;
            // 
            // DateSequenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(555, 356);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateSequenceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit dates sequence";
            this.tableLayoutPanelDefinition.ResumeLayout(false);
            this.tableLayoutPanelDefinition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDates)).EndInit();
            this.groupBoxDatesSequence.ResumeLayout(false);
            this.groupBoxDatesSequence.PerformLayout();
            this.tableLayoutPanelDatesSequence.ResumeLayout(false);
            this.tableLayoutPanelDatesSequence.PerformLayout();
            this.groupBoxParameterInfo.ResumeLayout(false);
            this.groupBoxParameterInfo.PerformLayout();
            this.tableLayoutPanelParameterInfo.ResumeLayout(false);
            this.tableLayoutPanelParameterInfo.PerformLayout();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageDefinition.ResumeLayout(false);
            this.tabPageDefinition.PerformLayout();
            this.tabPagePublishing.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutPanelStatusBar.ResumeLayout(false);
            this.tableLayoutPanelStatusBar.PerformLayout();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDefinition;
        private System.Windows.Forms.GroupBox groupBoxDatesSequence;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDatesSequence;
        private System.Windows.Forms.GroupBox groupBoxParameterInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelParameterInfo;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageDefinition;
        private System.Windows.Forms.TabPage tabPagePublishing;
        private DVPForms.Controls.PublishingInfoControl publishingInfoControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.DataGridView dataGridViewDates;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxExcludeStartDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelStatusBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelElementsCount;
        private ExpressionTypyingHelper.OneLineExpressionControl expressionStartDate;
        private ExpressionTypyingHelper.OneLineExpressionControl expressionEndDate;
        private System.Windows.Forms.ComboBox comboBoxFrequency;
        private System.Windows.Forms.CheckBox checkBoxGenerateFromStart;
        private System.Windows.Forms.CheckBox checkBoxFollowFrequency;
    }
}
