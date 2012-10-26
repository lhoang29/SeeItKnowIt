using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace VisualDictionary
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
        Spanish,
        Vietnamese
    }

    public class Common
    {
        /// <summary>
        /// List of words used to calibrate when adding a new site for lookup.
        /// </summary>
        public static string[] CalibrateWords = 
        { 
            "ubiquitous",
            "simultaneous",
            "hippopotamus",
            "labyrinth",
            "obfuscate",
            "ephemeral",
            "acquiesce",
            "poignant",
            "obsequious",
            "equivocate"
        };

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
                    "http://hk.dictionary.yahoo.com/dictionary?p={0}"
                }, 
                null
            ),
            // English - Hindi translation
            new Tuple<TranslationLanguage, TranslationLanguage, string[], string[]>( TranslationLanguage.English, TranslationLanguage.Hindi, 
                new string[]
                {
                    "http://www.shabdkosh.com/translate/{0}/{0}_meaning_in_Hindi_English"
                }, 
                null
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
                    "http://v2.vdict.com/{0},1,0,0.html"
                }, 
                new string[]
                {
                    "http://v2.vdict.com/{0},2,0,0.html"
                }
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
                    KeyValuePair<string[], string[]> value = new KeyValuePair<string[], string[]>(site.Item3, site.Item4);
                    Properties.Settings.Default.TranslateSites.Add(key, value);
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

        public static string[] GetTranslationSites(string sourceLanguage, string destinationLanguage)
        {
            string[] translationSites = null;

            string forwardKey = Common.MakeLanguageCombinationKey(sourceLanguage, destinationLanguage);
            if (Properties.Settings.Default.TranslateSites.Contains(forwardKey))
            {
                KeyValuePair<string[], string[]> biDirectionalSites = (KeyValuePair<string[], string[]>)Properties.Settings.Default.TranslateSites[forwardKey];
                translationSites = biDirectionalSites.Key;
            }
            else
            {
                string backwardKey = Common.MakeLanguageCombinationKey(destinationLanguage, sourceLanguage);
                if (Properties.Settings.Default.TranslateSites.Contains(backwardKey))
                {
                    KeyValuePair<string[], string[]> biDirectionalSites = (KeyValuePair<string[], string[]>)Properties.Settings.Default.TranslateSites[backwardKey];
                    translationSites = biDirectionalSites.Value;
                }
            }

            return translationSites;
        }

        public static void UpdateTranslationSites(string sourceLanguage, string destinationLanguage, string[] newSites)
        {
            string forwardKey = Common.MakeLanguageCombinationKey(sourceLanguage, destinationLanguage);
            if (Properties.Settings.Default.TranslateSites.Contains(forwardKey))
            {
                KeyValuePair<string[], string[]> biDirectionalSites = (KeyValuePair<string[], string[]>)Properties.Settings.Default.TranslateSites[forwardKey];
                Properties.Settings.Default.TranslateSites[forwardKey] = new KeyValuePair<string[], string[]>(newSites, biDirectionalSites.Value);
            }
            else
            {
                string backwardKey = Common.MakeLanguageCombinationKey(destinationLanguage, sourceLanguage);
                if (Properties.Settings.Default.TranslateSites.Contains(backwardKey))
                {
                    KeyValuePair<string[], string[]> biDirectionalSites = (KeyValuePair<string[], string[]>)Properties.Settings.Default.TranslateSites[backwardKey];
                    Properties.Settings.Default.TranslateSites[backwardKey] = new KeyValuePair<string[], string[]>(biDirectionalSites.Key, newSites);
                }
            }
        }

        public static string[] GetAvailableSourceLanguages()
        {
            List<string> sourceLanguages = new List<string>();

            foreach (object key in Properties.Settings.Default.TranslateSites.Keys)
            {
                string[] languageCombination = Common.GetLanguageCombination(key.ToString());
                string sourceLanguage = languageCombination[0];
                string destinationLanguage = languageCombination[1];

                KeyValuePair<string[], string[]> biDirectionalSites = (KeyValuePair<string[], string[]>)Properties.Settings.Default.TranslateSites[key];
                if (biDirectionalSites.Key != null && biDirectionalSites.Key.Length > 0)
                {
                    if (!sourceLanguages.Contains(sourceLanguage))
                    {
                        sourceLanguages.Add(sourceLanguage);
                    }
                }
                if (biDirectionalSites.Value != null && biDirectionalSites.Value.Length > 0)
                {
                    if (!sourceLanguages.Contains(destinationLanguage))
                    {
                        sourceLanguages.Add(destinationLanguage);
                    }
                }
            }
            sourceLanguages.Sort();

            return sourceLanguages.ToArray();
        }

        public static string[] GetAvailableDestinationLanguages(string sourceLanguage)
        {
            List<string> destinationLanguages = new List<string>();

            foreach (object key in Properties.Settings.Default.TranslateSites.Keys)
            {
                string keyString = key.ToString();
                if (keyString.Contains(sourceLanguage))
                {
                    string[] languageCombination = Common.GetLanguageCombination(key.ToString());
                    string currentSourceLanguage = languageCombination[0];
                    string destinationLanguage = languageCombination[1];
                    KeyValuePair<string[], string[]> translateSites = (KeyValuePair<string[], string[]>)Properties.Settings.Default.TranslateSites[key];

                    // If forward direction and there exists some translate site then add the destination language
                    if (currentSourceLanguage == sourceLanguage)
                    {
                        if (translateSites.Key != null && translateSites.Key.Length > 0)
                        {
                            if (!destinationLanguages.Contains(destinationLanguage))
                            {
                                destinationLanguages.Add(destinationLanguage);
                            }
                        }
                    }
                    // If backward direction and there exists some translate site then add the source language
                    else if (destinationLanguage == sourceLanguage)
                    {
                        if (translateSites.Value != null && translateSites.Value.Length > 0)
                        {
                            if (!destinationLanguages.Contains(currentSourceLanguage))
                            {
                                destinationLanguages.Add(currentSourceLanguage);
                            }
                        }
                    }
                }
            }

            destinationLanguages.Sort();
            return destinationLanguages.ToArray();
        }

        public static void InitializeLanguageComboBoxes(System.Windows.Forms.ComboBox cbSource, System.Windows.Forms.ComboBox cbDestination)
        {
            string[] availableSourceLanguages = Common.GetAvailableSourceLanguages();
            cbSource.DataSource = availableSourceLanguages;
            cbSource.SelectedItem = Properties.Settings.Default.SourceLanguage;

            string[] availableDestinationLanguages = Common.GetAvailableDestinationLanguages(Properties.Settings.Default.SourceLanguage);
            cbDestination.DataSource = availableDestinationLanguages;
            cbDestination.SelectedItem = Properties.Settings.Default.DestinationLanguage;
        }

        public static void SourceLanguageSelectionChanged(System.Windows.Forms.ComboBox cbSource, System.Windows.Forms.ComboBox cbDestination)
        {
            Properties.Settings.Default.SourceLanguage = cbSource.SelectedItem as string;

            string[] availableDestinationLanguages = Common.GetAvailableDestinationLanguages(Properties.Settings.Default.SourceLanguage);
            cbDestination.DataSource = availableDestinationLanguages;

            if (availableDestinationLanguages.Length > 0)
            {
                if (!availableDestinationLanguages.Contains(Properties.Settings.Default.DestinationLanguage))
                {
                    Properties.Settings.Default.DestinationLanguage = availableDestinationLanguages[0];
                }
            }
            else
            {
                Properties.Settings.Default.DestinationLanguage = String.Empty;
            }

            cbDestination.SelectedItem = Properties.Settings.Default.DestinationLanguage;
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
}
