namespace VisualDictionary
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
            this.lblWord = new System.Windows.Forms.Label();
            this.wbWordInfo = new System.Windows.Forms.WebBrowser();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.btnPastWords = new System.Windows.Forms.Button();
            this.btnPin = new System.Windows.Forms.Button();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerPastWords = new System.Windows.Forms.SplitContainer();
            this.lblPastWords = new System.Windows.Forms.Label();
            this.flowLayoutPanelPastWords = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPastWordsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPinToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnCloseToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxLanguageToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
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
            // wbWordInfo
            // 
            this.wbWordInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWordInfo.Location = new System.Drawing.Point(0, 0);
            this.wbWordInfo.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWordInfo.Name = "wbWordInfo";
            this.wbWordInfo.ScriptErrorsSuppressed = true;
            this.wbWordInfo.Size = new System.Drawing.Size(94, 240);
            this.wbWordInfo.TabIndex = 1;
            this.wbWordInfo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wbWordInfo_PreviewKeyDown);
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.SystemColors.Window;
            this.pnlTitleBar.Controls.Add(this.btnPastWords);
            this.pnlTitleBar.Controls.Add(this.btnPin);
            this.pnlTitleBar.Controls.Add(this.cbLanguage);
            this.pnlTitleBar.Controls.Add(this.btnClose);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(284, 21);
            this.pnlTitleBar.TabIndex = 2;
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseDown);
            this.pnlTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseMove);
            this.pnlTitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseUp);
            // 
            // btnPastWords
            // 
            this.btnPastWords.BackColor = System.Drawing.Color.Transparent;
            this.btnPastWords.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPastWords.FlatAppearance.BorderSize = 0;
            this.btnPastWords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPastWords.Image = global::VisualDictionary.Properties.Resources.pastwords;
            this.btnPastWords.Location = new System.Drawing.Point(221, 0);
            this.btnPastWords.Name = "btnPastWords";
            this.btnPastWords.Size = new System.Drawing.Size(21, 21);
            this.btnPastWords.TabIndex = 3;
            this.btnPastWords.UseVisualStyleBackColor = false;
            this.btnPastWords.Click += new System.EventHandler(this.btnPastWords_Click);
            // 
            // btnPin
            // 
            this.btnPin.BackColor = System.Drawing.Color.Transparent;
            this.btnPin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPin.FlatAppearance.BorderSize = 0;
            this.btnPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPin.Image = global::VisualDictionary.Properties.Resources.pin;
            this.btnPin.Location = new System.Drawing.Point(242, 0);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(21, 21);
            this.btnPin.TabIndex = 2;
            this.btnPin.UseVisualStyleBackColor = false;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // cbLanguage
            // 
            this.cbLanguage.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(0, 0);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 1;
            this.cbLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbLanguage_SelectionChangeCommitted);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::VisualDictionary.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(263, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 21);
            this.btnClose.TabIndex = 0;
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
            this.splitContainerMain.Panel1.Controls.Add(this.wbWordInfo);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerPastWords);
            this.splitContainerMain.Panel2.SizeChanged += new System.EventHandler(this.splitContainerMain_Panel2_SizeChanged);
            this.splitContainerMain.Panel2MinSize = 100;
            this.splitContainerMain.Size = new System.Drawing.Size(284, 240);
            this.splitContainerMain.SplitterDistance = 94;
            this.splitContainerMain.TabIndex = 3;
            this.splitContainerMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerMain_SplitterMoved);
            this.splitContainerMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainerMain_MouseDown);
            this.splitContainerMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitContainerMain_MouseUp);
            // 
            // splitContainerPastWords
            // 
            this.splitContainerPastWords.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPastWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPastWords.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPastWords.IsSplitterFixed = true;
            this.splitContainerPastWords.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPastWords.Name = "splitContainerPastWords";
            this.splitContainerPastWords.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPastWords.Panel1
            // 
            this.splitContainerPastWords.Panel1.Controls.Add(this.lblPastWords);
            // 
            // splitContainerPastWords.Panel2
            // 
            this.splitContainerPastWords.Panel2.Controls.Add(this.flowLayoutPanelPastWords);
            this.splitContainerPastWords.Size = new System.Drawing.Size(186, 240);
            this.splitContainerPastWords.SplitterDistance = 31;
            this.splitContainerPastWords.TabIndex = 0;
            // 
            // lblPastWords
            // 
            this.lblPastWords.AutoSize = true;
            this.lblPastWords.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastWords.Location = new System.Drawing.Point(0, 5);
            this.lblPastWords.Name = "lblPastWords";
            this.lblPastWords.Size = new System.Drawing.Size(77, 17);
            this.lblPastWords.TabIndex = 0;
            this.lblPastWords.Text = "Past Words";
            // 
            // flowLayoutPanelPastWords
            // 
            this.flowLayoutPanelPastWords.AutoScroll = true;
            this.flowLayoutPanelPastWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPastWords.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPastWords.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelPastWords.Name = "flowLayoutPanelPastWords";
            this.flowLayoutPanelPastWords.Size = new System.Drawing.Size(186, 205);
            this.flowLayoutPanelPastWords.TabIndex = 0;
            this.flowLayoutPanelPastWords.WrapContents = false;
            // 
            // WordInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this.lblWord);
            this.MinimumSize = new System.Drawing.Size(200, 35);
            this.Name = "WordInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.WordInfoForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WordInfoForm_FormClosing);
            this.Shown += new System.EventHandler(this.WordInfoForm_Shown);
            this.SizeChanged += new System.EventHandler(this.WordInfoForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WordInfoForm_KeyDown);
            this.pnlTitleBar.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerPastWords.Panel1.ResumeLayout(false);
            this.splitContainerPastWords.Panel1.PerformLayout();
            this.splitContainerPastWords.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPastWords)).EndInit();
            this.splitContainerPastWords.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.WebBrowser wbWordInfo;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbLanguage;
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
    }
}