using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VisualDictionary
{
    public partial class PastWordControl : UserControl
    {
        private string m_Word;
        private Color m_BorderColor;

        [DllImport("User32.dll", EntryPoint = "GetDCEx")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgn, int flags);

        public string Word
        {
            get { return m_Word; }
        }

        public event EventHandler WordChanged;
        public event EventHandler WordDeleted;

        public PastWordControl(string word)
        {
            InitializeComponent();
            m_BorderColor = SystemColors.Window;
            m_Word = word;

            ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Delete", ContextMenu_Delete));
            this.ContextMenu = contextMenu;

            this.UpdateContent();
        }

        private void UpdateContent()
        {
            lblPastWord.Text = m_Word;
        }

        private void ContextMenu_Delete(object sender, EventArgs e)
        {
            this.WordDeleted(this, e);
        }

        private void lblPastWord_Click(object sender, EventArgs e)
        {
            this.WordChanged(this, e);
        }

        private void PastWordControl_MouseEnter(object sender, EventArgs e)
        {
            m_BorderColor = Color.Gainsboro;
            this.Invalidate();
        }

        private void PastWordControl_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                m_BorderColor = SystemColors.Window;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            IntPtr hWnd = this.Handle;
            IntPtr hRgn = IntPtr.Zero;
            IntPtr hdc = GetDCEx(hWnd, hRgn, 1027);

            using (Graphics grfx = Graphics.FromHdc(hdc))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Pen pen = new Pen(m_BorderColor, 1);
                grfx.DrawRectangle(pen, rect);
            }
        }

        private void lblPastWord_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.WordChanged(this, e);
            }
        }
    }
}
