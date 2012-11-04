using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeeItKnowIt
{
    /* Icons are from http://www.gentleface.com/free_icon_set.html */

    /// <summary>
    /// An enumeration of available translation languages.
    /// </summary>
    public enum TranslationLanguage
    {
        English = 0,
        Chinese,
        French,
        Hindi,
        Japanese,
        Portuguese,
        Spanish,
        Vietnamese,
        Wikipedia,
        UrbanDictionary
    }

    public enum TranslateDirection
    { 
        Left = 0,
        Right,
        Both_Left,
        Both_Right
    }

    [Serializable]
    [System.Configuration.SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
    public class TranslateSitesInfo
    {
        private List<string> m_ForwardSites;
        private List<bool> m_ForwardSitesOptional;

        private List<string> m_BackwardSites;
        private List<bool> m_BackwardSitesOptional;

        public List<string> ForwardSites
        {
            get { return m_ForwardSites; }
            set { m_ForwardSites = value; }
        }

        public List<bool> ForwardSitesOptional
        {
            get { return m_ForwardSitesOptional; }
            set { m_ForwardSitesOptional = value; }
        }

        public List<string> BackwardSites
        {
            get { return m_BackwardSites; }
            set { m_BackwardSites = value; }
        }

        public List<bool> BackwardSitesOptional
        {
            get { return m_BackwardSitesOptional; }
            set { m_BackwardSitesOptional = value; }
        }

        public TranslateSitesInfo()
        {
            m_ForwardSites = new List<string>();
            m_ForwardSitesOptional = new List<bool>();
            m_BackwardSites = new List<string>();
            m_BackwardSitesOptional = new List<bool>();
        }
    }

    public class Common
    {
        /// <summary>
        /// List of translation sites for each language
        /// </summary>
        public static Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>[] DefaultTranslationSites = 
        { 
            // English - English translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.English, 
                new string[]
                {
                    "http://dictionary.com/browse/{0}",
                    "http://v2.vdict.com/{0},7,0,0.html"
#if DEBUG
                    ,"http://www.whatismybrowser.com/"
#endif
                },
                null
            ),
            // English - Chinese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Chinese, 
                new string[]
                {
                    "http://hk.dictionary.yahoo.com/dictionary?p={0}",
                    "http://www.chinesedic.com/en/{0}"
                }, 
                new string[]
                {
                    "http://hk.dictionary.yahoo.com/dictionary?p={0}"
                }
            ),
            // English - Hindi translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Hindi, 
                new string[]
                {
                    "http://www.shabdkosh.com/translate/{0}/{0}_meaning_in_Hindi_English"
                }, 
                null
            ),
            // English - Japanese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Japanese, 
                new string[]
                {
                    "http://jisho.org/words?jap=&eng={0}&dict=edict"
                }, 
                new string[]
                {
                    "http://jisho.org/words?jap={0}&eng=&dict=edict"
                }
            ),
            // English - Portuguese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Portuguese, 
                new string[]
                {
                    "http://en.bab.la/dictionary/english-portuguese/{0}"
                }, 
                new string[]
                {
                    "http://en.bab.la/dictionary/portuguese-english/{0}"
                }
            ),
            // English - Spanish translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Spanish, 
                new string[]
                {
                    "http://www.spanishdict.com/translate/{0}"
                }, 
                null
            ),
            // English - Vietnamese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Vietnamese, 
                new string[]
                {
                    "http://v2.vdict.com/{0},1,0,0.html",
                    "http://www.vietdictionary.com/?x=0&y=0&q={0}&db=ev&ft=all"
                }, 
                new string[]
                {
                    "http://v2.vdict.com/{0},2,0,0.html",
                    "http://www.vietdictionary.com/?x=49&y=53&q={0}&db=ve&ft=all"
                }
            ),
            // English - Wikipedia translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Wikipedia, 
                new string[]
                {
                    "http://en.wikipedia.org/wiki/{0}"
                }, 
                null
            ),
            // English - UrbanDictionary translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.UrbanDictionary, 
                new string[]
                {
                    "http://www.urbandictionary.com/define.php?term={0}"
                }, 
                null
            ),
            // Vietnamese - Vietnamese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.Vietnamese, TranslationLanguage.Vietnamese, 
                new string[]
                {
                    "http://v2.vdict.com/{0},3,0,0.html"
                },
                null
            ),
            // Vietnamese - French translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.Vietnamese, TranslationLanguage.French, 
                new string[]
                {
                    "http://v2.vdict.com/{0},4,0,0.html"
                },
                new string[]
                {
                    "http://v2.vdict.com/{0},5,0,0.html"
                }
            ),
            // Vietnamese - Chinese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.Vietnamese, TranslationLanguage.Chinese, 
                null,
                new string[]
                {
                    "http://v2.vdict.com/{0},8,0,0.html"
                }
            ),
            // French - Chinese translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.French, TranslationLanguage.Chinese, 
                new string[]
                {
                    "http://www.chinesedic.com/fr/{0}"
                },
                null
            )
        };

        public static void InitializeDefaultSettings()
        {
            // Load list of past words
            if (Properties.Settings.Default.PastWords == null)
            {
                Properties.Settings.Default.PastWords = new OrderedDictionary();
            }

            // Load list of translate sites
            if (Properties.Settings.Default.TranslateSites == null)
            {
                Properties.Settings.Default.TranslateSites = new OrderedDictionary();
                foreach (Tuple<TranslationLanguage, TranslationLanguage, string[], string[]> site in Common.DefaultTranslationSites)
                {
                    string key = Common.MakeLanguageCombinationKey(site.Item1.ToString(), site.Item2.ToString());

                    TranslateSitesInfo info = new TranslateSitesInfo();
                    info.ForwardSites = (site.Item3 != null) ? new List<string>(site.Item3) : info.ForwardSites;
                    info.BackwardSites = (site.Item4 != null) ? new List<string>(site.Item4) : info.BackwardSites;
                    if (info.ForwardSites.Count > 0)
                    {
                        info.ForwardSitesOptional = new List<bool>(new bool[info.ForwardSites.Count]);
                    }
                    if (info.BackwardSites.Count > 0)
                    {
                        info.BackwardSitesOptional = new List<bool>(new bool[info.BackwardSites.Count]);
                    }

                    Properties.Settings.Default.TranslateSites.Add(key, info);
                }
            }

            // Set default language
            if (String.IsNullOrEmpty(Properties.Settings.Default.SourceLanguage))
            {
                Properties.Settings.Default.SourceLanguage = TranslationLanguage.English.ToString();
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.DestinationLanguage))
            {
                Properties.Settings.Default.DestinationLanguage = TranslationLanguage.English.ToString();
            }
        }

        public static TranslateSitesInfo GetTranslationSitesInfo(string sourceLanguage, string destinationLanguage, ref bool forward)
        {
            TranslateSitesInfo info = null;

            string forwardKey = Common.MakeLanguageCombinationKey(sourceLanguage, destinationLanguage);
            if (Properties.Settings.Default.TranslateSites.Contains(forwardKey))
            {
                info = (TranslateSitesInfo)Properties.Settings.Default.TranslateSites[forwardKey];
                forward = true;
            }
            else
            {
                string backwardKey = Common.MakeLanguageCombinationKey(destinationLanguage, sourceLanguage);
                if (Properties.Settings.Default.TranslateSites.Contains(backwardKey))
                {
                    info = (TranslateSitesInfo)Properties.Settings.Default.TranslateSites[backwardKey];
                    forward = false;
                }
                else // Create new structure for new language
                {
                    info = new TranslateSitesInfo();
                    string key = Common.MakeLanguageCombinationKey(sourceLanguage, destinationLanguage);
                    Properties.Settings.Default.TranslateSites.Add(key, info);
                }
            }

            return info;
        }

        public static bool DeleteTranslationSite(string sourceLanguage, string destinationLanguage, string site)
        {
            bool deleted = false;

            string forwardKey = Common.MakeLanguageCombinationKey(sourceLanguage, destinationLanguage);
            if (Properties.Settings.Default.TranslateSites.Contains(forwardKey))
            {
                TranslateSitesInfo info = (TranslateSitesInfo)Properties.Settings.Default.TranslateSites[forwardKey];
                int indexToDelete = -1;
                for (int iSite = 0; iSite < info.ForwardSites.Count; iSite++)
                {
                    if (info.ForwardSites[iSite] == site)
                    {
                        indexToDelete = iSite;
                        break;
                    }
                }
                if (indexToDelete >= 0)
                {
                    info.ForwardSites.RemoveAt(indexToDelete);
                    info.ForwardSitesOptional.RemoveAt(indexToDelete);
                    deleted = true;
                }
                Properties.Settings.Default.TranslateSites[forwardKey] = info;
            }
            else
            {
                string backwardKey = Common.MakeLanguageCombinationKey(destinationLanguage, sourceLanguage);
                if (Properties.Settings.Default.TranslateSites.Contains(backwardKey))
                {
                    TranslateSitesInfo info = (TranslateSitesInfo)Properties.Settings.Default.TranslateSites[backwardKey];
                    int indexToDelete = -1;
                    for (int iSite = 0; iSite < info.BackwardSites.Count; iSite++)
                    {
                        if (info.BackwardSites[iSite] == site)
                        {
                            indexToDelete = iSite;
                            break;
                        }
                    }
                    if (indexToDelete >= 0)
                    {
                        info.BackwardSites.RemoveAt(indexToDelete);
                        info.BackwardSitesOptional.RemoveAt(indexToDelete);
                        deleted = true;
                    }
                    Properties.Settings.Default.TranslateSites[backwardKey] = info;
                }
            }

            return deleted;
        }

        public static bool AddTranslationSite(string sourceLanguage, string destinationLanguage, string site)
        {
            bool added = false;

            string forwardKey = Common.MakeLanguageCombinationKey(sourceLanguage, destinationLanguage);
            if (Properties.Settings.Default.TranslateSites.Contains(forwardKey))
            {
                TranslateSitesInfo info = (TranslateSitesInfo)Properties.Settings.Default.TranslateSites[forwardKey];

                if (!info.ForwardSites.Contains(site))
                {
                    info.ForwardSites.Add(site);
                    info.ForwardSitesOptional.Add(true);
                    added = true;
                }

                Properties.Settings.Default.TranslateSites[forwardKey] = info;
            }
            else
            {
                string backwardKey = Common.MakeLanguageCombinationKey(destinationLanguage, sourceLanguage);
                if (Properties.Settings.Default.TranslateSites.Contains(backwardKey))
                {
                    TranslateSitesInfo info = (TranslateSitesInfo)Properties.Settings.Default.TranslateSites[backwardKey];

                    if (!info.BackwardSites.Contains(site))
                    {
                        info.BackwardSites.Add(site);
                        info.BackwardSitesOptional.Add(true);
                        added = true;
                    }

                    Properties.Settings.Default.TranslateSites[backwardKey] = info;
                }
            }

            return added;
        }

        public static List<string> GetAvailableLanguages()
        {
            List<string> allAvailableLanguages = new List<string>();
            foreach (object key in Properties.Settings.Default.TranslateSites.Keys)
            {
                string[] languages = GetLanguageCombination(key as string);
                allAvailableLanguages.AddRange(languages);
            }
            allAvailableLanguages = allAvailableLanguages.Distinct().ToList();
            allAvailableLanguages.Sort();

            return allAvailableLanguages;
        }

        public static void InitializeLanguageComboBoxes(System.Windows.Forms.ComboBox cbSource, System.Windows.Forms.ComboBox cbDestination, bool addNewOption = false)
        {
            List<string> availableLanguages = Common.GetAvailableLanguages();

            cbSource.Items.Clear();
            cbDestination.Items.Clear();

            if (addNewOption)
            {
                cbSource.Items.Add(Properties.Resources.General_Add_Delete);
                cbDestination.Items.Add(Properties.Resources.General_Add_Delete);
            }

            foreach (string language in availableLanguages)
            {
                cbSource.Items.Add(language);
                cbDestination.Items.Add(language);
            }
            
            cbSource.SelectedItem = Properties.Settings.Default.SourceLanguage;
            cbDestination.SelectedItem = Properties.Settings.Default.DestinationLanguage;
        }

        public static void SourceLanguageSelectionChanged(System.Windows.Forms.ComboBox cbSource)
        {
            Properties.Settings.Default.SourceLanguage = cbSource.SelectedItem as string;
        }

        public static void DestinationLanguageSelectionChanged(System.Windows.Forms.ComboBox cbDestination)
        {
            Properties.Settings.Default.DestinationLanguage = cbDestination.SelectedItem as string;
        }

        public static string[] GetLanguageCombination(string languageCombinationKey)
        {
            return languageCombinationKey.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string MakeLanguageCombinationKey(string sourceLanguage, string destinationLanguage)
        {
            return (sourceLanguage + "-" + destinationLanguage);
        }

        public static bool DeleteLanguage(string language)
        {
            bool deleted = false;

            List<string> keysToRemove = new List<string>();
            foreach (string key in Properties.Settings.Default.TranslateSites.Keys)
            {
                if (key.Contains(language))
                {
                    keysToRemove.Add(key);
                }
            }

            if (keysToRemove.Count > 0)
            {
                deleted = true;
                foreach (string key in keysToRemove)
                {
                    Properties.Settings.Default.TranslateSites.Remove(key);
                }
            }

            return deleted;
        }

        /// <summary>
        /// Display an information dialog with the specified message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void PromptInformation(string message)
        {
            MessageBox.Show(message, Properties.Resources.Dialog_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Display an information dialog with the specified caption and message.
        /// </summary>
        /// <param name="caption">The caption to display.</param>
        /// <param name="message">The message to display.</param>
        public static void PromptInformation(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Display an error dialog with the specified message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void PromptError(string message)
        {
            MessageBox.Show(message, Properties.Resources.Dialog_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class SiteAddedEventArgs : EventArgs
    {
        private string m_SiteURL;

        public string SiteURL
        {
            get { return m_SiteURL; }
            set { m_SiteURL = value; }
        }
    }

    public delegate void SiteAddedEventHandler(object sender, SiteAddedEventArgs e);

    public class TranslateDirectionChangedEventArgs : EventArgs
    {
        private TranslateDirection m_TranslateDirection;

        public TranslateDirection TranslateDirection
        {
            get { return m_TranslateDirection; }
            set { m_TranslateDirection = value; }
        }
    }

    public delegate void TranslateDirectionChangedEventHandler(object sender, TranslateDirectionChangedEventArgs e);

    public class GlobalMouseHandler : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x200;
        public event MouseEventHandler GlobalMouseMove;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                this.GlobalMouseMove(this, null);
            }
            return false;
        }
    }
}
