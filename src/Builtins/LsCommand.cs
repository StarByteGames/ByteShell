using StarShell.Services;

namespace StarShell.builtins
{
    public class LsCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "ls";
        private LanguageManager languageManager;

        public LsCommand()
        {
            languageManager = new LanguageManager();
        }

        public void Execute(string[] args)
        {
            bool useColor = args.Length > 0 && args[0] == "--color";

            try
            {
                var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                var entries = directoryInfo.GetFileSystemInfos();

                foreach (var entry in entries)
                {
                    if (useColor)
                    {
                        if (entry.Attributes.HasFlag(FileAttributes.Directory))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{entry.Name}/");
                        }
                        else if (entry.Extension.Equals(".exe", StringComparison.OrdinalIgnoreCase) || 
                                 entry.Extension.Equals(".sh", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(entry.Name);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(entry.Name);
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(entry.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("ls_error", ex.Message));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("ls_help"));
            Console.WriteLine("--color      " + languageManager.GetText("ls_color_option"));
        }
    }
}
