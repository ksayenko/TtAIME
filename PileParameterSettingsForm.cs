using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;

namespace RunManatea
{
    public partial class PileParameterSettingsForm : Form
    {
        PileProgressions piles;

        PileProgressions pilesTemp;
        AppSettings appSettings;    
       
        private System.Windows.Forms.DataGridViewTextBoxColumn HammerEnergyPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn HammerEnergykJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration_seconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrikeRateMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrikeRateHz;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberofBlows;
        private System.Windows.Forms.DataGridViewTextBoxColumn DecibeldBOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn DbAdjustment;

        private System.Windows.Forms.DataGridViewButtonColumn btnInsertRow;
        private System.Windows.Forms.DataGridViewButtonColumn btnRemoveRow;
        private int iColumnHammerEnergyPercent;
        private int iColumnHammerEnergykJ;
        private int iColumnDuration_seconds;
        private int iColumnStrikeRateMin;
        private int iColumnStrikeRateHz;
        private int iColumnNumberofBlows;
        private int iColumnDecibeldBOffset;
        private int iColumnDbAdjustment;
        private int iColumnInsert;
        private int iColumnRemove;

        private int nCol;

        public PileProgressions Piles { get => piles; set { piles = value; pilesTemp = piles.Copy(); LoadPiles(); } }
    

        public PileParameterSettingsForm()
        {
            InitializeComponent();
            SetGridview();
            Piles = new PileProgressions();

                   
            if (piles.Read(appSettings.PileCSV) >0)
            LoadPiles();
        }

        private void PileParameterSettingsForm_Load(object sender, EventArgs e)
        {
            pilesTemp = piles.Copy();
            LoadPiles();
        }
        public PileParameterSettingsForm(PileProgressions p, AppSettings settings)  
        {
            InitializeComponent();
            SetGridview();
            this.piles = p;
            this.appSettings = settings;
            pilesTemp = piles.Copy();
            LoadPiles();
        }

        public PileParameterSettingsForm(AppSettings settings)
        {
            InitializeComponent();
            SetGridview();
         
            this.appSettings = settings;
            piles = new PileProgressions();
            piles.Read(appSettings.PileCSV);
            pilesTemp = piles.Copy();
            LoadPiles();
        }
        private double ConvertCellValue(DataGridViewRow row, int iCell)
        {
            object value = row.Cells[iCell].Value;

            if (value == null)
                return Double.NaN;

            double v = -88;

            LogHandler lh = new LogHandler();
            try
            {
                v = Double.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                lh.LogError("Can't convert " + value.ToString());
            }

            return v;

        }

        private void LoadPiles( )
        {
            if (dataGridView1.Rows.Count > 0)   
            dataGridView1.Rows.Clear();
            piles.ListPileProgression.Sort();

            foreach (PileProgression p in piles.ListPileProgression)
            {
                var index = this.dataGridView1.Rows.Add();



                dataGridView1.Rows[index].Cells[iColumnHammerEnergyPercent].Value = p.HammerEnergypercent;
                dataGridView1.Rows[index].Cells[iColumnHammerEnergykJ].Value = p.HammerEnergykJ;
                dataGridView1.Rows[index].Cells[iColumnDuration_seconds].Value = p.Duration;
                dataGridView1.Rows[index].Cells[iColumnStrikeRateMin].Value = p.BlowsPerMinute;

                dataGridView1.Rows[index].Cells[iColumnStrikeRateHz].Value = p.Blow_rate_Hz;

                dataGridView1.Rows[index].Cells[iColumnNumberofBlows].Value = p.NumberOfStrikes;
                dataGridView1.Rows[index].Cells[iColumnDecibeldBOffset].Value = p.DecibelOffset;
                dataGridView1.Rows[index].Cells[iColumnDbAdjustment].Value = p.DBadjust;


                dataGridView1.Rows[index].Cells[iColumnDbAdjustment + 1].Value = "+";
               dataGridView1.Rows[index].Cells[iColumnDbAdjustment + 2].Value = "-";
            }

            txtEquip.Text = piles.Equipment.ToString();
            txtMaximumHammerEnergy.Text = piles.MaximumHammerEnergy.ToString();
            txtPileDiam.Text = piles.PileDiameter_m.ToString();
            txtSourceLevel.Text = piles.SourceLevelDB.ToString();
         
            txtNumberOfStrikes.Text=piles.NumStrikes().ToString();  




        }

