namespace RunManatea
{
    partial class Manatea
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBinaryGridFileforSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBinaryGridFromDbSeaItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBinaryGridFromCSVItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayPileFileAndMakeChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxProgress = new System.Windows.Forms.RichTextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesLocationToolStripMenuItem,
            this.createBinaryGridFileforSpeedToolStripMenuItem,
            this.displayPileFileAndMakeChangesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(911, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesLocationToolStripMenuItem
            // 
            this.filesLocationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileLocationsToolStripMenuItem,
            this.logsFolderToolStripMenuItem});
            this.filesLocationToolStripMenuItem.Name = "filesLocationToolStripMenuItem";
            this.filesLocationToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.filesLocationToolStripMenuItem.Text = "&File";
            this.filesLocationToolStripMenuItem.Click += new System.EventHandler(this.filesLocationToolStripMenuItem_Click);
            // 
            // fileLocationsToolStripMenuItem
            // 
            this.fileLocationsToolStripMenuItem.Name = "fileLocationsToolStripMenuItem";
            this.fileLocationsToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.fileLocationsToolStripMenuItem.Text = "&File Locations";
            this.fileLocationsToolStripMenuItem.Click += new System.EventHandler(this.fileLocationsToolStripMenuItem_Click);
            // 
            // logsFolderToolStripMenuItem
            // 
            this.logsFolderToolStripMenuItem.Name = "logsFolderToolStripMenuItem";
            this.logsFolderToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.logsFolderToolStripMenuItem.Text = "&Logs Folder";
            this.logsFolderToolStripMenuItem.Click += new System.EventHandler(this.logsFolderToolStripMenuItem_Click);
            // 
            // createBinaryGridFileforSpeedToolStripMenuItem
            // 
            this.createBinaryGridFileforSpeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createBinaryGridFromDbSeaItem,
            this.createBinaryGridFromCSVItem});
            this.createBinaryGridFileforSpeedToolStripMenuItem.Name = "createBinaryGridFileforSpeedToolStripMenuItem";
            this.createBinaryGridFileforSpeedToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.createBinaryGridFileforSpeedToolStripMenuItem.Text = "Create Binary &Grid File";
            this.createBinaryGridFileforSpeedToolStripMenuItem.Click += new System.EventHandler(this.createBinaryGridFileforSpeedToolStripMenuItem_Click);
            // 
            // createBinaryGridFromDbSeaItem
            // 
            this.createBinaryGridFromDbSeaItem.Name = "createBinaryGridFromDbSeaItem";
            this.createBinaryGridFromDbSeaItem.Size = new System.Drawing.Size(364, 26);
            this.createBinaryGridFromDbSeaItem.Text = "Create Binary Grid From DBSea Ascii files";
            this.createBinaryGridFromDbSeaItem.Click += new System.EventHandler(this.createBinaryGridFromDbSeaItem_Click);
            // 
            // createBinaryGridFromCSVItem
            // 
            this.createBinaryGridFromCSVItem.Name = "createBinaryGridFromCSVItem";
            this.createBinaryGridFromCSVItem.Size = new System.Drawing.Size(364, 26);
            this.createBinaryGridFromCSVItem.Text = "Create Binary Grid From csv";
            this.createBinaryGridFromCSVItem.Click += new System.EventHandler(this.createBinaryGridFromCSVItem_Click);
            // 
            // displayPileFileAndMakeChangesToolStripMenuItem
            // 
            this.displayPileFileAndMakeChangesToolStripMenuItem.Name = "displayPileFileAndMakeChangesToolStripMenuItem";
            this.displayPileFileAndMakeChangesToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.displayPileFileAndMakeChangesToolStripMenuItem.Text = "&Pile Data File ";
            this.displayPileFileAndMakeChangesToolStripMenuItem.Click += new System.EventHandler(this.displayPileFileAndMakeChangesToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.button2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxProgress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelInfo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 494);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button2.Location = new System.Drawing.Point(695, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 126);
            this.button2.TabIndex = 3;
            this.button2.Text = "Open Output Folder";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxProgress
            // 
            this.textBoxProgress.BackColor = System.Drawing.Color.Azure;
            this.textBoxProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxProgress, 4);
            this.textBoxProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxProgress.Location = new System.Drawing.Point(11, 168);
            this.textBoxProgress.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxProgress.Name = "textBoxProgress";
            this.tableLayoutPanel1.SetRowSpan(this.textBoxProgress, 3);
            this.textBoxProgress.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.textBoxProgress.Size = new System.Drawing.Size(889, 315);
            this.textBoxProgress.TabIndex = 1;
            this.textBoxProgress.Text = "";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.SetColumnSpan(this.labelInfo, 2);
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelInfo.Location = new System.Drawing.Point(231, 1);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(447, 156);
            this.labelInfo.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button1.Location = new System.Drawing.Point(14, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 126);
            this.button1.TabIndex = 0;
            this.button1.Text = "Read  Files Run Manatea";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Manatea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 524);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Manatea";
            this.Text = "Manatea";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

                    }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBinaryGridFileforSpeedToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox textBoxProgress;
        private System.Windows.Forms.ToolStripMenuItem displayPileFileAndMakeChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBinaryGridFromCSVItem;
        private System.Windows.Forms.ToolStripMenuItem createBinaryGridFromDbSeaItem;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem fileLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsFolderToolStripMenuItem;
    }
}