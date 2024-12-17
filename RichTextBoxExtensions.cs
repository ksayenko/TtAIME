using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunManatea
{ 

    
    public static class RichTextBoxExtensions
    {
        
        public static void AppendText(this RichTextBox box, string text, Color color, float fontSize = 0, bool bBold = false, bool bItalic = false) 
        {

            Font font = box.Font;
            if (fontSize <=0)
                fontSize = font.Size;
            if (fontSize > 0 && bBold && bItalic)
            {
                font =  new Font(font.FontFamily, fontSize, FontStyle.Bold|FontStyle.Italic);   
            }
            else if ( bBold && !bItalic)
            {
                font = new Font(font.FontFamily, fontSize, FontStyle.Bold);
            }
            else if (!bBold && bItalic)
            {
                font = new Font(font.FontFamily, fontSize, FontStyle.Italic);
            }
            else 
            {
                font = new Font(font.FontFamily, fontSize);
            }
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionFont = font;   
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}


