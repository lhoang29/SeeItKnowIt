namespace VisualDictionary
{
    partial class ViewFlyoutControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbBoth = new System.Windows.Forms.PictureBox();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            this.pbRight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBoth
            // 
            this.pbBoth.BackgroundImage = global::VisualDictionary.Properties.Resources.both_right;
            this.pbBoth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbBoth.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbBoth.Location = new System.Drawing.Point(1, 51);
            this.pbBoth.Margin = new System.Windows.Forms.Padding(0);
            this.pbBoth.Name = "pbBoth";
            this.pbBoth.Size = new System.Drawing.Size(20, 20);
            this.pbBoth.TabIndex = 5;
            this.pbBoth.TabStop = false;
            this.pbBoth.EnabledChanged += new System.EventHandler(this.pbBoth_EnabledChanged);
            this.pbBoth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbBoth_MouseClick);
            this.pbBoth.MouseEnter += new System.EventHandler(this.pbBoth_MouseEnter);
            this.pbBoth.MouseLeave += new System.EventHandler(this.pbBoth_MouseLeave);
            // 
            // pbLeft
            // 
            this.pbLeft.BackgroundImage = global::VisualDictionary.Properties.Resources.left;
            this.pbLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbLeft.Location = new System.Drawing.Point(1, 26);
            this.pbLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(20, 20);
            this.pbLeft.TabIndex = 4;
            this.pbLeft.TabStop = false;
            this.pbLeft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbLeft_MouseClick);
            this.pbLeft.MouseEnter += new System.EventHandler(this.pbLeft_MouseEnter);
            this.pbLeft.MouseLeave += new System.EventHandler(this.pbLeft_MouseLeave);
            // 
            // pbRight
            // 
            this.pbRight.BackgroundImage = global::VisualDictionary.Properties.Resources.right;
            this.pbRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbRight.Location = new System.Drawing.Point(1, 1);
            this.pbRight.Margin = new System.Windows.Forms.Padding(0);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(20, 20);
            this.pbRight.TabIndex = 3;
            this.pbRight.TabStop = false;
            this.pbRight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbRight_MouseClick);
            this.pbRight.MouseEnter += new System.EventHandler(this.pbRight_MouseEnter);
            this.pbRight.MouseLeave += new System.EventHandler(this.pbRight_MouseLeave);
            // 
            // ViewFlyoutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pbBoth);
            this.Controls.Add(this.pbLeft);
            this.Controls.Add(this.pbRight);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ViewFlyoutControl";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(22, 72);
            this.VisibleChanged += new System.EventHandler(this.ViewFlyoutControl_VisibleChanged);
            this.MouseLeave += new System.EventHandler(this.ViewFlyoutControl_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pbBoth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBoth;
        private System.Windows.Forms.PictureBox pbLeft;
        private System.Windows.Forms.PictureBox pbRight;
    }
}
