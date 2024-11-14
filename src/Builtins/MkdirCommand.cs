using StarShell.Services;

namespace StarShell.builtins
{
    public class MkdirCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "mkdir";
        private LanguageManager languageManager;

        public MkdirCommand()
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

            string directoryName = args[0];

            try
            {
                Directory.CreateDirectory(directoryName);
                Console.WriteLine(languageManager.GetText("success_directory_created", directoryName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_creating_directory", directoryName));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("help_title"));
            Console.WriteLine(languageManager.GetText("help_description"));
            Console.WriteLine();
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine($"  [Verzeichnis]    {languageManager.GetText("help_options.directory")}");
            Console.WriteLine();
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}