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
    public partial class ConfigurationManageLanguageForm : Form
    {
        public event SiteAddedEventHandler LanguageAdded;
        public event SiteAddedEventHandler LanguageDeleted;

        public ConfigurationManageLanguageForm()
        {
            InitializeComponent();

            lblAddNewLanguage.Text = Properties.Resources.Configuration_AddLanguage_Title;
            lblDeleteLanguage.Text = Properties.Resources.Configuration_Language_Delete;

            cbLanguageToDelete.Items.Add(String.Empty);
            List<string> languages = Common.GetAvailableLanguages();
            foreach (string item in languages)
            {
                cbLanguageToDelete.Items.Add(item);
            }

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
            if (this.ValidateValueToAdd())
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
            lblError.Visible = false;
            btnAdd.Enabled = (tbNewLanguage.Text.Trim().Length > 0);
        }

        private void tbNewLanguage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private void btnDeleteLanguage_Click(object sender, EventArgs e)
        {
            if (this.ValidateValueToDelete())
            {
                DialogResult result = MessageBox.Show(Control.FromHandle(this.Handle),
                    Properties.Resources.Configuration_DeleteLanguage_Confirmation,
                    Properties.Resources.Dialog_Confirmation,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string languageToDelete = cbLanguageToDelete.SelectedItem as string;
                    bool deleted = Common.DeleteLanguage(languageToDelete);
                    if (deleted)
                    {
                        Common.PromptInformation(String.Format(
                            Properties.Resources.Configuration_DeleteLanguage_Success, 
                            languageToDelete));

                        WordInfoForm currentLookupForm = OverlayForm.GetLookupForm();
                        if (currentLookupForm != null && currentLookupForm.Visible)
                        {
                            currentLookupForm.Hide();
                        }

                        this.LanguageDeleted(this, new SiteAddedEventArgs() { SiteURL = languageToDelete });

                        this.Close();
                    }
                    else
                    {
                        Common.PromptInformation(String.Format(
                            Properties.Resources.Configuration_DeleteLanguage_Failure,
                            languageToDelete));
                    }
                }
            }
        }

        private void cbLanguageToDelete_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblError.Visible = false;
            string selectedLanguage = cbLanguageToDelete.SelectedItem as string;
            btnDeleteLanguage.Enabled = !String.IsNullOrEmpty(selectedLanguage);
        }

        private bool ValidateValueToAdd()
        {
            return true;
        }

        private bool ValidateValueToDelete()
        {
            bool valid = true;

            string language = cbLanguageToDelete.SelectedItem as string;
            if (!String.IsNullOrEmpty(language))
            {
                string[] defaultLanguages = Enum.GetNames(typeof(TranslationLanguage));
                if (defaultLanguages.Contains(language))
                {
                    valid = false;
                    lblError.Visible = true;
                    lblError.Text = Properties.Resources.Configuration_DeleteLanguage_LabelError_DefaultLanguage;
                }
            }

            return valid;
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
