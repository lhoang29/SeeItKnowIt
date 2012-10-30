using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualDictionary
{
    public partial class PastWordControl : UserControl
    {
        private string m_Word;

        public string Word
        {
            get { return m_Word; }
        }

        public event EventHandler WordChanged;
        public event EventHandler WordDeleted;

        public PastWordControl(string word)
        {
            InitializeComponent();
            m_Word = word;

            ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Delete", ContextMenu_Delete));
            this.ContextMenu = contextMenu;

            this.UpdateContent();
        }

        private void UpdateContent()
        {
            btnWord.Text = m_Word;
        }

        private void ContextMenu_Delete(object sender, EventArgs e)
        {
            this.WordDeleted(this, e);
        }

        private void btnWord_MouseEnter(object sender, EventArgs e)
        {
            btnWord.FlatAppearance.BorderColor = Color.LightGray;
        }

        private void btnWord_MouseLeave(object sender, EventArgs e)
        {
            btnWord.FlatAppearance.BorderColor = SystemColors.Window;
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            this.WordChanged(this, e);
        }
    }
}
