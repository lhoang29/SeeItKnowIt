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
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.btnAddNewSite = new System.Windows.Forms.Button();
            this.flowLayoutPanelSites = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDivider1 = new System.Windows.Forms.Panel();
            this.lblSites = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.pnlDivider2 = new System.Windows.Forms.Panel();
            this.btnRestore = new System.Windows.Forms.Button();
            this.lblRestoreSettings = new System.Windows.Forms.Label();
            this.pnlTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.SystemColors.Window;
            this.pnlTitleBar.Controls.Add(this.lblTitle);
            this.pnlTitleBar.Controls.Add(this.btnClose);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(416, 25);
            this.pnlTitleBar.TabIndex = 3;
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseDown);
            this.pnlTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseMove);
            this.pnlTitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(108, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "CONFIGURATION";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::VisualDictionary.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(395, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.splitContainerMain.Panel1.Controls.Add(this.pnlTitleBar);
            this.splitContainerMain.Panel1MinSize = 21;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerMain.Panel2.Controls.Add(this.btnRestore);
            this.splitContainerMain.Panel2.Controls.Add(this.lblRestoreSettings);
            this.splitContainerMain.Panel2.Controls.Add(this.pnlDivider2);
            this.splitContainerMain.Panel2.Controls.Add(this.btnAddNewSite);
            this.splitContainerMain.Panel2.Controls.Add(this.flowLayoutPanelSites);
            this.splitContainerMain.Panel2.Controls.Add(this.pnlDivider1);
            this.splitContainerMain.Panel2.Controls.Add(this.lblSites);
            this.splitContainerMain.Panel2.Controls.Add(this.lblLanguage);
            this.splitContainerMain.Panel2.Controls.Add(this.cbLanguage);
            this.splitContainerMain.Size = new System.Drawing.Size(416, 227);
            this.splitContainerMain.SplitterDistance = 25;
            this.splitContainerMain.SplitterWidth = 1;
            this.splitContainerMain.TabIndex = 4;
            // 
            // btnAddNewSite
            // 
            this.btnAddNewSite.AutoSize = true;
            this.btnAddNewSite.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnAddNewSite.Location = new System.Drawing.Point(155, 90);
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
            this.flowLayoutPanelSites.Location = new System.Drawing.Point(153, 122);
            this.flowLayoutPanelSites.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelSites.Name = "flowLayoutPanelSites";
            this.flowLayoutPanelSites.Size = new System.Drawing.Size(250, 74);
            this.flowLayoutPanelSites.TabIndex = 9;
            this.flowLayoutPanelSites.WrapContents = false;
            // 
            // pnlDivider1
            // 
            this.pnlDivider1.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlDivider1.Location = new System.Drawing.Point(144, 77);
            this.pnlDivider1.Name = "pnlDivider1";
            this.pnlDivider1.Size = new System.Drawing.Size(1, 110);
            this.pnlDivider1.TabIndex = 6;
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(153, 64);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(0, 13);
            this.lblSites.TabIndex = 3;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(12, 64);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(0, 13);
            this.lblLanguage.TabIndex = 2;
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(12, 91);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 0;
            this.cbLanguage.SelectedValueChanged += new System.EventHandler(this.cbLanguage_SelectedValueChanged);
            // 
            // pnlDivider2
            // 
            this.pnlDivider2.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlDivider2.Location = new System.Drawing.Point(15, 51);
            this.pnlDivider2.Name = "pnlDivider2";
            this.pnlDivider2.Size = new System.Drawing.Size(200, 1);
            this.pnlDivider2.TabIndex = 7;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(155, 13);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 13;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // lblRestoreSettings
            // 
            this.lblRestoreSettings.AutoSize = true;
            this.lblRestoreSettings.Location = new System.Drawing.Point(12, 18);
            this.lblRestoreSettings.Name = "lblRestoreSettings";
            this.lblRestoreSettings.Size = new System.Drawing.Size(0, 13);
            this.lblRestoreSettings.TabIndex = 12;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(416, 227);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Panel pnlDivider1;
        private System.Windows.Forms.Button btnAddNewSite;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSites;
        private System.Windows.Forms.Panel pnlDivider2;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblRestoreSettings;

    }
}