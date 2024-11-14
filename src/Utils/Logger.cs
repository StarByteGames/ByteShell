namespace StarShell.utils
{
    public class Logger
    {
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        private readonly LogLevel _currentLogLevel;

        public Logger(LogLevel logLevel = LogLevel.Info)
        {
            _currentLogLevel = logLevel;
        }

        public void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        private void Log(LogLevel level, string message)
        {
            if (level >= _currentLogLevel)
            {
                var prefix = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}]";
                Console.WriteLine($"{prefix} {message}");
            }
        }
    }
}