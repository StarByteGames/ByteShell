# StarShell - A Simple Shell Implementation

StarShell is a custom shell that allows users to interact with a set of built-in commands. It is implemented in C# and provides basic shell functionalities like file management, system navigation, and custom user commands. This shell is designed for educational purposes to help users understand how shell environments work.

## Features

- **Builtin Commands:** A variety of built-in commands like `cd`, `ls`, `echo`, `pwd`, and more are available.
- **Command Execution:** The shell reads user input, parses commands, and executes the corresponding built-in command.
- **Extendable:** New commands can be easily added by implementing the `IBuiltinCommand` interface.
- **Customizable:** Users can modify the shell's settings or add additional features according to their needs.

## Prerequisites

Make sure you have the following software installed:

- .NET SDK (for building and running the project)

## Installation

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/StarGames2025/StarShell/StarShell.git
   ```

2. Navigate into the project directory:

   ```bash
   cd StarShell
   ```

3. Build and run the project using the .NET CLI:

   ```bash
   dotnet run
   ```

4. The shell will start, and you will be prompted with a shell environment.

## How It Works

### Command Executor

The `CommandExecutor` class is responsible for managing and executing built-in commands. It stores the registered commands in a dictionary, where the command name is the key, and the command implementation (which implements `IBuiltinCommand`) is the value. Here is an example of how the commands are registered:

```csharp
public CommandExecutor()
{
    _builtinCommands = new Dictionary<string, BuiltinCommand.IBuiltinCommand>();

    RegisterBuiltin(new BackupCommand());
    RegisterBuiltin(new CatCommand());
    RegisterBuiltin(new CdCommand());
    // Other commands...
}
```

When a user enters a command, the `CommandExecutor` looks up the command name in the dictionary and executes the associated `IBuiltinCommand` implementation. If the command is not found, an error message is displayed.

### StarShell

The `StarShell` class serves as the main entry point of the shell. It initializes the settings, sets up the `CommandExecutor`, and enters a loop where it prompts the user for input, splits the input into a command and arguments, and then passes them to the `CommandExecutor`.

```csharp
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
```

### Example of Built-in Commands

Here are some examples of the built-in commands implemented in `StarShell`:

- `cd` - Change the current directory.
- `ls` - List the contents of a directory.
- `echo` - Output a message to the terminal.
- `pwd` - Print the current working directory.
- `touch` - Create a new file.
- `cp` - Copy files.
- `rm` - Remove files.

### Adding New Commands

To add a new command, implement the `IBuiltinCommand` interface and register the new command in the `CommandExecutor`. Here's a simple example of a new command:

```csharp
public class HelloWorldCommand : BuiltinCommand.IBuiltinCommand
{
    public string Name => "hello";

    public void Execute(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
```

Then, in the `CommandExecutor` constructor, register the new command:

```csharp
RegisterBuiltin(new HelloWorldCommand());
```

## Available Built-in Commands

- `backup` - Backup files
- `cat` - Concatenate and display file contents
- `cd` - Change directory
- `cheat` - Display cheat sheet (example command)
- `clear` - Clear the terminal screen
- `cp` - Copy files
- `date` - Show the current date and time
- `echo` - Print a message to the console
- `exit` - Exit the shell
- `find` - Search for files
- `grep` - Search inside files
- `history` - Show command history
- `ls` - List files and directories
- `mkdir` - Make a directory
- `mv` - Move or rename files
- `project` - Display project info (example command)
- `pwd` - Print the working directory
- `rm` - Remove files
- `todo` - Show or manage to-do list (example command)
- `touch` - Create an empty file
- `git` - Execute git commands (example command)

## Contributing

Feel free to fork the repository and contribute by opening pull requests with bug fixes or new features. Please make sure to follow the coding style and write tests for new functionalities.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
