using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualDictionary
{
    /// <summary>
    /// An enumeration of available translation languages.
    /// </summary>
    public enum TranslationLanguage
    {
        English = 0,
        Chinese,
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
        public static KeyValuePair<TranslationLanguage, string[]>[] TranslateSites = 
        {
            new KeyValuePair<TranslationLanguage, string[]>(TranslationLanguage.English, 
                new string[] 
                {
                    "http://dictionary.com/browse/{0}"
                }
            ),
            new KeyValuePair<TranslationLanguage, string[]>(TranslationLanguage.Chinese, 
                new string[] 
                {
                    "http://hk.dictionary.yahoo.com/dictionary?p={0}"
                }
            ),
            new KeyValuePair<TranslationLanguage, string[]>(TranslationLanguage.Hindi, 
                new string[]
                {
                    "http://www.shabdkosh.com/translate/{0}/{0}_meaning_in_Hindi_English"
                }
            ),
            new KeyValuePair<TranslationLanguage, string[]>(TranslationLanguage.Spanish, 
                new string[]
                {
                    "http://www.spanishdict.com/translate/{0}"
                }
            ),
            new KeyValuePair<TranslationLanguage, string[]>(TranslationLanguage.Vietnamese, 
                new string[] 
                {
                    "http://vdict.com/{0},1,0,0.html"
                }
            )
        };
    }
}
