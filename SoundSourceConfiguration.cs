using MathNet.Numerics;
using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;
using static System.Net.Mime.MediaTypeNames;

namespace RunManatea
{
    public partial class SoundSourceConfiguration : UserControl
    {
        LogHandler lh;
        AppSettings set;
        Read3mb.ReadRunWriteAnimat an = null;
        Read3mb.GridFile3dSoundLevel mat = null;
        SoundSourceMetric soundMetric;
        SoundSourceType soundSourceType;
        PileProgressions piles = null;
        PileProgressionVibratory pilesNonImpulsive;          

        bool bGridCompleted = false;
        bool bPileCompleted = false;    


        public SoundSourceConfiguration()
        {
            InitializeComponent();
            Init2();
            soundMetric = SoundSourceMetric.UNKNOWN; ;
            soundSourceType = SoundSourceType.Non_Impulsive;
            InitSoundSourceType();
            InitMericType();

            mat = new Read3mb.GridFile3dSoundLevel();
        }


        public LogHandler Lh { get => lh; set => lh = value; }
        public AppSettings Set { get => set; set => set = value; }
        public ReadRunWriteAnimat An { get => an; set => an = value; }
        public GridFile3dSoundLevel GridFle { get => mat; set => mat = value; }
        public PileProgressions Piles { get => piles; set => piles = value; }
        public SoundSourceMetric SoundMetric { get => soundMetric; set => soundMetric = value; }
        public SoundSourceType SoundSourceType { get => soundSourceType; set => soundSourceType = value; }
        public bool GridCompleted { get => bGridCompleted; set => bGridCompleted = value; }
        public bool PileCompleted { get => bPileCompleted; set => bPileCompleted = value; }
        public PileProgressionVibratory PilesNonImpulsive { get => pilesNonImpulsive; set => pilesNonImpulsive = value; }

