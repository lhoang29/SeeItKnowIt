using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SeeItKnowIt
{
    public static class ClickOnceHelper
    {
        public static void AddShortcutToStartupGroup(string publisherName, string productName)
        {
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                // The path to the key where Windows looks for startup applications
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

                //Path to launch shortcut
                string startPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), publisherName);
                startPath = Path.Combine(startPath, productName) + ".appref-ms";

                rkApp.SetValue(productName, "\"" + startPath + "\"");
            }
        }

        public static Version GetApplicationVersion()
        {
            Version version = null;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            return version;
        }
    }
}
