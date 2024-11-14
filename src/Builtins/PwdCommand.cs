using StarShell.Services;

namespace StarShell.builtins
{
    public class PwdCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "pwd";
        private LanguageManager languageManager;

        public PwdCommand()
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

            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                Console.WriteLine(currentDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_printing_directory"));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("help_title"));
            Console.WriteLine(languageManager.GetText("help_description"));
            Console.WriteLine();
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine();
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}