        public void Init2()
        {

            bool badded = false;
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.Name == tableLayoutPanel1.Name)
                {
                    badded = true; break;
                }
            }
            if (!badded)
                this.Controls.Add(tableLayoutPanel1);

        }


        public void InitSoundSourceType()
        {
            listBoxSourceType.Items.Clear();
            listBoxSourceType.Items.Add(SoundSourceType.Impulsive.ToDescriptionString());
            listBoxSourceType.Items.Add(SoundSourceType.Non_Impulsive.ToDescriptionString());
            listBoxSourceType.SelectedIndex = 0;    

        }

        public void InitMericType()
        {
            listBoxMetric.Items.Clear();
            listBoxMetric.Items.Add(SoundSourceMetric.SEL_Weighted.ToDescriptionString());
            listBoxMetric.Items.Add(SoundSourceMetric.SPL_Weighted.ToDescriptionString());
            listBoxMetric.Items.Add(SoundSourceMetric.SPL_Unweighted.ToDescriptionString());
            listBoxMetric.Items.Add(SoundSourceMetric.PK_Unweighted.ToDescriptionString());
            listBoxMetric.SelectedIndex = 0;

        }

        public void SetPiles(PileProgressions piles = null)
        {
            if (piles != null)
            {
                this.piles = piles;
                if (bPileCompleted)
                {
                    lblPiles.Text = "Equipment: " + piles.Equipment + "\n"
                        + "Pile Diameter: " + piles.PileDiameter_m.ToString() + " m" + "\n" //"12 m
                        + "Maximum Hammer Energy: " + piles.MaximumHammerEnergy.ToString() + " kJ" + "\n" //"12 m
                        + "Total Number of Strikes: " + piles.TotalNumberofStrikes.ToString(); //"12 m

                    lblPiles.BackColor = System.Drawing.SystemColors.Info;
                }
                else
                    lblPiles.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        public void SetPiles(PileProgressionVibratory piles = null)
        {
            if (piles != null)
            {
                this.pilesNonImpulsive = piles;
                if (bPileCompleted) { 
                    lblPiles.Text = "Equipment: " + pilesNonImpulsive.Equipment + "\n"
                        + "Pile Diameter: " + pilesNonImpulsive.PileDiameter_m.ToString() + " m" + "\n" //"12 m
                         + "Duration:: " + piles.Duration_min.ToString() + " min"; //"12 m

                    lblPiles.BackColor = System.Drawing.SystemColors.Info;
                }
                else
                    lblPiles.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        public void SetSoundSourceConfiguration(AppSettings set1, GridFile3dSoundLevel mat1 = null, PileProgressions piles = null, LogHandler lh1 = null)
        {
            if (lh1 != null)
                this.lh = lh1;
            if (set1 != null)
                this.set = set1;
            if (mat1 != null)
                this.mat = mat1;
            if (piles != null)
                this.piles = piles;
        }

        //var progress = new Progress<string>(
        //                      update =>
        //                      {
        //                          AddText(update);
        //                      });

        //   await Task.Run(() =>
        //   {
        //       ReadDBSeaGridFiles(progress);
        //       this.mat.WriteBinaryFile(set.GridBinary, progress);
        //   });

        private void ResetText()
        {
            rtbdbSeaGridInfo.Text = "";
        }

        private void AddText(string text, bool bNewLine = true,
            bool bBold = false, bool bItalic = false)
        {


            if (bNewLine)
                rtbdbSeaGridInfo.Text += "\n";
            RichTextBoxExtensions.AppendText(rtbdbSeaGridInfo, text, Color.BlueViolet, 12, false, true);

        }

        public async void LoadBinaryGrid()
        {

            ResetText();
            var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update, true);
                               });

            FileInfo binaryfile = null;
            if (set != null && set.GridBinary != null && set.GridBinary != "")
            {
                binaryfile = new FileInfo(set.GridBinary);
            }
            else

            {
                OpenGridFile(false);
                return;
            }


            binaryfile = new FileInfo(set.GridBinary);
            bool mMat = true;
            if (binaryfile.Exists)
            {
                await Task.Run(() =>
                {
                  mMat=  GridFle.ReadBinaryFile(binaryfile.FullName, progress);
                    bGridCompleted = true;

                });

                //Mat.ProjectName = "CVOWC";
                //Mat.ScenarioName = "Standard Drive";
                //Mat.CoordinateSystem = "UTM Zone 18N";Mat.GridType = GridSoundMetric.SEL_Unweighted;

                //Mat.WriteBinaryFile(binaryfile.FullName + "1s", progress);
                ResetText();
                List<string> info = dbSeaGridInfo();

                for (int i = 0; i < info.Count; i++)
                {
                    RichTextBoxExtensions.AppendText(this.rtbdbSeaGridInfo, info[i] + "\n", Color.Navy,10,false, true);
                }

                if (mat.GridType != SoundSourceMetric.UNKNOWN)
                { string s = mat.GridType.ToString();
                    int index = 0;
                    foreach (object item in listBoxMetric.Items)
                    {
                       string s1 = item.ToString();
                        if (s1 == s)
                        {
                            listBoxMetric.SelectedIndex = index;
                            break;
                        }
                        index++;
                    }
                }

                if (!mMat)
                    RichTextBoxExtensions.AppendText(this.rtbdbSeaGridInfo, "Incorrect file " + binaryfile.FullName, Color.Red,10,true);
            }
        }

        public void OpenGridFile(bool bSaveToConfig = true)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please Choose the SoundLevel Grid File";
            ofd.Filter = "grid files (*.grid)|*.grid|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (set == null) { set = new AppSettings(); set.ReadSettings(); }
            FileInfo fi = null;
            if (set != null && set.GridBinary != "")
            fi = new FileInfo(set.GridBinary);
            if (fi != null && fi.Exists)
            {
                ofd.FileName = set.GridBinary;
                ofd.InitialDirectory = fi.Directory.FullName;
            }
            
            ofd.DefaultExt = "grid";
            
            if (DialogResult.OK == ofd.ShowDialog())
            {
                set.GridBinary = ofd.FileName;

                set.SaveSoundGridBinaryFieToConfigFile();
            }
        }





        private List<string> dbSeaGridInfo()
        {
            List<string> dbGreadInfo = new List<string>();
            if (mat != null)
            {

                dbGreadInfo.Add("grid file ");
                dbGreadInfo.Add("Project Name :" + mat.ProjectName);
                dbGreadInfo.Add("Scenario Name:  :" + mat.ScenarioName);
                dbGreadInfo.Add("Project Area: ");
                dbGreadInfo.Add("xmin " + mat.Xmin.ToString() + " xmax " + mat.Xmax.ToString());
                dbGreadInfo.Add("ymin " + mat.Ymin.ToString() + " ymax " + mat.Ymax.ToString());
                dbGreadInfo.Add("Grid Size :");
                dbGreadInfo.Add("SIZE X : " + mat.Sizex.ToString() + "; SIZE Y : " + mat.Sizez.ToString()
                    + "; SIZE Z : " + mat.Sizez.ToString() +";");
                dbGreadInfo.Add("__________________________");
                dbGreadInfo.Add("Coordinate System :" + mat.CoordinateSystem);
                dbGreadInfo.Add("Grid Type :" + mat.GridType.ToString());
                if (mat.ErrorMessage !="")
                    dbGreadInfo.Add("Info :" + mat.ErrorMessage);
            }
            else
            {
                dbGreadInfo.Add("No File");

            }


            return dbGreadInfo;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void CollectData(ref ManateaCalculationStructure input)
        {
            input.GridFile = GridFle;
            input.PilesImpulsive = Piles;
            input.PilesNonImpulsive = PilesNonImpulsive;
            input.SourceMetric = SoundMetric;
            input.SoundSourceType = soundSourceType;
        }

            private void btnPile_Click(object sender, EventArgs e)
        {

            if (soundSourceType == SoundSourceType.Impulsive)
            {
                PileParameterSettingsForm p = new PileParameterSettingsForm(set);

                if (DialogResult.OK == p.ShowDialog())
                {
                    piles = p.Piles;
                    if (piles != null && piles.Count >0)  
                    {
                                                bPileCompleted = true;
                        this.SetPiles(piles);
                    }

                }
            }

            if (soundSourceType == SoundSourceType.Non_Impulsive)
            {
                PileParameterVibratoryForm p = new PileParameterVibratoryForm(set);


                if (DialogResult.OK == p.ShowDialog())
                {
                    pilesNonImpulsive = p.Piles;
                    if (piles != null)


                        this.SetPiles(pilesNonImpulsive);
                    bPileCompleted = true;
                }

            }

        }

        private void btnLoadDbSeaGrid_Click(object sender, EventArgs e)
        {
            //set.GridBinary = "";
            OpenGridFile(false);
            LoadBinaryGrid();
       
        }

        private void listViewSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = listBoxSourceType.Items[listBoxSourceType.SelectedIndex].ToString();
            soundSourceType = text.ToSoundSourceTypeFromDescription();
            
            
        }

        private void SoundSourceConfiguration_Load(object sender, EventArgs e)
        {
            if (piles != null)
            {
                SetPiles(piles);
            }
        }

        private void labelControlName_Click(object sender, EventArgs e)
        {

        }

        private void listBoxMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = listBoxMetric.Items[listBoxMetric.SelectedIndex].ToString();
            soundMetric = text.ToSoundSourceMetricFromDescription();
        }
    }

  




}
