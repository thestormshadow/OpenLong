using Long.Network.Sockets;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Channels;

namespace Long.Network.Packets
{
    /// <summary>
    ///     Packet processor for handling packets in background tasks using unbounded
    ///     channel. Allows for multiple writers, such as each remote client's accepted socket
    ///     receive loop, to write to an assigned channel. Each reader has an associated
    ///     channel to guarantee client packet processing order.
    /// </summary>
    /// <typeparam name="TClient">Type of client being processed with the packet</typeparam>
    public class PacketProcessor<TClient> : BackgroundService
        where TClient : TcpServerActor
    {
        private static readonly ILogger logger = Log.ForContext<PacketProcessor<TClient>>();

        // Fields and Properties
        protected readonly Task[] ReadBackgroundTasks;
        protected readonly Channel<Message>[] ReadChannels;
        protected readonly Task[] WriteBackgroundTasks;
        protected readonly Channel<Message>[] WriteChannels;
        protected readonly Partition[] ReadPartitions;
        protected readonly Partition[] WritePartitions;
        protected readonly Func<TClient, byte[], Task> Process;
        protected CancellationToken CancelReads;
        protected CancellationToken CancelWrites;

        /// <summary>
        ///     Instantiates a new instance of <see cref="PacketProcessor{TClient}" /> using a default
        ///     amount of worker tasks to initialize. Tasks will not be started.
        /// </summary>
        /// <param name="process">Processing task for channel messages</param>
        /// <param name="readCount">Number of threads to be created</param>
        public PacketProcessor(
            Func<TClient, byte[], Task> process,
            int readCount = 0,
            int writeCount = 0)
        {
            // Initialize the channels and tasks as parallel arrays
            readCount = readCount == 0 ? Math.Max(1, Environment.ProcessorCount / 2) : readCount;
            writeCount = writeCount == 0 ? Math.Max(1, Environment.ProcessorCount / 2) : writeCount;

            CancelReads = new CancellationToken();
            CancelWrites = new CancellationToken();

            ReadBackgroundTasks = new Task[readCount];
            ReadChannels = new Channel<Message>[readCount];
            ReadPartitions = new Partition[readCount];

            WriteBackgroundTasks = new Task[writeCount];
            WriteChannels = new Channel<Message>[writeCount];
            WritePartitions = new Partition[writeCount];

            Process = process;
        }

        /// <summary>
        ///     Triggered when the application host is ready to execute background tasks for
        ///     dequeuing and processing work from unbounded channels. Work is queued by a
        ///     connected and assigned client.
        /// </summary>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (var i = 0; i < ReadBackgroundTasks.Length; i++)
            {
                ReadPartitions[i] = new Partition { ID = (uint)i };
                ReadChannels[i] = Channel.CreateUnbounded<Message>();
                ReadBackgroundTasks[i] = DequeueReadAsync(i, ReadChannels[i]);
            }

            for (var i = 0; i < WriteBackgroundTasks.Length; i++)
            {
                WritePartitions[i] = new Partition { ID = (uint)i };
                WriteChannels[i] = Channel.CreateUnbounded<Message>();
                WriteBackgroundTasks[i] = DequeueWriteAsync(i, WriteChannels[i]);
            }

            return Task.WhenAll(ReadBackgroundTasks);
        }

        /// <summary>
        ///     Queues work by writing to a message channel. Work is queued by a connected
        ///     client, and dequeued by the server's packet processing worker tasks. Each
        ///     work item contains a single packet to be processed.
        /// </summary>
        /// <param name="actor">Actor requesting packet processing</param>
        /// <param name="packet">Packet bytes to be processed</param>
        public void QueueRead(TClient actor, byte[] packet)
        {
            if (!CancelWrites.IsCancellationRequested)
            {
                ReadChannels[actor.ReadPartition].Writer.TryWrite(new Message
                {
                    Actor = actor,
                    Packet = packet
                });
            }
        }

        public void QueueWrite(TClient actor, byte[] packet)
        {
            if (!CancelWrites.IsCancellationRequested)
            {
                WriteChannels[actor.WritePartition].Writer.TryWrite(new Message
                {
                    Actor = actor,
                    Packet = packet
                });
            }
        }

