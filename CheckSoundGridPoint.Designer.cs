namespace RunManatea
{
    partial class CheckSoundGridPoint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckSoundGridPoint));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtX = new RunManatea.LabelAndTextBox2();
            this.txtValue = new RunManatea.LabelAndTextBox2();
            this.txtY = new RunManatea.LabelAndTextBox2();
            this.txtDepth = new RunManatea.LabelAndTextBox2();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.txtX, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtValue, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtY, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDepth, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonOK, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 4, 3);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 189);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtX
            // 
            this.txtX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.txtX, 2);
            this.txtX.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX.IsDecimalNumber = true;
            this.txtX.IsTextBoxMultiline = true;
            this.txtX.LabelMargin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtX.LabelText = "      X";
            this.txtX.Location = new System.Drawing.Point(3, 23);
            this.txtX.Name = "txtX";
            this.txtX.Orientation = RunManatea.LabelAndTextBox2Orientation.LabelTBOneRow;
            this.txtX.PercentTextBox = 70;
            this.txtX.Size = new System.Drawing.Size(288, 54);
            this.txtX.TabIndex = 7;
            this.txtX.TextBoxMargin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.txtX.TextBoxValue = "0";
            // 
            // txtValue
            // 
            this.txtValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.txtValue, 6);
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.IsDecimalNumber = false;
            this.txtValue.IsTextBoxMultiline = true;
            this.txtValue.LabelMargin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtValue.LabelText = "      Recived Level";
            this.txtValue.Location = new System.Drawing.Point(3, 83);
            this.txtValue.Name = "txtValue";
            this.txtValue.Orientation = RunManatea.LabelAndTextBox2Orientation.LabelTBOneRow;
            this.txtValue.PercentTextBox = 60;
            this.txtValue.Size = new System.Drawing.Size(878, 54);
            this.txtValue.TabIndex = 9;
            this.txtValue.TextBoxMargin = new System.Windows.Forms.Padding(3);
            this.txtValue.TextBoxValue = "0";
            // 
            // txtY
            // 
            this.txtY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.txtY, 2);
            this.txtY.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY.IsDecimalNumber = true;
            this.txtY.IsTextBoxMultiline = true;
            this.txtY.LabelMargin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtY.LabelText = "     Y";
            this.txtY.Location = new System.Drawing.Point(297, 23);
            this.txtY.Name = "txtY";
            this.txtY.Orientation = RunManatea.LabelAndTextBox2Orientation.LabelTBOneRow;
            this.txtY.PercentTextBox = 70;
            this.txtY.Size = new System.Drawing.Size(288, 54);
            this.txtY.TabIndex = 8;
            this.txtY.TextBoxMargin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.txtY.TextBoxValue = "0";
            // 
            // txtDepth
            // 
            this.txtDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.txtDepth, 2);
            this.txtDepth.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepth.IsDecimalNumber = true;
            this.txtDepth.IsTextBoxMultiline = true;
            this.txtDepth.LabelMargin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtDepth.LabelText = "     Depth";
            this.txtDepth.Location = new System.Drawing.Point(591, 23);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Orientation = RunManatea.LabelAndTextBox2Orientation.LabelTBOneRow;
            this.txtDepth.PercentTextBox = 70;
            this.txtDepth.Size = new System.Drawing.Size(290, 54);
            this.txtDepth.TabIndex = 10;
            this.txtDepth.TextBoxMargin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.txtDepth.TextBoxValue = "0";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(150, 143);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(141, 44);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(591, 143);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(141, 44);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // CheckSoundGridPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 189);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1200, 350);
            this.Name = "CheckSoundGridPoint";
            this.Text = "CheckSoundGridPoint";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private LabelAndTextBox2 txtX;
        private LabelAndTextBox2 txtY;
        private LabelAndTextBox2 txtValue;
        private LabelAndTextBox2 txtDepth;
    }
}