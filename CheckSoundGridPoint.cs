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
using System.Xml.XPath;

namespace RunManatea
{
    public partial class CheckSoundGridPoint : Form
    {
        Read3mb.GridFile3dSoundLevel mat = null;
        double x;
        double y;
        double result;
        double depth;
        public CheckSoundGridPoint(GridFile3dSoundLevel mat)
        {
            InitializeComponent();
            this.mat = mat;
            Init();
        }

        public void Init()
        {
            txtX.PercentTextBox = 90;
            txtX.LabelTextBold();
            txtY.LabelTextBold();
            txtDepth.LabelTextBold();
            txtValue.LabelTextBold();
            txtValue.TextBold();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CollectTextBoxDataAndCalculate();
        }

        private void CollectTextBoxDataAndCalculate()
        {
            x = y =  depth = result = -9999;
            Double.TryParse(txtX.Text, out x);
            Double.TryParse(txtY.Text, out y);
            Double.TryParse(txtDepth.Text, out depth);
            bool bin = true;

            result = MatLabLike.Grid3Dinterpolation(mat, x, y, depth, out bin);
            txtValue.Text = result.ToString();
            if (!bin)
                txtValue.Text += " The x y is off the grid";


        }
    }
}
