using StarShell.Constants;

namespace StarShell.Services
{
    public class LanguageManager
    {
        private Dictionary<string, string> translations;
        private string currentLanguage;

        public LanguageManager()
        {
            currentLanguage = Config.Language;
            LoadLanguageFile();
        }

        private void LoadLanguageFile()
        {
            string filePath = $"./languages/Language_{currentLanguage}.json";

            if (File.Exists(filePath))
            {
                string[] content = File.ReadAllLines(filePath);
                Config.Language = content.Where(f => f.Contains("language=")).LastOrDefault() ?? "en";
            }
            else
            {
                Console.WriteLine($"The language file for '{currentLanguage}' could not be found.");
                Console.WriteLine($"Using default language 'en'");
                Config.Language = "en";
                currentLanguage = "en";
                filePath = $"./languages/Language_{currentLanguage}.json";
                Console.WriteLine($"Using default language 'en'");
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Could not find the language file.\nPlease make sure the language file exists.");
                }
            }
        }

        public string GetText(string key, params object[] args)
        {
            if (translations.ContainsKey(key))
            {
                string text = translations[key];
                return string.Format(text, args);
            }

            return string.Empty;
        }
    }
}