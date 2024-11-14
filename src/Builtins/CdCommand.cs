using StarShell.Services;

namespace StarShell.builtins
{
    public class CdCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "cd";
        private bool verbose;
        private LanguageManager languageManager;

        public CdCommand(bool verboseMode = false)
        {
            verbose = verboseMode;
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

            string targetDirectory = args[0];

            try
            {
                Directory.SetCurrentDirectory(targetDirectory);

                if (verbose)
                {
                    Console.WriteLine(languageManager.GetText("success_directory_changed", targetDirectory));
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(languageManager.GetText("error_directory_not_found", targetDirectory));
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(languageManager.GetText("error_access_denied", targetDirectory));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
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
