namespace SeeItKnowIt
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
            this.btnWord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWord
            // 
            this.btnWord.AutoSize = true;
            this.btnWord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWord.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this.btnWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWord.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWord.Location = new System.Drawing.Point(0, 0);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(10, 10);
            this.btnWord.TabIndex = 0;
            this.btnWord.UseVisualStyleBackColor = true;
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            this.btnWord.MouseEnter += new System.EventHandler(this.btnWord_MouseEnter);
            this.btnWord.MouseLeave += new System.EventHandler(this.btnWord_MouseLeave);
            // 
            // PastWordControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnWord);
            this.Name = "PastWordControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWord;

    }
}
