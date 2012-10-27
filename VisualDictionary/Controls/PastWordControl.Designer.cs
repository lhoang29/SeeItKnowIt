namespace VisualDictionary
{
    partial class PastWordControl
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
            this.lblPastWord = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPastWord
            // 
            this.lblPastWord.AutoEllipsis = true;
            this.lblPastWord.AutoSize = true;
            this.lblPastWord.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastWord.Location = new System.Drawing.Point(5, 5);
            this.lblPastWord.Name = "lblPastWord";
            this.lblPastWord.Size = new System.Drawing.Size(0, 17);
            this.lblPastWord.TabIndex = 0;
            this.lblPastWord.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblPastWord_MouseClick);
            // 
            // PastWordControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblPastWord);
            this.Name = "PastWordControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(100, 27);
            this.MouseEnter += new System.EventHandler(this.PastWordControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.PastWordControl_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPastWord;
    }
}
