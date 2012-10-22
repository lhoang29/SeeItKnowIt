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
        private Point m_MouseDownPoint = Point.Empty;
        
        public ConfigurationForm()
        {
            InitializeComponent();

            lblRestoreSettings.Text = Properties.Resources.Configuration_Label_RestoreSettings;
            btnRestore.Text = Properties.Resources.Configuration_RestoreText;

            // Populate the languages combo box
            foreach (object translateSite in Properties.Settings.Default.TranslateSites.Keys)
            {
                cbLanguage.Items.Add(translateSite.ToString());
            }

            // Select the default language
            cbLanguage.SelectedItem = Properties.Settings.Default.Language;

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

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x00C00000; // remove WS_CAPTION
                parms.ClassStyle |= 0x00020000; // add drop shadow;
                return parms;
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

        private void cbLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            this.PopulateSiteControls();
        }

        private void PopulateSiteControls()
        {
            flowLayoutPanelSites.Controls.Clear();
            string selectedValue = cbLanguage.SelectedItem as string;
            if (Properties.Settings.Default.TranslateSites.Contains(selectedValue))
            {
                string[] translateSites = Properties.Settings.Default.TranslateSites[selectedValue] as string[];
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
            string selectedValue = cbLanguage.SelectedItem as string;
            string[] translateSites = Properties.Settings.Default.TranslateSites[selectedValue] as string[];
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

        void TranslationSiteControl_SiteDeleted(object sender, EventArgs e)
        {
            TranslationSiteControl tsc = sender as TranslationSiteControl;

            // Delete the site from saved list
            string deletedSiteAddress = tsc.TranslateSiteAddress;
            string selectedValue = cbLanguage.SelectedItem as string;
            string[] translateSites = Properties.Settings.Default.TranslateSites[selectedValue] as string[];
            string[] newTranslateSites = translateSites.Where(siteAddress => siteAddress != deletedSiteAddress).ToArray();
            Properties.Settings.Default.TranslateSites[selectedValue] = newTranslateSites;

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
                string selectedValue = cbLanguage.SelectedItem as string;
                string[] translateSites = Properties.Settings.Default.TranslateSites[selectedValue] as string[];
                if (!translateSites.Contains(newSiteURL))
                {
                    string[] newTranslateSites = translateSites.Union(new string[] { newSiteURL }).ToArray();
                    Properties.Settings.Default.TranslateSites[selectedValue] = newTranslateSites;
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
    }
}
