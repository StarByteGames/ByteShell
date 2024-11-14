using StarShell.Services;

namespace StarShell.builtins
{
    public class ExitCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "exit";
        private LanguageManager languageManager;

        public ExitCommand()
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

            Console.WriteLine(languageManager.GetText("exit_message"));
            Environment.Exit(0);
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