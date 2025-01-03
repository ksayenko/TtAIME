namespace RunManatea
{
    partial class LabelAndTextBox2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.theLabel = new System.Windows.Forms.Label();
            this.theTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.theTextBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 122);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // theLabel
            // 
            this.theLabel.AutoSize = true;
            this.theLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.theLabel.Location = new System.Drawing.Point(3, 0);
            this.theLabel.Name = "theLabel";
            this.theLabel.Size = new System.Drawing.Size(152, 122);
            this.theLabel.TabIndex = 0;
            this.theLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.theLabel.UseCompatibleTextRendering = true;
            // 
            // theTextBox
            // 
            this.theTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.theTextBox.Location = new System.Drawing.Point(161, 3);
            this.theTextBox.Multiline = true;
            this.theTextBox.Name = "theTextBox";
            this.theTextBox.Size = new System.Drawing.Size(232, 116);
            this.theTextBox.TabIndex = 1;
            this.theTextBox.Text = "0";
            this.theTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.theTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.theTextBox_KeyPress);
            // 
            // LabelAndTextBox2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LabelAndTextBox2";
            this.Size = new System.Drawing.Size(396, 122);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label theLabel;
        private System.Windows.Forms.TextBox theTextBox;
    }
}
