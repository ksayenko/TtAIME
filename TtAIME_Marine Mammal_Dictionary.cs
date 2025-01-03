using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;

namespace RunManatea
{
    public partial class TtAIME_Marine_Mammal_Dictionary : Form
    {
        AppSettings set;
        LogHandler lh;
        Marine_Mammal_Dictionary dict;

        //*

        private System.Windows.Forms.DataGridViewTextBoxColumn MarineSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScientificName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HearingGroup;
        private int iMarineSpecies = 0;
        private int iScientificName = 1;
        private int iHearingGroup = 2;

        private System.Windows.Forms.DataGridViewTextBoxColumn[] AcousticThresholds;
        private int iStartThreshhods = 3;
        List<Marine_Species_Threshhold> defaultList;
        List<StandardAcousticThreshold> headers;

        public TtAIME_Marine_Mammal_Dictionary(AppSettings set, LogHandler lh)
        {
            InitializeComponent();
            this.set = set;
            this.lh = lh;

            dict = new Marine_Mammal_Dictionary(set, lh);
            defaultList = dict.DefaultList;
            headers = dict.Headers;


            CreateGridView();


        }

        public void CreateGridView()
        {
            if (dict == null)
            {
                return;
            }


            this.MarineSpecies = new DataGridViewTextBoxColumn();
            this.MarineSpecies.HeaderText = "Marine Species";
            this.MarineSpecies.MinimumWidth = 6;
            this.MarineSpecies.Width = 100;
            this.dataGridView1.Columns.Add(this.MarineSpecies);

            this.ScientificName = new DataGridViewTextBoxColumn();
            this.ScientificName.HeaderText = "Scientific Name";
            this.ScientificName.MinimumWidth = 6;
            this.ScientificName.Width = 100;
            this.dataGridView1.Columns.Add(this.ScientificName);


            this.HearingGroup = new DataGridViewTextBoxColumn();
            this.HearingGroup.HeaderText = "Hearing Group/Auditory Weighting Function";
            this.HearingGroup.MinimumWidth = 6;
            this.HearingGroup.Width = 100;
            this.dataGridView1.Columns.Add(this.HearingGroup);

            AcousticThresholds = new DataGridViewTextBoxColumn[headers.Count]
                ;
            for (int i = 0; i < headers.Count; i++)
            {
                AcousticThresholds[i] = new DataGridViewTextBoxColumn();
                this.AcousticThresholds[i].HeaderText = headers[i].ToString();

                this.AcousticThresholds[i].MinimumWidth = 6;
                this.AcousticThresholds[i].Width = 100;
                int j = this.dataGridView1.Columns.Add(this.AcousticThresholds[i]);
                this.dataGridView1.Columns[j].Name = headers[i].Threshhold.Name;

            }
            dataGridView1.AutoResizeColumnHeadersHeight();

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowHeadersVisible = true;

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            int c = e.ColumnIndex;
            int r = e.RowIndex;

            string sValue = dataGridView1.Rows[r].Cells[c].Value.ToString();
            string name = dataGridView1.Rows[r].Cells[0].Value.ToString();
            string scientificName = dataGridView1.Rows[r].Cells[1].Value.ToString();
            string sa = dataGridView1.Rows[0].Cells[c].Value.ToString();
            string header = AcousticThresholds[c].HeaderText;
            //string header = dataGridView1.
            Acoustic_Threshold thr = Marine_Mammal_Dictionary.GetSpeciesThreshhold(dict, scientificName, header);
            try
            { int a = 2; }
            catch (Exception ex)
            { lh.LogError(ex); }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            { int a = 2; }
            catch (Exception ex)
            { lh.LogError(ex); }
        }

       
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                e.Handled = true;
                DataGridViewCell cell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.CurrentCell = cell;
                dataGridView1.BeginEdit(true);
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

        public void PopulateData()
        {
            List<Marine_Species> distSpec = dict.SortByOrderHearingGroupName(dict.DistinctSpecies());

            if (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows.Clear();



            foreach (Marine_Species m in distSpec)
            {
                var index = this.dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[iMarineSpecies].Value = m.Name;
                dataGridView1.Rows[index].Cells[iScientificName].Value = m.Scientific_name;
                dataGridView1.Rows[index].Cells[iHearingGroup].Value = m.HearingGroup;
                dataGridView1.Rows[index].HeaderCell.Value = "# " + (m.OrderInTheList + 1).ToString();


                for (int i = 0; i < headers.Count; i++)
                {
                    StandardAcousticThreshold standardAcousticThreshold = headers[i];

                    //look for the value
                    string value = "";
                    double dValue = -88;
                    bool bExists1 = false;

                    foreach (Marine_Species_Threshhold mstr in defaultList)
                    {
                        Marine_Species ms1 = mstr.Marine_Species;
                        Acoustic_Threshold thresh = mstr.Acoustic_Threshold;

                        if (ms1.CompareTo(m) == 0 &&
                             headers[i].Threshhold.CompareByName(thresh) == 0)

                        {
                            bExists1 = true;
                            dValue = thresh.DThreshold;
                            break;
                        }
                    }

                    if (bExists1)
                    {
                        if (!double.IsNaN(dValue) && dValue > 0)
                        {
                            value = dValue.ToString();
                        }
                        else
                        {
                            value = "Not applicable";
                        }
                    }
                    dataGridView1.Rows[index].Cells[iStartThreshhods + i].Value = value;

                }

            }


        }



        private void TtAIME_Marine_Mammal_Dictionary_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
    }
}
