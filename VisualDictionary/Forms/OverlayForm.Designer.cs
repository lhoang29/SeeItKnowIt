﻿namespace SeeItKnowIt
{
    partial class OverlayForm
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
            this.SuspendLayout();
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverlayForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.OverlayForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OverlayForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OverlayForm_FormClosed);
            this.Shown += new System.EventHandler(this.OverlayForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OverlayForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OverlayForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OverlayForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OverlayForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OverlayForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

