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
            this.btnAddNewSite = new System.Windows.Forms.Button();
            this.flowLayoutPanelSites = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDivider1 = new System.Windows.Forms.Panel();
            this.lblSites = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
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
            this.splitContainerMain.Panel2.Controls.Add(this.btnAddNewSite);
            this.splitContainerMain.Panel2.Controls.Add(this.flowLayoutPanelSites);
            this.splitContainerMain.Panel2.Controls.Add(this.pnlDivider1);
            this.splitContainerMain.Panel2.Controls.Add(this.lblSites);
            this.splitContainerMain.Panel2.Controls.Add(this.lblLanguage);
            this.splitContainerMain.Panel2.Controls.Add(this.cbLanguage);
            this.splitContainerMain.Size = new System.Drawing.Size(416, 214);
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
            // btnAddNewSite
            // 
            this.btnAddNewSite.AutoSize = true;
            this.btnAddNewSite.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnAddNewSite.Location = new System.Drawing.Point(160, 50);
            this.btnAddNewSite.Name = "btnAddNewSite";
            this.btnAddNewSite.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewSite.TabIndex = 8;
            this.btnAddNewSite.UseVisualStyleBackColor = true;
            this.btnAddNewSite.Click += new System.EventHandler(this.btnAddNewSite_Click);
            // 
            // flowLayoutPanelSites
            // 
            this.flowLayoutPanelSites.AutoScroll = true;
            this.flowLayoutPanelSites.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelSites.Location = new System.Drawing.Point(158, 82);
            this.flowLayoutPanelSites.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelSites.Name = "flowLayoutPanelSites";
            this.flowLayoutPanelSites.Size = new System.Drawing.Size(250, 74);
            this.flowLayoutPanelSites.TabIndex = 9;
            this.flowLayoutPanelSites.WrapContents = false;
            // 
            // pnlDivider1
            // 
            this.pnlDivider1.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlDivider1.Location = new System.Drawing.Point(149, 37);
            this.pnlDivider1.Name = "pnlDivider1";
            this.pnlDivider1.Size = new System.Drawing.Size(1, 110);
            this.pnlDivider1.TabIndex = 6;
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(158, 24);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(0, 13);
            this.lblSites.TabIndex = 3;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(17, 24);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(0, 13);
            this.lblLanguage.TabIndex = 2;
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(17, 51);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 0;
            this.cbLanguage.SelectedValueChanged += new System.EventHandler(this.cbLanguage_SelectedValueChanged);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(416, 214);
            this.Controls.Add(this.splitContainerMain);
            this.MaximizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Panel pnlDivider1;
        private System.Windows.Forms.Button btnAddNewSite;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSites;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblRestoreSettings;

    }
}