        public void QueueWrite(TClient actor, byte[] packet, Func<Task> task)
        {
            if (!CancelWrites.IsCancellationRequested)
            {
                WriteChannels[actor.WritePartition].Writer.TryWrite(new Message
                {
                    Actor = actor,
                    Packet = packet,
                    Action = task
                });
            }
        }

        /// <summary>
        ///     Dequeues work in a loop. For as long as the thread is running and work is
        ///     available, work will be dequeued and processed. After dequeuing a message,
        ///     the packet processor's <see cref="Process" /> action will be called.
        /// </summary>
        /// <param name="channel">Channel to read messages from</param>
        protected async Task DequeueReadAsync(int channelIndex, Channel<Message> channel)
        {
            while (!CancelReads.IsCancellationRequested)
            {
                try
                {
                    Message msg = await channel.Reader.ReadAsync(CancelReads);
                    if (msg != null)
                    {
                        await Process(msg.Actor, msg.Packet);
                    }
                }
                catch (OperationCanceledException)
                {
                    logger.Information("PacketProcessor read channel {0} graceful shutdown.", channelIndex);
                    break;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error processing packet on channel {0}. {1}", channelIndex, ex.Message);
                }
            }
        }

        protected async Task DequeueWriteAsync(int channelIndex, Channel<Message> channel)
        {
            while (!CancelReads.IsCancellationRequested)
            {
                try
                {
                    Message msg = await channel.Reader.ReadAsync(CancelReads);
                    if (msg != null)
                    {
                        msg.Actor.InternalSend(msg.Packet);
                        if (msg.Action != null)
                        {
                            await msg.Action();
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    logger.Information("PacketProcessor write channel {0} graceful shutdown.", channelIndex);
                    break;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error sending packet on channel {0}. {1}", channelIndex, ex.Message);
                }
            }
        }

        /// <summary>
        ///     Triggered when the application host is stopping the background task with a
        ///     graceful shutdown. Requests that writes into the channel stop, and then reads
        ///     from the channel stop.
        /// </summary>
        public new async Task StopAsync(CancellationToken cancellationToken)
        {
            CancelWrites = new CancellationToken(true);
            CancelReads = new CancellationToken(true);
            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        ///     Selects a partition for the client actor based on partition weight. The
        ///     partition with the least population will be chosen first. After selecting a
        ///     partition, that partition's weight will be increased by one.
        /// </summary>
        public uint SelectReadPartition()
        {
            uint partition = ReadPartitions.Aggregate((aggr, next) => next.Weight.CompareTo(aggr.Weight) < 0 ? next : aggr).ID;
            Interlocked.Increment(ref ReadPartitions[partition].Weight);
            return partition;
        }

        /// <summary>
        ///     Deselects a partition after the client actor disconnects.
        /// </summary>
        /// <param name="partition">The partition id to reduce the weight of</param>
        public void DeselectReadPartition(uint partition)
        {
            Interlocked.Decrement(ref ReadPartitions[partition].Weight);
        }

        /// <summary>
        ///     Selects a partition for the client actor based on partition weight. The
        ///     partition with the least population will be chosen first. After selecting a
        ///     partition, that partition's weight will be increased by one.
        /// </summary>
        public uint SelectWritePartition()
        {
            uint partition = WritePartitions.Aggregate((aggr, next) => next.Weight.CompareTo(aggr.Weight) < 0 ? next : aggr).ID;
            Interlocked.Increment(ref WritePartitions[partition].Weight);
            return partition;
        }

        /// <summary>
        ///     Deselects a partition after the client actor disconnects.
        /// </summary>
        /// <param name="partition">The partition id to reduce the weight of</param>
        public void DeselectWritePartition(uint partition)
        {
            Interlocked.Decrement(ref WritePartitions[partition].Weight);
        }

        /// <summary>
        ///     Defines a message for the <see cref="PacketProcessor{TClient}" />'s unbounded channel
        ///     for queuing packets and actors requesting work. Each message defines a single
        ///     unit of work - a single packet for processing.
        /// </summary>
        protected class Message
        {
            public TClient Actor { get; set; }
            public byte[] Packet { get; set; }
            public Func<Task> Action { get; set; }
        }

        /// <summary>
        ///     Defines a partition for the <see cref="PacketProcessor{TClient}" />. This allows the
        ///     background service to track partition weight and assign clients to less
        ///     populated partitions.
        /// </summary>
        protected class Partition
        {
            public uint ID;
            public int Weight;
        }
    }
}
