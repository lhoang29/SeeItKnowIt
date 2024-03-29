﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net;

namespace SeeItKnowIt
{
    public partial class WordInfoForm : Form
    {
        private string m_Word;

        private bool m_AllowClose;

        private bool m_Online;
        private bool m_Pinned;
        private Point m_MouseDownPoint = Point.Empty;
        private Control m_FocusedControl = null;
        private TranslateDirection m_ActiveTranslateDirection;
        private GlobalMouseHandler m_GlobalMouseHandler;

        private ViewFlyoutControl m_ViewFlyoutControl = null;

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WS_CAPTION = 0x00C00000;

        private BindingSource m_WordGridSource = new BindingSource();

        public bool AllowClose
        {
            get { return m_AllowClose; }
            set { m_AllowClose = value; }
        }

        public TranslateDirection ActiveTranslateDirection
        {
            get { return m_ActiveTranslateDirection; }
            set 
            { 
                m_ActiveTranslateDirection = value;
                Properties.Settings.Default.TranslateDirection = (int)value;
                switch (m_ActiveTranslateDirection)
                {
                    case TranslateDirection.Left:
                        pbDirection.BackgroundImage = Properties.Resources.left;
                        break;
                    case TranslateDirection.Right:
                        pbDirection.BackgroundImage = Properties.Resources.right;
                        break;
                    case TranslateDirection.Both_Left:
                        pbDirection.BackgroundImage = Properties.Resources.both_left;
                        break;
                    case TranslateDirection.Both_Right:
                        pbDirection.BackgroundImage = Properties.Resources.both_right;
                        break;
                    default:
                        break;
                }
            }
        }

        public WordInfoForm(string word, bool online)
        {
            InitializeComponent();
            InitializeToolTip();

            m_AllowClose = false;

            this.Text = this.ProductName + ":" + word;

            m_GlobalMouseHandler = new GlobalMouseHandler();
            m_GlobalMouseHandler.GlobalMouseMove += new MouseEventHandler(GlobalMouseHandler_GlobalMouseMove);
            Application.AddMessageFilter(m_GlobalMouseHandler);

            m_ViewFlyoutControl = new ViewFlyoutControl(pbDirection.Location);
            m_ViewFlyoutControl.Visible = false;
            m_ViewFlyoutControl.HideRequest += new EventHandler(ViewFlyoutControl_HideRequest);
            m_ViewFlyoutControl.TranslateDirectionChanged += new TranslateDirectionChangedEventHandler(ViewFlyoutControl_TranslateDirectionChanged);
            this.Controls.Add(m_ViewFlyoutControl);

            Type dgvType = gridPastWords.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(gridPastWords, true, null);

            splitContainerPastWords.SplitterWidth = 1;
            splitContainerWebBrowser.SplitterWidth = 1;

            m_Online = online;

            this.Icon = Properties.Resources.app_icon_ico;

            this.LoadPersonalSettings();

            Common.InitializeLanguageComboBoxes(cbSourceLanguage, cbDestinationLanguage, addNewOption: true);

            if (cbSourceLanguage.SelectedItem == cbDestinationLanguage.SelectedItem)
            {
                this.AdjustTranslateDirectionForSameLanguages();
            }

            switch (ActiveTranslateDirection)
            {
                case TranslateDirection.Left:
                    this.SwapLanguageSelectionComboBoxes();
                    break;
                case TranslateDirection.Right:
                    break;
                case TranslateDirection.Both_Left:
                    this.SwapLanguageSelectionComboBoxes();
                    this.ShowSourceTranslation();
                    this.SwapWebBrowserIfNeeded(TranslateDirection.Left);
                    this.GetTranslation(word, useDestinationLanguage: false);
                    break;
                case TranslateDirection.Both_Right:
                    this.ShowSourceTranslation();
                    this.GetTranslation(word, useDestinationLanguage: false);
                    break;
                default:
                    break;
            }
            this.GetTranslation(word, useDestinationLanguage: true);
        }

        private bool AdjustTranslateDirectionForSameLanguages()
        {
            TranslateDirection oldDirection = ActiveTranslateDirection;
            if (ActiveTranslateDirection == TranslateDirection.Both_Left)
            {
                ActiveTranslateDirection = TranslateDirection.Left;
            }
            else if (ActiveTranslateDirection == TranslateDirection.Both_Right)
            {
                ActiveTranslateDirection = TranslateDirection.Right;
            }
            return (ActiveTranslateDirection != oldDirection);
        }

