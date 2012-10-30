namespace VisualDictionary
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.lblRestoreSettings = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.pbAddNewSite = new System.Windows.Forms.PictureBox();
            this.cbDestinationLanguage = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanelSites = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDivider1 = new System.Windows.Forms.Panel();
            this.lblSites = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cbSourceLanguage = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewSite)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerMain.Panel1.Controls.Add(this.lblRestoreSettings);
            this.splitContainerMain.Panel1.Controls.Add(this.btnRestore);
            this.splitContainerMain.Panel1MinSize = 21;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerMain.Panel2.Controls.Add(this.pbAddNewSite);
            this.splitContainerMain.Panel2.Controls.Add(this.cbDestinationLanguage);
            this.splitContainerMain.Panel2.Controls.Add(this.flowLayoutPanelSites);
            this.splitContainerMain.Panel2.Controls.Add(this.pnlDivider1);
            this.splitContainerMain.Panel2.Controls.Add(this.lblSites);
            this.splitContainerMain.Panel2.Controls.Add(this.lblLanguage);
            this.splitContainerMain.Panel2.Controls.Add(this.cbSourceLanguage);
            this.splitContainerMain.Size = new System.Drawing.Size(313, 277);
            this.splitContainerMain.SplitterWidth = 1;
            this.splitContainerMain.TabIndex = 4;
            // 
            // lblRestoreSettings
            // 
            this.lblRestoreSettings.AutoSize = true;
            this.lblRestoreSettings.Location = new System.Drawing.Point(17, 20);
            this.lblRestoreSettings.Name = "lblRestoreSettings";
            this.lblRestoreSettings.Size = new System.Drawing.Size(0, 13);
            this.lblRestoreSettings.TabIndex = 12;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(160, 15);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 13;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // pbAddNewSite
            // 
            this.pbAddNewSite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbAddNewSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAddNewSite.Image = global::VisualDictionary.Properties.Resources.plus_disabled;
            this.pbAddNewSite.Location = new System.Drawing.Point(174, 85);
            this.pbAddNewSite.Name = "pbAddNewSite";
            this.pbAddNewSite.Size = new System.Drawing.Size(16, 16);
            this.pbAddNewSite.TabIndex = 24;
            this.pbAddNewSite.TabStop = false;
            this.pbAddNewSite.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbAddNewSite_MouseClick);
            this.pbAddNewSite.MouseEnter += new System.EventHandler(this.pbAddNewSite_MouseEnter);
            this.pbAddNewSite.MouseLeave += new System.EventHandler(this.pbAddNewSite_MouseLeave);
            // 
            // cbDestinationLanguage
            // 
            this.cbDestinationLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestinationLanguage.FormattingEnabled = true;
            this.cbDestinationLanguage.Location = new System.Drawing.Point(146, 41);
            this.cbDestinationLanguage.Name = "cbDestinationLanguage";
            this.cbDestinationLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbDestinationLanguage.TabIndex = 10;
            this.cbDestinationLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbDestinationLanguage_SelectionChangeCommitted);
            // 
            // flowLayoutPanelSites
            // 
            this.flowLayoutPanelSites.AutoScroll = true;
            this.flowLayoutPanelSites.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelSites.Location = new System.Drawing.Point(17, 111);
            this.flowLayoutPanelSites.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelSites.Name = "flowLayoutPanelSites";
            this.flowLayoutPanelSites.Size = new System.Drawing.Size(250, 109);
            this.flowLayoutPanelSites.TabIndex = 9;
            this.flowLayoutPanelSites.WrapContents = false;
            // 
            // pnlDivider1
            // 
            this.pnlDivider1.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlDivider1.Location = new System.Drawing.Point(19, 74);
            this.pnlDivider1.Name = "pnlDivider1";
            this.pnlDivider1.Size = new System.Drawing.Size(200, 1);
            this.pnlDivider1.TabIndex = 6;
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(17, 87);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(0, 13);
            this.lblSites.TabIndex = 3;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(17, 19);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(0, 13);
            this.lblLanguage.TabIndex = 2;
            // 
            // cbSourceLanguage
            // 
            this.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceLanguage.FormattingEnabled = true;
            this.cbSourceLanguage.Location = new System.Drawing.Point(19, 41);
            this.cbSourceLanguage.Name = "cbSourceLanguage";
            this.cbSourceLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbSourceLanguage.TabIndex = 0;
            this.cbSourceLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbSourceLanguage_SelectionChangeCommitted);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(313, 277);
            this.Controls.Add(this.splitContainerMain);
            this.MaximizeBox = false;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewSite)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ComboBox cbSourceLanguage;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Panel pnlDivider1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSites;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblRestoreSettings;
        private System.Windows.Forms.ComboBox cbDestinationLanguage;
        private System.Windows.Forms.PictureBox pbAddNewSite;

    }
}