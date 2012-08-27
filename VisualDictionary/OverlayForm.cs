using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using ShortCutLibrary = IWshRuntimeLibrary;

namespace VisualDictionary
{
    public partial class OverlayForm : Form
    {
        private Rectangle m_CaptureRectangle;
        private Point m_MouseDownPosition;
        private Color m_TransparencyKey = Color.Gray;

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

        private static string g_SettingsFile = "Settings.dat";
        private static string g_ShortCutName = "VisualDictionary.lnk";
        public static SavedSetings g_PersonalSettings = new SavedSetings();

        private const int HotKey_ActivateWindow = 1;

        public OverlayForm()
        {
            InitializeComponent();

            string startupFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), g_ShortCutName);
            if (!File.Exists(startupFilePath))
            {
                this.CreateShortcut(System.Reflection.Assembly.GetExecutingAssembly().Location, startupFilePath);
            }

            //Rectangle bounds = Screen.AllScreens.Select(x => x.Bounds).Aggregate(Rectangle.Union);
            //this.Left = bounds.Left;
            //this.Top = bounds.Top;
            //this.Height = bounds.Height;
            //this.Width = bounds.Width;
            //this.DoubleBuffered = true;
            //this.Cursor = Cursors.Cross;
            //this.Opacity = 0.6;
            //this.TransparencyKey = m_TransparencyKey;
            this.LoadPersonalSettings(g_SettingsFile);
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

        private void OverlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SavePersonalSettings(g_SettingsFile);
        }

        private void LoadPersonalSettings(string file)
        {
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(File.Open(file, FileMode.Open)))
                {
                    string[] size = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    g_PersonalSettings.TranslateWindow_Width = Convert.ToInt32(size[0].Trim());
                    g_PersonalSettings.TranslateWindow_Height = Convert.ToInt32(size[1].Trim());
                    g_PersonalSettings.Language = (TranslationLanguage)Convert.ToInt32(sr.ReadLine().Trim());
                }
            }
            else
            {
                g_PersonalSettings.TranslateWindow_Width = 0;
                g_PersonalSettings.TranslateWindow_Height = 0;
                g_PersonalSettings.Language = TranslationLanguage.English;
            }
        }

        private void SavePersonalSettings(string file)
        {
            StreamWriter sw = null;
            if (File.Exists(file))
            {
                sw = new StreamWriter(File.Open(file, FileMode.Truncate));
            }
            else
            {
                sw = new StreamWriter(File.Create(file));
            }
            using (sw)
            {
                sw.WriteLine(g_PersonalSettings.TranslateWindow_Width + " " + g_PersonalSettings.TranslateWindow_Height);
                sw.WriteLine((int)g_PersonalSettings.Language);
            }
        }

        private void CreateShortcut(string fromPath, string toPath)
        {
            ShortCutLibrary.WshShell shell = new ShortCutLibrary.WshShell();
            ShortCutLibrary.IWshShortcut shortcut = (ShortCutLibrary.IWshShortcut)shell.CreateShortcut(toPath);
            shortcut.TargetPath = fromPath;
            shortcut.Save();
        }

        public static void PromptInformation(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void PromptError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public enum TranslationLanguage
    {
        English = 0,
        Vietnamese,
        Chinese
    }

    public class SavedSetings
    {
        public int TranslateWindow_Width;
        public int TranslateWindow_Height;
        public TranslationLanguage Language;
    }
}


