using StarShell.Services;

namespace StarShell.builtins
{
    public class TodoCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "todo";
        private LanguageManager languageManager;
        private string todoFilePath = "todo_list.txt";

        public TodoCommand()
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

            string action = args[0].ToLower();

            switch (action)
            {
                case "add":
                    if (args.Length > 1)
                    {
                        AddTask(string.Join(" ", args[1..]));
                    }
                    else
                    {
                        Console.WriteLine(languageManager.GetText("todo_add_error"));
                    }
                    break;

                case "list":
                    ListTasks();
                    break;

                case "done":
                    if (args.Length > 1 && int.TryParse(args[1], out int taskNumber))
                    {
                        MarkTaskAsDone(taskNumber);
                    }
                    else
                    {
                        Console.WriteLine(languageManager.GetText("todo_done_error"));
                    }
                    break;

                default:
                    ShowHelp();
                    break;
            }
        }

        private void AddTask(string task)
        {
            File.AppendAllText(todoFilePath, $"{task}\n");
            Console.WriteLine(languageManager.GetText("todo_added", task));
        }

        private void ListTasks()
        {
            if (File.Exists(todoFilePath))
            {
                var tasks = File.ReadAllLines(todoFilePath);
                for (int i = 0; i < tasks.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
            else
            {
                Console.WriteLine(languageManager.GetText("todo_no_tasks"));
            }
        }

        private void MarkTaskAsDone(int taskNumber)
        {
            if (File.Exists(todoFilePath))
            {
                var tasks = new List<string>(File.ReadAllLines(todoFilePath));
                if (taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    Console.WriteLine(languageManager.GetText("todo_marked_done", tasks[taskNumber - 1]));
                    tasks.RemoveAt(taskNumber - 1);
                    File.WriteAllLines(todoFilePath, tasks);
                }
                else
                {
                    Console.WriteLine(languageManager.GetText("todo_invalid_number"));
                }
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(languageManager.GetText("todo_help"));
        }
    }
}
