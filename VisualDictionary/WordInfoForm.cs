﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;

namespace VisualDictionary
{
    public partial class WordInfoForm : Form
    {
        private string m_Word;
        private bool m_Online;
        private bool m_Pinned;
        private Point m_MouseDownPoint = Point.Empty;
        private Control m_FocusedControl = null;

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        private string m_Language = TranslationLanguage.English.ToString();

        public WordInfoForm(string word, bool online)
        {
            InitializeComponent();
            splitContainerPastWords.SplitterWidth = 1;

            m_Online = online;

            btnConfigToolTip.SetToolTip(btnConfiguration, Properties.Resources.TrayIcon_MenuItem_Configuration);
            btnPastWordsToolTip.SetToolTip(btnPastWords, Properties.Resources.ButtonPastWordsToolTip);
            btnPinToolTip.SetToolTip(btnPin, Properties.Resources.ButtonPinToolTip);
            btnCloseToolTip.SetToolTip(btnClose, Properties.Resources.ButtonCloseToolTip);
            comboBoxLanguageToolTip.SetToolTip(cbLanguage, Properties.Resources.ComboBoxLanguageToolTip);

            foreach (object key in Properties.Settings.Default.TranslateSites.Keys)
            {
                cbLanguage.Items.Add(key.ToString());
            }

            this.LoadPersonalSettings();
            this.GetTranslation(word);
        }

        private void GetTranslation(string word)
        {
            m_Word = word;
            if (!m_Online)
            {
            }
            else
            {
                lblWord.Visible = false;
                string address = String.Format(
                    (Properties.Settings.Default.TranslateSites[m_Language] as string[])[0],
                    m_Word
                );

                try
                {
                    wbWordInfo.Document.Write(Properties.Resources.WebBrowser_LoadingHTMLText);
                    wbWordInfo.Url = new Uri(address);
                }
                catch (Exception ex)
                {
                    if (ex is UriFormatException)
                    {
                        wbWordInfo.DocumentText = Properties.Resources.Error_InvalidUriFormat;
                    }
                    else
                    {
                        wbWordInfo.DocumentText = ex.ToString();
                    }
                }
            }
        }

        private void LoadPersonalSettings()
        {
            if (Properties.Settings.Default.WindowWidth != 0 && Properties.Settings.Default.WindowHeight != 0)
            {
                this.Width = Properties.Settings.Default.WindowWidth;
                this.Height = Properties.Settings.Default.WindowHeight;
            }
            
            m_Pinned = Properties.Settings.Default.WindowPinned;
            this.TopMost = m_Pinned;

            m_Language = Properties.Settings.Default.Language;
            splitContainerMain.Panel2Collapsed = !Properties.Settings.Default.PastWordsPanelExpanded;
            if (Properties.Settings.Default.PastWordsPanelExpandedWidth != 0)
            {
                splitContainerMain.SplitterDistance = splitContainerMain.Width - Properties.Settings.Default.PastWordsPanelExpandedWidth;
            }
            
            this.RedrawPastWordsButton();
            this.RedrawPinButton();
            this.UpdatePastWordsPanel();

            cbLanguage.SelectedItem = m_Language;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void WordInfoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void wbWordInfo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void WordInfoForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void WordInfoForm_Deactivate(object sender, EventArgs e)
        {
            if (!m_Pinned)
            {
                this.Close();
            }
        }
        
        private void WordInfoForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.Created)
            {
                Properties.Settings.Default.WindowWidth = this.Width;
                Properties.Settings.Default.WindowHeight = this.Height;
            }
        }

        private void WordInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Activate the configuration form if it's currently opened
            if (OverlayForm.g_ConfigurationForm != null && !OverlayForm.g_ConfigurationForm.IsDisposed)
            {
                OverlayForm.g_ConfigurationForm.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseDownPoint = new Point(e.X, e.Y);
        }

