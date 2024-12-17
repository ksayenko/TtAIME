namespace RunManatea
{
    partial class PileProgressionForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.HammerEnergyPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HammerEnergykJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration_minutes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlowsperMinute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberofBlows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DecibeldBOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HammerEnergyPercent,
            this.HammerEnergykJ,
            this.Duration_minutes,
            this.BlowsperMinute,
            this.NumberofBlows,
            this.DecibeldBOffset});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(794, 354);
            this.dataGridView1.TabIndex = 0;
            // 
            // HammerEnergyPercent
            // 
            this.HammerEnergyPercent.HeaderText = "Hammer Energy (%)";
            this.HammerEnergyPercent.MinimumWidth = 6;
            this.HammerEnergyPercent.Name = "HammerEnergyPercent";
            this.HammerEnergyPercent.Width = 125;
            // 
            // HammerEnergykJ
            // 
            this.HammerEnergykJ.HeaderText = "Hammer Energy (kJ)";
            this.HammerEnergykJ.MinimumWidth = 6;
            this.HammerEnergykJ.Name = "HammerEnergykJ";
            this.HammerEnergykJ.Width = 125;
            // 
            // Duration_minutes
            // 
            this.Duration_minutes.HeaderText = "Duration (minutes)";
            this.Duration_minutes.MinimumWidth = 6;
            this.Duration_minutes.Name = "Duration_minutes";
            this.Duration_minutes.Width = 125;
            // 
            // BlowsperMinute
            // 
            this.BlowsperMinute.HeaderText = "Blows per Minute";
            this.BlowsperMinute.MinimumWidth = 6;
            this.BlowsperMinute.Name = "BlowsperMinute";
            this.BlowsperMinute.Width = 125;
            // 
            // NumberofBlows
            // 
            this.NumberofBlows.HeaderText = "Number of Blows";
            this.NumberofBlows.MinimumWidth = 6;
            this.NumberofBlows.Name = "NumberofBlows";
            this.NumberofBlows.Width = 125;
            // 
            // DecibeldBOffset
            // 
            this.DecibeldBOffset.HeaderText = "Decibel (dB) Offset";
            this.DecibeldBOffset.MinimumWidth = 6;
            this.DecibeldBOffset.Name = "DecibeldBOffset";
            this.DecibeldBOffset.Width = 125;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PileProgressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PileProgressionForm";
            this.Text = "PileProgressionForm";
            this.Load += new System.EventHandler(this.PileProgressionForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn HammerEnergyPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn HammerEnergykJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration_minutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn BlowsperMinute;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberofBlows;
        private System.Windows.Forms.DataGridViewTextBoxColumn DecibeldBOffset;
        private System.Windows.Forms.Button button1;
    }
}