        public void Reopen(string word)
        {
            this.WindowState = FormWindowState.Normal;
            Common.InitializeLanguageComboBoxes(cbSourceLanguage, cbDestinationLanguage, addNewOption: true);

            string wordKey = word.ToUpper();
            if (Properties.Settings.Default.PastWords[wordKey] != null &&
                (int)Properties.Settings.Default.PastWords[wordKey] == 1)
            {
                m_WordGridSource.Add(new { Word = wordKey });
            }

            if (cbSourceLanguage.SelectedItem == cbDestinationLanguage.SelectedItem)
            {
                bool adjusted = this.AdjustTranslateDirectionForSameLanguages();
                if (adjusted)
                {
                    this.ViewFlyoutControl_TranslateDirectionChanged(null, new TranslateDirectionChangedEventArgs() { TranslateDirection = ActiveTranslateDirection });
                }
            }

            this.Show();
            this.Activate();
            this.Reload(word);
        }

        void GlobalMouseHandler_GlobalMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsDisposed)
            {
                Point cursor = m_ViewFlyoutControl.PointToClient(Cursor.Position);
                if (m_ViewFlyoutControl.Visible && !m_ViewFlyoutControl.DisplayRectangle.Contains(cursor))
                {
                    m_ViewFlyoutControl.Visible = false;
                }
            }
        }

        private void InitializeToolTip()
        {
            btnConfigToolTip.SetToolTip(btnConfiguration, Properties.Resources.TrayIcon_MenuItem_Configuration);
            btnPastWordsToolTip.SetToolTip(btnPastWords, Properties.Resources.ButtonPastWordsToolTip);
            btnPinToolTip.SetToolTip(btnPin, Properties.Resources.ButtonPinToolTip);
            btnCloseToolTip.SetToolTip(btnClose, Properties.Resources.ButtonCloseToolTip);
            comboBoxLanguageToolTip.SetToolTip(cbSourceLanguage, Properties.Resources.ComboBoxLanguageToolTip);
        }

        void ViewFlyoutControl_TranslateDirectionChanged(object sender, TranslateDirectionChangedEventArgs e)
        {
            if (e != null)
            {
                TranslateDirection currentDirection = this.GetActualTranslateDirection();
                ActiveTranslateDirection = e.TranslateDirection;
                switch (e.TranslateDirection)
                {
                    case TranslateDirection.Left:
                    {
                        this.SuspendLayout();
                        this.HideSourceTranslation();
                        this.ResumeLayout();

                        if (currentDirection != TranslateDirection.Left)
                        {
                            this.GetReverseTranslation();
                        }

                        break;
                    }
                    case TranslateDirection.Right:
                    {
                        this.SuspendLayout();
                        this.HideSourceTranslation();
                        this.ResumeLayout();

                        if (currentDirection != TranslateDirection.Right)
                        {
                            this.GetReverseTranslation();
                        }

                        break;
                    }
                    case TranslateDirection.Both_Left:
                    case TranslateDirection.Both_Right:
                    {
                        this.SuspendLayout();
                        this.ShowSourceTranslation();
                        this.SwapWebBrowserIfNeeded(currentDirection);
                        this.ResumeLayout();

                        this.GetTranslation(m_Word, useDestinationLanguage: false);

                        break;
                    }
                    default:
                        break;
                }
            }
        }

        private void SwapWebBrowserIfNeeded(TranslateDirection currentDirection)
        {
            if (currentDirection == TranslateDirection.Right)
            {
                if (splitContainerWebBrowser.Panel2.Contains(wbSourceTranslation))
                {
                    wbSourceTranslation.Parent = splitContainerWebBrowser.Panel1;
                    wbDestinationTranslation.Parent = splitContainerWebBrowser.Panel2;
                }
            }
            else if (currentDirection == TranslateDirection.Left)
            {
                if (splitContainerWebBrowser.Panel1.Contains(wbSourceTranslation))
                {
                    wbSourceTranslation.Parent = splitContainerWebBrowser.Panel2;
                    wbDestinationTranslation.Parent = splitContainerWebBrowser.Panel1;
                }
            }
        }

        private bool IsSourceTranslationVisible()
        {
            return wbSourceTranslation.Visible;
        }

        private void ShowSourceTranslation()
        {
            if (splitContainerWebBrowser.Panel1.Contains(wbSourceTranslation))
            {
                splitContainerWebBrowser.Panel1Collapsed = false;
            }
            else
            {
                splitContainerWebBrowser.Panel2Collapsed = false;
            }
        }

        private void HideSourceTranslation()
        {
            if (splitContainerWebBrowser.Panel1.Contains(wbSourceTranslation))
            {
                splitContainerWebBrowser.Panel1Collapsed = true;
            }
            else
            {
                splitContainerWebBrowser.Panel2Collapsed = true;
            }
        }

        private TranslateDirection GetActualTranslateDirection()
        {
            TranslateDirection currentDirection = (cbSourceLanguage.Location.X < cbDestinationLanguage.Location.X) ? TranslateDirection.Right : TranslateDirection.Left;
            return currentDirection;
        }

        void ViewFlyoutControl_HideRequest(object sender, EventArgs e)
        {
            m_ViewFlyoutControl.Visible = false;
        }

        private void GetReverseTranslation()
        {
            this.SuspendLayout();

            this.SwapLanguageSelectionComboBoxes();

            if (cbSourceLanguage.SelectedItem != cbDestinationLanguage.SelectedItem)
            {
                string sourceLanguage = cbSourceLanguage.SelectedItem as string;
                cbSourceLanguage.SelectedItem = cbDestinationLanguage.SelectedItem;
                cbDestinationLanguage.SelectedItem = sourceLanguage;

                Common.SourceLanguageSelectionChanged(cbSourceLanguage);
                Common.DestinationLanguageSelectionChanged(cbDestinationLanguage);
                this.GetTranslation(m_Word, useDestinationLanguage: true);
            }

            this.ResumeLayout();
        }

        private void SwapLanguageSelectionComboBoxes()
        {
            Point cbSourceLocation = cbSourceLanguage.Location;
            cbSourceLanguage.Location = cbDestinationLanguage.Location;
            cbDestinationLanguage.Location = cbSourceLocation;
        }

        private void GetTranslation(string word, bool useDestinationLanguage)
        {
            m_Word = word;
            if (!m_Online)
            {
            }
            else
            {
                lblWord.Visible = false;
                
                string sourceLanguage = Properties.Settings.Default.SourceLanguage;
                string destinationLanguage = useDestinationLanguage ? Properties.Settings.Default.DestinationLanguage : sourceLanguage;

                WebBrowser wbControl = useDestinationLanguage ? wbDestinationTranslation : wbSourceTranslation;
                wbControl.Document.OpenNew(false);

                bool forward = false;
                TranslateSitesInfo info = Common.GetTranslationSitesInfo(sourceLanguage, destinationLanguage, ref forward);

                List<string> translateSites = forward ? info.ForwardSites : info.BackwardSites;
                if (translateSites.Count > 0)
                {
                    string address = String.Format(translateSites[0], m_Word);

                    try
                    {
                        wbControl.Document.Write(String.Format(Properties.Resources.WebBrowser_LoadingHTMLText, address));
                        wbControl.Url = new Uri(address);
                    }
                    catch (Exception ex)
                    {
                        if (ex is UriFormatException)
                        {
                            wbControl.Document.Write(Properties.Resources.Error_InvalidUriFormat);
                        }
                        else
                        {
                            wbControl.Document.Write(ex.ToString());
                        }
                    }
                }
                else
                {
                    string siteMissingText = String.Format(Properties.Resources.WebBrowser_SiteURLMissingText,
                        sourceLanguage, destinationLanguage);

                    wbControl.Document.Write(siteMissingText);
                }
            }
        }

        private void LoadPersonalSettings()
        {
            if (Properties.Settings.Default.WindowWidth != 0 && Properties.Settings.Default.WindowHeight != 0)
            {
                this.Width = Properties.Settings.Default.WindowWidth;
                this.Height = Properties.Settings.Default.WindowHeight;
            }
            
            m_Pinned = Properties.Settings.Default.WindowPinned;
            this.TopMost = m_Pinned;

            splitContainerMain.Panel2Collapsed = !Properties.Settings.Default.PastWordsPanelExpanded;
            if (Properties.Settings.Default.PastWordsPanelExpandedWidth != 0)
            {
                splitContainerMain.SplitterDistance = splitContainerMain.Width - Properties.Settings.Default.PastWordsPanelExpandedWidth;
            }

            this.ActiveTranslateDirection = (TranslateDirection)Properties.Settings.Default.TranslateDirection;

            this.RedrawPastWordsButton();
            this.RedrawPinButton();
            this.UpdatePastWordsPanel();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                cp.Style &= ~WS_CAPTION; 
                return cp;
            }
        }

        private void WordInfoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }

        private void wbWordInfo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }

        private void WordInfoForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }
        
        private void WordInfoForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.Created)
            {
                Properties.Settings.Default.WindowWidth = this.Width;
                Properties.Settings.Default.WindowHeight = this.Height;
            }
        }

        private void WordInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                if (this.AllowClose)
                {
                    wbSourceTranslation.Dispose();
                    wbDestinationTranslation.Dispose();

                    // Activate the configuration form if it's currently opened
                    if (OverlayForm.g_ConfigurationForm != null && !OverlayForm.g_ConfigurationForm.IsDisposed)
                    {
                        OverlayForm.g_ConfigurationForm.Focus();
                    }
                }
                else
                {
                    e.Cancel = true;
                    this.Hide();
                }
            }
        }

        private void WordInfoForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                wbSourceTranslation.Url = new Uri("about:blank");
                wbDestinationTranslation.Url = new Uri("about:blank");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseDownPoint = new Point(e.X, e.Y);
        }

        private void pnlTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_MouseDownPoint.IsEmpty)
            {
                int x = this.Location.X + (e.X - m_MouseDownPoint.X);
                int y = this.Location.Y + (e.Y - m_MouseDownPoint.Y);
                this.Location = new Point(x, y);
            }
        }

        private void pnlTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseDownPoint = Point.Empty;
        }

        private void cbSourceLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((string)cbSourceLanguage.SelectedItem == Properties.Resources.General_Add_Delete)
            {
                cbSourceLanguage.SelectedItem = Properties.Settings.Default.SourceLanguage;
                this.OpenConfigurationForm(manageLanguage: true, useSourceLanguage: true);
            }
            else
            {
                Common.SourceLanguageSelectionChanged(cbSourceLanguage);

                // If already showing side-by-side translation
                if (this.IsInSideBySideMode())
                {
                    // If changed to same language translation in side-by-side mode then turn off side-by-side and 
                    // view in normal mode.
                    if (cbSourceLanguage.SelectedItem == cbDestinationLanguage.SelectedItem)
                    {
                        this.HideSourceTranslation();
                        ActiveTranslateDirection = this.GetActualTranslateDirection();
                    }
                    else
                    {
                        this.GetTranslation(m_Word, useDestinationLanguage: false);
                    }
                }
                this.GetTranslation(m_Word, useDestinationLanguage: true);
            }
        }

        private bool IsInSideBySideMode()
        {
            return this.IsSourceTranslationVisible();
        }

        private void cbDestinationLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((string)cbDestinationLanguage.SelectedItem == Properties.Resources.General_Add_Delete)
            {
                cbDestinationLanguage.SelectedItem = Properties.Settings.Default.DestinationLanguage;
                this.OpenConfigurationForm(manageLanguage: true, useSourceLanguage: false);
            }
            else
            {
                Common.DestinationLanguageSelectionChanged(cbDestinationLanguage);

                // If already showing side-by-side translation
                if (this.IsInSideBySideMode())
                {
                    // If changed to same language translation in side-by-side mode then turn off side-by-side and 
                    // view in normal mode.
                    if (cbSourceLanguage.SelectedItem == cbDestinationLanguage.SelectedItem)
                    {
                        this.HideSourceTranslation();
                        ActiveTranslateDirection = this.GetActualTranslateDirection();
                    }
                }
                this.GetTranslation(m_Word, useDestinationLanguage: true);
            }
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            m_Pinned = !m_Pinned;
            Properties.Settings.Default.WindowPinned = m_Pinned;
            this.TopMost = m_Pinned;
            this.RedrawPinButton();
        }

        private void btnPastWords_Click(object sender, EventArgs e)
        {
            splitContainerMain.Panel2Collapsed = !splitContainerMain.Panel2Collapsed;
            Properties.Settings.Default.PastWordsPanelExpanded = !splitContainerMain.Panel2Collapsed;
            this.RedrawPastWordsButton();
            this.UpdatePastWordsPanel();
        }

        private void splitContainerMain_Panel2_SizeChanged(object sender, EventArgs e)
        {
            lblPastWords.Location = new Point((splitContainerMain.Panel2.Width - lblPastWords.Width) / 2, lblPastWords.Top);
        }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.Created)
            {
                Properties.Settings.Default.PastWordsPanelExpandedWidth = splitContainerMain.Width - splitContainerMain.SplitterDistance;
            }
        }

        private void splitContainerMain_MouseDown(object sender, MouseEventArgs e)
        {
            m_FocusedControl = this.GetFocused(this.Controls);
        }

        private void splitContainerMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_FocusedControl != null)
            {
                m_FocusedControl.Focus();
                m_FocusedControl = null;
            }
        }

        private void UpdatePastWordsPanel()
        {
            if (!splitContainerMain.Panel2Collapsed && gridPastWords.Columns.Count < 2)
            {
                var bindingWords = from word in Properties.Settings.Default.PastWords.Keys.Cast<string>()
                                   select new {Word = word};

                m_WordGridSource.DataSource = bindingWords.ToList();

                gridPastWords.DataSource = m_WordGridSource;
                gridPastWords.Columns[0].DisplayIndex = 1;
                gridPastWords.Columns[1].DisplayIndex = 0;
            }
        }

        public void Reload(string word)
        {
            this.Text = this.ProductName + ":" + word;

            this.GetTranslation(word, useDestinationLanguage: true);
            if (this.IsInSideBySideMode())
            {
                this.GetTranslation(word, useDestinationLanguage: false);
            }
        }

        private Control GetFocused(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.Focused)
                {
                    // Return the focused control
                    return c;
                }
                else if (c.ContainsFocus)
                {
                    // If the focus is contained inside a control's children
                    // return the child
                    return GetFocused(c.Controls);
                }
            }
            // No control on the form has focus
            return null;
        }

        private void RedrawPastWordsButton()
        {
            btnPastWords.BackColor = splitContainerMain.Panel2Collapsed ? Color.Transparent : Color.Gainsboro;
        }

        private void RedrawPinButton()
        {
            btnPin.BackColor = m_Pinned ? Color.Gainsboro : Color.Transparent;
        }

        private void pnlTitleBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Gainsboro, 1), 
                new Point(pnlTitleBar.Left, pnlTitleBar.Bottom - 1), 
                new Point(pnlTitleBar.Right, pnlTitleBar.Bottom - 1));
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            this.OpenConfigurationForm(manageLanguage: false, useSourceLanguage: false);
        }

        private void OpenConfigurationForm(bool manageLanguage, bool useSourceLanguage)
        {
            OverlayForm.OpenConfigurationForm(manageLanguage, useSourceLanguage);
        }

        private void pbRight_MouseEnter(object sender, EventArgs e)
        {
            m_ViewFlyoutControl.ActiveDirection = m_ActiveTranslateDirection;
            m_ViewFlyoutControl.SideBySideEnabled = (cbSourceLanguage.SelectedItem != cbDestinationLanguage.SelectedItem);
            m_ViewFlyoutControl.Visible = true;
            m_ViewFlyoutControl.BringToFront();
        }

        private void gridPastWords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string deletedWord = gridPastWords.Rows[e.RowIndex].Cells[1].Value as string;
                Properties.Settings.Default.PastWords.Remove(deletedWord.ToUpper());
                gridPastWords.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void gridPastWords_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            gridPastWords.Rows[rowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            gridPastWords.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;

            if (e.ColumnIndex == 0)
            {
                gridPastWords.Cursor = Cursors.Hand;
            }
        }

        private void gridPastWords_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            gridPastWords.Rows[rowIndex].DefaultCellStyle.BackColor = SystemColors.Window;
            gridPastWords.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = SystemColors.Window;

            if (e.ColumnIndex == 0)
            {
                gridPastWords.Cursor = Cursors.Default;
            }
        }

        private void gridPastWords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string word = gridPastWords.Rows[e.RowIndex].Cells[1].Value as string;
                this.Reload(word);
            }
        }

        private void gridPastWords_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = gridPastWords.Columns[0].HeaderText;
        }
    }
}
