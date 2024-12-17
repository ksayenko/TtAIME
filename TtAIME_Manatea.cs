
using Read3mb;
using System;
using System.Collections.Generic;

using System.Drawing;

using System.IO;
using System.Linq;

using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;

using Microsoft.WindowsAPICodePack.Dialogs;


namespace RunManatea
{
    public partial class TtAIME_Manatea : Form
    {
        string _3mbIniFile;
        LogHandler lh;
        AppSettings set;
        Read3mb.ReadRunWriteAnimat an = null;
        Read3mb.GridFile3dSoundLevel mat = null;
        PileProgressions piles = null;
        SoundSourceMetric selectedSourceMetric = SoundSourceMetric.UNKNOWN;
        SoundSourceType selectedSoundSourceType = SoundSourceType.Impulsive;
        List<Acoustic_Threshold> selectedThreshold = new List<Acoustic_Threshold>();
        ManateaCalculationStructure input;
        ManateaRunSummaryResult runSummary;


        //controls
        SoundSourceConfiguration ssControl = null;
        MarineSpeciesConfiguration msControl = null;
        CurrentScreen curScreen = CurrentScreen.None;

        CommonOpenFileDialog dialog;

        public List<Acoustic_Threshold> SelectedThreshold { get => selectedThreshold; set => selectedThreshold = value; }

        public TtAIME_Manatea()
        {
            InitializeComponent();
            lh =  new LogHandler();
            set = new AppSettings();
            piles = new PileProgressions();
            set.ReadSettings();
            curScreen = CurrentScreen.None;
            InitSoundSourceConfiguration();
            InitMarineSpeciesConfiguration();
            AddRemoveControl(ssControl, false);
            AddRemoveControl(msControl, false);
            dialog = new CommonOpenFileDialog();
            dialog.EnsureFileExists = true;
            dialog.EnsurePathExists = true;
            input = new ManateaCalculationStructure();
            runSummary = new ManateaRunSummaryResult();


        }

        public void AddRemoveControl(Control control, bool bAdd)
        {
            bool bExist = false;
            if (control == null) return;
            try
            {

                foreach (Control c in this.Controls)
                {
                    if (c.Name == control.Name)
                    {
                        bExist = true;
                        break;
                    }
                }

                if (!bExist && bAdd)
                {
                    this.Controls.Add(control);
                }

                if (bExist && !bAdd)
                {
                    this.Controls.Remove(control);
                }
            }

            catch (Exception ex)
            {
                lh.LogError(ex);
            }

            bExist = false;
            try
            {
                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    if (c.Name == control.Name)
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist && bAdd)
                {
                    this.tableLayoutPanel1.Controls.Add(control);
                }
                if (bExist && !bAdd)
                {
                    this.tableLayoutPanel1.Controls.Remove(control);
                }
            }
            catch (Exception ex)
            {
                lh.LogError(ex);
            }

        }

        public void InitSoundSourceConfiguration()
        {
           
            try
            {
                if (ssControl == null)
                {
                    ssControl = new SoundSourceConfiguration();
                    ssControl.SetSoundSourceConfiguration(set, mat,piles, lh);
                }

              
            }
            catch (Exception ex)
            {
                lh.LogError(ex);
            }

           

        }

        public void InitMarineSpeciesConfiguration()
        {
        
            try
            {
                if (msControl == null)
                {
                    an = new ReadRunWriteAnimat("");
                    msControl = new MarineSpeciesConfiguration();
                    msControl.SetMarineSpeciesConfiguration(set, an, mat, piles, lh);
                }


            }
            catch (Exception ex)
            {
                lh.LogError(ex);
            }



        }

        private void openLogFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (lh != null)
            {
                System.Diagnostics.Process.Start("explorer.exe", this.lh.ThePath);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadSoundLevelGrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSoundSourceConfiguration(true);
            ssControl.OpenGridFile();
            ssControl.LoadBinaryGrid();
           
        }

