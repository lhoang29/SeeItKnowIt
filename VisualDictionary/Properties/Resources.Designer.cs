﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisualDictionary.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VisualDictionary.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to {0}.
        /// </summary>
        internal static string Application_WelcomeMessage {
            get {
                return ResourceManager.GetString("Application_WelcomeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Close.
        /// </summary>
        internal static string ButtonCloseToolTip {
            get {
                return ResourceManager.GetString("ButtonCloseToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show Past Words.
        /// </summary>
        internal static string ButtonPastWordsToolTip {
            get {
                return ResourceManager.GetString("ButtonPastWordsToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pin This Window.
        /// </summary>
        internal static string ButtonPinToolTip {
            get {
                return ResourceManager.GetString("ButtonPinToolTip", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap close {
            get {
                object obj = ResourceManager.GetObject("close", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change the translation language.
        /// </summary>
        internal static string ComboBoxLanguageToolTip {
            get {
                return ResourceManager.GetString("ComboBoxLanguageToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string Dialog_Error {
            get {
                return ResourceManager.GetString("Dialog_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Information.
        /// </summary>
        internal static string Dialog_Information {
            get {
                return ResourceManager.GetString("Dialog_Information", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t register keyboard shortcut activation.
        /// </summary>
        internal static string Error_RegisterHotKey {
            get {
                return ResourceManager.GetString("Error_RegisterHotKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Highlight any word then press Windows + {0} to translate.
        /// </summary>
        internal static string Information_Tutorial {
            get {
                return ResourceManager.GetString("Information_Tutorial", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap pastwords {
            get {
                object obj = ResourceManager.GetObject("pastwords", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Icon pastwordsIcon {
            get {
                object obj = ResourceManager.GetObject("pastwordsIcon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap pin {
            get {
                object obj = ResourceManager.GetObject("pin", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is running.
        /// </summary>
        internal static string TrayIcon_BalloonTipText {
            get {
                return ResourceManager.GetString("TrayIcon_BalloonTipText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exit.
        /// </summary>
        internal static string TrayIcon_MenuItem_Exit {
            get {
                return ResourceManager.GetString("TrayIcon_MenuItem_Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Version: {0}.
        /// </summary>
        internal static string TrayIcon_Text {
            get {
                return ResourceManager.GetString("TrayIcon_Text", resourceCulture);
            }
        }
    }
}
