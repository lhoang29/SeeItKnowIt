namespace SeeItKnowIt
{
    partial class WordInfoForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordInfoForm));
            this.lblWord = new System.Windows.Forms.Label();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.cbDestinationLanguage = new System.Windows.Forms.ComboBox();
            this.pbDirection = new System.Windows.Forms.PictureBox();
            this.btnConfiguration = new System.Windows.Forms.Button();
            this.btnPastWords = new System.Windows.Forms.Button();
            this.btnPin = new System.Windows.Forms.Button();
            this.cbSourceLanguage = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerWebBrowser = new System.Windows.Forms.SplitContainer();
            this.wbSourceTranslation = new System.Windows.Forms.WebBrowser();
            this.wbDestinationTranslation = new System.Windows.Forms.WebBrowser();
            this.splitContainerPastWords = new System.Windows.Forms.SplitContainer();
            this.lblPastWords = new System.Windows.Forms.Label();
            this.flowLayoutPanelPastWords = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPastWordsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPinToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnCloseToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxLanguageToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnConfigToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerWebBrowser)).BeginInit();
            this.splitContainerWebBrowser.Panel1.SuspendLayout();
            this.splitContainerWebBrowser.Panel2.SuspendLayout();
            this.splitContainerWebBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPastWords)).BeginInit();
            this.splitContainerPastWords.Panel1.SuspendLayout();
            this.splitContainerPastWords.Panel2.SuspendLayout();
            this.splitContainerPastWords.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Location = new System.Drawing.Point(12, 9);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(0, 13);
            this.lblWord.TabIndex = 0;
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.SystemColors.Window;
            this.pnlTitleBar.Controls.Add(this.cbDestinationLanguage);
            this.pnlTitleBar.Controls.Add(this.pbDirection);
            this.pnlTitleBar.Controls.Add(this.btnConfiguration);
            this.pnlTitleBar.Controls.Add(this.btnPastWords);
            this.pnlTitleBar.Controls.Add(this.btnPin);
            this.pnlTitleBar.Controls.Add(this.cbSourceLanguage);
            this.pnlTitleBar.Controls.Add(this.btnClose);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(584, 21);
            this.pnlTitleBar.TabIndex = 2;
            this.pnlTitleBar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTitleBar_Paint);
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseDown);
            this.pnlTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseMove);
            this.pnlTitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseUp);
            // 
            // cbDestinationLanguage
            // 
            this.cbDestinationLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestinationLanguage.FormattingEnabled = true;
            this.cbDestinationLanguage.Location = new System.Drawing.Point(145, 0);
            this.cbDestinationLanguage.Name = "cbDestinationLanguage";
            this.cbDestinationLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbDestinationLanguage.TabIndex = 2;
            this.cbDestinationLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbDestinationLanguage_SelectionChangeCommitted);
            // 
            // pbDirection
            // 
            this.pbDirection.BackgroundImage = global::SeeItKnowIt.Properties.Resources.right;
            this.pbDirection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbDirection.Location = new System.Drawing.Point(123, 0);
            this.pbDirection.Margin = new System.Windows.Forms.Padding(0);
            this.pbDirection.Name = "pbDirection";
            this.pbDirection.Size = new System.Drawing.Size(20, 20);
            this.pbDirection.TabIndex = 25;
            this.pbDirection.TabStop = false;
            this.pbDirection.MouseEnter += new System.EventHandler(this.pbRight_MouseEnter);
            // 
            // btnConfiguration
            // 
            this.btnConfiguration.BackColor = System.Drawing.Color.Transparent;
            this.btnConfiguration.BackgroundImage = global::SeeItKnowIt.Properties.Resources.config;
            this.btnConfiguration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConfiguration.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfiguration.FlatAppearance.BorderSize = 0;
            this.btnConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguration.Location = new System.Drawing.Point(500, 0);
            this.btnConfiguration.Name = "btnConfiguration";
            this.btnConfiguration.Size = new System.Drawing.Size(21, 21);
            this.btnConfiguration.TabIndex = 21;
            this.btnConfiguration.UseVisualStyleBackColor = false;
            this.btnConfiguration.Click += new System.EventHandler(this.btnConfiguration_Click);
            // 
            // btnPastWords
            // 
            this.btnPastWords.BackColor = System.Drawing.Color.Transparent;
            this.btnPastWords.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPastWords.BackgroundImage")));
            this.btnPastWords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPastWords.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPastWords.FlatAppearance.BorderSize = 0;
            this.btnPastWords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPastWords.Location = new System.Drawing.Point(521, 0);
            this.btnPastWords.Name = "btnPastWords";
            this.btnPastWords.Size = new System.Drawing.Size(21, 21);
            this.btnPastWords.TabIndex = 22;
            this.btnPastWords.UseVisualStyleBackColor = false;
            this.btnPastWords.Click += new System.EventHandler(this.btnPastWords_Click);
            // 
            // btnPin
            // 
            this.btnPin.BackColor = System.Drawing.Color.Transparent;
            this.btnPin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPin.BackgroundImage")));
            this.btnPin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPin.FlatAppearance.BorderSize = 0;
            this.btnPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPin.Location = new System.Drawing.Point(542, 0);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(21, 21);
            this.btnPin.TabIndex = 23;
            this.btnPin.UseVisualStyleBackColor = false;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // cbSourceLanguage
            // 
            this.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceLanguage.FormattingEnabled = true;
            this.cbSourceLanguage.Location = new System.Drawing.Point(0, 0);
            this.cbSourceLanguage.Name = "cbSourceLanguage";
            this.cbSourceLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbSourceLanguage.TabIndex = 0;
            this.cbSourceLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbSourceLanguage_SelectionChangeCommitted);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(563, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 21);
            this.btnClose.TabIndex = 24;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 21);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerWebBrowser);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerPastWords);
            this.splitContainerMain.Panel2.SizeChanged += new System.EventHandler(this.splitContainerMain_Panel2_SizeChanged);
            this.splitContainerMain.Panel2MinSize = 100;
            this.splitContainerMain.Size = new System.Drawing.Size(584, 563);
            this.splitContainerMain.SplitterDistance = 480;
            this.splitContainerMain.SplitterWidth = 1;
            this.splitContainerMain.TabIndex = 5;
            this.splitContainerMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerMain_SplitterMoved);
            this.splitContainerMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainerMain_MouseDown);
            this.splitContainerMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitContainerMain_MouseUp);
            // 
            // splitContainerWebBrowser
            // 
            this.splitContainerWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.splitContainerWebBrowser.Name = "splitContainerWebBrowser";
            // 
            // splitContainerWebBrowser.Panel1
            // 
            this.splitContainerWebBrowser.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerWebBrowser.Panel1.Controls.Add(this.wbSourceTranslation);
            this.splitContainerWebBrowser.Panel1Collapsed = true;
            // 
            // splitContainerWebBrowser.Panel2
            // 
            this.splitContainerWebBrowser.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerWebBrowser.Panel2.Controls.Add(this.wbDestinationTranslation);
            this.splitContainerWebBrowser.Size = new System.Drawing.Size(480, 563);
            this.splitContainerWebBrowser.SplitterDistance = 230;
            this.splitContainerWebBrowser.SplitterWidth = 1;
            this.splitContainerWebBrowser.TabIndex = 0;
            // 
            // wbSourceTranslation
            // 
            this.wbSourceTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbSourceTranslation.Location = new System.Drawing.Point(0, 0);
            this.wbSourceTranslation.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbSourceTranslation.Name = "wbSourceTranslation";
            this.wbSourceTranslation.ScriptErrorsSuppressed = true;
            this.wbSourceTranslation.Size = new System.Drawing.Size(230, 100);
            this.wbSourceTranslation.TabIndex = 2;
            this.wbSourceTranslation.Url = new System.Uri("", System.UriKind.Relative);
            this.wbSourceTranslation.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wbWordInfo_PreviewKeyDown);
            // 
            // wbDestinationTranslation
            // 
            this.wbDestinationTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbDestinationTranslation.Location = new System.Drawing.Point(0, 0);
            this.wbDestinationTranslation.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDestinationTranslation.Name = "wbDestinationTranslation";
            this.wbDestinationTranslation.ScriptErrorsSuppressed = true;
            this.wbDestinationTranslation.Size = new System.Drawing.Size(480, 563);
            this.wbDestinationTranslation.TabIndex = 3;
            this.wbDestinationTranslation.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            this.wbDestinationTranslation.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wbWordInfo_PreviewKeyDown);
            // 
            // splitContainerPastWords
            // 
            this.splitContainerPastWords.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainerPastWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPastWords.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPastWords.IsSplitterFixed = true;
            this.splitContainerPastWords.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPastWords.Name = "splitContainerPastWords";
            this.splitContainerPastWords.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPastWords.Panel1
            // 
            this.splitContainerPastWords.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPastWords.Panel1.Controls.Add(this.lblPastWords);
            // 
            // splitContainerPastWords.Panel2
            // 
            this.splitContainerPastWords.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPastWords.Panel2.Controls.Add(this.flowLayoutPanelPastWords);
            this.splitContainerPastWords.Size = new System.Drawing.Size(103, 563);
            this.splitContainerPastWords.SplitterDistance = 25;
            this.splitContainerPastWords.SplitterWidth = 1;
            this.splitContainerPastWords.TabIndex = 0;
            // 
            // lblPastWords
            // 
            this.lblPastWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPastWords.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastWords.Location = new System.Drawing.Point(0, 0);
            this.lblPastWords.Name = "lblPastWords";
            this.lblPastWords.Size = new System.Drawing.Size(103, 25);
            this.lblPastWords.TabIndex = 0;
            this.lblPastWords.Text = "PAST WORDS";
            this.lblPastWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelPastWords
            // 
            this.flowLayoutPanelPastWords.AutoScroll = true;
            this.flowLayoutPanelPastWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPastWords.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPastWords.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelPastWords.Name = "flowLayoutPanelPastWords";
            this.flowLayoutPanelPastWords.Size = new System.Drawing.Size(103, 537);
            this.flowLayoutPanelPastWords.TabIndex = 0;
            this.flowLayoutPanelPastWords.WrapContents = false;
            // 
            // btnConfigToolTip
            // 
            this.btnConfigToolTip.BackColor = System.Drawing.SystemColors.Window;
            // 
            // WordInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 584);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this.lblWord);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(365, 35);
            this.Name = "WordInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.WordInfoForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WordInfoForm_FormClosing);
            this.Shown += new System.EventHandler(this.WordInfoForm_Shown);
            this.SizeChanged += new System.EventHandler(this.WordInfoForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WordInfoForm_KeyDown);
            this.pnlTitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDirection)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerWebBrowser.Panel1.ResumeLayout(false);
            this.splitContainerWebBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerWebBrowser)).EndInit();
            this.splitContainerWebBrowser.ResumeLayout(false);
            this.splitContainerPastWords.Panel1.ResumeLayout(false);
            this.splitContainerPastWords.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPastWords)).EndInit();
            this.splitContainerPastWords.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbSourceLanguage;
        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerPastWords;
        private System.Windows.Forms.Label lblPastWords;
        private System.Windows.Forms.Button btnPastWords;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPastWords;
        private System.Windows.Forms.ToolTip btnPastWordsToolTip;
        private System.Windows.Forms.ToolTip btnPinToolTip;
        private System.Windows.Forms.ToolTip btnCloseToolTip;
        private System.Windows.Forms.ToolTip comboBoxLanguageToolTip;
        private System.Windows.Forms.Button btnConfiguration;
        private System.Windows.Forms.ToolTip btnConfigToolTip;
        private System.Windows.Forms.PictureBox pbDirection;
        private System.Windows.Forms.ComboBox cbDestinationLanguage;
        private System.Windows.Forms.SplitContainer splitContainerWebBrowser;
        private System.Windows.Forms.WebBrowser wbSourceTranslation;
        private System.Windows.Forms.WebBrowser wbDestinationTranslation;
    }
}