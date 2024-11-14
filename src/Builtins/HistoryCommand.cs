using StarShell.Services;

namespace StarShell.builtins
{
    public class HistoryCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "history";
        private LanguageManager languageManager;
        private List<string> commandHistory;

        public HistoryCommand()
        {
            languageManager = new LanguageManager();
            commandHistory = new List<string>();
        }

        public void Execute(string[] args)
        {
            if (args.Length == 1 && (args[0] == "--help"))
            {
                ShowHelp();
                return;
            }

            Console.WriteLine(languageManager.GetText("command_history"));

            foreach (var command in commandHistory)
            {
                Console.WriteLine(command);
            }
        }

        public void AddToHistory(string command)
        {
            commandHistory.Add(command);
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