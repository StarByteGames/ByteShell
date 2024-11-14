using StarShell.builtins;

namespace StarShell
{
    public class CommandExecutor
    {
        private readonly Dictionary<string, BuiltinCommand.IBuiltinCommand> _builtinCommands;

        public CommandExecutor()
        {
            _builtinCommands = new Dictionary<string, BuiltinCommand.IBuiltinCommand>();

            RegisterBuiltin(new BackupCommand());
            RegisterBuiltin(new CatCommand());
            RegisterBuiltin(new CdCommand());
            RegisterBuiltin(new CheatCommand());
            RegisterBuiltin(new ClearCommand());
            RegisterBuiltin(new CpCommand());
            RegisterBuiltin(new DateCommand());
            RegisterBuiltin(new EchoCommand());
            RegisterBuiltin(new ExitCommand());
            RegisterBuiltin(new FindCommand());
            RegisterBuiltin(new GrepCommand());
            RegisterBuiltin(new HistoryCommand());
            RegisterBuiltin(new LsCommand());
            RegisterBuiltin(new MkdirCommand());
            RegisterBuiltin(new MvCommand());
            RegisterBuiltin(new ProjectCommand());
            RegisterBuiltin(new PwdCommand());
            RegisterBuiltin(new RmCommand());
            RegisterBuiltin(new TodoCommand());
            RegisterBuiltin(new TouchCommand());
            RegisterBuiltin(new GitCommand());

        }

        private void RegisterBuiltin(BuiltinCommand.IBuiltinCommand command)
        {
            _builtinCommands[command.Name] = command;
        }

        public void ExecuteCommand(string commandName, string[] args)
        {
            if (_builtinCommands.TryGetValue(commandName, out var command))
            {
                command.Execute(args);
            }
            else
            {
                Console.WriteLine($"Command not found: {commandName}");
            }
        }
    }
}