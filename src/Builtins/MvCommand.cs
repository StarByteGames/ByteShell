using StarShell.Services;

namespace StarShell.builtins
{
    public class MvCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "mv";
        private LanguageManager languageManager;

        public MvCommand()
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
                Console.WriteLine(languageManager.GetText("error_no_source_destination"));
                return;
            }

            string sourcePath = args[0];
            string destinationPath = args[1];

            try
            {
                File.Move(sourcePath, destinationPath);
                Console.WriteLine(languageManager.GetText("success_moved", sourcePath, destinationPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_moving_file", sourcePath, destinationPath));
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