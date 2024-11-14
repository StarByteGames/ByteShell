using StarShell.Services;

namespace StarShell
{
    public class StarShell
    {
        private readonly CommandExecutor _executor;

        public StarShell()
        {
            SettingsLoader.init();
            _executor = new CommandExecutor();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Starcrusher Shell!");
            while (true)
            {
                Console.WriteLine(Environment.MachineName + "@" + Environment.UserName + "  " + Directory.GetCurrentDirectory());
                Console.Write("$ ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                string[] parts = input.Split(' ');
                string commandName = parts[0];
                string[] args = parts.Skip(1).ToArray();

                _executor.ExecuteCommand(commandName, args);
            }
        }
    }
}