        // Calls the Employee.RequestStatus method.
        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int row = e.RowIndex;
            int col = e.ColumnIndex;
            // Ignore clicks that are not on button cells. 

            if (col != iColumnRemove && col != iColumnInsert)
                return;

            if (col == iColumnRemove)
            {
                dataGridView1.Rows.RemoveAt(row);
                PileProgression premove = piles.ListPileProgression[row];
                piles.ListPileProgression.Remove(premove);
            }


            if (col == iColumnInsert && row > -1)
            {
                DataGridViewRow oldrow = dataGridView1.Rows[row];
                DataGridViewRow newrow = (DataGridViewRow)oldrow.Clone();
                int j = 0;
                for (j = 0; j < oldrow.Cells.Count; j++)
                {

                    newrow.Cells[j].Value = oldrow.Cells[j].Value;                 
                }


                dataGridView1.Rows.Insert(row + 1, newrow);

                dataGridView1.Rows[row].Selected = true;
            }



        }
        private void SetGridview()
        {
            int i = 0;
            iColumnHammerEnergyPercent = i++;
            iColumnHammerEnergykJ=i++;
            iColumnDuration_seconds = i++;
            iColumnStrikeRateMin = i++;
            iColumnStrikeRateHz = i++;
            iColumnNumberofBlows = i++;
            iColumnDecibeldBOffset = i++;
            iColumnDbAdjustment = i++;
            iColumnInsert = i++;
            iColumnRemove = i;
            nCol = i + 1;
         

            // 
            // HammerEnergyPercent
            // 
            this.HammerEnergyPercent = new DataGridViewTextBoxColumn();
            this.HammerEnergyPercent.HeaderText = "Hammer Energy (%)";
            this.HammerEnergyPercent.MinimumWidth = 6;       
            this.HammerEnergyPercent.Width = 100;
            this.dataGridView1.Columns.Add(this.HammerEnergyPercent);
        
            // 
            // HammerEnergykJ
            // 
            this.HammerEnergykJ = new DataGridViewTextBoxColumn();
            this.HammerEnergykJ.HeaderText = "Hammer Energy (kJ)";
            this.HammerEnergykJ.MinimumWidth = 6;  
            this.HammerEnergykJ.Width = 100;
            this.dataGridView1.Columns.Add(HammerEnergykJ);
            // 
            // Duration_minutes
            // 
            this.Duration_seconds = new DataGridViewTextBoxColumn();    
            this.Duration_seconds.HeaderText = "Duration (seconds)";
            this.Duration_seconds.MinimumWidth = 6;        
            this.Duration_seconds.Width = 100;
            this.dataGridView1.Columns.Add(Duration_seconds);
            // 
            // BlowsperMinute
            // 
            this.StrikeRateMin= new DataGridViewTextBoxColumn();    
            this.StrikeRateMin.HeaderText = "Strike Rate (Minute)";
            this.StrikeRateMin.MinimumWidth = 6;     
            this.StrikeRateMin.Width = 100;
            this.dataGridView1.Columns.Add(StrikeRateMin);
            //     // BlowsperMinute
            // 
            this.StrikeRateHz = new DataGridViewTextBoxColumn();    
            this.StrikeRateHz.HeaderText = "Strike Rate (Hz)";         
            this.StrikeRateHz.MinimumWidth = 6;
       
            this.StrikeRateHz.Width = 100;
            this.dataGridView1.Columns.Add(StrikeRateHz);
            dataGridView1.Columns[iColumnStrikeRateHz].ReadOnly = true;

            // 
            // NumberofBlows
            // 
            this.NumberofBlows= new DataGridViewTextBoxColumn();    
            this.NumberofBlows.HeaderText = "Number of Blows";
            this.NumberofBlows.MinimumWidth = 6;
       
            this.NumberofBlows.Width = 100;
            this.dataGridView1.Columns.Add(NumberofBlows);
            // 
            // DecibeldBOffset
            // 
            this.DecibeldBOffset= new DataGridViewTextBoxColumn();
            this.DecibeldBOffset.HeaderText = "Decibel (dB) Offset";
            this.DecibeldBOffset.MinimumWidth = 6;
            this.DecibeldBOffset.Width = 100;
            this.dataGridView1.Columns.Add(DecibeldBOffset);

            // 
            // DecibeldBOffset
            // 
            this.DbAdjustment= new DataGridViewTextBoxColumn();
            this.DbAdjustment.HeaderText = "dB Adjustment";
            this.DbAdjustment.MinimumWidth = 6;
            this.DbAdjustment.Width = 100;
            this.dataGridView1.Columns.Add(DbAdjustment);

            //buttons

            this.btnInsertRow = new DataGridViewButtonColumn();
            this.dataGridView1.Columns.Add(btnInsertRow);
            this.btnInsertRow.Width = 20;
            btnInsertRow.UseColumnTextForButtonValue = true;    
            this.btnInsertRow.HeaderText = "+";
         
            this.btnRemoveRow = new DataGridViewButtonColumn();
            this.btnRemoveRow.HeaderText = "-";
            this.btnRemoveRow.Width = 20;
            btnRemoveRow.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(btnRemoveRow);


            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

            // Add a CellClick handler to handle clicks in the button column.
            dataGridView1.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

        if (row >= piles.Count)
                return; 
            CalculatePiles();
            if (e.ColumnIndex == iColumnStrikeRateMin)
            {
                txtNumberOfStrikes.Text = piles.TotalNumberofStrikes.ToString();
                dataGridView1.Rows[row].Cells[iColumnStrikeRateHz].Value = piles.ListPileProgression[row].Blow_rate_Hz;
            }

            if (e.ColumnIndex == iColumnHammerEnergykJ)
            {
                txtNumberOfStrikes.Text = piles.MaximumHammerEnergy.ToString(); 
            }



        }

