using StarShell.Services;

namespace StarShell.builtins
{
    public class BackupCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "backup";
        private LanguageManager languageManager;

        public BackupCommand()
        {
            languageManager = new LanguageManager();
        }

        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                ShowHelp();
                return;
            }

            string sourcePath = args[0];
            string destinationPath = args[1];

            CreateBackup(sourcePath, destinationPath);
        }

        private void CreateBackup(string sourcePath, string destinationPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    DirectoryCopy(sourcePath, destinationPath, true);
                    Console.WriteLine(languageManager.GetText("backup_success", destinationPath));
                }
                else
                {
                    Console.WriteLine(languageManager.GetText("backup_error"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destDirName);

            foreach (FileInfo file in dir.GetFiles())
            {
                file.CopyTo(Path.Combine(destDirName, file.Name), false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    DirectoryCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), copySubDirs);
                }
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("backup_help"));
        }
    }
}
