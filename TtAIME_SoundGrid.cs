using Microsoft.VisualBasic;
using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetratechTools;
using System.IO;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;



namespace RunManatea
{
    public partial class TtAIME_SoundGrid : Form
    {
        string _3mbIniFile;
        LogHandler lh;
        AppSettings set;
        Read3mb.ReadRunWriteAnimat an = null;
        Read3mb.GridFile3dSoundLevel mat = null;
        DBSeaGridFiles gridfiles = null;
        double dStep = 1;

        string folderAsciiFiles = "";
        CommonOpenFileDialog dialog;

        bool bCheckFolder = false;
            bool bCheckFile = false;
        bool bGridCompleted = false;

        private string SuggestedFileLocation = "";
        private string SuggestedFileName = "";

        public TtAIME_SoundGrid()
        {
            InitializeComponent();
            set = new AppSettings();
            dialog = new CommonOpenFileDialog();
            dialog.EnsureFileExists = true;
            dialog.EnsurePathExists = true;

            cbGridType.Items.Clear();
            mat = new GridFile3dSoundLevel();


            cbGridType.Items.AddRange(new string[]{SoundSourceMetric.SEL_Weighted.ToDescriptionString(),
                SoundSourceMetric.SPL_Unweighted.ToDescriptionString(),
                SoundSourceMetric.PK_Unweighted.ToDescriptionString(),
                SoundSourceMetric.SPL_Weighted.ToDescriptionString() });
            cbGridType.SelectedIndex = 0;
            ltbFolder.IsFolder = true;
            ltbFile.IsFolder = false;
            this.ltbFolder.BUseCustomClickEvent = true;
            this.ltbFolder.TextBoxEditable = false;
            this.ltbFile.TextBoxEditable = false;
            this.ltbFile.BUseCustomClickEvent = true;
            this.ltbFile.ControlClicked += FileChanged;
            this.ltbFolder.ControlClicked += FolderChanged;

            set.ReadSettings();
     
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileMenuClicked()
        {

        }

        private void FileChanged(object sender, EventArgs e)
        {
            ChooseOutputGridFileFullPath();
        }


        private void FolderChanged(object sender, EventArgs e)
        {
            ChooseAsciiFilesFolder();
        }


        private void loadGrideToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStripTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void loadGridSeriesFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseAsciiFilesFolder();

        }

        private void ChooseAsciiFilesFolder()
        {
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = set.GridAsciiFolder;
            dialog.DefaultFileName = set.GridAsciiFolder;
            dialog.Title = "Select The Folder To Process";


            if (CommonFileDialogResult.Ok == dialog.ShowDialog())
            {
                 
                // get all the directories in selected dirctory
                var dirs = dialog.FileNames.ToArray();
                DirectoryInfo dir = new DirectoryInfo(dirs[0]);

                //check if * asc exixts

                FileInfo[] files = dir.GetFiles();

                bool bAsc = false;
                for (int i = 0; i < files.Length; i++)
                    if (files[i].Extension.ToLower().Equals("asc"))
                    {
                        bAsc = true; break;
                    }

                if (bAsc)
                {
                    MessageBox.Show("Folder " + dir.FullName + " doesn't contain grid files (*.asc)");

                }
                else
                {
                    set.GridAsciiFolder = dirs[0].ToLower();
                    SuggestedFileLocation = dir.Parent.FullName;
                    SuggestedFileName = dir.Name + ".grid";
                    ltbFolder.TextBoxText = set.GridAsciiFolder;
                    txtProjName.Text = dir.Name;
                    txtSceName.Text = dir.Name;
                    txtCoordSystem.Text = "UTM Zone 18N";
                    bCheckFolder = true;
                    set.GridBinary = ""; 
                    set.SaveAsciiFolderToConfigFile();
                    bCheckFile = false;
                }

            }
        }

        private void AddText(string text, bool bNewLine = true,
           bool bBold = false, bool bItalic = false)
        {

            RichTextBoxExtensions.AppendText(this.txtInfo, text+"\n", Color.Navy);

        }

        private async void process3DSoundGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await process3DSoundGrid();
        }

        private async Task process3DSoundGrid()
        {
            if (!bCheckFolder)
            {

                MessageBox.Show("Please choose DBAscii folder");
                ChooseAsciiFilesFolder();


            }

            if (!bCheckFile)
            {
                MessageBox.Show("Please choose output file");
                ChooseOutputGridFileFullPath();
            }

            if (bCheckFile && bCheckFolder)
            {

                mat.ProjectName = txtProjName.Text;
                mat.CoordinateSystem = txtCoordSystem.Text;
                mat.ScenarioName = txtSceName.Text;
                string sGridType = cbGridType.Items[cbGridType.SelectedIndex].ToString();
                mat.SetGridType(sGridType.ToSoundSourceMetricFromDescription());

                await ProcessGrid();

            }
        }

        private async Task ProcessGrid()
        {
            DirectoryInfo dir = new DirectoryInfo(set.GridAsciiFolder);
          
            if (dir.Exists)
            {
                EnterGripDepthStep();
                if (!(dStep > 0))
                    return;
                var progress = new Progress<string>(
                               update =>
                               {
                                   AddText(update);
                               });

                await Task.Run(() =>
                {
                    ReadDBSeaGridFilesStart(progress);                   

                    this.mat.WriteBinaryFile(set.GridBinary, progress);
                });
            }
        }

