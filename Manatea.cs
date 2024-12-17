using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Read3mb;


namespace RunManatea
{
    public partial class Manatea : Form
    {

        string _3mbIniFile;
        LogHandler lh;
        Settings set;
        Read3mb.ReadRunWriteAnimat an = null;
        Read3mb.GridFile3dSoundLevel mat = null;
        DBSeaGridFiles gridfiles = null;

        public Manatea()
        {
            Init2();
            lh = new LogHandler();
            set = new Settings(lh);
            textBoxProgress.Text = "";

            mat = new Read3mb.GridFile3dSoundLevel();

        }

        public void Init2()
        {
            InitializeComponent();
            bool badded = false;
            foreach (Control c in this.Controls)
            {
                if (c.Name == tableLayoutPanel1.Name)
                {
                    badded = true; break;
                }
            }
            if (!badded)
                this.Controls.Add(tableLayoutPanel1);

            badded = false;
            foreach (Control c in this.Controls)
            {
                if (c.Name == menuStrip1.Name)
                {
                    badded = true; break;
                }
            }
            if (!badded)
                this.Controls.Add(menuStrip1);
        }

        private void filesLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {

           
        }

        private void openAppFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void createBinaryGridFileforSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            AddText("\n");
            var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update);
                               });
            await Task.Run(() => RunManatea(progress));

        }

        private void RunManatea(IProgress<string> progress)
        {

            if (progress != null)
            {
                progress.Report("Start Reading 3mb File");
            }

            an = new Read3mb.ReadRunWriteAnimat(set.A3mbIniFile);
            //an.Read(null);
            an.Read(progress);
            if (progress != null)
            {
                progress.Report("Done Reading 3mb File");
            }
            string text = "";
            for (int i = 0; i < an.file3MB.Animats.Count; i++)
            {
                Animat a = an.file3MB.Animats[i];
                text += " Animat " + a.SpeciesName + " " + " individual " + a.Individual.ToString() + " Pod " + a.Pod.ToString();
                if (a.NWentOffScreen > 0)
                {
                    text += " Went offscreen " + a.NWentOffScreen.ToString() + " times.";
                }
                else
                    text += " Never went offscreen.";
                if (progress != null)
                {
                    progress.Report(text);
                    text = "";
                }

                ////for (int j = 0; j < a.nIterations; j++)
                ////    Console.WriteLine(j.ToString() + " - "
                ////        + an.file3MB.Animats[0].GetAnimatMovement(j).BWentOffTheScreen.ToString());
            }
           
                if (progress != null)
            {
                progress.Report("3mb file has been read " + set.A3mbIniFile);
                progress.Report("Starting reading grid file from the binary" + set.GridBinary);
            }
            if (mat.InitGridData == null)
            {
                bool bmat = mat.ReadBinaryFile(set.GridBinary);
                if (!bmat)
                    MessageBox.Show("error ReadBinaryFile");
            }
            if (progress != null)
            {
                progress.Report("Grid File has been read");
            }

            PileProgressions piles = new PileProgressions();
            piles.Read(set.PileCSV);
            if (progress != null)
            {
                progress.Report("Reading pile " + set.PileCSV);
            }

            lh.LogWarning("Running Manatea");
            if (progress != null)
            {

                progress.Report("Starting Running Manatea");
            }
            an.RunManatea(this.mat, piles,progress);
            //  AddText("Ready to write output with Manatea");
            if (progress != null)
            {
                progress.Report("Ready to write output with Manatea");
            }
            if (an == null) return;
            if (an.File3MB == null) return;
            an.File3MB.Animats.Sort();
            FileInfo fi = new FileInfo(set.A3mbIniFile);
            string outp = set.Output;
            //string header[] = ReadRunWriteAnimat.WriteHeaderAnimatLies(ReadRunWriteAnimat.WriteHeaderAnimat(sel)
            an.File3MB.WriteFiles(fi.DirectoryName, ref outp,null);
            set.Output = outp;
            set.SaveOutputFolderToConfigFile();


            if (progress != null)
            {
                progress.Report("\n\n DONE! ");
            }
        }

        private void AddText(string text, bool bNewLine = true,
            bool bBold = false, bool bItalic = false)
        {

            if (bNewLine)
                textBoxProgress.Text += "\n";
            textBoxProgress.Text += text;
            labelInfo.Text = text;  

        }

        private void displayPileFileAndMakeChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PileProgressions piles = new PileProgressions();
            piles.Read(set.PileCSV);

            PileProgressionForm form = new PileProgressionForm(piles);
            if (form.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(set.PileCSV);
                DateTime dtnow = DateTime.Now;
                string add = dtnow.Year.ToString() +
                    dtnow.Month.ToString() +
                    dtnow.Day.ToString() +
                    dtnow.Hour.ToString() +
                    dtnow.Minute.ToString() +
                    dtnow.Second.ToString();

                string filename = fi.DirectoryName +
                    "\\" + fi.Name.Replace(fi.Extension, "") + "_" + add + ".csv";
                FileInfo newfi = new FileInfo(filename);

                StreamWriter sw = new StreamWriter(newfi.FullName);
                sw.WriteLine("Hammer Energy(%),Hammer Energy(kJ),  Duration(minutes),  Blows per Minute, Number of Blows, Decibel(dB) Offset");
                foreach (PileProgression ob in piles.ListPileProgression)
                {
                    string line = "";
                    line += String.Format("{0},{1},{2},{3},{4},{5}",
                        ob.HammerEnergypercent,
                        ob.HammerEnergykJ,
                        ob.Duration,
                        ob.BlowsPerMinute,
                        ob.NumberOfStrikes,
                        ob.DecibelOffset);
                    sw.WriteLine(line);
                }
                //Hammer Energy(%)	Hammer Energy(kJ)  Duration(minutes)  Blows per Minute Number of Blows Decibel(dB) Offset
                sw.Close();
                set.PileCSV = newfi.FullName;
                set.SaveSetting(APP_Config_Keys.pileCSV,
                    set.PileCSV);
            }
        }

        private async void readDbSeaAsciFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddText("\n");
            var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update);
                               });
            await Task.Run(() => ReadDBSeaGridFiles(progress));


        }
        private void ReadDBSeaGridFiles(IProgress<string> progress)
        {
            bool b;
            double dStep = 1;
            string message = "Please Enter the Depth Step";
            do
            {
                var value = Interaction.InputBox(message, "Depth Step (m)", dStep.ToString()); ;
                b = double.TryParse(value, out dStep);
                if (b)
                    b = dStep > 0;
                message = "Please Enter the Depth Step > 0; " + value + " is incorrect value";
            } while (!b);

            if (progress != null)
            {
                progress.Report("Start Reading DBSeaGridFiles");
                progress.Report("Step is " + dStep.ToString());
            }
            gridfiles = new DBSeaGridFiles(set.DbSeaData, ".asc");
            gridfiles.DepthStep = dStep;
            if (gridfiles.Read(progress))
            {
                gridfiles.Sort(progress);
                gridfiles.CreateGridForInterpolation(progress);
                mat = gridfiles.Grid3dSoundLevel;
            }
        }

        private void createBinaryGridFromCSVItem_Click(object sender, EventArgs e)
        {
            CreateGridBinary form = new CreateGridBinary(set);
            form.ShowDialog();
            set = form.Setttings;
        }

        private async void createBinaryGridFromDbSeaItem_Click(object sender, EventArgs e)
        {
            var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update);
                               });

            await Task.Run(() =>
            {
                ReadDBSeaGridFiles(progress);
                this.mat.WriteBinaryFile(set.GridBinary, progress);
            });


        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", set.Output);

        }

        private void fileLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            set.ShowDialog();
        }

        private void logsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (lh != null)
            {
                System.Diagnostics.Process.Start("explorer.exe", this.lh.ThePath);
            }
        }
    }
}

