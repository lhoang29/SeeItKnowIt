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

namespace SeeItKnowIt
{
    public partial class OverlayForm : Form
    {
        private Rectangle m_CaptureRectangle;
        private Point m_MouseDownPosition;
        private Color m_TransparencyKey = Color.Gray;

        private NotifyIcon m_TrayIcon = null;

        private static WordInfoForm g_WordInfoForm = null;
        public static ConfigurationForm g_ConfigurationForm = null;

        private static uint[] g_PotentialHotKeys = { (uint)'A', (uint)'Z', (uint)'W', (uint)'K' };

        public static uint m_HotKey = 0;

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

            Common.InitializeDefaultSettings();

            ClickOnceHelper.AddShortcutToStartupGroup(this.CompanyName, this.ProductName);

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

        /// <summary>
        /// Create tray icon for the application.
        /// </summary>
        private void CreateTrayIcon()
        {
            m_TrayIcon = new NotifyIcon();
            m_TrayIcon.Icon = Icon.FromHandle(Properties.Resources.pastwords.GetHicon());
            m_TrayIcon.BalloonTipText = String.Format(Properties.Resources.TrayIcon_BalloonTipText, this.ProductName);
            m_TrayIcon.Visible = true;
            
            // Set the text to display on mouse hover over tray icon
            Version currentVersion = ClickOnceHelper.GetApplicationVersion();
            m_TrayIcon.Text = (currentVersion != null) ? 
                String.Format(Properties.Resources.TrayIcon_Text, currentVersion.ToString()) : 
                String.Empty;
            
            // Set the context menu when clicking on tray icon
            ContextMenu trayIconContextMenu = new ContextMenu();
            trayIconContextMenu.MenuItems.Add(Properties.Resources.TrayIcon_MenuItem_Configuration, new EventHandler(this.TrayIcon_MenuItem_Configuration_Clicked));
            trayIconContextMenu.MenuItems.Add(Properties.Resources.TrayIcon_MenuItem_Exit, new EventHandler(this.TrayIcon_MenuItem_Exit_Clicked));
            m_TrayIcon.ContextMenu = trayIconContextMenu;

            m_TrayIcon.MouseUp += new MouseEventHandler(TrayIcon_MouseUp);

            // Only show the balloon tooltip the first time the application runs
            if (Properties.Settings.Default.FirstUse)
            {
                m_TrayIcon.ShowBalloonTip(2000);
            }
        }

