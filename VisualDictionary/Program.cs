using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

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
                Common.PromptInformation("Dictionary is already running");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetWebBrowserControlVersion();
            Application.Run(new OverlayForm());
        }

        /// <summary>
        /// Sets the specified IE version level that the WebBrowser control will use to display websites.
        /// </summary>
        private static void SetWebBrowserControlVersion()
        {
            Version installedIEVersion = (new WebBrowser()).Version;
            if (installedIEVersion != null)
            {
                string productName = Assembly.GetExecutingAssembly().GetName().Name;
                int ieVersionLevel = 7000; // Default to IE 7 mode
                if (installedIEVersion.Major == 8)
                {
                    ieVersionLevel = 8888;
                }
                else if (installedIEVersion.Major == 9)
                {
                    ieVersionLevel = 9999;
                }
                else if (installedIEVersion.Major == 10)
                {
                    ieVersionLevel = 10001;
                }
#if DEBUG
                string appName = productName + ".vshost.exe";
#else
                string appName = productName + ".exe";
#endif
                UpdateBrowserEmulationVersionRegistry("HKEY_LOCAL_MACHINE", appName, ieVersionLevel);
                UpdateBrowserEmulationVersionRegistry("HKEY_LOCAL_MACHINE", appName, ieVersionLevel);
                UpdateBrowserEmulationVersionRegistry("HKEY_CURRENT_USER", appName, ieVersionLevel);
                UpdateBrowserEmulationVersionRegistry("HKEY_CURRENT_USER", appName, ieVersionLevel);
            }
        }

        /// <summary>
        /// Updates the registry to enable web browser emulation using different specified IE versions.
        /// </summary>
        /// <param name="root">The root path of the registry key.</param>
        /// <param name="appName">The name of the application that needs to emulate browser version.</param>
        /// <param name="ieVersionLevel">
        /// The level of IE version to emulate. Following are available levels:
        /// 9999 (0x270F) - Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
        /// 9000 (0x2328) - Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
        /// 8888 (0x22B8) -Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.
        /// 8000 (0x1F40) - Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.
        /// 7000 (0x1B58) - Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.
        /// </param>
        private static void UpdateBrowserEmulationVersionRegistry(string root, string appName, int ieVersionLevel)
        {
            try
            {
                Registry.SetValue(root + @"\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", appName, ieVersionLevel);
            }
            catch (Exception)
            {
                // some config will hit access rights exceptions
                // this is why we try with both LOCAL_MACHINE and CURRENT_USER
            }
        }
    }
}
