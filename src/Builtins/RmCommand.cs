using StarShell.Services;

namespace StarShell.builtins
{
    public class RmCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "rm";
        private LanguageManager languageManager;

        public RmCommand()
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
                File.Delete(fileName);
                Console.WriteLine(languageManager.GetText("success_file_deleted", fileName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_deleting_file", fileName));
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