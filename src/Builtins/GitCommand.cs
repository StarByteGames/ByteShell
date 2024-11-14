using StarShell.Services;
using System.Diagnostics;

namespace StarShell.builtins
{
    public class GitCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "git";
        private LanguageManager languageManager;

        public GitCommand()
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
                Console.WriteLine(languageManager.GetText("error_git_no_command"));
                return;
            }

            string gitCommand = args[0];
            string arguments = string.Join(" ", args, 1, args.Length - 1);

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = $"{gitCommand} {arguments}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process gitProcess = Process.Start(startInfo);
                string output = gitProcess.StandardOutput.ReadToEnd();
                string error = gitProcess.StandardError.ReadToEnd();
                gitProcess.WaitForExit();

                if (gitProcess.ExitCode == 0)
                {
                    Console.WriteLine(output);
                }
                else
                {
                    Console.WriteLine(languageManager.GetText("error_git_command_failed", gitCommand));
                    Console.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(languageManager.GetText("error_git_execution", ex.Message));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("help_title"));
            Console.WriteLine(languageManager.GetText("help_description"));
            Console.WriteLine();
            Console.WriteLine("Optionen:");
            Console.WriteLine($"  -h, --help      {languageManager.GetText("help_options.help")}");
            Console.WriteLine($"  [Git-Befehl]     {languageManager.GetText("help_options.git_command")}");
            Console.WriteLine();
            Console.WriteLine("Beispiel:");
            Console.WriteLine(languageManager.GetText("example"));
        }
    }
}