        private void LoadMarineSpeciesConfiguration(bool bVisible)
        {
            try
            {
                txtInfo.Visible = !bVisible;
                AddRemoveControl(ssControl, !bVisible);
                AddRemoveControl(msControl, bVisible);
                msControl.Visible = bVisible;
                ssControl.Visible = !bVisible;
       
            

                if (bVisible)
                {
                    msControl.SelectedSoundSourceType = ssControl.SoundSourceType;
                    this.selectedSoundSourceType = ssControl.SoundSourceType;
                    msControl.SelectedSourceMetric = ssControl.SoundMetric;
                    this.selectedSourceMetric = ssControl.SoundMetric;

                    msControl.SetSoundSourceType(ssControl.SoundSourceType);
                    msControl.SetSoundSourceMetric(ssControl.SoundMetric);
                    msControl.SetOtherByScientificName();

                    curScreen = CurrentScreen.File3Mb;
                    btnStartOver.Text = "Back";
                    btnNext.Text = "Save and Run Calculation";
                    txtInfo.Visible = !bVisible;
                    ssControl.Visible = !bVisible;

                    msControl.Dock = DockStyle.Fill;
                    tableLayoutPanel1.SetColumn(msControl, 0);
                    tableLayoutPanel1.SetColumnSpan(msControl, 5);
                    tableLayoutPanel1.SetRow(msControl, 0);
                    tableLayoutPanel1.SetRowSpan(msControl, 4);
                }
                else
                { msControl.Visible = bVisible; }
            }
            catch (Exception ex)
            {
                lh.LogError(ex);
            }
        }

        private void LoadMain(bool bVisible)
        {
            try
            {
                txtInfo.Visible = bVisible;
                if (bVisible)
                {
                    curScreen = CurrentScreen.None;
                    AddRemoveControl(ssControl, !bVisible);
                    AddRemoveControl(msControl, !bVisible);
                }
            }

            catch (Exception ex)
            {
                lh.LogError(ex);
            }
        }

        private void LoadSoundSourceConfiguration(bool bVisible)
        {
            try
            {
                txtInfo.Visible = !bVisible;
                ssControl.Visible = bVisible;
                if (this.msControl == null)
                    InitMarineSpeciesConfiguration();
                msControl.Visible = !bVisible;
                AddRemoveControl(ssControl, bVisible);
                AddRemoveControl(msControl, !bVisible);
               
                if (bVisible)
                {

                    txtInfo.Visible = !bVisible;
                    msControl.Visible |= bVisible;
                    curScreen = CurrentScreen.SoundSource;
                  
                    ssControl.Dock = DockStyle.Fill;    
                    tableLayoutPanel1.SetColumn(ssControl, 0);
                    tableLayoutPanel1.SetColumnSpan(ssControl, 5);
                    tableLayoutPanel1.SetRow(ssControl, 0);
                    tableLayoutPanel1.SetRowSpan(ssControl, 4);
                }
                else
                { ssControl.Visible = bVisible;}
            }
            catch (Exception ex)
            {
                lh.LogError(ex);
            }
        }

