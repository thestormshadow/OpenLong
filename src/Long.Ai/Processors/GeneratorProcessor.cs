using Long.Ai.Managers;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text;
using System.Threading.Channels;

namespace Long.Ai.Processors
{
    public class GeneratorProcessor : BackgroundService
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<GeneratorProcessor>();

        protected readonly Task[] backgroundTasks;
        protected readonly Channel<Func<Task>>[] channels;
        protected readonly Partition[] partitions;
        protected CancellationToken cancelReads;
        protected CancellationToken cancelWrites;

        public int Count { get; init; }

        public GeneratorProcessor(CancellationToken cancellationToken)
        {
            Count = 5;

            backgroundTasks = new Task[Count];
            partitions = new Partition[Count];

            cancelReads = cancellationToken;
            cancelWrites = cancellationToken;
        }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            for (var i = 0; i < Count; i++)
            {
                partitions[i] = new Partition
                {
                    ID = (uint)i
                };
                backgroundTasks[i] = ProcessAsync(i);
            }

            return Task.WhenAll(backgroundTasks);
        }

        protected virtual async Task ProcessAsync(int partition)
        {
            while (!cancelReads.IsCancellationRequested)
            {
                try
                {
                    await GeneratorManager.OnTimerAsync(partition);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error on process generator. {}", ex.Message);
                }
                finally
                {
                    await Task.Delay(500);
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
            uint partition = partitions.Aggregate((aggr, next) => next.Weight.CompareTo(aggr.Weight) < 0
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
            stringBuilder.AppendLine($"Server Processor running with {Count} tasks");
            for (int i = 0; i < Count; i++)
            {
                stringBuilder.AppendLine($"Channel {i:00}, Queued: {channels[i].Reader.Count}, Weight: {partitions[i].Weight}");
            }
            return stringBuilder.ToString();
        }

        protected class Partition
        {
            public uint ID;
            public int Weight;
        }
    }
}
