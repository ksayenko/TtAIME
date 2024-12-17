
using Read3mb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunManatea
{

    public struct LabelButtonColorsTextForAsync
    {
        public string textLabel;
        public string foreColorLabel;
        public string backColorLabel;
        public string textBtn;
        public string foreColorBtn;
        public string backColorBtn;
        public bool isEnabled;
    }

    public partial class CreateGridBinary : Form
    {
        double step = 1;
        int nx = 635, ny = 605, nz = 509;
        double xcorner = 384370, ycornrer = 4023453;
        double dx = 2.292825136501570e+02;
        double dy = 2.292825136501570e+02;

        string fullnameCSV;
        string fullnameBIN;
        Settings set;
        Read3mb.GridFile3dSoundLevel mat = null;
        Color c = Color.White;

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

        }

        public CreateGridBinary(Settings set)
        {
            Init2();
            txtSizex.Text = nx.ToString();
            txtSizey.Text = ny.ToString();
            txtSizez.Text = nz.ToString();
            txtxllCorner0.Text = xcorner.ToString();
            txtyllCorner0.Text = ycornrer.ToString();
            txtStep.Text = step.ToString();
            txtDx.Text = txtDy.Text = dx.ToString();
            this.set = set;
            this.fullnameCSV = set.GridCSV;
            this.fullnameBIN = set.GridBinary;
        }

        public Settings Setttings { get => set; set => set = value; }

        private async void btnReadMatFile_Click(object sender, EventArgs e)
        {

            FileInfo fi = new FileInfo(fullnameCSV);
            string path, name;
            if (!fi.Exists)
            {
                MessageBox.Show("CSV File Wasn't Found");
                set.ShowDialog();
                return;
            }




            //load('CVOWC_Complex_Grid_01092024_LF.mat', 'dx0', 'dy0', 'grid3D', 'xllCorner0', 'yllCorner0', 'nCols0', 'nRows0', 'depthStep', 'nFiles');
            path = fi.DirectoryName;
            name = fi.Name;
            //@"C:\Users\kateryna.sayenko\Documents\AuditoryBiophysicsLab\VS Manatea\RunManatea";
            //name = "CVOWC_Complex_Grid_01092024_LF.mat";
            nx = Int32.Parse(txtSizex.Text);
            ny = Int32.Parse(txtSizey.Text);
            nz = Int32.Parse(txtSizez.Text);
            xcorner = Double.Parse(txtxllCorner0.Text);
            ycornrer = Double.Parse(txtyllCorner0.Text);
            step = Double.Parse(txtStep.Text);
            dx = double.Parse(txtDx.Text);
            mat = new Read3mb.GridFile3dSoundLevel(path, name, nx, ny, nz, xcorner, ycornrer, step, dx, dy);
            c = this.btnReadMatFile.BackColor;
            this.btnReadMatFile.BackColor = Color.Coral;
            this.btnReadMatFile.Enabled = false;       
            lblStatus.Text = " READING GRID FILE...." + fullnameCSV;
           
            var progress = new Progress<LabelButtonColorsTextForAsync>(
               update => { lblStatus.Text = update.textLabel;
                   lblStatus.ForeColor = Color.FromName(update.foreColorLabel);
                   btnReadMatFile.ForeColor = Color.FromName(update.foreColorBtn);
                   btnReadMatFile.Enabled = update.isEnabled;
               }); 
            System.Threading.Thread.Sleep(1000);

            await Task.Run(() => Read (progress));
            
          

        }


        public void Read(IProgress<LabelButtonColorsTextForAsync> progress)
        {
            int result=0;
            LabelButtonColorsTextForAsync l;
            if (progress != null)
            {
                l = new LabelButtonColorsTextForAsync();
                l.textLabel = " READING GRID FILE...." + fullnameCSV;
                l.backColorLabel = "Coral";                
                l.foreColorLabel = "Red";
                l.foreColorBtn = "Coral";
                l.backColorBtn = "Pink";
                l.isEnabled = false;

                progress.Report(l);

            }


            // Complex calculation here
            result = mat.ReadGrid3dCSV(fullnameCSV);
            if (progress != null)
            {
                l = new LabelButtonColorsTextForAsync();
                l.textLabel = "Done Reading Csv";
                l.backColorLabel = "Grey";
                l.backColorBtn = "Pink";
                l.foreColorLabel = "Red";
                l.foreColorBtn = "Coral";
                l.isEnabled = false;
                progress.Report(l);

            }

            result = mat.WriteBinaryFile(fullnameBIN);
            if (progress != null)
            {
                l = new LabelButtonColorsTextForAsync();
                l.textLabel = "Done Writing grid binary";
                l.backColorLabel = "blue";
                l.backColorBtn = "Red";
                l.foreColorLabel = "Black";
                l.foreColorBtn = "White";
                l.isEnabled = true;
                progress.Report(l);

            }



        }

        public async Task<int> Write()
        {
            int result = 0;
            Task<int> task = Task.Run(() =>
            {
                // Complex calculation here              
                result = mat.WriteBinaryFile(fullnameBIN);
                this.btnReadMatFile.BackColor = c;
                this.btnReadMatFile.Enabled = true;
                return result;
            });

            return result;

        }

    }
}
