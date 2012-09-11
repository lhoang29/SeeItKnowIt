using System;
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
        private WebRequest m_WebRequest = null;
        private WebResponse m_WebResponse = null;
        private Point m_MouseDownPoint = Point.Empty;
        private Control m_FocusedControl = null;

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        private TranslationLanguage m_Language = TranslationLanguage.English;

        public WordInfoForm(string word, bool online)
        {
            InitializeComponent();
            m_Word = word;
            m_Online = online;
            m_Pinned = false;
            cbLanguage.DataSource = Enum.GetValues(typeof(TranslationLanguage));

            this.LoadPersonalSettings();
            this.GetTranslation(word);
        }

        private void GetTranslation(string word)
        {
            wbWordInfo.ScriptErrorsSuppressed = true;
            if (!m_Online)
            {
            }
            else
            {
                lblWord.Visible = false;
                string address = "";
                switch (m_Language)
                {
                    case TranslationLanguage.Chinese:
                        address = "http://hk.dictionary.yahoo.com/dictionary?p=" + m_Word;
                        break;
                    case TranslationLanguage.Vietnamese:
                        address = "http://vdict.com/" + m_Word + ",1,0,0.html";
                        break;
                    case TranslationLanguage.English:
                        address = "http://dictionary.com/browse/" + m_Word;
                        break;
                }

                try
                {
                    m_WebRequest = WebRequest.Create(address);
                    if (m_WebRequest != null)
                    {
                        m_WebResponse = m_WebRequest.GetResponse();
                        wbWordInfo.DocumentStream = m_WebResponse.GetResponseStream();
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.ToString());
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
            m_Language = (TranslationLanguage)Properties.Settings.Default.Language;
            splitContainerMain.Panel2Collapsed = !Properties.Settings.Default.PastWordsPanelExpanded;
            splitContainerMain.SplitterDistance = splitContainerMain.Width - Properties.Settings.Default.PastWordsPanelExpandedWidth;
            
            this.RedrawPastWordsButton();
            this.RedrawPinButton();
            this.UpdatePastWordsPanel();

            cbLanguage.SelectedIndex = (int)m_Language;
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

        private void WordInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_WebRequest != null) m_WebRequest.Abort();
            if (m_WebResponse != null) m_WebResponse.Close();

            Properties.Settings.Default.WindowWidth = this.Width;
            Properties.Settings.Default.WindowHeight = this.Height;
            Properties.Settings.Default.WindowPinned = m_Pinned;
            Properties.Settings.Default.Language = (int)m_Language;
            Properties.Settings.Default.PastWordsPanelExpanded = !splitContainerMain.Panel2Collapsed;
            Properties.Settings.Default.PastWordsPanelExpandedWidth = splitContainerMain.Width - splitContainerMain.SplitterDistance;
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
            m_Language = (TranslationLanguage)cbLanguage.SelectedIndex;
            this.GetTranslation(m_Word);
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            m_Pinned = !m_Pinned;
            this.RedrawPinButton();
        }

        private void btnPastWords_Click(object sender, EventArgs e)
        {
            splitContainerMain.Panel2Collapsed = !splitContainerMain.Panel2Collapsed;
            this.RedrawPastWordsButton();
            this.UpdatePastWordsPanel();
        }

        private void splitContainerMain_Panel2_SizeChanged(object sender, EventArgs e)
        {
            lblPastWords.Location = new Point((splitContainerMain.Panel2.Width - lblPastWords.Width) / 2, lblPastWords.Top);
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
                    string word = key as string;
                    Button wordButton = new Button();
                    wordButton.AutoSize = true;
                    wordButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                    wordButton.FlatStyle = FlatStyle.Flat;
                    wordButton.BackColor = Color.Transparent;
                    wordButton.Text = word;
                    wordButton.FlatAppearance.BorderSize = 0;
                    flowLayoutPanelPastWords.Controls.Add(wordButton);
                }
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
            btnPastWords.BackColor = splitContainerMain.Panel2Collapsed ? Color.Transparent : Color.White;
        }

        private void RedrawPinButton()
        {
            btnPin.BackColor = m_Pinned ? Color.White : Color.Transparent;
        }
    }
}