        private void ReadDBSeaGridFilesStart(IProgress<string> progress)
        {
           
            if (progress != null)
            {
                progress.Report("Start Reading DBSeaGridFiles");
                progress.Report("Step is " + dStep.ToString());
            }
            gridfiles = new DBSeaGridFiles(set.GridAsciiFolder, mat, ".asc");
            gridfiles.DepthStep = set.DGridStep;
            if (gridfiles != null)
                ReadDBSeaGridFiles(progress);
            else
            {
                MessageBox.Show("No Ascii Files found.");
            }
        }


        private void ReadDBSeaGridFiles(IProgress<string> progress)
        {           
            if (gridfiles.Read(progress))
            {
                gridfiles.Sort(progress);
                gridfiles.CreateGridForInterpolation(progress);
                mat = gridfiles.Grid3dSoundLevel;
            }
        }

        private void EnterGripDepthStep()
        {
            if (set.DGridStep < 0) set.DGridStep = 1.0;
            dStep = set.DGridStep;
            bool b = false; ;
            dStep = 1;
            string message = "Please Enter the Depth Step";
            int iTry = 0;
            do
            {
                var value = Interaction.InputBox(message, "Depth Step (m)", dStep.ToString());
                if (value.ToString() !="")
                b = double.TryParse(value, out dStep);
                if (b)
                    b = dStep > 0;
                message = "Please Enter the Depth Step > 0; " + value + " is incorrect value";
                iTry++; 
            } while (!b && iTry < 1);

            if (!b && !(dStep >0))
            {
                bCheckFile = false;
                return;
            }
            else if(!b && dStep > 0)
            {
                MessageBox.Show("Depth Step (m) - " + dStep.ToString() , "Using Depth", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            set.DGridStep = dStep;
            set.SaveSoundGridStepToConfigFile();
        }

        private void ChooseOutputGridFileFullPath()
        {
            if (set != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = ".grid";
                sfd.InitialDirectory = set.GridBinary;
                if (SuggestedFileLocation != "")
                    sfd.InitialDirectory = SuggestedFileLocation;
                if (SuggestedFileName != "")
                    sfd.FileName = SuggestedFileName;
                else
                    sfd.FileName = "SoundSource.grid";

                if (DialogResult.OK == sfd.ShowDialog())
                {
                    set.GridBinary = sfd.FileName;
                    ltbFile.TextBoxText = sfd.FileName;
                    bCheckFile = true;
                }
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (lh != null)
            {
                System.Diagnostics.Process.Start("explorer.exe", this.lh.ThePath);
            }
        }

        private void ltbFolder_DoubleClick(object sender, EventArgs e)
        {
            ChooseAsciiFilesFolder();
        }

        private async void ltbFile_DoubleClick(object sender, EventArgs e)
        {
            await ProcessGrid();
        }

        private void load3DGridToolStripMenuItem1_Click(object sender, EventArgs e)
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
            LoadBinaryGrid();
        }
        private void ResetText()
        {
            txtInfo.Text = "";
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
            


            binaryfile = new FileInfo(set.GridBinary);
            bool mMat = true;
            if (binaryfile.Exists)
            {
                await Task.Run(() =>
                {
                    mMat = mat.ReadBinaryFile(binaryfile.FullName, progress);
                    bGridCompleted = true;

                });

            //    //Mat.ProjectName = "CVOWC";
            //    //Mat.ScenarioName = "Standard Drive";
            //    //Mat.CoordinateSystem = "UTM Zone 18N";Mat.GridType = GridSoundMetric.SEL_Unweighted;

            //    //Mat.WriteBinaryFile(binaryfile.FullName + "1s", progress);
            //    ResetText();
            //    List<string> info = dbSeaGridInfo();

            //    for (int i = 0; i < info.Count; i++)
            //    {
            //        RichTextBoxExtensions.AppendText(this.rtbdbSeaGridInfo, info[i] + "\n", Color.Navy, 10, false, true);
            //    }

            //    if (mat.GridType != SoundSourceMetric.UNKNOWN)
            //    {
            //        string s = mat.GridType.ToString();
            //        int index = 0;
            //        foreach (object item in listBoxMetric.Items)
            //        {
            //            string s1 = item.ToString();
            //            if (s1 == s)
            //            {
            //                listBoxMetric.SelectedIndex = index;
            //                break;
            //            }
            //            index++;
            //        }
            //    }

            //    if (!mMat)
            //        RichTextBoxExtensions.AppendText(this.rtbdbSeaGridInfo, "Incorrect file " + binaryfile.FullName, Color.Red, 10, true);
            }
        }

        private void ltbFile_Click(object sender, EventArgs e)
        {
            int a = 2;
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            await process3DSoundGrid();
        }

        private void TtAIME_SoundGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            set.SaveSettings();
           
        }

        private void ltbFolder_Load(object sender, EventArgs e)
        {
            
           
        }

        private void ltbFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void checkThePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bGridCompleted)
            {
                CheckSoundGridPoint form = new CheckSoundGridPoint(mat);
                form.ShowDialog();  

            }
            else { MessageBox.Show("Please Load the GridFile First");
                load3DGridToolStripMenuItem1.PerformClick();
            }
        }
    }
}

