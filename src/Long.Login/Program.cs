using Long.Database;
using Long.Network.Services;
using Long.Shared.Helpers;
using Serilog;
using System.Reflection;

namespace Long.Login
{
    internal class Program
    {
        public static string Version => Assembly.GetEntryAssembly()?
                                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                                .InformationalVersion ?? "0.0.0.0";

        private static readonly ILogger logger = Log.ForContext<Program>();

        public static async Task<int> Main(params string[] args)
        {
            Console.Title = "Loading...";

            Console.WriteLine("\tLong: Account Server");
            Console.WriteLine($"\t\tCopyright 2023-{DateTime.Now:yyyy} Felipe \"Konichu\"");
            Console.WriteLine($"\t\tVersion: {Version}");
            Console.WriteLine("\tSome Rights Reserved");
            Console.WriteLine();

            Logger.Initialize("AccServer");

            logger.Information("Logger initialized with success!");
            logger.Information("Initializating account settings");

            ServerSettings settings = new ServerSettings(args);
            AbstractDbContext.Configuration = settings.Database;

            Console.WriteLine();
            logger.Information("Database settings: {0} {1} **** {2}", settings.Database.Hostname, settings.Database.Username, settings.Database.Schema);
            logger.Information("Login Listener settings:");
            logger.Information("\tPort: {0}", settings.Login.Port);
            logger.Information("\tRecv processors: {0}", settings.Login.Listener.RecvProcessors);
            logger.Information("\tSend processors: {0}", settings.Login.Listener.SendProcessors);
            logger.Information("Game Listener settings:");
            logger.Information("\tPort: {0}", settings.Game.Port);
            logger.Information("\tRecv processors: {0}", settings.Game.Listener.RecvProcessors);
            logger.Information("\tSend processors: {0}", settings.Game.Listener.SendProcessors);
            Console.WriteLine();

            var tasks = new List<Task>
            {
                RandomnessService.Instance.StartAsync(CancellationToken.None)
            };
            Task.WaitAll(tasks.ToArray());

            if (!await Kernel.InitializeAsync(settings))
            {
                logger.Fatal("Could not initialize server!!!!");
                return 1;
            }

            await CommandLineAsync();
            return 0;
        }

        private static async Task CommandLineAsync()
        {
            string command;
            do
            {
                command = Console.ReadLine();
                if (string.IsNullOrEmpty(command))
                {
                    continue;
                }
                await ProcessCommandAsync(command);
            }
            while (!"exit".Equals(command));
        }

        private static async Task ProcessCommandAsync(string command)
        {
            string[] splitCmd = command.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (splitCmd.Length == 0)
            {
                return;
            }

            switch (splitCmd[0])
            {
                case "/version":
                    {
                        Console.WriteLine($"Version: {Version}");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unknown command.");
                        break;
                    }
            }
        }
    }
}