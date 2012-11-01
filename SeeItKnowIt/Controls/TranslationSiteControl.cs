using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SeeItKnowIt
{
    public partial class TranslationSiteControl : UserControl
    {
        public event EventHandler SiteActive;
        public event EventHandler SiteDeleted;

        [DllImport("User32.dll", EntryPoint = "GetDCEx")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgn, int flags);

        private string m_TranslateSiteAddress;
        private bool m_IsActive;
        private bool m_IsRequired;

        public bool IsRequired
        {
            get { return m_IsRequired; }
            set 
            { 
                m_IsRequired = value;
                if (m_IsRequired)
                {
                    btnDelete.Enabled = false;
                }
            }
        }

        public bool IsActive
        {
            get { return m_IsActive; }
            set 
            { 
                m_IsActive = value;
                if (m_IsActive)
                {
                    translateSiteAddressToolTip.SetToolTip(this, String.Empty);
                    this.BackColor = Color.Gainsboro;
                    lblTranslateSiteAddress.Cursor = Cursors.Default;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    translateSiteAddressToolTip.SetToolTip(this, Properties.Resources.Configuration_TranslateSite_MakeActive);
                    lblTranslateSiteAddress.Cursor = Cursors.Hand;
                    this.Cursor = Cursors.Hand;
                    this.BackColor = Color.Transparent;
                }
            }
        }

        public TranslationSiteControl(string site)
        {
            InitializeComponent();
            m_TranslateSiteAddress = site;

            this.UpdateContent();
        }

        public string TranslateSiteAddress
        {
            get { return m_TranslateSiteAddress; }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Control.FromHandle(this.Handle),
                Properties.Resources.Configuration_DeleteSite_Confirmation,
                Properties.Resources.Dialog_Confirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.SiteDeleted(this, e);                
            }
        }

        private void UpdateContent()
        {
            lblTranslateSiteAddress.Text = m_TranslateSiteAddress;
            translateSiteAddressToolTip.SetToolTip(this, Properties.Resources.Configuration_TranslateSite_MakeActive);
            translateSiteAddressToolTip.SetToolTip(lblTranslateSiteAddress, m_TranslateSiteAddress);
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
                Color borderColor = this.IsActive ? Color.Silver : Color.Gainsboro;
                Pen pen = new Pen(borderColor, 1);
                grfx.DrawRectangle(pen, rect);
            }
        }

        private void TranslationSiteControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetActive();
        }

        private void lblTranslateSiteAddress_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetActive();
        }

        private void SetActive()
        {
            if (!this.IsActive)
            {
                this.SiteActive(this, null);
                this.IsActive = true;
            }
        }

        private void TranslationSiteControl_MouseEnter(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                this.BackColor = Color.WhiteSmoke;
            }
        }

        private void TranslationSiteControl_MouseLeave(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    this.BackColor = Color.Transparent;
                }
            }
        }

        private void btnDelete_EnabledChanged(object sender, EventArgs e)
        {
            if (btnDelete.Enabled)
            {
                btnDelete.BackgroundImage = Properties.Resources.delete;
            }
            else
            {
                btnDelete.BackgroundImage = Properties.Resources.delete_disabled;
            }
        }
    }
}
