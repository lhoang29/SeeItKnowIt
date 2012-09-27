using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace VisualDictionary
{
    public partial class OverlayForm : Form
    {
        private Rectangle m_CaptureRectangle;
        private Point m_MouseDownPosition;
        private Color m_TransparencyKey = Color.Gray;
        private OrderedDictionary m_PastWords = null;
        private NotifyIcon m_TrayIcon = null;

        private static WordInfoForm g_WordInfoForm = null;

        // Register a hot key with Windows
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr windowHandle, int id, uint modifiers, uint virtualKey);

        // Unregister a certain hot key with Windows
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr windowHandle, int id);

        //Win32 message ID for the HotKey event
        private const int WM_HOTKEY = 0x0312;
        public static int MOD_ALT = 0x1;
        public static int MOD_CONTROL = 0x2;
        public static int MOD_SHIFT = 0x4;
        public static int MOD_WIN = 0x8;

        private const int HotKey_ActivateWindow = 1;

        public OverlayForm()
        {
            InitializeComponent();

            this.CreateTrayIcon();

            if (Properties.Settings.Default.PastWords == null)
            {
                Properties.Settings.Default.PastWords = new OrderedDictionary();
            }
            m_PastWords = Properties.Settings.Default.PastWords;

            ClickOnceHelper.AddShortcutToStartupGroup("Luong Hoang", "VisualDictionary");

            //Rectangle bounds = Screen.AllScreens.Select(x => x.Bounds).Aggregate(Rectangle.Union);
            //this.Left = bounds.Left;
            //this.Top = bounds.Top;
            //this.Height = bounds.Height;
            //this.Width = bounds.Width;
            //this.DoubleBuffered = true;
            //this.Cursor = Cursors.Cross;
            //this.Opacity = 0.6;
            //this.TransparencyKey = m_TransparencyKey;
        }

        private void CreateTrayIcon()
        {
            m_TrayIcon = new NotifyIcon();
            m_TrayIcon.Icon = Properties.Resources.pastwordsIcon;
            m_TrayIcon.BalloonTipText = "VisualDictionary is running";
            m_TrayIcon.Visible = true;
            ContextMenu trayIconContextMenu = new ContextMenu();
            trayIconContextMenu.MenuItems.Add("Exit", new EventHandler(this.TrayIcon_MenuItem_Exit_Clicked));
            m_TrayIcon.ContextMenu = trayIconContextMenu;
            if (Properties.Settings.Default.DisplayTrayIconBalloonTip)
            {
                m_TrayIcon.ShowBalloonTip(2000);
                Properties.Settings.Default.DisplayTrayIconBalloonTip = false;
            }
        }

        private void OverlayForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.MinimizeApplication();
            }
        }

        private void MinimizeApplication()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            m_CaptureRectangle = Rectangle.Empty;
        }

        private void OverlayForm_MouseMove(object sender, MouseEventArgs e)
        {
            // This makes sure that the left mouse button is pressed.
            if (e.Button == MouseButtons.Left)
            {
                int left = Math.Min(m_MouseDownPosition.X, e.X);
                int top = Math.Min(m_MouseDownPosition.Y, e.Y);
                int width = Math.Abs(m_MouseDownPosition.X - e.X);
                int height = Math.Abs(m_MouseDownPosition.Y - e.Y);
                // Draws the rectangle as the mouse moves
                m_CaptureRectangle = new Rectangle(left, top, width, height);
            }
            this.Invalidate();
        }

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawRectangle(pen, m_CaptureRectangle);
            }
            using (SolidBrush brush = new SolidBrush(m_TransparencyKey))
            {
                e.Graphics.FillRectangle(brush, m_CaptureRectangle);
            }
        }

        private void OverlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_MouseDownPosition = new Point(e.X, e.Y);
            }
        }

        private void OverlayForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && m_CaptureRectangle.Width > 0 && m_CaptureRectangle.Height > 0)
            {
                using (Bitmap snapshot = new Bitmap(m_CaptureRectangle.Width, m_CaptureRectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using (Graphics graphics = Graphics.FromImage(snapshot))
                    {
                        graphics.CopyFromScreen(m_CaptureRectangle.Left, m_CaptureRectangle.Top, 0, 0, m_CaptureRectangle.Size);

                        using (Bitmap enlargedSnapshot = ResizeBitmap(snapshot, 3 * snapshot.Width, 3 * snapshot.Height))
                        {
                            // TODO: Insert OCR code here
                        }
                    }
                }
                this.MinimizeApplication();
            }
        }

        private void OverlayForm_Deactivate(object sender, EventArgs e)
        {
            this.MinimizeApplication();
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }

        private void OverlayForm_Shown(object sender, EventArgs e)
        {
            IntPtr hWnd = this.Handle;
            if (!RegisterHotKey(hWnd, HotKey_ActivateWindow, (uint)MOD_WIN, (uint)'Z'))
            {
                OverlayForm.PromptError("Couldn't register the hotkey.");
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                // The WM_ACTIVATEAPP message occurs when the application
                // becomes the active application or becomes inactive.
                case WM_HOTKEY:
                    // get the keys.
                    uint key = (uint)(((int)m.LParam >> 16) & 0xFFFF);
                    int modifiers = ((int)m.LParam & 0xFFFF);
                    
                    HandleHotKeys(modifiers, key);

                    break;
            }
            base.WndProc(ref m);
        }

        public void HandleHotKeys(int modifiers, uint key)
        {
            if (key == (uint)Keys.A && modifiers == MOD_WIN)
            {
                //ShowApplication();
            }
            else if (key == (uint)Keys.Z && modifiers == MOD_WIN)
            {
                SimulateKeys.Keyboard.SimulateKeyStroke('c', ctrl: true);
                Thread.Sleep(100);

                // Need to perform a loop until abling to retrieve clipboard text as 
                // some other applications may still be using it and therefore locking it.
                string paste = "";
                bool success = false;
                while (!success)
                {
                    try
                    {
                        paste = Clipboard.GetText().Trim();
                        success = true;
                    }
                    catch (ExternalException) { }
                }

                // Remember words that have been looked up
                string capitalWord = paste.ToUpper();
                if (capitalWord.Length > 0) // Don't add empty word to history
                {
                    if (m_PastWords[capitalWord] != null)
                    {
                        m_PastWords[capitalWord] = (int)m_PastWords[capitalWord] + 1;
                    }
                    else
                    {
                        m_PastWords.Add(capitalWord, 1);
                    }
                }

                // Show the translation window regardless of whether a word was highlighted to be translated.
                g_WordInfoForm = new WordInfoForm(paste, true);
                g_WordInfoForm.Location = Cursor.Position;
                g_WordInfoForm.Show();
            }
        }

        private void ShowApplication()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void OverlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, HotKey_ActivateWindow);
        }

        private void TrayIcon_MenuItem_Exit_Clicked(object sender, EventArgs e)
        {
            m_TrayIcon.Visible = false;
            this.Close();
        }

        public static void PromptInformation(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void PromptError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OverlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PastWords = m_PastWords;
            Properties.Settings.Default.Save();
        }
    }

    public enum TranslationLanguage
    {
        English = 0,
        Vietnamese,
        Chinese
    }
}


