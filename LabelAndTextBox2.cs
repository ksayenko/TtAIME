using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RunManatea
{
    
    public partial class LabelAndTextBox2 : UserControl
    {

        private LabelAndTextBox2Orientation orientation;
        private bool isDecimalNumber = true;
        private int percentTextBox = 60;

               public LabelAndTextBox2()
        {
            InitializeComponent();
            Init2();
        }

        public void LabelTextBold(bool b = true) {
            if (b)
                theLabel.Font = new Font(theLabel.Font, FontStyle.Bold);
            else
                theLabel.Font = new Font(theLabel.Font, FontStyle.Regular);

          
        }
        public void TextBold(bool b = true)
        {
            if (b)
                theTextBox.Font = new Font(theTextBox.Font, FontStyle.Bold);
            else
                theTextBox.Font = new Font(theTextBox.Font, FontStyle.Regular);


        }
       


        public void Init2()
        {
            if (orientation == LabelAndTextBox2Orientation.LabelTBOneRow ||
                orientation == LabelAndTextBox2Orientation.TBLabelOneRow
                )
            {
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 2;

            }
            else
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 1;
            }

            if (orientation == LabelAndTextBox2Orientation.LabelTBOneRow)
            {
                this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.theTextBox, 1, 0);
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                    (float)100- percentTextBox));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
            }


            if (orientation == LabelAndTextBox2Orientation.TBLabelOneRow)
            {
                this.tableLayoutPanel1.Controls.Add(this.theLabel, 1, 0);
                this.tableLayoutPanel1.Controls.Add(this.theTextBox, 0, 0);
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                 (float)100 - percentTextBox));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
            }


            if (orientation == LabelAndTextBox2Orientation.LabelTBOneColumn)
            {
                this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.theTextBox, 0, 1);
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                 (float)100 - percentTextBox));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
            }

            if (orientation == LabelAndTextBox2Orientation.TBLabelOneColumn)
            {
                this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 1);
                this.tableLayoutPanel1.Controls.Add(this.theTextBox, 0, 0);
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                 (float)100 - percentTextBox));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
            }

        }

        public void SetData(string labeltext, double datatext)
        {
            SetData(labeltext, datatext.ToString());
        }

        public void SetData(string labeltext, string datatext)
        {
            theLabel.Text = labeltext;
            theTextBox.Text = datatext; 
        }

        public string GetData() { return theTextBox.Text; }
        public double GetDEcimalData()
        {
            string datatext = theTextBox.Text;
            double rv = Double.NaN;
            Double.TryParse(datatext, out rv);
            return rv;

        }


        public LabelAndTextBox2Orientation Orientation { get => orientation; set { orientation = value;

                if (orientation == LabelAndTextBox2Orientation.LabelTBOneRow ||
                orientation == LabelAndTextBox2Orientation.TBLabelOneRow
                )
                {
                    tableLayoutPanel1.RowCount = 1;
                    tableLayoutPanel1.ColumnCount = 2;

                }
                else
                {
                    tableLayoutPanel1.RowCount = 2;
                    tableLayoutPanel1.ColumnCount = 1;
                }

                if (orientation == LabelAndTextBox2Orientation.LabelTBOneRow)
                {
                    this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 0);
                    this.tableLayoutPanel1.Controls.Add(this.theTextBox, 1, 0);
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                        (float)100 - percentTextBox));
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }


                if (orientation == LabelAndTextBox2Orientation.TBLabelOneRow)
                {
                    this.tableLayoutPanel1.Controls.Add(this.theLabel, 1, 0);
                    this.tableLayoutPanel1.Controls.Add(this.theTextBox, 0, 0);
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                     (float)100 - percentTextBox));
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }


                if (orientation == LabelAndTextBox2Orientation.LabelTBOneColumn)
                {
                    this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 0);
                    this.tableLayoutPanel1.Controls.Add(this.theTextBox, 0, 1);
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                     (float)100 - percentTextBox));
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }

                if (orientation == LabelAndTextBox2Orientation.TBLabelOneColumn)
                {
                    this.tableLayoutPanel1.Controls.Add(this.theLabel, 0, 1);
                    this.tableLayoutPanel1.Controls.Add(this.theTextBox, 0, 0);
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                     (float)100 - percentTextBox));
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }
            } }

        public bool IsDecimalNumber { get => isDecimalNumber; set
            {
                isDecimalNumber = value;
                if (isDecimalNumber) 
                    theTextBox.TextAlign = HorizontalAlignment.Right;
            
            } }
        public int PercentTextBox { get => percentTextBox;
            set { percentTextBox = value; 
                if (orientation == LabelAndTextBox2Orientation.LabelTBOneRow)
                {
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                        (float)100 - percentTextBox));
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }


                if (orientation == LabelAndTextBox2Orientation.TBLabelOneRow)                {
        
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                     (float)100 - percentTextBox));
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }


                if (orientation == LabelAndTextBox2Orientation.LabelTBOneColumn)                {
         
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                     (float)100 - percentTextBox));
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }

                if (orientation == LabelAndTextBox2Orientation.TBLabelOneColumn)                {
           
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,
                     (float)100 - percentTextBox));
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentTextBox));
                }
            } }

        public string LabelText { get => theLabel.Text; set => theLabel.Text = value; }
        public string TextBoxValue { get => theTextBox.Text; set => theTextBox.Text = value; }
        public bool IsTextBoxMultiline { get => theTextBox.Multiline; set => theTextBox.Multiline = value; }

        public Padding TextBoxMargin {  get => theTextBox.Margin; set => theTextBox.Margin = value; }   
        public Padding LabelMargin { get => theLabel.Margin; set=> theLabel.Margin = value; } 

        private void theTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isDecimalNumber)
            {
                bool isDigit = !char.IsDigit(e.KeyChar)
                         && !char.IsControl(e.KeyChar);
                if (isDigit)
                {
                    bool isDot = e.KeyChar == '.';

                    if (isDot)
                    {
                        string text = ((System.Windows.Forms.TextBox)sender).Text;
                        int iDots = text.IndexOf('.');
                        if (iDots < 0) //not any dots
                            isDigit = false;


                    }
                }
                e.Handled = isDigit;
            }
        }

        public override string Text
        {
            get
            {
                return theTextBox.Text; 
            }
            set
            {
                theTextBox.Text= value;
            }
        }

    }

    public enum LabelAndTextBox2Orientation { LabelTBOneRow, LabelTBOneColumn, TBLabelOneRow, TBLabelOneColumn };
}
