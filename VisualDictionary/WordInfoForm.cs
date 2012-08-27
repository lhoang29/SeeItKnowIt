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

            //Region = new System.Drawing.Region(CreateRoundRectGraphicsPath(0, 0, Width, Height, m_CornerRadius));
        }

        private void GetTranslation(string word)
        {
            wbWordInfo.ScriptErrorsSuppressed = true;
            if (!m_Online)
            {
                lblWord.Text = m_Word;
                string translationFilePath = @"C:\Users\lhoang\Dropbox\Personal\Projects\VisualDictionary\VisualDictionary\Translations\" +
                    Enum.GetName(typeof(TranslationLanguage), m_Language) + @"\" + word + ".html";
                if (File.Exists(translationFilePath))
                {
                    using (StreamReader sr = new StreamReader(File.Open(translationFilePath, FileMode.Open)))
                    {
                        wbWordInfo.DocumentText = this.TrimScript(sr.ReadToEnd());
                    }
                }
                else
                {
                    wbWordInfo.DocumentText = "Word \"" + word + "\" not found in dictionary.";
                }
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
            SavedSetings personalSettings = OverlayForm.g_PersonalSettings;
            if (personalSettings.TranslateWindow_Width != 0 && personalSettings.TranslateWindow_Height != 0)
            {
                this.Width = personalSettings.TranslateWindow_Width;
                this.Height = personalSettings.TranslateWindow_Height;
            }
            m_Language = personalSettings.Language;

            cbLanguage.SelectedIndex = (int)m_Language;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.DrawPath(new Pen(Color.DarkGray), CreateRoundRectGraphicsPath(0, 0, Width - 1, Height - 1, m_CornerRadius));
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

        private GraphicsPath CreateRoundRectGraphicsPath(float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y); // Top
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90); // Top right corner
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2)); // Bottom right
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Bottom right corner
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height); // Bottom
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Bottom left corner
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius); // Left
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90); // Top left corner
            gp.CloseFigure();
            return gp;
        }

        private string TrimScript(string htmlDocText)
        {
            string bodyText = "";
            string trimJavascript = "<script type=\"text/javascript\"((.|\r|\n)*?)</script>";
            Regex regexTrimJs = new Regex(trimJavascript);
            bodyText = regexTrimJs.Replace(htmlDocText, "");

            string trimEmptyDivTags = @"<div([^<])*>(\s)*</div>";
            Regex regexTrimEmpties = new Regex(trimEmptyDivTags);
            bodyText = regexTrimEmpties.Replace(bodyText, "");
            return bodyText;
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

            SavedSetings settings = OverlayForm.g_PersonalSettings;
            settings.TranslateWindow_Width = this.Width;
            settings.TranslateWindow_Height = this.Height;
            settings.Language = m_Language;
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
            if (m_Pinned)
            {
                btnPin.BackColor = Color.White;
            }
            else
            {
                btnPin.BackColor = Color.Transparent;
            }
        }
    }
}
