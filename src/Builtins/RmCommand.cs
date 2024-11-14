using System;

namespace Shell.builtins
{
    public class ExitCommand : BuiltinCommand.IBuiltinCommand
    {
        public string Name => "exit";

        public void Execute(string[] args)
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }
        
        public string Description => "Exits the application";
    }
}