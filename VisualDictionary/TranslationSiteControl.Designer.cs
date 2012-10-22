﻿namespace VisualDictionary
{
    partial class TranslationSiteControl
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
            this.components = new System.ComponentModel.Container();
            this.lblTranslateSiteAddress = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.translateSiteAddressToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblTranslateSiteAddress
            // 
            this.lblTranslateSiteAddress.AutoEllipsis = true;
            this.lblTranslateSiteAddress.AutoSize = true;
            this.lblTranslateSiteAddress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTranslateSiteAddress.Location = new System.Drawing.Point(3, 4);
            this.lblTranslateSiteAddress.MaximumSize = new System.Drawing.Size(200, 16);
            this.lblTranslateSiteAddress.Name = "lblTranslateSiteAddress";
            this.lblTranslateSiteAddress.Size = new System.Drawing.Size(0, 13);
            this.lblTranslateSiteAddress.TabIndex = 0;
            this.lblTranslateSiteAddress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTranslateSiteAddress_MouseDown);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImage = global::VisualDictionary.Properties.Resources.delete;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(203, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(16, 16);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.EnabledChanged += new System.EventHandler(this.btnDelete_EnabledChanged);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // translateSiteAddressToolTip
            // 
            this.translateSiteAddressToolTip.BackColor = System.Drawing.SystemColors.Window;
            // 
            // TranslationSiteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblTranslateSiteAddress);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "TranslationSiteControl";
            this.Size = new System.Drawing.Size(220, 22);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TranslationSiteControl_MouseDown);
            this.MouseEnter += new System.EventHandler(this.TranslationSiteControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.TranslationSiteControl_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTranslateSiteAddress;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolTip translateSiteAddressToolTip;
    }
}
