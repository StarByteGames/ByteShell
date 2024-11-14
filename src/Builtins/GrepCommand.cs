using StarShell.Services;

namespace StarShell.builtins
{
    public class GrepCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "grep";
        private LanguageManager languageManager;

        public GrepCommand()
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

            if (args.Length < 2)
            {
                Console.WriteLine(languageManager.GetText("error_no_pattern_or_file"));
                return;
            }

            string pattern = args[0];
            string fileName = args[1];

            try
            {
                foreach (var line in File.ReadLines(fileName))
                {
                    if (line.Contains(pattern))
                    {
                        Console.WriteLine(line);
                    }
                }
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
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}