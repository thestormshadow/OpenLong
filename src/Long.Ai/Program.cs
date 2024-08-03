using Long.Ai.Database;
using Long.Ai.Database.Repositories;
using Long.Database;
using Long.Database.Entities;
using Long.Shared.Helpers;
using Long.Shared.Managers;
using Serilog;
using System.Reflection;

namespace Long.Ai
{
    internal class Program
    {
        public static string Version => Assembly.GetEntryAssembly()?
                                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                                .InformationalVersion ?? "0.0.0.0";

        const int NO_ERROR = 0;
        const int INITIALIZATION_ERROR = -1;
        const int DATABASE_ERROR = -2;

        private static Serilog.ILogger logger;
        private static readonly CancellationTokenSource cancellationTokenSource = new();

        public static async Task<int> Main(params string[] args)
        {
            Console.Title = "Loading...";

            Console.WriteLine("\tLong: Ai Server");
            Console.WriteLine($"\t\tCopyright 2023-{DateTime.Now:yyyy} Felipe Vieira Vendramini \"Konichu\"");
            Console.WriteLine($"\t\tVersion: {Version}");
            Console.WriteLine("\tSome Rights Reserved");
            Console.WriteLine();

            Console.WriteLine("Initializating log factory");

            logger = Log.ForContext<Program>();

            Logger.Initialize("AIServer");

			logger.Information("Starting game server settings...");
            ServerConfiguration.Configuration = new ServerConfiguration(args);
            AbstractDbContext.Configuration = ServerConfiguration.Configuration.Database;

            logger.Information("Initializating realm '{RealmName}'", ServerConfiguration.Configuration.Ai.Name);

            logger.Information("Checking if database '{}' is accessible", ServerConfiguration.Configuration.Database.Schema);
            if (!ServerDbContext.Ping())
            {
                logger.Fatal("Database is inaccessible.");
                return DATABASE_ERROR;
            }
            logger.Information("Database is valid");

            logger.Information("Initializating required services");
            Kernel.Services.Processor = new(cancellationTokenSource.Token);
            Kernel.Services.GeneratorProcessor = new(cancellationTokenSource.Token);
            var tasks = new List<Task>
            {
                Kernel.Services.Randomness.StartAsync(cancellationTokenSource.Token),
                Kernel.Services.Processor.StartAsync(cancellationTokenSource.Token),
                Kernel.Services.GeneratorProcessor.StartAsync(cancellationTokenSource.Token)
            };
            Task.WaitAll(tasks.ToArray());

            if (!await Kernel.InitializeAsync())
            {
                logger.Fatal("Initialization failed!");
                return INITIALIZATION_ERROR;
            }

            await CommandCenterAsync();

            cancellationTokenSource.Cancel();
            await Kernel.StopAsync();
            return NO_ERROR;
        }

        private static async Task<bool> CommandCenterAsync()
        {
            string text;
            do
            {
                text = Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }

                try
                {
                    string[] splitCmd = text.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    switch (splitCmd[0])
                    {
                        case "/version":
                            {
                                logger.Information("Current version: {Version}", Version);
                                break;
                            }

                        case "/processor":
                            {
                                Console.WriteLine(Kernel.Services.Processor.ToString());
                                break;
                            }

                        case "/clear":
                        case "/cls":
                            {
                                Console.Clear();
                                break;
                            }

                        case "/roleids":
                            {
                                // TODO check why monsters stop spawning in certain maps
                                Console.WriteLine($"Monster Remaining IDs: {IdentityManager.Monster.IdentitiesCount()}");
                                break;
                            }

#if DEBUG
                        case "/update_racepotion":
                            {
                                uint[] mapIds =
                                {
                                    1950,2061,2062,2063,2064,2065,2066,2067
                                };
                                KeyValuePair<int, uint>[] monsterTypes =
                                {
                                    new KeyValuePair<int, uint>(500, 2443),  // GuardPotion
                                    new KeyValuePair<int, uint>(500, 2445),  // SluggishPotion
                                    new KeyValuePair<int, uint>(1000, 2446),  // DizzyHammer
                                    new KeyValuePair<int, uint>(500, 2447),  // RestorePotion
                                    new KeyValuePair<int, uint>(1000, 2448),  // ScreamBomb
                                    new KeyValuePair<int, uint>(500, 2449),  // SuperExclamationMark
                                    new KeyValuePair<int, uint>(3500, 2450),  // SuperQuestionMark
                                    new KeyValuePair<int, uint>(500, 2451),  // SpiritPotion
                                    new KeyValuePair<int, uint>(1000, 2452),  // ExcitementPotion
                                    new KeyValuePair<int, uint>(500, 2453),  // ChaosBomb
                                    new KeyValuePair<int, uint>(500, 2454),  // SuperExcitementPotion
                                };

                                int currentRate = 0;
                                Dictionary<int, uint> potions = new Dictionary<int, uint>();
                                foreach (var type in monsterTypes)
                                {
                                    currentRate += type.Key;
                                    potions[currentRate] = type.Value;  
                                }

                                int debugTestSum = monsterTypes.Sum(x => x.Key);
                                logger.Debug($"Sum of potions chance: {debugTestSum}");
                                var generators = await GeneratorRepository.GetAsync();
                                var updatedGens = new List<DbGenerator>();
                                foreach (var gen in generators.Where(x => mapIds.Any(id => id == x.Mapid)))
                                {
                                    int random = await NextAsync(10_000);
                                    foreach (var type in potions.OrderBy(x => x.Key))
                                    {
                                        if (random < type.Key)
                                        {
                                            gen.Npctype = type.Value;
                                            break;
                                        }
                                    }

                                    updatedGens.Add(gen);
                                }
                                await ServerDbContext.SaveRangeAsync(updatedGens);
                                logger.Information($"Generators updated, please restart the server to take effect");
                                break;
                            }
#endif
                    }
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex, "{}", ex.Message);
                }
            }
            while (!"exit".Equals(text));
            return true;
        }
    }
}