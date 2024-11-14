using StarShell.Services;

namespace StarShell.builtins
{
    public class EchoCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "echo";
        private LanguageManager languageManager;

        public EchoCommand()
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
                Console.WriteLine(languageManager.GetText("error_no_text"));
                return;
            }

            string textToEcho = string.Join(" ", args);
            Console.WriteLine(textToEcho);
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("help_title"));
            Console.WriteLine(languageManager.GetText("help_description"));
            Console.WriteLine();
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine($"  [Text]          {languageManager.GetText("help_options.text")}");
            Console.WriteLine();
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}