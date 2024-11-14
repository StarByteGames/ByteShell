using StarShell.Services;

namespace StarShell.builtins
{
    public class CatCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "cat";
        private LanguageManager languageManager;

        public CatCommand()
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
                Console.WriteLine(languageManager.GetText("error_no_file"));
                return;
            }

            string fileName = args[0];

            try
            {
                string content = File.ReadAllText(fileName);
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_reading_file", fileName));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("help_title"));
            Console.WriteLine(languageManager.GetText("help_description"));
            Console.WriteLine();
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine($"  [Dateiname]      {languageManager.GetText("help_options.file")}");
            Console.WriteLine();
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}