        private void pnlTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_MouseDownPoint.IsEmpty)
            {
                int x = this.Location.X + (e.X - m_MouseDownPoint.X);
                int y = this.Location.Y + (e.Y - m_MouseDownPoint.Y);
                this.Location = new Point(x, y);
            }
        }

        private void pnlTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseDownPoint = Point.Empty;
        }

        private void cbLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_Language = cbLanguage.SelectedItem as string;
            Properties.Settings.Default.Language = m_Language;
            this.GetTranslation(m_Word);
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            m_Pinned = !m_Pinned;
            Properties.Settings.Default.WindowPinned = m_Pinned;
            this.TopMost = m_Pinned;
            this.RedrawPinButton();
        }

        private void btnPastWords_Click(object sender, EventArgs e)
        {
            splitContainerMain.Panel2Collapsed = !splitContainerMain.Panel2Collapsed;
            Properties.Settings.Default.PastWordsPanelExpanded = !splitContainerMain.Panel2Collapsed;
            this.RedrawPastWordsButton();
            this.UpdatePastWordsPanel();
        }

        private void splitContainerMain_Panel2_SizeChanged(object sender, EventArgs e)
        {
            lblPastWords.Location = new Point((splitContainerMain.Panel2.Width - lblPastWords.Width) / 2, lblPastWords.Top);
        }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.Created)
            {
                Properties.Settings.Default.PastWordsPanelExpandedWidth = splitContainerMain.Width - splitContainerMain.SplitterDistance;
            }
        }

        private void splitContainerMain_MouseDown(object sender, MouseEventArgs e)
        {
            m_FocusedControl = this.GetFocused(this.Controls);
        }

        private void splitContainerMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_FocusedControl != null)
            {
                m_FocusedControl.Focus();
                m_FocusedControl = null;
            }
        }

        private void UpdatePastWordsPanel()
        {
            if (!splitContainerMain.Panel2Collapsed)
            {
                flowLayoutPanelPastWords.Controls.Clear();
                foreach (object key in Properties.Settings.Default.PastWords.Keys)
                {
                    string pastWord = key as string;
                    this.CreatePastWordControl(pastWord);
                }
            }
        }

        void pastWordButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button pastWordButton = sender as Button;
                this.GetTranslation(pastWordButton.Text);
            }
        }

        private void CreatePastWordControl(string word)
        {
            PastWordControl pastWordControl = new PastWordControl(word);
            pastWordControl.WordChanged += new EventHandler(PastWordControl_WordChanged);
            pastWordControl.WordDeleted += new EventHandler(PastWordControl_WordDeleted);

            flowLayoutPanelPastWords.Controls.Add(pastWordControl);
        }

        void PastWordControl_WordChanged(object sender, EventArgs e)
        {
            if (sender is PastWordControl)
            {
                PastWordControl pwc = sender as PastWordControl;
                string word = pwc.Word;
                this.GetTranslation(word);
            }
        }

        void PastWordControl_WordDeleted(object sender, EventArgs e)
        {
            if (sender is PastWordControl)
            {
                PastWordControl pastWordControl = sender as PastWordControl;
                flowLayoutPanelPastWords.Controls.Remove(pastWordControl);
                Properties.Settings.Default.PastWords.Remove(pastWordControl.Word.ToUpper());
            }
        }

        private Control GetFocused(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.Focused)
                {
                    // Return the focused control
                    return c;
                }
                else if (c.ContainsFocus)
                {
                    // If the focus is contained inside a control's children
                    // return the child
                    return GetFocused(c.Controls);
                }
            }
            // No control on the form has focus
            return null;
        }

        private void RedrawPastWordsButton()
        {
            btnPastWords.BackColor = splitContainerMain.Panel2Collapsed ? Color.Transparent : Color.Gainsboro;
        }

        private void RedrawPinButton()
        {
            btnPin.BackColor = m_Pinned ? Color.Gainsboro : Color.Transparent;
        }

        private void pnlTitleBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Gainsboro, 1), 
                new Point(pnlTitleBar.Left, pnlTitleBar.Bottom - 1), 
                new Point(pnlTitleBar.Right, pnlTitleBar.Bottom - 1));
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            OverlayForm.OpenConfigurationForm();
            if (m_Pinned)
            {
                this.Close();
            }
        }
    }
}
