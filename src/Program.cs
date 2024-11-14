using System.Diagnostics;

namespace Shell
{
    class main
    {
        
        static void Main()
        {
            while (true)
            {
                Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name + " " + Directory.GetCurrentDirectory());
                Console.Write("$ ");
                Console.Out.Flush();

                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                var commands = input.Trim().Split(" && ").ToList();
                Process? previousCommand = null;

                foreach (var command in commands)
                {
                    var parts = command.Trim().Split(' ');
                    var cmd = parts[0];
                    var args = parts.Skip(1).ToArray();

                    switch (cmd)
                    {
                        case "cd":
                            var newDir = args.Length > 0 ? args[0] : "/";
                            try
                            {
                                Directory.SetCurrentDirectory(newDir);
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine(e.Message);
                            }

                            previousCommand = null;
                            break;

                        case "exit":
                            return;

                        default:
                            var processStartInfo = new ProcessStartInfo
                            {
                                FileName = cmd,
                                Arguments = string.Join(" ", args),
                                RedirectStandardInput = previousCommand != null,
                                RedirectStandardOutput = commands.IndexOf(command) < commands.Count - 1,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };

                            try
                            {
                                var process = Process.Start(processStartInfo);

                                if (previousCommand != null)
                                {
                                    using (var writer = process.StandardInput)
                                    using (var reader = previousCommand.StandardOutput)
                                    {
                                        reader.BaseStream.CopyTo(writer.BaseStream);
                                    }

                                    previousCommand.WaitForExit();
                                }

                                previousCommand = process;
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine(e.Message);
                                previousCommand = null;
                            }

                            break;
                    }
                }

                previousCommand?.WaitForExit();
            }
        }
    }
}