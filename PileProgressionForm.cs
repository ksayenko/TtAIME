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

namespace RunManatea
{
    public partial class PileProgressionForm : Form
    {
        PileProgressions piles;

        public PileProgressions Piles { get => piles; set => piles = value; }

        public PileProgressionForm(PileProgressions p)
        {
            InitializeComponent();
            this.piles = p;
        }

        private void PileProgressionForm_Load(object sender, EventArgs e)
        {
            foreach(PileProgression p in piles.ListPileProgression)
            {
                var index = this.dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[0].Value = p.HammerEnergypercent;
                dataGridView1.Rows[index].Cells[1].Value = p.HammerEnergykJ;
                dataGridView1.Rows[index].Cells[2].Value = p.Duration;
                dataGridView1.Rows[index].Cells[3].Value = p.BlowsPerMinute;
                dataGridView1.Rows[index].Cells[4].Value = p.NumberOfStrikes;
                dataGridView1.Rows[index].Cells[5].Value = p.DecibelOffset;


            }

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
            catch(Exception ex)
            {
                lh.LogError("Can't convert " + value.ToString());
            }

            return v;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            LogHandler lh = new LogHandler();
            
            piles.ListPileProgression.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                //check first item
                if (row.Cells[0].Value == null) break;
                try
                {
                    PileProgression p = new PileProgression();
                    p.HammerEnergypercent = ConvertCellValue(row, 0);
                                  p.HammerEnergykJ = ConvertCellValue(row, 1);
                    p.Duration = ConvertCellValue(row,2);
                    p.BlowsPerMinute = ConvertCellValue(row, 3);
                  
                    p.NumberOfStrikes = (int)ConvertCellValue(row, 4);
                    p.DecibelOffset = ConvertCellValue(row, 5);
          
                    p.InitialPileCalculations();
                    piles.ListPileProgression.Add(p);
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); 
                    lh.LogError(ex); }

            }
            this.DialogResult = DialogResult.OK; 
            this.Close();
        }
    }
}
