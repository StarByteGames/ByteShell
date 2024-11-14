using StarShell.Services;

namespace StarShell.builtins
{
    public class CheatCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "cheat";
        private LanguageManager languageManager;

        public CheatCommand()
        {
            languageManager = new LanguageManager();
        }

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            string query = args[0].ToLower();
            DisplayCheatSheet(query);
        }

        private void DisplayCheatSheet(string query)
        {
            // Simulierter Inhalt eines Cheat-Sheets für Demo-Zwecke
            switch (query)
            {
                case "git":
                    Console.WriteLine("Git Cheat Sheet:\n- git clone <url>\n- git commit -m \"message\"\n- git push");
                    break;
                case "python":
                    Console.WriteLine("Python Cheat Sheet:\n- for x in range(10):\n- def function():\n- print(\"Hello World\")");
                    break;
                default:
                    Console.WriteLine(languageManager.GetText("cheat_no_entry", query));
                    break;
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("cheat_help"));
        }
    }
}