        private void TtAIME_Manatea_FormClosing(object sender, FormClosingEventArgs e)
        {
            set.SaveSettings();
            System.Windows.Forms.Application.Exit();

        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void loadPileProgressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (set.PileCSV == "")
            {

                OpenFileDialog ofd = new OpenFileDialog();
                if (set == null) { set = new AppSettings(); }
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    set.PileCSV = ofd.FileName; 
                    set.SaveSetting(APP_Config_Keys.pileCSV, set.PileCSV);  
                }
            }
            PileParameterSettingsForm p = new PileParameterSettingsForm(set);
            if (DialogResult.OK == p.ShowDialog())
            {
                piles = p.Piles;
                if (piles != null)
                    ssControl.SetPiles(piles);
            }
        }

        private void load3MBMovementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMarineSpeciesConfiguration(true);
        
            msControl.Open3MBMovementFile();
           msControl.Load3MBMovementFile();
        }

        private void btnStartOver_Click(object sender, EventArgs e)
        {
            if(curScreen == CurrentScreen.None || curScreen == CurrentScreen.File3Mb)
            {
                curScreen = CurrentScreen.SoundSource;
                if (curScreen == CurrentScreen.None)
                    btnStartOver.Text = "Start";
                else
                    btnStartOver.Text = "Star Over";
                btnNext.Text = "Next";
                LoadSoundSourceConfiguration(true);
            }
           

        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
           
            if (curScreen == CurrentScreen.None)
            {
                curScreen = CurrentScreen.SoundSource;
                btnStartOver.Text = "Start Over";
                LoadSoundSourceConfiguration(true);
            }
            else if (curScreen == CurrentScreen.SoundSource)
            {
                if (!ssControl.GridCompleted)
                { MessageBox.Show("Please Load Sound Level Grid Binary File");
                    return;

                }
                    if (!ssControl.PileCompleted)
                {
                    MessageBox.Show("Please Load Pile Infornation ");
                    return;

                }
                curScreen = CurrentScreen.File3Mb;
                btnStartOver.Text = "Save and Run Calculation";
                ssControl.CollectData(ref input);

                msControl.IsCheckThresholdNeeded = true;

                LoadMarineSpeciesConfiguration(true);
            }
            else if (curScreen == CurrentScreen.File3Mb)
            {
                if (!msControl.AnimatReadCompleted)
                {
                    MessageBox.Show("Please Load 3 Mb File");
                    return;

                }
                LoadMarineSpeciesConfiguration(false);
                LoadSoundSourceConfiguration(false);
                LoadMain(true);
                bool bRun = ChooseOutputFilesFolder();
                runSummary.BRunCompleted = false;                

                if (!bRun)
                    return;

                    var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update);
                               });

                await Task.Run(() => RunManatea(progress, ref runSummary));
            }
        }

        private void AddText(string text, bool bNewLine = true,
          bool bBold = false, bool bItalic = false)
        {

            if (bNewLine)
                txtInfo.Text += "\n";
            RichTextBoxExtensions.AppendText(txtInfo, text, Color.Blue, 10, false, true);
        }

        private void openMarineMammalTheshholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TtAIME_Marine_Mammal_Dictionary form = new TtAIME_Marine_Mammal_Dictionary(set,lh);
            form.ShowDialog();  
        }



        private void RunManatea(IProgress<string> progress, ref ManateaRunSummaryResult result)
        {
            if (result == null)
                    result = new ManateaRunSummaryResult();

            an = msControl.RunAnimat;
            //mat = ssControl.GridFle;
     

            //double density = msControl.DModelDensity;
            //double scaleFactor = msControl.DScalingFactor;

            ssControl.CollectData(ref input);
            msControl.CollectData(ref input);

        
            result.Species = input.Species;
            result.SclaledFactor = input.ScalingFactor;
            result.SoundSourceMetric = input.SourceMetric;
            result.Sourcetype = input.SoundSourceType;
            result.Threshold = input.Threshold; 
            result.FileName = an.file3MB.GetSummaryName();
            input.OutputPath = set.Output;

            if (an == null) return;
            if (an.File3MB == null) return;
            an.File3MB.Animats.Sort();

          

            if (progress != null)
            {

                progress.Report("Starting Running Manatea");
            }

            bool bRunCompleted = an.RunManatea(this.input, ref result,  progress);


            if (progress != null)
            {

                progress.Report("Starting Collecting Results");
            }

            if (result != null && bRunCompleted)
            {
                an.CreateSummary(input, ref result, progress);
                result.BRunCompleted = true;    
            }


            //  AddText("Ready to write output with Manatea");
            if (progress != null)
            {
                progress.Report("Ready to write output with Manatea");
            }
         


            FileInfo fi = new FileInfo(set.A3mbIniFile);

            string outp = set.Output;
     

            //an.File3MB.WriteFiles(outp, input.SoundSourceType, input.SourceMetric, ref outp, header, "");

            //result.WriteSummary(outp);

            if (progress != null)
            {
                progress.Report("\n\n DONE! ");
            }


        }

        private bool ChooseOutputFilesFolder()
        {
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = set.Output;
            //dialog.DefaultFileName = set.A3mbIniFile;
            dialog.Title = "Select The Folder For Writing Output Files";


            if (CommonFileDialogResult.Ok == dialog.ShowDialog())
            {

                // get all the directories in selected dirctory
                var dirs = dialog.FileNames.ToArray();
                DirectoryInfo dir = new DirectoryInfo(dirs[0]);
                if (!dir.Exists) {  dir.Create(); }     
                set.Output = dir.FullName;
                set.SaveOutputFolderToConfigFile();
                return true;

            }

            return false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenOutputFolder();
        }

        private void OpenOutputFolder()
        {
            try
            {
                if (set != null && set.Output != null && set.Output.Length > 0)
                {
                    DirectoryInfo dir = new DirectoryInfo(set.Output);
                    if (dir.Exists)
                        System.Diagnostics.Process.Start("explorer.exe", dir.FullName);
                }
            }
            catch(Exception ex) {   lh.LogError(ex); }  
        }

        private void readOutputFieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
           DialogResult dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                ReadWriteAnimat readWriteAnimat = new ReadWriteAnimat();
                readWriteAnimat.ReadBinaryFile(dialog.FileName);
            }
        }
    }

    public enum CurrentScreen { None, SoundSource, File3Mb, RunAnimat};
}
