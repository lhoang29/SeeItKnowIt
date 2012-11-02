namespace SeeItKnowIt
{
    partial class ConfigurationManageLanguageForm
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
            this.tbNewLanguage = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAddNewLanguage = new System.Windows.Forms.Label();
            this.lblDeleteLanguage = new System.Windows.Forms.Label();
            this.cbLanguageToDelete = new System.Windows.Forms.ComboBox();
            this.btnDeleteLanguage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbNewLanguage
            // 
            this.tbNewLanguage.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNewLanguage.Location = new System.Drawing.Point(15, 40);
            this.tbNewLanguage.Name = "tbNewLanguage";
            this.tbNewLanguage.Size = new System.Drawing.Size(174, 24);
            this.tbNewLanguage.TabIndex = 1;
            this.tbNewLanguage.TextChanged += new System.EventHandler(this.tbNewLanguage_TextChanged);
            this.tbNewLanguage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNewLanguage_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Enabled = false;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(195, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(12, 130);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(195, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblAddNewLanguage
            // 
            this.lblAddNewLanguage.AutoSize = true;
            this.lblAddNewLanguage.Location = new System.Drawing.Point(12, 12);
            this.lblAddNewLanguage.Name = "lblAddNewLanguage";
            this.lblAddNewLanguage.Size = new System.Drawing.Size(0, 13);
            this.lblAddNewLanguage.TabIndex = 12;
            // 
            // lblDeleteLanguage
            // 
            this.lblDeleteLanguage.AutoSize = true;
            this.lblDeleteLanguage.Location = new System.Drawing.Point(12, 73);
            this.lblDeleteLanguage.Name = "lblDeleteLanguage";
            this.lblDeleteLanguage.Size = new System.Drawing.Size(0, 13);
            this.lblDeleteLanguage.TabIndex = 13;
            // 
            // cbLanguageToDelete
            // 
            this.cbLanguageToDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguageToDelete.FormattingEnabled = true;
            this.cbLanguageToDelete.Location = new System.Drawing.Point(15, 96);
            this.cbLanguageToDelete.Name = "cbLanguageToDelete";
            this.cbLanguageToDelete.Size = new System.Drawing.Size(174, 21);
            this.cbLanguageToDelete.TabIndex = 3;
            this.cbLanguageToDelete.SelectionChangeCommitted += new System.EventHandler(this.cbLanguageToDelete_SelectionChangeCommitted);
            // 
            // btnDeleteLanguage
            // 
            this.btnDeleteLanguage.AutoSize = true;
            this.btnDeleteLanguage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeleteLanguage.Enabled = false;
            this.btnDeleteLanguage.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteLanguage.Location = new System.Drawing.Point(195, 96);
            this.btnDeleteLanguage.Name = "btnDeleteLanguage";
            this.btnDeleteLanguage.Size = new System.Drawing.Size(50, 25);
            this.btnDeleteLanguage.TabIndex = 4;
            this.btnDeleteLanguage.Text = "Delete";
            this.btnDeleteLanguage.UseVisualStyleBackColor = true;
            this.btnDeleteLanguage.Click += new System.EventHandler(this.btnDeleteLanguage_Click);
            // 
            // ConfigurationAddLanguageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(254, 185);
            this.Controls.Add(this.btnDeleteLanguage);
            this.Controls.Add(this.cbLanguageToDelete);
            this.Controls.Add(this.lblDeleteLanguage);
            this.Controls.Add(this.lblAddNewLanguage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbNewLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfigurationAddLanguageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfigurationAddLanguageForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNewLanguage;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAddNewLanguage;
        private System.Windows.Forms.Label lblDeleteLanguage;
        private System.Windows.Forms.ComboBox cbLanguageToDelete;
        private System.Windows.Forms.Button btnDeleteLanguage;
    }
}