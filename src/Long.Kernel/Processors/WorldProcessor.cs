using Long.Kernel.Settings;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace Long.Kernel.Processors
{
    public sealed class WorldProcessor : BackgroundService
    {
        public const int NO_MAP_GROUP = 0;
        public const int PVP_MAP_GROUP = 1;
        public const int NORMAL_MAP_GROUP = 2;

        private static readonly ILogger logger = Log.ForContext<WorldProcessor>();

        private readonly Task[] backgroundTasks;
        private readonly Channel<Func<Task>>[] channels;
        private readonly Partition[] partitions;
        private CancellationToken cancelReads;
        private CancellationToken cancelWrites;

        private WorldProcessor(CancellationToken cancellationToken)
        {
            var serverSettings = GameServerSettings.Instance;
            Count = Math.Min(serverSettings.Game.Processors, Environment.ProcessorCount) + NORMAL_MAP_GROUP;

            backgroundTasks = new Task[Count];
            channels = new Channel<Func<Task>>[Count];
            partitions = new Partition[Count];

            cancelReads = cancellationToken;
            cancelWrites = cancellationToken;
        }

        public static void Create(CancellationToken cancellationToken)
        {
            Instance = new WorldProcessor(cancellationToken);
            _ = Instance.StartAsync(cancellationToken);
        }

        public static WorldProcessor Instance { get; private set; }

        public int Count { get; }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            for (var i = 0; i < Count; i++)
            {
                partitions[i] = new Partition { ID = (uint)i, Weight = 0 };
                channels[i] = Channel.CreateUnbounded<Func<Task>>();
                backgroundTasks[i] = DequeueAsync(i, channels[i]);
            }

            return Task.WhenAll(backgroundTasks);
        }

        public void Queue(int partition, Func<Task> task)
        {
            cancelWrites.ThrowIfCancellationRequested();
            channels[partition].Writer.TryWrite(task);
        }

        private async Task DequeueAsync(int partition, Channel<Func<Task>> channel)
        {
            while (!cancelReads.IsCancellationRequested)
            {
                try
                {
                    Func<Task> action = await channel.Reader.ReadAsync(cancelReads);
                    if (action != null)
                    {
                        await action.Invoke();
                    }
                }
                catch (OperationCanceledException)
                {
                    logger.Information("WorldProcessor process channel {0} graceful shutdown.", partition);
                    break;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "[WorldProcessor] Error processing action on channel {0}. {1}", partition, ex.Message);
                }
            }
        }

        /// <summary>
        ///     Selects a partition for the client actor based on partition weight. The
        ///     partition with the least popluation will be chosen first. After selecting a
        ///     partition, that partition's weight will be increased by one.
        /// </summary>
        public uint SelectPartition()
        {
            uint partition = partitions.Where(x => x.ID >= NORMAL_MAP_GROUP).Aggregate((aggr, next) =>
                                                                            next.Weight.CompareTo(aggr.Weight) < 0
                                                                                ? next
                                                                                : aggr).ID;
            Interlocked.Increment(ref partitions[partition].Weight);
            return partition;
        }

        public void SelectPartition(uint partition)
        {
            Interlocked.Increment(ref partitions[partition].Weight);
        }

        /// <summary>
        ///     Deslects a partition after the client actor disconnects.
        /// </summary>
        /// <param name="partition">The partition id to reduce the weight of</param>
        public void DeselectPartition(uint partition)
        {
            Interlocked.Decrement(ref partitions[partition].Weight);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"Server Processor running with {Count} tasks [Tick Frequency: {Stopwatch.Frequency}]");
            for (int i = 0; i < Count; i++)
            {
                stringBuilder.AppendLine($"Channel {i:00}, Weight: {partitions[i].Weight}");
            }
            return stringBuilder.ToString();
        }

        private class Partition
        {
            public uint ID;
            public int Weight;
        }
    }
}
