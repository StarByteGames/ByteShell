using StarShell.Services;

namespace StarShell.builtins
{
    public class ProjectCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "project";
        private LanguageManager languageManager;

        public ProjectCommand()
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

            string projectName = args[1];
            string template = args.Length > 2 ? args[2].ToLower() : "default";

            bool isMinimal = false;
            bool createReadme = true;
            bool createLicense = true;
            bool createGitignore = true;

            for (int i = 3; i < args.Length; i++)
            {
                if (args[i] == "--minimal" && args.Length > i + 1 && (args[i + 1].ToLower() == "true" || args[i + 1].ToLower() == ""))
                {
                    isMinimal = true;
                }
                else if (args[i] == "--README" && args.Length > i + 1 && args[i + 1].ToLower() == "false")
                {
                    createReadme = false;
                }
                else if (args[i] == "--LICENSE" && args.Length > i + 1 && args[i + 1].ToLower() == "false")
                {
                    createLicense = false;
                }
                else if (args[i] == "--gitignore" && args.Length > i + 1 && args[i + 1].ToLower() == "false")
                {
                    createGitignore = false;
                }
            }

            CreateProjectStructure(projectName, template, isMinimal, createReadme, createLicense, createGitignore);
        }

        private void CreateProjectStructure(string projectName, string template, bool isMinimal, bool createReadme, bool createLicense, bool createGitignore)
        {
            string projectPath = Path.Combine(Directory.GetCurrentDirectory(), projectName);
            Directory.CreateDirectory(projectPath);
            Console.WriteLine(languageManager.GetText("project_created", projectPath));

            if (!isMinimal)
            {
                switch (template)
                {
                    case "nodejs":
                        CreateNodejsTemplate(projectPath, projectName);
                        break;

                    case "python":
                        CreatePythonTemplate(projectPath, projectName);
                        break;

                    case "webapp":
                        CreateWebAppTemplate(projectPath, projectName);
                        break;

                    case "console":
                        CreateConsoleTemplate(projectPath, projectName);
                        break;

                    default:
                        CreateDefaultTemplate(projectPath, projectName);
                        break;
                }
            }
            else
            {
                Console.WriteLine(languageManager.GetText("minimal_project_created", projectPath));
            }

            if (createReadme)
            {
                File.WriteAllText(Path.Combine(projectPath, "README.md"), "# " + projectName + "\n\nThis is the README for your project.");
                Console.WriteLine(languageManager.GetText("additional_file_created", "README.md"));
            }

            if (createLicense)
            {
                File.WriteAllText(Path.Combine(projectPath, "LICENSE"), "MIT License\n\nCopyright (c) " + DateTime.Now.Year);
                Console.WriteLine(languageManager.GetText("additional_file_created", "LICENSE"));
            }

            if (createGitignore)
            {
                File.WriteAllText(Path.Combine(projectPath, ".gitignore"), "node_modules/\n.DS_Store\n*.log");
                Console.WriteLine(languageManager.GetText("additional_file_created", ".gitignore"));
            }
        }

        private void CreateWebAppTemplate(string projectPath, string projectName)
        {
            Directory.CreateDirectory(Path.Combine(projectPath, "css"));
            Directory.CreateDirectory(Path.Combine(projectPath, "js"));
            Directory.CreateDirectory(Path.Combine(projectPath, "assets"));
            File.WriteAllText(Path.Combine(projectPath, "index.html"), "<!DOCTYPE html>\n<html>\n<body>\n<h1>" + projectName + "</h1>\n</body>\n</html>");
            Console.WriteLine(languageManager.GetText("project_template_created", projectName));
        }

        private void CreateNodejsTemplate(string projectPath, string projectName)
        {
            File.WriteAllText(Path.Combine(projectPath, "package.json"), "{\n  \"name\": \"" + projectName + "\",\n  \"version\": \"1.0.0\"\n}");
            Console.WriteLine(languageManager.GetText("project_template_created", projectName));
        }

        private void CreatePythonTemplate(string projectPath, string projectName)
        {
            File.WriteAllText(Path.Combine(projectPath, "main.py"), "print('Hello, World from " + projectName + "')");
            Console.WriteLine(languageManager.GetText("project_template_created", projectName));
        }

        private void CreateConsoleTemplate(string projectPath, string projectName)
        {
            File.WriteAllText(Path.Combine(projectPath, "Program.cs"), "using System;\nnamespace " + projectName + " {\n    class Program {\n        static void Main(string[] args) {\n            Console.WriteLine(\"Hello World from " + projectName + "\");\n        }\n    }\n}");
            Console.WriteLine(languageManager.GetText("project_template_created", projectName));
        }

        private void CreateDefaultTemplate(string projectPath, string projectName)
        {
            File.WriteAllText(Path.Combine(projectPath, "README.md"), "# " + projectName + "\n\nThis is a default project template.");
            Console.WriteLine(languageManager.GetText("project_template_created", projectName));
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("project_help"));
        }
    }
}
