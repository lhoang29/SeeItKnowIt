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
    public partial class ViewFlyoutControl : UserControl
    {
        private TranslateDirection m_ActiveDirection;

        public TranslateDirection ActiveDirection
        {
            get { return m_ActiveDirection; }
            set { m_ActiveDirection = value; }
        }

        public event EventHandler HideRequest;
        public event TranslateDirectionChangedEventHandler TranslateDirectionChanged;

        public ViewFlyoutControl(Point overlayedButtonLocation, TranslateDirection direction)
        {
            InitializeComponent();

            int x = overlayedButtonLocation.X - this.Padding.Left;
            int y = overlayedButtonLocation.Y - this.Padding.Top;
            this.Location = new Point(x, y);
        }

        [DllImport("User32.dll", EntryPoint = "GetDCEx")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgn, int flags);
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            IntPtr hWnd = this.Handle;
            IntPtr hRgn = IntPtr.Zero;
            IntPtr hdc = GetDCEx(hWnd, hRgn, 1027);

            using (Graphics grfx = Graphics.FromHdc(hdc))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Pen pen = new Pen(Color.Black, 1);
                grfx.DrawRectangle(pen, rect);
            }
        }

        private void ViewFlyoutControl_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                this.HideRequest(this, e);
            }
        }

        private void pbRight_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (m_ActiveDirection != TranslateDirection.Right)
                {
                    TranslateDirectionChangedEventArgs tde = new TranslateDirectionChangedEventArgs();
                    tde.TranslateDirection = TranslateDirection.Right;
                    this.TranslateDirectionChanged(this, tde);
                }
                this.HideRequest(this, e);
            }
        }

        private void pbLeft_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (m_ActiveDirection != TranslateDirection.Left)
                {
                    TranslateDirectionChangedEventArgs tde = new TranslateDirectionChangedEventArgs();
                    tde.TranslateDirection = TranslateDirection.Left;
                    this.TranslateDirectionChanged(this, tde);
                }
                this.HideRequest(this, e);
            }
        }

        private void pbBoth_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (m_ActiveDirection != TranslateDirection.Both)
                {
                    TranslateDirectionChangedEventArgs tde = new TranslateDirectionChangedEventArgs();
                    tde.TranslateDirection = TranslateDirection.Both;
                    this.TranslateDirectionChanged(this, tde);
                }
                this.HideRequest(this, e);
            }
        }

        private void Rearrange(TranslateDirection topDirection)
        {
            switch (topDirection)
            {
                case TranslateDirection.Left:
                    this.MoveTop(pbLeft);
                    this.MoveMiddle(pbRight);
                    this.MoveBottom(pbBoth);
                    break;
                case TranslateDirection.Right:
                    this.MoveTop(pbRight);
                    this.MoveMiddle(pbLeft);
                    this.MoveBottom(pbBoth);
                    break;
                case TranslateDirection.Both:
                    this.MoveTop(pbBoth);
                    this.MoveMiddle(pbLeft);
                    this.MoveBottom(pbRight);
                    break;
                default:
                    break;
            }
        }

        private void MoveTop(PictureBox control)
        {
            control.Location = new Point(1, 1);
        }

        private void MoveMiddle(PictureBox control)
        {
            control.Location = new Point(1, 26);
        }

        private void MoveBottom(PictureBox control)
        {
            control.Location = new Point(1, 51);
        }

        private void ViewFlyoutControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                pbLeft.BackColor = Color.Transparent;
                pbRight.BackColor = Color.Transparent;
                pbBoth.BackColor = Color.Transparent;

                switch (m_ActiveDirection)
                {
                    case TranslateDirection.Left:
                        pbLeft.BackColor = Color.Gainsboro;
                        break;
                    case TranslateDirection.Right:
                        pbRight.BackColor = Color.Gainsboro;
                        break;
                    case TranslateDirection.Both:
                        pbBoth.BackColor = Color.Gainsboro;
                        break;
                    default:
                        break;
                }
                this.Rearrange(m_ActiveDirection);
            }
        }
    }
}
