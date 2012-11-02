using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SeeItKnowIt
{
    public partial class ConfigurationAddSiteForm : Form
    {
        public event SiteAddedEventHandler SiteAdded;
        private string m_Tutorial;

        [DllImport("User32.dll", EntryPoint = "GetDCEx")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgn, int flags);

        public ConfigurationAddSiteForm()
        {
            InitializeComponent();

            lblAddNewSite.Text = Properties.Resources.Configuration_AddSite_Title;
            this.SetWatermark(tbCalibrateSiteAddress, Properties.Resources.Configuration_AddSite_EditWatermark);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            IntPtr hWnd = this.Handle;
            IntPtr hRgn = IntPtr.Zero;
            IntPtr hdc = GetDCEx(hWnd, hRgn, 1027);

            using (Graphics grfx = Graphics.FromHdc(hdc))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Pen pen = new Pen(Color.Black, 1);
                grfx.DrawRectangle(pen, rect);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.ValidateValues())
            {
                string newSiteURL = tbCalibrateSiteAddress.Text;

                // Raise the SiteAdded event to let the parent form know the new URL
                SiteAddedEventArgs args = new SiteAddedEventArgs();
                args.SiteURL = newSiteURL;
                this.SiteAdded(this, args);

                this.Close();
            }
        }

        private void btnHelp_MouseLeave(object sender, EventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.tip_disabled;
        }

        private void btnHelp_MouseEnter(object sender, EventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.tip;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(m_Tutorial))
            {
                m_Tutorial = String.Format(Properties.Resources.Configuration_AddSite_Tutorial,
                    Properties.Settings.Default.SourceLanguage,
                    Properties.Settings.Default.DestinationLanguage);
                m_Tutorial = m_Tutorial.Replace("\\n", Environment.NewLine);
            }

            Common.PromptInformation(m_Tutorial);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbCalibrateSiteAddress_TextChanged(object sender, EventArgs e)
        {
            this.UpdateValidState();
        }
        
        private void tbCalibrateSiteAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private bool ValidateValues()
        {
            return this.ValidateURL();
        }

        private bool ValidateURL()
        {
            bool isValid = false;

            string newURL = tbCalibrateSiteAddress.Text;
            string errorMessage = String.Empty;
            if (newURL.Length > 0)
            {
                string testUrl = newURL.Replace("{0}", "word");
                isValid = Uri.IsWellFormedUriString(testUrl, UriKind.Absolute);
                if (isValid)
                {
                    if (!newURL.Contains("{0}"))
                    {
                        errorMessage = String.Format(Properties.Resources.Configuration_AddSite_LabelError_IncompatibleURL);
                        isValid = false;
                    }
                }
                else
                {
                    errorMessage = Properties.Resources.Configuration_AddSite_LabelError_InvalidURL;
                }
            }

            if (isValid == false)
            {
                lblError.Text = errorMessage;
                lblError.Visible = true;
            }
            return isValid;
        }

        private void UpdateValidState()
        {
            lblError.Visible = false;
            btnAdd.Enabled = (tbCalibrateSiteAddress.Text.Trim().Length > 0);
        }

        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private void SetWatermark(TextBox textBox, string watermarkText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermarkText);
        }
    }
}
