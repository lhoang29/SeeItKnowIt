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
    public partial class ConfigurationForm : Form
    {
        private ConfigurationManageLanguageForm m_ManageLanguageForm = null;

        private Keys m_TempHotkey_Modifiers = Keys.None;
        private Keys m_TempHotkey_MainKey = Keys.None;

        public ConfigurationForm()
        {
            InitializeComponent();

            this.MinimumSize = this.MaximumSize = this.Size;

            this.Icon = Icon.FromHandle(Properties.Resources.config_taskbar.GetHicon());

            string version = String.Empty;
#if DEBUG
            version = "Debug";
#else
            Version applicationVersion = ClickOnceHelper.GetApplicationVersion();
            version = (applicationVersion != null) ? applicationVersion.ToString() : version;
#endif
            lblVersion.Text = String.Format(Properties.Resources.Configuration_VersionInfo, this.ProductName, version);
            lblHotkeyCombination.Text = Properties.Resources.Configuration_HotkeyCombinationLabel;
            lblRestoreSettings.Text = Properties.Resources.Configuration_Label_RestoreSettings;
            lblTo.Text = Properties.Resources.Configuration_Label_To;
            btnRestore.Text = Properties.Resources.Configuration_RestoreText;
            lblLanguage.Text = Properties.Resources.Configuration_Label_Languages;
            lblSites.Text = Properties.Resources.Configuration_Label_TranslateSites;

            this.DisplayCurrentHotkey();

            // Populate the languages combo box
            Common.InitializeLanguageComboBoxes(cbSourceLanguage, cbDestinationLanguage, true);
            this.PopulateSiteControls();

            // Load the personalized location of the form
            if (Properties.Settings.Default.ConfigurationForm_Location != Point.Empty)
            {
                this.Location = Properties.Settings.Default.ConfigurationForm_Location;
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void DisplayCurrentHotkey()
        {
            tbHotkeyCombination.Text = Common.CreateHotkeyDisplayText();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateSiteControls()
        {
            flowLayoutPanelSites.Controls.Clear();
            string sourceLanguage = cbSourceLanguage.SelectedItem as string;
            string destinationLanguage = cbDestinationLanguage.SelectedItem as string;

            bool forward = false;
            TranslateSitesInfo info = Common.GetTranslationSitesInfo(sourceLanguage, destinationLanguage, ref forward);

            List<string> translateSites = forward ? info.ForwardSites : info.BackwardSites;
            List<bool> optionalSites = forward ? info.ForwardSitesOptional : info.BackwardSitesOptional;

            if (translateSites != null && optionalSites != null)
            {
                for (int iSite = 0; iSite < translateSites.Count; iSite++)
                {
                    TranslationSiteControl translationSiteControl = new TranslationSiteControl(translateSites[iSite]);
                    translationSiteControl.IsRequired = !optionalSites[iSite];
                    translationSiteControl.SiteDeleted += new EventHandler(TranslationSiteControl_SiteDeleted);
                    translationSiteControl.SiteActive += new EventHandler(TranslationSiteControl_SiteActive);
                    flowLayoutPanelSites.Controls.Add(translationSiteControl);
                }
            }

            // Mark the first site as the currently active site
            if (flowLayoutPanelSites.Controls.Count > 0)
            {
                TranslationSiteControl firstSite = (TranslationSiteControl)flowLayoutPanelSites.Controls[0];
                firstSite.IsActive = true;
            }
        }

        void TranslationSiteControl_SiteActive(object sender, EventArgs e)
        {
            TranslationSiteControl oldActiveSiteControl = null;
            TranslationSiteControl newActiveSiteControl = sender as TranslationSiteControl;

            // Mark the previously active control as inactive
            foreach (Control siteControl in flowLayoutPanelSites.Controls)
            {
                TranslationSiteControl currentSiteControl = (TranslationSiteControl)siteControl;
                if (currentSiteControl.IsActive)
                {
                    oldActiveSiteControl = currentSiteControl;
                    break;
                }
            }
            oldActiveSiteControl.IsActive = false;

            // Now store the new active site address by swapping it with the first entry in the saved list
            string sourceLanguage = cbSourceLanguage.SelectedItem as string;
            string destinationLanguage = cbDestinationLanguage.SelectedItem as string;

            bool forward = false;
            TranslateSitesInfo info = Common.GetTranslationSitesInfo(sourceLanguage, destinationLanguage, ref forward);

            List<string> translateSites = forward ? info.ForwardSites : info.BackwardSites;
            List<bool> optionalSites = forward ? info.ForwardSitesOptional : info.BackwardSitesOptional;

            for (int iSite = 0; iSite < translateSites.Count; iSite++)
            {
                if (translateSites[iSite] == newActiveSiteControl.TranslateSiteAddress)
                {
                    string oldActiveSiteAddress = translateSites[0];
                    translateSites[0] = translateSites[iSite];
                    translateSites[iSite] = oldActiveSiteAddress;

                    bool oldOptionalSite = optionalSites[0];
                    optionalSites[0] = optionalSites[iSite];
                    optionalSites[iSite] = oldOptionalSite;

                    break;
                }
            }
        }

        void TranslationSiteControl_SiteDeleted(object sender, EventArgs e)
        {
            TranslationSiteControl tsc = sender as TranslationSiteControl;

            // Delete the site from saved list
            string deletedSiteAddress = tsc.TranslateSiteAddress;
            string sourceLanguage = cbSourceLanguage.SelectedItem as string;
            string destinationLanguage = cbDestinationLanguage.SelectedItem as string;

            bool deleted = Common.DeleteTranslationSite(sourceLanguage, destinationLanguage, deletedSiteAddress);

            if (deleted)
            {
                // Remove the control and mark the new top site as active
                flowLayoutPanelSites.Controls.Remove(tsc);
                if (flowLayoutPanelSites.Controls.Count > 0)
                {
                    TranslationSiteControl newActiveSite = flowLayoutPanelSites.Controls[0] as TranslationSiteControl;
                    newActiveSite.IsActive = true;
                }
            }
        }

        private void btnAddSite_Click(object sender, EventArgs e)
        {
            ConfigurationAddSiteForm addSiteForm = new ConfigurationAddSiteForm();
            addSiteForm.SiteAdded += new SiteAddedEventHandler(ConfigurationAddSiteForm_SiteAdded);
            addSiteForm.ShowDialog(Control.FromHandle(this.Handle));
        }

        private void btnAddSite_MouseEnter(object sender, EventArgs e)
        {
            btnAddSite.Image = Properties.Resources.plus;
        }

        private void btnAddSite_MouseLeave(object sender, EventArgs e)
        {
            btnAddSite.Image = Properties.Resources.plus_disabled;
        }

        private void ConfigurationAddSiteForm_SiteAdded(object sender, SiteAddedEventArgs e)
        {
            if (e != null)
            {
                string newSiteURL = e.SiteURL;
                string sourceLanguage = cbSourceLanguage.SelectedItem as string;
                string destinationLanguage = cbDestinationLanguage.SelectedItem as string;

                bool added = Common.AddTranslationSite(sourceLanguage, destinationLanguage, newSiteURL);
                if (added)
                {
                    this.PopulateSiteControls();
                }
            }
        }

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ConfigurationForm_Location = this.Location;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Control.FromHandle(this.Handle),
                Properties.Resources.Configuration_RestoreSettings_Confirmation,
                Properties.Resources.Dialog_Confirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Common.InitializeDefaultSettings();
                bool success = Common.AssignHotkey();

                if (success)
                {
                    MessageBox.Show(Control.FromHandle(this.Handle),
                        Properties.Resources.Configuration_SuccessfulRestore,
                        Properties.Resources.Dialog_Success, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                else
                {
                    Common.PromptWarning(Properties.Resources.Configuration_RegisterHotKey_Failure);
                }

                this.Close();
            }
        }

        private void cbSourceLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool updateSites = true;

            if ((string)cbSourceLanguage.SelectedItem == Properties.Resources.General_Add_Delete)
            {
                cbSourceLanguage.SelectedItem = Properties.Settings.Default.SourceLanguage;
                updateSites = this.ManageLanguage(cbSourceLanguage, cbDestinationLanguage);
            }

            if (updateSites)
            {
                Common.SourceLanguageSelectionChanged(cbSourceLanguage);
                this.PopulateSiteControls();
            }
        }

        private void cbDestinationLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool updateSites = true;

            if ((string)cbDestinationLanguage.SelectedItem == Properties.Resources.General_Add_Delete)
            {
                cbDestinationLanguage.SelectedItem = Properties.Settings.Default.DestinationLanguage;
                updateSites = this.ManageLanguage(cbDestinationLanguage, cbSourceLanguage);
            }

            if (updateSites)
            {
                Common.DestinationLanguageSelectionChanged(cbDestinationLanguage);
                this.PopulateSiteControls();
            }
        }

        public void AddDeleteLanguage(bool useSourceLanguage)
        {
            bool updateSites = true;
            if (useSourceLanguage)
            {
                updateSites = this.ManageLanguage(cbSourceLanguage, cbDestinationLanguage);
            }
            else
            {
                updateSites = this.ManageLanguage(cbDestinationLanguage, cbSourceLanguage);
            }
            if (updateSites)
            {
                Common.DestinationLanguageSelectionChanged(useSourceLanguage ? cbSourceLanguage : cbDestinationLanguage);
                this.PopulateSiteControls();
            }
        }

        private bool ManageLanguage(ComboBox cbAddFrom, ComboBox cbCompanion)
        {
            bool added = true;
            string newLanguage = String.Empty;

            if (m_ManageLanguageForm != null)
            {
                m_ManageLanguageForm.Close();
            }

            m_ManageLanguageForm = new ConfigurationManageLanguageForm();
            m_ManageLanguageForm.LanguageAdded += (s, ee) => { newLanguage = ee.SiteURL; };
            m_ManageLanguageForm.LanguageDeleted += new SiteAddedEventHandler(ManageLanguageForm_LanguageDeleted);
            m_ManageLanguageForm.ShowDialog(Control.FromHandle(this.Handle));

            if (!String.IsNullOrEmpty(newLanguage))
            {
                if (!cbAddFrom.Items.Contains(newLanguage))
                {
                    cbAddFrom.Items.Add(newLanguage);
                }
                cbAddFrom.SelectedItem = newLanguage;

                if (!cbCompanion.Items.Contains(newLanguage))
                {
                    cbCompanion.Items.Add(newLanguage);
                }
                Common.PromptInformation(String.Format(
                    Properties.Resources.Configuration_AddLanguage_Success,
                    newLanguage));
            }
            else
            {
                added = false;
            }
            return added;
        }

        private void ManageLanguageForm_LanguageDeleted(object sender, SiteAddedEventArgs e)
        {
            string deletedLanguage = e.SiteURL;
            if (!String.IsNullOrEmpty(deletedLanguage))
            {
                string selectedSourceLanguage = cbSourceLanguage.SelectedItem as string;
                string selectedDestinationLanguage = cbDestinationLanguage.SelectedItem as string;

                cbSourceLanguage.Items.Remove(deletedLanguage);
                cbDestinationLanguage.Items.Remove(deletedLanguage);

                if (selectedSourceLanguage == deletedLanguage)
                {
                    cbSourceLanguage.SelectedItem = TranslationLanguage.English.ToString();
                }
                if (selectedDestinationLanguage == deletedLanguage)
                {
                    cbDestinationLanguage.SelectedItem = TranslationLanguage.English.ToString();
                }
                Common.SourceLanguageSelectionChanged(cbSourceLanguage);
                Common.DestinationLanguageSelectionChanged(cbDestinationLanguage);
                this.PopulateSiteControls();
            }
        }

        private void tbHotkeyCombination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void tbHotkeyCombination_KeyDown(object sender, KeyEventArgs e)
        {
            string hotkeyDisplay = String.Empty;

            uint keyValue = (uint)e.KeyValue;

            // Convert from numpad number to regular number
            if (keyValue >= (uint)Keys.NumPad0 && keyValue <= (uint)Keys.NumPad9)
            {
                keyValue = (keyValue - (uint)Keys.NumPad0 + (uint)Keys.D0);
            }

            // Only allow hotkey using letters from A-Z or 0-9 or F1-F12
            if ((keyValue >= (uint)Keys.A  && keyValue <= (uint)Keys.Z)   ||
                (keyValue >= (uint)Keys.D0 && keyValue <= (uint)Keys.D9)  ||
                (keyValue >= (uint)Keys.F1 && keyValue <= (uint)Keys.F12))
            {
                hotkeyDisplay = "Control + Alt + ";
                m_TempHotkey_Modifiers = Keys.Control | Keys.Alt;
                m_TempHotkey_MainKey = (Keys)keyValue;

                if (e.Alt && e.Control && e.Shift)
                {
                    hotkeyDisplay += "Shift + ";
                    m_TempHotkey_Modifiers |= Keys.Shift;
                }
                if (keyValue > (uint)Keys.Z)
                {
                    hotkeyDisplay += e.KeyCode.ToString();
                }
                else
                {
                    hotkeyDisplay += Convert.ToChar(keyValue);
                }
            }

            bool isValidHotkey = 
                (m_TempHotkey_MainKey != Keys.None) &&
                (m_TempHotkey_Modifiers != Keys.None) &&
                ((m_TempHotkey_MainKey != Properties.Settings.Default.HotkeyMainKey) || 
                (m_TempHotkey_Modifiers != Properties.Settings.Default.HotkeyModifiers)) &&
                (m_TempHotkey_MainKey != Keys.C);

            btnAssignHotkey.Enabled = isValidHotkey;

            if (!String.IsNullOrEmpty(hotkeyDisplay))
            {
                tbHotkeyCombination.Text = hotkeyDisplay;
            }

        }

        private void btnAssignHotkey_Click(object sender, EventArgs e)
        {
            if (m_TempHotkey_Modifiers != Keys.None && m_TempHotkey_MainKey != Keys.None)
            {
                Common.Hotkey_MainKey = m_TempHotkey_MainKey;
                Common.Hotkey_Modifiers = m_TempHotkey_Modifiers;
                
                Common.RemoveHotkey();
                
                if (!Common.AssignHotkey())
                {
                    Common.PromptWarning(Properties.Resources.Configuration_RegisterHotKey_Failure);
                }
                else
                {
                    Common.PromptInformation(Properties.Resources.Configuration_RegisterHotKey_Success);
                }
            }
        }
    }
}
