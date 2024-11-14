namespace StarShell.builtins
{
    public class BuiltinCommand
    {
        public interface IBuiltinCommand
        {
            string Name { get; }
            void Execute(string[] args);
            void ShowHelp();
        }
    }
}