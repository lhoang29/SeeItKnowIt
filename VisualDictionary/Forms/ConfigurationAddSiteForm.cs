using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VisualDictionary
{
    public partial class ConfigurationAddSiteForm : Form
    {
        private string m_CalibrateWord;

        public event SiteAddedEventHandler SiteAdded;

        [DllImport("User32.dll", EntryPoint = "GetDCEx")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgn, int flags);

        public ConfigurationAddSiteForm()
        {
            InitializeComponent();
            m_CalibrateWord = Common.CalibrateWords[(new Random()).Next(Common.CalibrateWords.Length)];
            string tutorial = String.Format(Properties.Resources.Configuration_AddSite_Tutorial, m_CalibrateWord);
            string[] tutorialSplits = tutorial.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (tutorialSplits.Length >= 4)
            {
                textBox1.Text = tutorialSplits[0];
                textBox2.Text = tutorialSplits[1];
                textBox3.Text = tutorialSplits[2];
                textBox4.Text = tutorialSplits[3];
            }
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
            if (this.ValidateURL())
            {
                // Modifies the URL to have the proper formatting syntax
                string newSiteURL = tbCalibrateSiteAddress.Text;
                newSiteURL = newSiteURL.Replace(m_CalibrateWord, "{0}");

                // Raise the SiteAdded event to let the parent form know the new URL
                SiteAddedEventArgs args = new SiteAddedEventArgs();
                args.SiteURL = newSiteURL;
                this.SiteAdded(this, args);

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbCalibrateSiteAddress_TextChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            btnAdd.Enabled = (tbCalibrateSiteAddress.Text.Length > 0);
        }

        private bool ValidateURL()
        {
            bool isValid = false;

            string newURL = tbCalibrateSiteAddress.Text;
            string errorMessage = String.Empty;
            if (newURL.Length > 0)
            {
                isValid = Uri.IsWellFormedUriString(newURL, UriKind.Absolute);
                if (isValid)
                {
                    isValid = newURL.Contains(m_CalibrateWord);
                    if (!isValid)
                    {
                        errorMessage = String.Format(Properties.Resources.Configuration_AddSite_LabelError_IncompatibleURL, m_CalibrateWord);
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