        public void CopyToClipboardWithHeaders(DataGridView _dgv)
        {
            //Copy to clipboard
            _dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = _dgv.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void CalculatePiles()
        {
            LogHandler lh = new LogHandler();

            piles.ListPileProgression.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                //check first item
                object first = row.Cells[0].Value;
                if (first == null || first.ToString() == "")
                    continue;// skipping the row
                try
                {
                    PileProgression p = new PileProgression();
                 
                    p.HammerEnergypercent = ConvertCellValue(row, iColumnHammerEnergyPercent);
                    p.HammerEnergykJ = ConvertCellValue(row, iColumnHammerEnergykJ);
                    p.Duration = ConvertCellValue(row, iColumnDuration_seconds);
                    p.BlowsPerMinute = ConvertCellValue(row, iColumnStrikeRateMin);

                    p.NumberOfStrikes = (int)ConvertCellValue(row, iColumnNumberofBlows);
                    p.DecibelOffset = ConvertCellValue(row, iColumnDecibeldBOffset);
                   

                    p.InitialPileCalculations();
                    piles.ListPileProgression.Add(p);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    lh.LogError(ex);
                }


            }

            piles.ListPileProgression.Sort();
            piles.Equipment = txtEquip.Text;
            piles.SourceLevelDB = Double.Parse(txtSourceLevel.Text);
            piles.PileDiameter_m = Double.Parse(txtPileDiam.Text);

            piles.MaximumHammerEnergy = piles.ListPileProgression[piles.Count - 1].HammerEnergykJ;
            piles.TotalNumberofStrikes = piles.NumStrikes();

        }

            private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isDigit = !char.IsDigit(e.KeyChar)
                    && !char.IsControl(e.KeyChar);
            if (isDigit)
            {
                bool isDot = e.KeyChar == '.';

                if (isDot)
                {
                    string text = ((TextBox)sender).Text;
                    int iDots = text.IndexOf('.');
                    if (iDots < 0) //not any dots
                        isDigit = false;


                }
            }
            e.Handled = isDigit;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadFromCSV_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please Choose the Pile Progression  File";
            ofd.Filter = "csm files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.DefaultExt = "csv";
            if (appSettings == null) { appSettings = new AppSettings(); }

            if (DialogResult.OK == ofd.ShowDialog())
            {
                appSettings.PileCSV = ofd.FileName;
                appSettings.SaveSetting(APP_Config_Keys.pileCSV, appSettings.PileCSV);
                piles.Read(appSettings.PileCSV);
                LoadPiles();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            piles = pilesTemp;
            LoadPiles();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            CalculatePiles();
            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                appSettings.PileCSV = saveFileDialog.FileName;
                appSettings.SaveSetting(APP_Config_Keys.pileCSV, appSettings.PileCSV);
                piles.Save(appSettings.PileCSV);
           
            }

            this.DialogResult = DialogResult.OK;    
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
