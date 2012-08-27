using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisualDictionary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName("VisualDictionary").Length > 1)
            {
                OverlayForm.PromptInformation("Dictionary is already running");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OverlayForm());
        }
    }
}
