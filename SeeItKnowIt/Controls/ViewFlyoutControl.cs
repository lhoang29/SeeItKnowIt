using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SeeItKnowIt
{
    public partial class ViewFlyoutControl : UserControl
    {
        private bool m_SideBySideEnabled;
        private TranslateDirection m_ActiveDirection;

        public bool SideBySideEnabled
        {
            get { return m_SideBySideEnabled; }
            set 
            {
                pbBoth.Enabled = m_SideBySideEnabled = value;
            }
        }

        public TranslateDirection ActiveDirection
        {
            get { return m_ActiveDirection; }
            set 
            { 
                m_ActiveDirection = value;
                this.UpdateImageBoth();
            }
        }

        public event EventHandler HideRequest;
        public event TranslateDirectionChangedEventHandler TranslateDirectionChanged;

        public ViewFlyoutControl(Point overlayedButtonLocation)
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
                if (m_ActiveDirection == TranslateDirection.Left || m_ActiveDirection == TranslateDirection.Right)
                {
                    TranslateDirectionChangedEventArgs tde = new TranslateDirectionChangedEventArgs();
                    tde.TranslateDirection = (m_ActiveDirection == TranslateDirection.Left) ? TranslateDirection.Both_Left : TranslateDirection.Both_Right;
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
                case TranslateDirection.Both_Left:
                case TranslateDirection.Both_Right:
                    this.MoveTop(pbBoth);
                    this.MoveMiddle(pbRight);
                    this.MoveBottom(pbLeft);
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
                    case TranslateDirection.Both_Left:
                    case TranslateDirection.Both_Right:
                        pbBoth.BackColor = Color.Gainsboro;
                        break;
                    default:
                        break;
                }
                this.Rearrange(m_ActiveDirection);
            }
        }

        private void pbRight_MouseEnter(object sender, EventArgs e)
        {
            pbRight.BackColor = Color.Gainsboro;
        }

        private void pbRight_MouseLeave(object sender, EventArgs e)
        {
            pbRight.BackColor = Color.Transparent;
        }

        private void pbLeft_MouseEnter(object sender, EventArgs e)
        {
            pbLeft.BackColor = Color.Gainsboro;
        }

        private void pbLeft_MouseLeave(object sender, EventArgs e)
        {
            pbLeft.BackColor = Color.Transparent;
        }

        private void pbBoth_MouseEnter(object sender, EventArgs e)
        {
            pbBoth.BackColor = Color.Gainsboro;
        }

        private void pbBoth_MouseLeave(object sender, EventArgs e)
        {
            pbBoth.BackColor = Color.Transparent;
        }

        private void pbBoth_EnabledChanged(object sender, EventArgs e)
        {
            UpdateImageBoth();
        }

        private void UpdateImageBoth()
        {
            if (pbBoth.Enabled)
            {
                switch (m_ActiveDirection)
                {
                    case TranslateDirection.Left:
                    case TranslateDirection.Both_Left:
                        pbBoth.BackgroundImage = Properties.Resources.both_left;
                        break;
                    case TranslateDirection.Right:
                    case TranslateDirection.Both_Right:
                        pbBoth.BackgroundImage = Properties.Resources.both_right;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (m_ActiveDirection)
                {
                    case TranslateDirection.Left:
                    case TranslateDirection.Both_Left:
                        pbBoth.BackgroundImage = Properties.Resources.both_left_disabled;
                        break;
                    case TranslateDirection.Right:
                    case TranslateDirection.Both_Right:
                        pbBoth.BackgroundImage = Properties.Resources.both_right_disabled;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
