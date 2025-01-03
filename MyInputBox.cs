﻿using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace RunManatea
{  
        public class CustomInputBox
        {
            #region Interface
            public static string ShowDialog(string prompt, string title, string defaultValue = null, int? xPos = null, int? yPos = null)
            {
                InputBoxDialog form = new InputBoxDialog(prompt, title, defaultValue, xPos, yPos);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.Cancel)
                    return null;
                else
                    return form.Value;
            }
            #endregion

            #region Auxiliary class
            private class InputBoxDialog : Form
            {
                public string Value { get { return _txtInput.Text; } }

                private System.Windows.Forms.Label _lblPrompt;
                private TextBox _txtInput;
                private Button _btnOk;
                private Button _btnCancel;

                #region Constructor
                public InputBoxDialog(string prompt, string title, string defaultValue = null, int? xPos = null, int? yPos = null)
                {
                    if (xPos == null && yPos == null)
                    {
                        StartPosition = FormStartPosition.CenterParent;
                    }
                    else
                    {
                        StartPosition = FormStartPosition.Manual;

                        if (xPos == null) xPos = (Screen.PrimaryScreen.WorkingArea.Width - Width) >> 1;
                        if (yPos == null) yPos = (Screen.PrimaryScreen.WorkingArea.Height - Height) >> 1;

                        Location = new Point(xPos.Value, yPos.Value);
                    }

                    InitializeComponent();

                    if (title == null) title = Application.ProductName;
                    Text = title;

                    _lblPrompt.Text = prompt;
                    Graphics graphics = CreateGraphics();
                    _lblPrompt.Size = graphics.MeasureString(prompt, _lblPrompt.Font).ToSize();
                    int promptWidth = _lblPrompt.Size.Width;
                    int promptHeight = _lblPrompt.Size.Height;

                    _txtInput.Location = new Point(8, 30 + promptHeight);
                    int inputWidth = promptWidth < 206 ? 206 : promptWidth;
                    _txtInput.Size = new Size(inputWidth, 21);
                    _txtInput.Text = defaultValue;
                    _txtInput.SelectAll();
                    _txtInput.Focus();

                    Height = 200 + promptHeight;
                    Width = inputWidth + 50;

                    _btnOk.Location = new Point(8, 60 + promptHeight);
                    _btnOk.Size = new Size(100, 26);

                    _btnCancel.Location = new Point(120, 60 + promptHeight);
                    _btnCancel.Size = new Size(100, 26);

                    return;
                }
                #endregion

                #region Methods
                protected void InitializeComponent()
                {
                this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold);
                    _lblPrompt = new System.Windows.Forms.Label();
                    _lblPrompt.Location = new Point(12, 9);
                    _lblPrompt.TabIndex = 0;
                    _lblPrompt.BackColor = Color.Transparent;
                    _lblPrompt.MaximumSize = new Size(350, 0);
                     _lblPrompt.AutoSize = true;

                _txtInput = new TextBox();
                    _txtInput.Size = new Size(156, 20);
                    _txtInput.TabIndex = 1;

                    _btnOk = new Button();
                    _btnOk.TabIndex = 2;
                    _btnOk.Size = new Size(75, 26);
                    _btnOk.Text = "&OK";
                    _btnOk.DialogResult = DialogResult.OK;
                _btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left));


                _btnCancel = new Button();
                    _btnCancel.TabIndex = 3;
                _btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom)| System.Windows.Forms.AnchorStyles.Right));
                _btnCancel.Size = new Size(75, 26);
                    _btnCancel.Text = "&Cancel";
                    _btnCancel.DialogResult = DialogResult.Cancel;

                    AcceptButton = _btnOk;
                    CancelButton = _btnCancel;

                    Controls.Add(_lblPrompt);
                    Controls.Add(_txtInput);
                    Controls.Add(_btnOk);
                    Controls.Add(_btnCancel);

                    FormBorderStyle = FormBorderStyle.FixedDialog;
                    MaximizeBox = false;
                    MinimizeBox = false;

                    return;
                }
                #endregion
            }
            #endregion
        }
    }

