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
    public partial class ConfigurationAddLanguageForm : Form
    {
        public event SiteAddedEventHandler LanguageAdded;

        public ConfigurationAddLanguageForm()
        {
            InitializeComponent();

            lblAddNewLanguage.Text = Properties.Resources.Configuration_AddLanguage_Title;
            this.SetWatermark(tbNewLanguage, Properties.Resources.Configuration_AddLanguage_EditWatermark);
        }

        [DllImport("User32.dll", EntryPoint = "GetDCEx")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgn, int flags);

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
                string newLanguage = tbNewLanguage.Text;

                // Raise the LanguageAdded event to let the parent form know the new language
                SiteAddedEventArgs args = new SiteAddedEventArgs();
                args.SiteURL = newLanguage;

                this.LanguageAdded(this, args);

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbNewLanguage_TextChanged(object sender, EventArgs e)
        {
            this.UpdateValidState();
        }

        private void tbNewLanguage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private bool ValidateValues()
        {
            return true;
        }

        private void UpdateValidState()
        {
            lblError.Visible = false;
            btnAdd.Enabled = (tbNewLanguage.Text.Trim().Length > 0);
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
