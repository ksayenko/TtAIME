using MathNet.Numerics;
using Microsoft.VisualBasic;
using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace RunManatea
{
    public partial class MarineSpeciesConfiguration : UserControl
    {
        LogHandler lh;
        AppSettings set;
        Read3mb.ReadRunWriteAnimat an = null;
        Read3mb.GridFile3dSoundLevel mat = null;
        Marine_Mammal_Dictionary thedictionary;
        SoundSourceMetric selectedSourceMetric = SoundSourceMetric.UNKNOWN;
        SoundSourceType selectedSoundSourceType = SoundSourceType.Impulsive;
        bool bAnimatCompleted = false;

        private List<Marine_Species> species = new List<Marine_Species>();
        public List<Marine_Species> Species { get => species; set => species = value; }

        string strselectedThreshhold = "";
        List<Acoustic_Threshold> selectedTreshholds = new List<Acoustic_Threshold>();
        List<Acoustic_Threshold> aud = new List<Acoustic_Threshold>();
        List<Acoustic_Threshold> tts = new List<Acoustic_Threshold>();
        List<Acoustic_Threshold> beh = new List<Acoustic_Threshold>();
        List<Acoustic_Threshold> prob = new List<Acoustic_Threshold>();

        bool isCheckThresholdNeeded = true;

        //Scaling factor
        double dScalingFactor = 1.0;
        // Real-world density(animals/km2)
        double dRealDensity = 0.00497;
        // Modeled density(animats/km2)
        double dModelDensity = 1.0;

        bool bHaveSndsrc = false;


        PileProgressions piles = null;

        public LogHandler Lh { get => lh; set => lh = value; }
        public AppSettings Set { get => set; set => set = value; }
        public ReadRunWriteAnimat RunAnimat { get => an; set => an = value; }
        public GridFile3dSoundLevel Grid3dSound { get => mat; set => mat = value; }
        public PileProgressions Piles { get => piles; set => piles = value; }
        public SoundSourceMetric SelectedSourceMetric { get => selectedSourceMetric; set => selectedSourceMetric = value; }
        public SoundSourceType SelectedSoundSourceType { get => selectedSoundSourceType; set => selectedSoundSourceType = value; }
        public double DScalingFactor { get => dScalingFactor; set => dScalingFactor = value; }



        public double DModelDensity { get => dModelDensity; set => dModelDensity = value; }
        public bool BHaveSndsrc { get => bHaveSndsrc; set => bHaveSndsrc = value; }
        public List<Acoustic_Threshold> SelectedTreshholds { get => selectedTreshholds; set => selectedTreshholds = value; }
        public bool IsCheckThresholdNeeded { get => isCheckThresholdNeeded; set => isCheckThresholdNeeded = value; }
        public double DRealDensity { get => dRealDensity; set => dRealDensity = value; }
        public bool AnimatReadCompleted { get => bAnimatCompleted; set => bAnimatCompleted = value; }

        public MarineSpeciesConfiguration()
        {
            InitializeComponent();
            Init2();


            an = new ReadRunWriteAnimat("");
            thedictionary = new Marine_Mammal_Dictionary(set, lh);
            LoadComboboxes();
        }

        public void LoadComboboxes()
        {
            HearingGroups groups = new HearingGroups();
            cbHearingGoups.Items.Clear();
            foreach (HearingGroup g in groups.TheHearingGroups)
                cbHearingGoups.Items.Add(g.Name);

            txtRealWorldDensity.Text = dRealDensity.ToString();
        }



        public void SetMarineSpeciesConfiguration(AppSettings set1, ReadRunWriteAnimat an = null, GridFile3dSoundLevel mat1 = null, PileProgressions piles = null, LogHandler lh1 = null)
        {
            if (lh1 != null)
                this.lh = lh1;
            if (an != null)
                this.an = an;
            if (set1 != null)
                this.set = set1;
            if (mat1 != null)
                this.mat = mat1;
            if (piles != null)
                this.piles = piles;

            thedictionary = new Marine_Mammal_Dictionary(set, lh);
        }

        public MarineSpeciesConfiguration(AppSettings set1, ReadRunWriteAnimat an = null, GridFile3dSoundLevel mat1 = null, PileProgressions piles = null, LogHandler lh1 = null)
        {
            if (lh1 != null)
                this.lh = lh1;
            if (an != null)
                this.an = an;
            if (set1 != null)
                this.set = set1;
            if (mat1 != null)
                this.mat = mat1;
            if (piles != null)
                this.piles = piles;
            thedictionary = new Marine_Mammal_Dictionary(set, lh);
        }

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

        private void btn3MB_Click(object sender, EventArgs e)
        {
            Open3MBMovementFile();
            Load3MBMovementFile();
        }

        public void Open3MBMovementFile(bool bSaveToConfig = true)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Please Choose the Animat Movement File fromj 3MB";
            ofd.Filter = "3MB files (*.3mb)|*.3mb|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.DefaultExt = "3mb";


            if (set == null) { set = new AppSettings(); }
            if (DialogResult.OK == ofd.ShowDialog())
            {
                set.A3mbIniFile = ofd.FileName;
                an = new ReadRunWriteAnimat(set.A3mbIniFile);
                if (bSaveToConfig)
                    set.Save3MBMovemetFileToConfigFile();
            }
        }
        private new void ResetText()
        {
            rtbInfo.Text = "";
        }
        private void AddText(string text, bool bNewLine = true,
            bool bBold = false, bool bItalic = false)
        {


            if (bNewLine)
                rtbInfo.Text += "\n";
            RichTextBoxExtensions.AppendText(rtbInfo, text, Color.BlueViolet, 12, false, true);

        }
        public async void Load3MBMovementFile()
        {

            ResetText();
            var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update, true);
                               });

            FileInfo _3mbFile = null;
            if (set != null && set.A3mbIniFile != null && set.A3mbIniFile != "")
            {
                _3mbFile = new FileInfo(set.A3mbIniFile);
            }
            else

            {
                Open3MBMovementFile(false);
                return;
            }


            _3mbFile = new FileInfo(set.A3mbIniFile);
            bool bReadAnimats = true;
            if (_3mbFile.Exists)
            {
                an.fullpath = _3mbFile.FullName;
                bReadAnimats |= false;  
                await Task.Run(() =>
                {
                    bReadAnimats = an.Read(progress);

                });


                ResetText();
                List<string> info = animatMovemenInfo();

                SetOtherByScientificName();

                for (int i = 0; i < info.Count; i++)
                {
                    string s = info[i];
                    if (s.Contains("~"))
                    {
                        s = s.Replace("^", "");
                        RichTextBoxExtensions.AppendText(this.rtbInfo, s + "\n", Color.Navy, 10, false, true);
                    }
                    else if (s.StartsWith("!"))
                    {
                        s = s.Substring(1);
                        RichTextBoxExtensions.AppendText(this.rtbInfo, s + "\n", Color.Red, 10, true);
                    }
                    else
                        RichTextBoxExtensions.AppendText(this.rtbInfo, s + "\n", Color.Navy, 10);
                }


                if (!bReadAnimats)
                {
                    RichTextBoxExtensions.AppendText(this.rtbInfo, "Incorrect file " + _3mbFile.FullName, Color.Red, 10, true);

                    bAnimatCompleted = false;
                }
                else
                    bAnimatCompleted = true;
            }

            int nAnimatNotSoundSource = 0;
            for (int i = 0; i < an.File3MB.nAnimats; i++)
            {
                Animat animat = an.File3MB.Animats[i];
                if (animat.IsSoundSource)
                    continue;
                nAnimatNotSoundSource++;
                an.File3MB.AnimatToWrite.Add(animat.indexAnimat);
            }

            if (an.MaxAnimatWrite < nAnimatNotSoundSource)
            {
                int nA = an.MaxAnimatWrite;
                string message = "Number of animats being exposed is large.\n\rPlease enter a number of inidividual animat results you would like in the summnary file.";
                int iTry = 0;
                bool b = false;
                do
                {
                  //  var value = Interaction.InputBox(message, "Number of inidividual animat", nA.ToString());
                 
  var value = CustomInputBox.ShowDialog(message, "Number of inidividual animat", nA.ToString());

                if (value !=null)
                    if (value.ToString() != "")
                        b = int.TryParse(value, out nA);
                    if (b)
                        b = nA > 0 && nA < 100;
                    message = "Number of inidividual animat: " + value + " is incorrect value";
                    iTry++;
                } while (!b && iTry < 1);

                an.MaxAnimatWrite = nA;
                an.File3MB.AnimatToWrite = new List<int>();

                for (int k = 0; k < an.MaxAnimatWrite; k++)
                {
                    b = true;
                    do
                    {
                        int rand = GetRandomNumber(0, an.file3MB.Animats.Count);
                        Animat a = an.file3MB.GetAnimat(rand);
                        if (a != null && !a.IsSoundSource && !an.File3MB.AnimatToWrite.Contains(rand))
                        {
                            an.File3MB.AnimatToWrite.Add(rand);
                            lh.LogWarning((k+1).ToString()+". Will write " + (rand + 1).ToString() + " amnimat");
                            b = false;
                        }
                    } while (b);
                }
                an.File3MB.AnimatToWrite.Sort();    
            }
            

        }

        //Function to get random number
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        public void SetOtherByScientificName()
        {
            List<string> thespecies = new List<string>();
            strselectedThreshhold = "";
            lbAppliedThresholds.Text = "";
            cbAUD.Checked = false;
            cbAUD.Enabled = cbBeh.Enabled = cbBeh.Enabled = cbProb.Enabled = true;
            cbBeh.Checked = false;
            cbProb.Checked = false;
            cbTTS.Checked = false;



            File3MB file3 = an.File3MB;
            if (file3 == null)
                return;
            for (int i = 0; i < file3.speciesData.totalSpeciesCount; i++)
            {
                string species = file3.speciesData.speciesBehaviorNamesAndInformaiton[i].speciesBinaryOutInformation.fileTitle.ToString().Trim();

                if (file3.speciesData.speciesBehaviorNamesAndInformaiton[i].speciesBinaryOutInformation.description.group == SPECIESGROUP.SOUNDSOURCE)
                {
                    bHaveSndsrc = true;
                    continue;
                }
                thespecies.Add(species);

                Marine_Species m1 = GetSpeciesByScientificName(species);
                this.species.Add(m1);
            }

            Marine_Species m = GetSpeciesByScientificName(thespecies[thespecies.Count - 1]);
            //this.species.Add(m);    

            List<Marine_Species_Threshhold> list1 = GetSpeciesTreshholdsByScientificName(m.Scientific_name);
            List<Marine_Species_Threshhold> list2 = GetSpeciesTreshholdsByHearingGroup(m.HearingGroup.Name);
            if (thespecies.Count > 0)
            {
                SetComboboxHearingGroup(m);
                if (list1.Count > 0)
                    SetTreshholdsCheckBoxes(list1, selectedSourceMetric, selectedSoundSourceType);
                else
                    SetTreshholdsCheckBoxes(list2, selectedSourceMetric, selectedSoundSourceType);
            }


        }


        private void SetTreshholdsCheckBoxes(List<Marine_Species_Threshhold> list, SoundSourceMetric metric, SoundSourceType soundType)
        {
            bool baud = false;
            bool btts = false;
            bool bbeh = false;
            bool bprob = false;
            aud = new List<Acoustic_Threshold>();
            tts = new List<Acoustic_Threshold>();
            beh = new List<Acoustic_Threshold>();
            prob = new List<Acoustic_Threshold>();
            if (list == null | list.Count > 0)
            {
                foreach (Marine_Species_Threshhold m in list)
                {
                    if (m.Acoustic_Threshold.Shortname.ToLower().StartsWith("aud")
                        && soundType == m.Acoustic_Threshold.TheSoundSourceType
                        && metric == m.Acoustic_Threshold.TheSoundSourceMetric)
                    {
                        baud = true;
                        aud.Add(m.Acoustic_Threshold);
                    }
                    if (m.Acoustic_Threshold.Shortname.ToLower().StartsWith("tts")
                        && soundType == m.Acoustic_Threshold.TheSoundSourceType
                        && metric == m.Acoustic_Threshold.TheSoundSourceMetric)
                    {
                        btts = true;
                        tts.Add(m.Acoustic_Threshold);
                    }
                    if (m.Acoustic_Threshold.Name.ToLower().StartsWith("beh")
                       && soundType == m.Acoustic_Threshold.TheSoundSourceType
                       && metric == m.Acoustic_Threshold.TheSoundSourceMetric)
                    {
                        bbeh = true;
                        beh.Add(m.Acoustic_Threshold);
                    }
                    if (m.Acoustic_Threshold.Name.ToLower().StartsWith("prob")
                      && soundType == m.Acoustic_Threshold.TheSoundSourceType
                      && metric == m.Acoustic_Threshold.TheSoundSourceMetric)
                    {
                        bprob = true;
                        prob.Add(m.Acoustic_Threshold);
                    }

                }

                if (baud)
                {

                    foreach (Acoustic_Threshold a in aud)
                    {
                        strselectedThreshhold += a.Display() + "\r\n";
                    }
                    cbAUD.Checked = true;

                }
                if (btts)
                {
                    foreach (Acoustic_Threshold a in aud)
                    {
                        strselectedThreshhold += a.Display() + "\r\n";
                    }
                    cbTTS.Checked = true;

                }
                if (bbeh)
                {
                    foreach (Acoustic_Threshold a in beh)
                    {
                        strselectedThreshhold += a.Display() + "\r\n";
                    }
                    cbBeh.Checked = true;

                }
                if (bprob)
                {
                    foreach (Acoustic_Threshold a in prob)
                    {
                        strselectedThreshhold += a.Display()+"\r\n";
                    }
                    cbProb.Checked = true;
                }
            }

            CheckBoxEnabled(cbTTS, btts);
            CheckBoxEnabled(cbAUD, baud);
            CheckBoxEnabled(cbBeh, bbeh);
            CheckBoxEnabled(cbProb, bprob);

            lbAppliedThresholds.Text = strselectedThreshhold;

        }

        private void CheckBoxEnabled(CheckBox cb, bool enabled)
        {
            cb.Enabled = enabled;
            Font f1 = new Font("Calibri", 10, FontStyle.Bold);
            Font f2 = new Font("Calibri", 10, FontStyle.Regular);
            if (enabled)
            {
                cb.ForeColor = Color.Black;
                cb.Font = f1;
                cb.FlatAppearance.BorderSize = 2;
            }
            else
            {
                cb.ForeColor = Color.DarkGray;
                cb.Font = f2;
                cb.FlatAppearance.BorderSize = 1;
            }
        }

        private void SetComboboxHearingGroup(Marine_Species m) {


            bool bFound = false;
            for (int j = 0; j < cbHearingGoups.Items.Count; j++)
            {
                if (cbHearingGoups.Items[j].ToString().ToLower() ==
                    m.HearingGroup.Name.ToLower().Trim())
                {

                    cbHearingGoups.SelectedIndex = j;
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                cbHearingGoups.Items.Add(m.HearingGroup.Name);
                cbHearingGoups.SelectedIndex = cbHearingGoups.Items.Count - 1;
            }

        }

        private List<string> animatMovemenInfo()
        {
            List<string> listInfo = new List<string>();
            if (an != null)
            {
                File3MB file3 = an.File3MB;
                if (file3 != null)
                {
                    listInfo.Add("Animat File Information ");
                    listInfo.Add("Project Name :" + an.name);
                    listInfo.Add("File Title :" + file3.fileTitle);
                    int nspecies = file3.speciesData.totalSpeciesCount;
                    listInfo.Add("Total Species : " + nspecies.ToString());
                    string species = "Species:";

                    string scenario = file3.sceParams.ToString();
                    bHaveSndsrc = false;
                    for (int i = 0; i < nspecies; i++)
                    {
                        if (file3.speciesData.speciesBehaviorNamesAndInformaiton[i].speciesBinaryOutInformation.description.group == SPECIESGROUP.SOUNDSOURCE)
                            bHaveSndsrc = true;
                        else
                            species += "\n~" + file3.speciesData.speciesBehaviorNamesAndInformaiton[i].speciesBinaryOutInformation.fileTitle.ToString() + ";";
                    }
                    listInfo.Add(species);

                    double area = file3.bathymetry.GetWaterSurfaceAreaMeters() / (1000 * 1000);
                    listInfo.Add("Seedable Aea, km2: " + area.ToString());
                    CalcDensity(area);
                    listInfo.Add("Species Model Density(animats / km ^ 2): ");
                    string sDensity = String.Format("{0:E5}", dModelDensity);
                    listInfo.Add(sDensity);

                    listInfo.Add("Total Animats (exclude soundsource) " + an.File3MB.CountNonSoundSourceAnimat().ToString());

                    /*
                     * Scenario Start Time: 08:00
                        Scenario Duration: 1.5 hrs
                     */

                    HHMMSS hhmmss1 = GeneralMethods.Time_To24HrMinSec((int)file3.sceParams.startTime);
                    HHMMSS hhmmss2 = GeneralMethods.Time_ToHrMinSec((int)file3.sceParams.duration);
                    listInfo.Add("Scenario Start Time: " + hhmmss1.ToString());
                    listInfo.Add("Scenario Duration:  " + hhmmss2.ToString() + " hrs");


                }
                else
                {
                    listInfo.Add("No File");

                }
            }
            else
            {
                listInfo.Add("No File");

            }


            return listInfo;

        }

        private void CalcDensity(double area_km2)
        {
            int nAnimats = an.file3MB.CountNonSoundSourceAnimat();// non sound source animats code.
            dModelDensity = nAnimats / area_km2;
            CalcScalingFactor();
        }

        private void CalcScalingFactor()
        {
            DScalingFactor = DRealDensity / DModelDensity;
            if (DScalingFactor < 0.0001 | DScalingFactor > 10000)
                lbScalingFactor.Text = String.Format("{0:E5}", DScalingFactor);
            else
                lbScalingFactor.Text = String.Format("{0:N4}", DScalingFactor);
        } 

        private Marine_Species GetSpeciesByScientificName(string scientificName)
        {
            List<Marine_Species> distSpec = thedictionary.DistinctSpecies();
            foreach (Marine_Species m in distSpec)
                if (m.Scientific_name.ToLower().Trim() == scientificName.ToLower().Trim())
                    return m;

            return new Marine_Species("Na", "na", "NA");   
        }

        private List<Marine_Species_Threshhold> GetSpeciesTreshholdsByScientificName(string scientificName)
        {
            List<Marine_Species_Threshhold> all = thedictionary.DefaultList;
            Marine_Species_Threshhold m1 = new Marine_Species_Threshhold(new Marine_Species("Na", "na", "NA"), new Acoustic_Threshold());
            List<Marine_Species_Threshhold> list1 = new List<Marine_Species_Threshhold>();
            foreach (Marine_Species_Threshhold m in all)
            {
                if (m.Marine_Species.Scientific_name.ToLower().Trim() == scientificName.ToLower().Trim())
                   list1.Add(m);
            }
            return list1;
        }

        private List<Marine_Species_Threshhold> GetSpeciesTreshholdsByHearingGroup(string group)
        {
            List<Marine_Species_Threshhold> all = thedictionary.DefaultList;
            Marine_Species_Threshhold m1 = new Marine_Species_Threshhold(new Marine_Species("Na", "na", "NA"), new Acoustic_Threshold());
            List<Marine_Species_Threshhold> list1 = new List<Marine_Species_Threshhold>();
            foreach (Marine_Species_Threshhold m in all)
            {
                if (m.Marine_Species.HearingGroup.Name.ToLower().Trim() == group.ToLower().Trim())
                    list1.Add(m);
            }
            return list1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnApliedAcousticThreshholds_Click(object sender, EventArgs e)
        {

        }

        private void btnAcousticThresholdDatabase_Click(object sender, EventArgs e)
        {
            TtAIME_Marine_Mammal_Dictionary form = new TtAIME_Marine_Mammal_Dictionary(set, lh);
            form.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MarineSpeciesConfiguration_Load(object sender, EventArgs e)
        {
            SetSoundSourceType(SelectedSoundSourceType);
        }

        public void SetSoundSourceType(SoundSourceType type)
        {
            selectedSoundSourceType = type; 
           // labelInfo.Text = type.ToDescriptionString();
        }
        public void SetSoundSourceMetric(SoundSourceMetric type)
        {
            selectedSourceMetric = type;    
           // labelInfo.Text = type.ToDescriptionString();
        }

        private void cbThresHolds_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            bool bChecked = cb.Checked;
            bool bEnabled = cb.Enabled;

            try
            {

                if (bChecked && bEnabled)
                {
                    foreach (Acoustic_Threshold a in aud)
                    {
                        if (cbAUD.Name == cb.Name && a.Name != "NA")
                            selectedTreshholds.Add(a);
                    }
                    foreach (Acoustic_Threshold a in beh)
                    {
                        if (cbBeh.Name == cb.Name && a.Name != "NA")
                            selectedTreshholds.Add(a);
                    }
                    // selectedTreshholds.Add(beh);
                    foreach (Acoustic_Threshold a in prob)
                    {
                        if (cbProb.Name == cb.Name && a.Name != "NA")
                            selectedTreshholds.Add(a);
                    }
                    //   selectedTreshholds.Add(prob);
                    foreach (Acoustic_Threshold a in tts)
                    {
                        if (cbTTS.Name == cb.Name && a.Name != "NA")
                            selectedTreshholds.Add(a);
                    }
                }
                if (!bChecked && bEnabled)
                {
                    if (cbAUD.Name == cb.Name)
                        foreach (Acoustic_Threshold a in aud)
                        {
                            selectedTreshholds.Remove(a);
                        }
                    if (cbBeh.Name == cb.Name)
                        foreach (Acoustic_Threshold a in beh)
                        {
                            selectedTreshholds.Remove(a);
                        }
                    if (cbProb.Name == cb.Name)
                        foreach (Acoustic_Threshold a in prob)
                        {
                            selectedTreshholds.Remove(a);
                        }
                    if (cbTTS.Name == cb.Name)
                        foreach (Acoustic_Threshold a in tts)
                        {
                            selectedTreshholds.Remove(a);
                        }
                }

                strselectedThreshhold = "";
                selectedTreshholds.Remove(new Acoustic_Threshold());
                    foreach (Acoustic_Threshold a in selectedTreshholds)
                {

                    strselectedThreshhold += a.Display() + "\r\n";
                }
                lbAppliedThresholds.Text = strselectedThreshhold;

            }
            catch(Exception ex)
            {
                lh.LogError(ex);    
            }

        }

        public void CollectData(ref ManateaCalculationStructure input)
        {
            input.Threshold = this.selectedTreshholds;
            input.RealWorldDensity = Double.Parse(txtRealWorldDensity.Text);
            input.ModelDensity = this.dModelDensity;
            input.ScalingFactor = this.dScalingFactor;
            var distSp = this.species.ToList().Distinct();
            List<Marine_Species> list = (List<Marine_Species>)distSp.ToList();
            input.Species = list;


        }

        private void CollectThreshhods()
        { }

        private void btnSelectGroup_Click(object sender, EventArgs e)
        {

        }

        private void btnModifyEvalThresh_Click(object sender, EventArgs e)
        {

        }

        private void txtRealWorldDensity_TextChanged(object sender, EventArgs e)
        {
            if (txtRealWorldDensity.Text != "")
            {
                dRealDensity = Double.Parse(txtRealWorldDensity.Text);
                CalcScalingFactor();
            }
        }
    }

}
