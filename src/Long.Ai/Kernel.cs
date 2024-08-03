using Long.Ai.Managers;
using Long.Ai.Processors;
using Long.Ai.Sockets;
using Long.Ai.Threading;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Services;
using Long.Shared.Threads;
using Long.World;
using Serilog;

namespace Long.Ai
{
    public class Kernel
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<Kernel>();

        private Kernel() { }

        private static SchedulerFactory schedulerFactory;
        public static readonly NetworkMonitor NetworkMonitor = new();

        public static async Task<bool> InitializeAsync()
        {
            try
            {
                await MapDataManager.LoadDataAsync().ConfigureAwait(true);
                await MapManager.InitializeAsync().ConfigureAwait(true);
                await RoleManager.InitializeAsync().ConfigureAwait(true);
                await GeneratorManager.InitializeAsync().ConfigureAwait(true);

                schedulerFactory = new SchedulerFactory();
                await schedulerFactory.StartAsync();
                await schedulerFactory.ScheduleAsync<BasicThread>("* * * * * ?");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "{Message}", ex.Message);
                return false;
            }
            return true;
        }

        public static async Task StopAsync()
        {
            for (var i = 5; i >= 0; i--)
            {
                logger.Warning("Server will close in {Seconds} seconds...", i);
                await Task.Delay(1000);
            }

            await schedulerFactory.StopAsync();
        }

        public static class Services
        {
            public static readonly RandomnessService Randomness = new();
            public static ServerProcessor Processor;
            public static GeneratorProcessor GeneratorProcessor;
        }

        #region Random

        public static class RandomServices
        {
            /// <summary>
            ///     Returns the next random number from the generator.
            /// </summary>
            /// <param name="maxValue">One greater than the greatest legal return value.</param>
            public static Task<int> NextAsync(int maxValue)
            {
                return NextAsync(0, maxValue);
            }

            public static Task<double> NextRateAsync(double range)
            {
                return Services.Randomness.NextRateAsync(range);
            }

            /// <summary>Writes random numbers from the generator to a buffer.</summary>
            /// <param name="buffer">Buffer to write bytes to.</param>
            public static Task NextBytesAsync(byte[] buffer)
            {
                return Services.Randomness.NextBytesAsync(buffer);
            }

            /// <summary>
            ///     Returns the next random number from the generator.
            /// </summary>
            /// <param name="minValue">The least legal value for the Random number.</param>
            /// <param name="maxValue">One greater than the greatest legal return value.</param>
            public static Task<int> NextAsync(int minValue, int maxValue)
            {
                return Services.Randomness.NextIntegerAsync(minValue, maxValue);
            }

            public static async Task<bool> ChanceCalcAsync(int chance, int outOf)
            {
                return await NextAsync(outOf) < chance;
            }

            /// <summary>
            ///     Calculates the chance of success based in a rate.
            /// </summary>
            /// <param name="chance">Rate in percent.</param>
            /// <returns>True if the rate is successful.</returns>
            public static async Task<bool> ChanceCalcAsync(double chance)
            {
                const int divisor = 10_000_000;
                const int maxValue = 100 * divisor;
                try
                {
                    return await NextAsync(0, maxValue) <= chance * divisor;
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex, "ChanceCalcAsync(double): {Message}", ex.Message);
                    return false;
                }
            }
        }

        #endregion

        public static class BroadcastService
        {
            public static void BroadcastMsg(IPacket msg)
            {
                if (GameServerHandler.Instance == null)
                {
                    return;
                }

                GameServerHandler.Instance.Send(GameServerHandler.Instance.GameServer, msg.Encode());
            }
        }
    }
}
