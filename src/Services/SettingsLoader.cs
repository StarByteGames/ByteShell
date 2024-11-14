using StarShell.Constants;

namespace StarShell.Services
{
    public class SettingsLoader
    {
        static string[] GetConfig()
        {
            if (File.Exists("database.cfg"))
            {
                string[] lines = File.ReadAllLines("database.cfg");
                return lines;
            }
            return null;
        }
        
        public static void init()
        {
            string[] content = GetConfig();
            
            if (content != null)
                Config.Language = content.Where(l => l.Contains("language")).Last().Replace("Language","");
        }
    }
}