        void TrayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.ShowTranslationWindow(String.Empty);
            }
        }


        /// <summary>
        /// Handles KeyDown event for the form
        /// </summary>
        private void OverlayForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.MinimizeApplication();
            }
        }

        /// <summary>
        /// Minimizes the application, also sets the visibility to false.
        /// </summary>
        private void MinimizeApplication()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            m_CaptureRectangle = Rectangle.Empty;
        }

        /// <summary>
        /// Handles the MouseMove event.
        /// </summary>
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

        /// <summary>
        /// Handles the Paint event.
        /// </summary>
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

        /// <summary>
        /// Handles the MouseDown event.
        /// </summary>
        private void OverlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_MouseDownPosition = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// Handles the MouseUp event.
        /// </summary>
        private void OverlayForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && m_CaptureRectangle.Width > 0 && m_CaptureRectangle.Height > 0)
            {
                using (Bitmap snapshot = new Bitmap(m_CaptureRectangle.Width, m_CaptureRectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using (Graphics graphics = Graphics.FromImage(snapshot))
                    {
                        Point captureRectangleScreenCoordinates = this.PointToScreen(new Point(m_CaptureRectangle.Left, m_CaptureRectangle.Top));
                        graphics.CopyFromScreen(captureRectangleScreenCoordinates.X, captureRectangleScreenCoordinates.Y, 0, 0, m_CaptureRectangle.Size);

                        using (Bitmap enlargedSnapshot = ResizeBitmap(snapshot, 3 * snapshot.Width, 3 * snapshot.Height))
                        {
                            // TODO: Insert OCR code here
                        }
                    }
                }
                this.MinimizeApplication();
            }
        }

        /// <summary>
        /// Handles the Deactivate event.
        /// </summary>
        private void OverlayForm_Deactivate(object sender, EventArgs e)
        {
            this.MinimizeApplication();
        }

        /// <summary>
        /// Resizes a bitmap image to the specified size.
        /// </summary>
        /// <param name="sourceBMP">The source bitmap image.</param>
        /// <param name="width">The width of the resized bitmap image.</param>
        /// <param name="height">The height of the resized bitmap image.</param>
        /// <returns>The resized bitmap image.</returns>
        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }

        /// <summary>
        /// Handles the Shown event.
        /// </summary>
        private void OverlayForm_Shown(object sender, EventArgs e)
        {
            IntPtr hWnd = this.Handle;

            // Cycle through each potential hotkey combinations and attempts to register until successful
            foreach (uint hotKey in g_PotentialHotKeys)
            {
                if (RegisterHotKey(hWnd, HotKey_ActivateWindow, (uint)MOD_WIN, hotKey))
                {
                    m_HotKey = hotKey;
                    break;
                }
            }
            // No hotkey combination was available
            if (m_HotKey == 0)
            {
                Common.PromptError(Properties.Resources.Error_RegisterHotKey);
            }
            else if (Properties.Settings.Default.FirstUse) // Only display tutorial the first time the application runs
            {
                Common.PromptInformation(
                    String.Format(Properties.Resources.Application_WelcomeMessage, this.ProductName),
                    String.Format(Properties.Resources.Information_Tutorial, ((Char)m_HotKey).ToString())
                    );
            }
            Properties.Settings.Default.FirstUse = false;
        }

        /// <summary>
        /// Handles the WndProc procedure.
        /// </summary>
        /// <param name="m">The message.</param>
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
                    
                    this.HandleHotKeys(modifiers, key);

                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Handle hotkey activation for the application.
        /// </summary>
        /// <param name="modifiers">The modifier keys that were pressed.</param>
        /// <param name="key">The actual keys that were pressed.</param>
        public void HandleHotKeys(int modifiers, uint key)
        {
            if (m_HotKey != 0 && key == m_HotKey && modifiers == MOD_WIN)
            {
                SimulateKeys.Keyboard.SimulateKeyStroke('c', ctrl: true);
                Thread.Sleep(100);
                Application.DoEvents();

                // Need to perform a loop until abling to retrieve clipboard text as 
                // some other applications may still be using it and therefore locking it.
                string paste = "";
                bool success = false;
                if (Clipboard.ContainsText())
                {
                    while (!success)
                    {
                        try
                        {
                            paste = Clipboard.GetText().Trim();
                            success = true;
                        }
                        catch (ExternalException) { }
                    }
                }

                // Remember words that have been looked up
                string capitalWord = paste.ToUpper();
                if (capitalWord.Length > 0) // Don't add empty word to history
                {
                    if (Properties.Settings.Default.PastWords[capitalWord] != null)
                    {
                        Properties.Settings.Default.PastWords[capitalWord] = (int)Properties.Settings.Default.PastWords[capitalWord] + 1;
                    }
                    else
                    {
                        Properties.Settings.Default.PastWords.Add(capitalWord, 1);
                    }
                }

                // Show the translation window regardless of whether a word was highlighted to be translated.
                this.ShowTranslationWindow(paste);
            }
        }

        private void ShowTranslationWindow(string word)
        {
            if (g_WordInfoForm == null || g_WordInfoForm.IsDisposed)
            {
                g_WordInfoForm = new WordInfoForm(word, online: true);

                Rectangle bounds = Screen.FromPoint(Cursor.Position).Bounds;

                Point formLocation = new Point(
                    Math.Min(Cursor.Position.X, bounds.Right - g_WordInfoForm.Width),
                    Math.Min(Cursor.Position.Y, bounds.Bottom - g_WordInfoForm.Height));

                g_WordInfoForm.Location = formLocation;
                g_WordInfoForm.Show();
            }
            else
            {
                g_WordInfoForm.Reopen(word);
            }
        }

        /// <summary>
        /// Shows the application.
        /// </summary>
        private void ShowApplication()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        /// <summary>
        /// Handles the FormClosed event.
        /// </summary>
        private void OverlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, HotKey_ActivateWindow);
        }

        /// <summary>
        /// Handles the Configuration task in the context menu for the tray icon.
        /// </summary>
        private void TrayIcon_MenuItem_Configuration_Clicked(object sender, EventArgs e)
        {
            OverlayForm.OpenConfigurationForm(manageLanguage: false, useSourceLanguage: false);
        }

        public static void OpenConfigurationForm(bool manageLanguage, bool useSourceLanguage)
        {
            if (g_ConfigurationForm == null || g_ConfigurationForm.IsDisposed)
            {
                g_ConfigurationForm = new ConfigurationForm();
                g_ConfigurationForm.Show();
            }
            else
            {
                g_ConfigurationForm.Activate();
            }
            if (manageLanguage)
            {
                g_ConfigurationForm.AddDeleteLanguage(useSourceLanguage);
            }
        }

        /// <summary>
        /// Handles the Exit task in the context menu for the tray icon.
        /// </summary>
        private void TrayIcon_MenuItem_Exit_Clicked(object sender, EventArgs e)
        {
            m_TrayIcon.Visible = false;
            this.Close();
        }

        /// <summary>
        /// Handles the FormClosing event.
        /// </summary>
        private void OverlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (g_WordInfoForm != null && !g_WordInfoForm.IsDisposed)
            {
                g_WordInfoForm.AllowClose = true;
                g_WordInfoForm.Close();
            }
            Properties.Settings.Default.Save();
        }
    }
}


