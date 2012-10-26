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
    public partial class ConfigurationForm : Form
    {   
        public ConfigurationForm()
        {
            InitializeComponent();

            this.Icon = Icon.FromHandle(Properties.Resources.pastwords.GetHicon());
            lblRestoreSettings.Text = Properties.Resources.Configuration_Label_RestoreSettings;
            btnRestore.Text = Properties.Resources.Configuration_RestoreText;

            // Populate the languages combo box
            Common.InitializeLanguageComboBoxes(cbSourceLanguage, cbDestinationLanguage);
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

            lblLanguage.Text = Properties.Resources.Configuration_Label_Languages;
            lblSites.Text = Properties.Resources.Configuration_Label_TranslateSites;
            btnAddNewSite.Text = Properties.Resources.Configuration_TranslateSite_AddNew;
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
            string[] translateSites = Common.GetTranslationSites(sourceLanguage, destinationLanguage);

            if (translateSites != null)
            {
                foreach (string site in translateSites)
                {
                    TranslationSiteControl translationSiteControl = new TranslationSiteControl(site);
                    translationSiteControl.SiteDeleted += new EventHandler(TranslationSiteControl_SiteDeleted);
                    translationSiteControl.SiteActive += new EventHandler(TranslationSiteControl_SiteActive);
                    flowLayoutPanelSites.Controls.Add(translationSiteControl);
                }

                // Mark the first site as the currently active site
                if (flowLayoutPanelSites.Controls.Count > 0)
                {
                    TranslationSiteControl firstSite = (TranslationSiteControl)flowLayoutPanelSites.Controls[0];
                    firstSite.IsActive = true;

                    // If only one site is present then make it required so it can't be deleted
                    if (flowLayoutPanelSites.Controls.Count == 1)
                    {
                        firstSite.IsRequired = true;
                    }
                }
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

            string[] translateSites = Common.GetTranslationSites(sourceLanguage, destinationLanguage);
            if (translateSites != null)
            {
                for (int iSite = 0; iSite < translateSites.Length; iSite++)
                {
                    if (translateSites[iSite] == newActiveSiteControl.TranslateSiteAddress)
                    {
                        string oldActiveSiteAddress = translateSites[0];
                        translateSites[0] = newActiveSiteControl.TranslateSiteAddress;
                        translateSites[iSite] = oldActiveSiteAddress;
                        break;
                    }
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

            string[] translateSites = Common.GetTranslationSites(sourceLanguage, destinationLanguage);
            if (translateSites != null)
            {
                string[] newTranslateSites = translateSites.Where(siteAddress => siteAddress != deletedSiteAddress).ToArray();
                Common.UpdateTranslationSites(sourceLanguage, destinationLanguage, newTranslateSites);
            }

            // Remove the control and mark the new top site as active
            flowLayoutPanelSites.Controls.Remove(tsc);
            if (flowLayoutPanelSites.Controls.Count > 0)
            {
                TranslationSiteControl newActiveSite = flowLayoutPanelSites.Controls[0] as TranslationSiteControl;
                newActiveSite.IsActive = true;

                // If only one site is present then make it required so it can't be deleted
                if (flowLayoutPanelSites.Controls.Count == 1)
                {
                    newActiveSite.IsRequired = true;
                }
            }
        }

        private void btnAddNewSite_Click(object sender, EventArgs e)
        {
            ConfigurationAddSiteForm addSiteForm = new ConfigurationAddSiteForm();
            addSiteForm.StartPosition = FormStartPosition.CenterParent;
            addSiteForm.SiteAdded += new SiteAddedEventHandler(ConfigurationAddSiteForm_SiteAdded);
            addSiteForm.ShowDialog(Control.FromHandle(this.Handle));
        }

        private void ConfigurationAddSiteForm_SiteAdded(object sender, SiteAddedEventArgs e)
        {
            if (e != null)
            {
                string newSiteURL = e.SiteURL;
                string sourceLanguage = cbSourceLanguage.SelectedItem as string;
                string destinationLanguage = cbDestinationLanguage.SelectedItem as string;

                string[] translateSites = Common.GetTranslationSites(sourceLanguage, destinationLanguage);
                if (translateSites != null && !translateSites.Contains(newSiteURL))
                {
                    string[] newTranslateSites = translateSites.Union(new string[] { newSiteURL }).ToArray();
                    Common.UpdateTranslationSites(sourceLanguage, destinationLanguage, newTranslateSites);
                    this.PopulateSiteControls();
                }
            }
        }

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ConfigurationForm_Location = this.Location;
            Properties.Settings.Default.Save();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Common.InitializeDefaultSettings();

            MessageBox.Show(Control.FromHandle(this.Handle), 
                Properties.Resources.Configuration_SuccessfulRestore, 
                Properties.Resources.Dialog_Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbSourceLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Common.SourceLanguageSelectionChanged(cbSourceLanguage, cbDestinationLanguage);
            this.PopulateSiteControls();
        }

        private void cbDestinationLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Common.DestinationLanguageSelectionChanged(cbDestinationLanguage);
            this.PopulateSiteControls();
        }
    }
}
