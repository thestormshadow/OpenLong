using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text;
using System.Threading.Channels;

namespace Long.Ai.Processors
{
    public class ServerProcessor : BackgroundService
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<ServerProcessor>();

		protected readonly Task[] backgroundTasks;
        protected readonly Channel<Func<Task>>[] channels;
        protected readonly Partition[] partitions;
        protected CancellationToken cancelReads;
        protected CancellationToken cancelWrites;

        public int Count { get; init; }

        public ServerProcessor(CancellationToken cancellationToken)
        {
            Count = 1;

            backgroundTasks = new Task[Count];
            channels = new Channel<Func<Task>>[Count];
            partitions = new Partition[Count];

            cancelReads = cancellationToken;
            cancelWrites = cancellationToken;
        }

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

        protected virtual async Task DequeueAsync(int partition, Channel<Func<Task>> channel)
        {
            while (!cancelReads.IsCancellationRequested)
            {
                Func<Task> action = await channel.Reader.ReadAsync(cancelReads);
                if (action != null)
                {
                    try
                    {
                        await action.Invoke();
                    }
                    catch (Exception ex)
                    {
                        logger.Fatal(ex, "Exception thrown when dequeuing action on partition [{Partition}]\n{Message}", partition, ex.Message);
                    }
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

        public async Task Completion()
        {
            foreach (Channel<Func<Task>> channel in channels)
            {
                if (channel.Reader.Count > 0)
                {
                    await channel.Reader.Completion;
                }
            }
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
