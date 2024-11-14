using StarShell.Services;

namespace StarShell.builtins
{
    public class FindCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "find";
        private LanguageManager languageManager;

        public FindCommand()
        {
            languageManager = new LanguageManager();
        }

        public void Execute(string[] args)
        {
            if (args.Length == 1 && (args[0] == "--help"))
            {
                ShowHelp();
                return;
            }

            if (args.Length == 0)
            {
                Console.WriteLine(languageManager.GetText("error_no_directory"));
                return;
            }

            string searchPattern = args[0];
            string currentDir = Directory.GetCurrentDirectory();

            try
            {
                var files = Directory.GetFiles(currentDir, searchPattern, SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_searching", searchPattern));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("help_title"));
            Console.WriteLine(languageManager.GetText("help_description"));
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}