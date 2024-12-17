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
    public partial class PileParameterVibratoryForm : Form
    {
        PileProgressionVibratory piles;

        PileProgressionVibratory pilesTemp;
        AppSettings appSettings;    
       
      

        public PileProgressionVibratory Piles { get => piles; set { piles = value; pilesTemp = piles.Copy(); LoadPiles(); } }
    

        public PileParameterVibratoryForm()
        {
            InitializeComponent();
         
            Piles = new PileProgressionVibratory();

                   
       
            LoadPiles();
        }

        private void PileParameterVibratoryForm_Load(object sender, EventArgs e)
        {
            pilesTemp = piles.Copy();
            LoadPiles();
        }
        public PileParameterVibratoryForm(PileProgressionVibratory p, AppSettings settings)  
        {
            InitializeComponent();
            this.piles = p;
            this.appSettings = settings;
            pilesTemp = piles.Copy();
            LoadPiles();
        }

        public PileParameterVibratoryForm(AppSettings settings)
        {
            InitializeComponent();

         
            this.appSettings = settings;
            piles = new PileProgressionVibratory();          
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
            txtDurationMin.Text = piles.Duration_min.ToString();
            txtEquip.Text = piles.Equipment.ToString();
            txtPileDiam.Text = piles.PileDiameter_m.ToString();





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

        //private void btnLoadFromCSV_Click(object sender, EventArgs e)
        //{

        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.Title = "Please Choose the Pile Progression  File";
        //    ofd.Filter = "csm files (*.csv)|*.csv|All files (*.*)|*.*";
        //    ofd.FilterIndex = 1;
        //    ofd.RestoreDirectory = true;
        //    ofd.DefaultExt = "csv";
        //    if (appSettings == null) { appSettings = new AppSettings(); }

        //    if (DialogResult.OK == ofd.ShowDialog())
        //    {
        //        appSettings.PileCSV = ofd.FileName;
        //        appSettings.SaveSetting(APP_Config_Keys.pileCSV, appSettings.PileCSV);
        //        piles.Read(appSettings.PileCSV);
        //        LoadPiles();
        //    }
        //}

        private void btnClear_Click(object sender, EventArgs e)
        {
            piles = pilesTemp;
            LoadPiles();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //CalculatePiles();
            //if (DialogResult.OK == saveFileDialog.ShowDialog())
            //{
            //    appSettings.PileCSV = saveFileDialog.FileName;
            //    appSettings.SaveSetting(APP_Config_Keys.pileCSV, appSettings.PileCSV);
               
           
            //}

            //this.DialogResult = DialogResult.OK;    
            //this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
