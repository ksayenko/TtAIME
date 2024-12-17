namespace RunManatea
{
    partial class SoundSourceConfiguration
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
            this.label5 = new System.Windows.Forms.Label();
            this.lblPiles = new System.Windows.Forms.Label();
            this.btnPile = new System.Windows.Forms.Button();
            this.rtbdbSeaGridInfo = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadDbSeaGrid = new System.Windows.Forms.Button();
            this.labelControlName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxSourceType = new System.Windows.Forms.ListBox();
            this.listBoxMetric = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(300, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 124);
            this.label5.TabIndex = 4;
            this.label5.Text = "Select Metric";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPiles
            // 
            this.lblPiles.AutoSize = true;
            this.lblPiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPiles.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPiles.Location = new System.Drawing.Point(594, 204);
            this.lblPiles.Name = "lblPiles";
            this.tableLayoutPanel1.SetRowSpan(this.lblPiles, 2);
            this.lblPiles.Size = new System.Drawing.Size(196, 248);
            this.lblPiles.TabIndex = 9;
            this.lblPiles.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnPile
            // 
            this.btnPile.AutoSize = true;
            this.btnPile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPile.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPile.Location = new System.Drawing.Point(594, 83);
            this.btnPile.MaximumSize = new System.Drawing.Size(120, 50);
            this.btnPile.MinimumSize = new System.Drawing.Size(120, 50);
            this.btnPile.Name = "btnPile";
            this.btnPile.Size = new System.Drawing.Size(120, 50);
            this.btnPile.TabIndex = 3;
            this.btnPile.Text = "Pile Parameters";
            this.btnPile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPile.UseVisualStyleBackColor = false;
            this.btnPile.Click += new System.EventHandler(this.btnPile_Click);
            // 
            // rtbdbSeaGridInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.rtbdbSeaGridInfo, 3);
            this.rtbdbSeaGridInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbdbSeaGridInfo.Location = new System.Drawing.Point(155, 85);
            this.rtbdbSeaGridInfo.Margin = new System.Windows.Forms.Padding(5);
            this.rtbdbSeaGridInfo.Name = "rtbdbSeaGridInfo";
            this.tableLayoutPanel1.SetRowSpan(this.rtbdbSeaGridInfo, 2);
            this.rtbdbSeaGridInfo.Size = new System.Drawing.Size(431, 238);
            this.rtbdbSeaGridInfo.TabIndex = 7;
            this.rtbdbSeaGridInfo.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 124);
            this.label3.TabIndex = 2;
            this.label3.Text = "Source Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnLoadDbSeaGrid
            // 
            this.btnLoadDbSeaGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadDbSeaGrid.AutoSize = true;
            this.btnLoadDbSeaGrid.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLoadDbSeaGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadDbSeaGrid.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadDbSeaGrid.Location = new System.Drawing.Point(27, 83);
            this.btnLoadDbSeaGrid.MaximumSize = new System.Drawing.Size(120, 50);
            this.btnLoadDbSeaGrid.MinimumSize = new System.Drawing.Size(120, 50);
            this.btnLoadDbSeaGrid.Name = "btnLoadDbSeaGrid";
            this.btnLoadDbSeaGrid.Size = new System.Drawing.Size(120, 50);
            this.btnLoadDbSeaGrid.TabIndex = 1;
            this.btnLoadDbSeaGrid.Text = "DbSea Grid :";
            this.btnLoadDbSeaGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadDbSeaGrid.UseVisualStyleBackColor = false;
            this.btnLoadDbSeaGrid.Click += new System.EventHandler(this.btnLoadDbSeaGrid_Click);
            // 
            // labelControlName
            // 
            this.labelControlName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelControlName, 4);
            this.labelControlName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControlName.Font = new System.Drawing.Font("Calibri", 16.2F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlName.Location = new System.Drawing.Point(153, 0);
            this.labelControlName.Name = "labelControlName";
            this.labelControlName.Size = new System.Drawing.Size(637, 80);
            this.labelControlName.TabIndex = 0;
            this.labelControlName.Text = "Sound Source Configuration";
            this.labelControlName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControlName.Click += new System.EventHandler(this.labelControlName_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel1.Controls.Add(this.labelControlName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLoadDbSeaGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.rtbdbSeaGridInfo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPile, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPiles, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.listBoxSourceType, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.listBoxMetric, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.78628F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.40458F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.40458F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.40458F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 470);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listBoxSourceType
            // 
            this.listBoxSourceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSourceType.ItemHeight = 18;
            this.listBoxSourceType.Location = new System.Drawing.Point(153, 331);
            this.listBoxSourceType.Name = "listBoxSourceType";
            this.listBoxSourceType.Size = new System.Drawing.Size(141, 118);
            this.listBoxSourceType.TabIndex = 10;
            this.listBoxSourceType.SelectedIndexChanged += new System.EventHandler(this.listViewSourceType_SelectedIndexChanged);
            // 
            // listBoxMetric
            // 
            this.listBoxMetric.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMetric.ItemHeight = 18;
            this.listBoxMetric.Location = new System.Drawing.Point(447, 331);
            this.listBoxMetric.Name = "listBoxMetric";
            this.listBoxMetric.Size = new System.Drawing.Size(141, 118);
            this.listBoxMetric.TabIndex = 11;
            this.listBoxMetric.SelectedIndexChanged += new System.EventHandler(this.listBoxMetric_SelectedIndexChanged);
            // 
            // SoundSourceConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SoundSourceConfiguration";
            this.Size = new System.Drawing.Size(793, 470);
            this.Load += new System.EventHandler(this.SoundSourceConfiguration_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPiles;
        private System.Windows.Forms.Button btnPile;
        private System.Windows.Forms.RichTextBox rtbdbSeaGridInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelControlName;
        private System.Windows.Forms.Button btnLoadDbSeaGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxSourceType;
        private System.Windows.Forms.ListBox listBoxMetric;
    }
}
