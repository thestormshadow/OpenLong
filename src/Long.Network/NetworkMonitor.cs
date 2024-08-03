namespace Long.Network
{
    /// <summary>
    ///     A monitor for the networking I/O. From COPS V6 Enhanced Edition.
    /// </summary>
    public sealed class NetworkMonitor
    {
        private long totalRecvBytes;
        private int totalRecvPackets;

        private long totalSentBytes;
        private int totalSentPackets;

        private int recvBytes;
        private int recvPackets;

        private int sentBytes;
        private int sentPackets;

        private int acceptedSocket5min;
        private int disconnectedSocket5min;
        private int sentBytes5min;
        private int sentPackets5min;
        private int recvBytes5min;
        private int recvPackets5min;

        public int PacketsSent => sentPackets;
        public int PacketsRecv => recvPackets;
        public int BytesSent => sentBytes;
        public int BytesRecv => recvBytes;
        public long TotalPacketsSent => totalSentPackets;
        public long TotalPacketsRecv => totalRecvPackets;
        public long TotalBytesSent => totalSentBytes;
        public long TotalBytesRecv => totalRecvBytes;

        /// <summary>
        ///     Called by the timer.
        /// </summary>
        public string UpdateStats(int interval)
        {
            double download = recvBytes / (double)interval * 8.0 / 1024.0;
            double upload = sentBytes / (double)interval * 8.0 / 1024.0;
            int sent = sentPackets;
            int recv = recvPackets;

            Interlocked.Exchange(ref recvBytes, 0);
            Interlocked.Exchange(ref sentBytes, 0);
            Interlocked.Exchange(ref recvPackets, 0);
            Interlocked.Exchange(ref sentPackets, 0);

            return $"Network(↑{upload:F2} kbps [{sent:0000}], ↓{download:F2} kbps [{recv:0000}])";
        }

        public CurrentInfo GetCurrentInfo(int interval)
        {
            double download = recvBytes / (double)interval * 8.0 / 1024.0;
            double upload = sentBytes / (double)interval * 8.0 / 1024.0;
            int sent = sentPackets;
            int recv = recvPackets;

            Interlocked.Exchange(ref recvBytes, 0);
            Interlocked.Exchange(ref sentBytes, 0);
            Interlocked.Exchange(ref recvPackets, 0);
            Interlocked.Exchange(ref sentPackets, 0);

            return new CurrentInfo
            {
                Download = download,
                Upload = upload,
                SentPackets = sent,
                RecvPackets = recv,

                AcceptedSocket5min = acceptedSocket5min,
                ClosedSocket5min = disconnectedSocket5min,
                BytesRecv5min = recvBytes5min,
                PacketsRecv5min = recvPackets5min,
                BytesSent5min = sentBytes5min,
                PacketsSent5min = sentPackets5min,

                TotalBytesRecv = totalRecvBytes,
                TotalBytesSent = totalSentBytes,
                TotalPacketsRecv = totalRecvPackets,
                TotalPacketsSent = totalSentPackets,
            };
        }

        public void Accept()
        {
            Interlocked.Increment(ref acceptedSocket5min);
        }

        public void Disconnect()
        {
            Interlocked.Increment(ref disconnectedSocket5min);
        }

        public string AnalyticsString(int interval)
        {
            int download = recvBytes5min;
            int upload = sentBytes5min;
            int sent = sentPackets5min;
            int recv = recvPackets5min;
            int accepted = acceptedSocket5min;
            int disconnected = disconnectedSocket5min;

            Interlocked.Exchange(ref recvBytes5min, 0);
            Interlocked.Exchange(ref sentBytes5min, 0);
            Interlocked.Exchange(ref sentPackets5min, 0);
            Interlocked.Exchange(ref recvPackets5min, 0);
            Interlocked.Exchange(ref acceptedSocket5min, 0);
            Interlocked.Exchange(ref disconnectedSocket5min, 0);

            return $"\nNew sockets/5min: {accepted}, Close socket/5min: {disconnected}\n" +
                $"Packet sent/5min: {sent}, Bytes sent/5min: {upload}\n" +
                $"Packet recv/5min: {recv}, Bytes recv/5min: {download}\n";
        }

        /// <summary>
        ///     Signal to the monitor that aLength bytes were sent.
        /// </summary>
        /// <param name="aLength">The number of bytes sent.</param>
        public void Send(int aLength)
        {
            Interlocked.Increment(ref sentPackets);
            Interlocked.Increment(ref totalSentPackets);
            Interlocked.Add(ref sentBytes, aLength);
            Interlocked.Add(ref totalSentBytes, aLength);
            Interlocked.Increment(ref sentPackets5min);
            Interlocked.Add(ref sentBytes5min, aLength);
        }

        /// <summary>
        ///     Signal to the monitor that aLength bytes were received.
        /// </summary>
        /// <param name="aLength">The number of bytes received.</param>
        public void Receive(int aLength)
        {
            Interlocked.Increment(ref recvPackets);
            Interlocked.Increment(ref totalRecvPackets);
            Interlocked.Add(ref recvBytes, aLength);
            Interlocked.Add(ref totalRecvBytes, aLength);
            Interlocked.Increment(ref recvPackets5min);
            Interlocked.Add(ref recvBytes5min, aLength);
        }

        public struct CurrentInfo
        {
            public double Upload { get; set; }
            public double Download { get; set; }
            public int SentPackets { get; set; }
            public int RecvPackets { get; set; }

            public int AcceptedSocket5min { get; set; }
            public int ClosedSocket5min { get; set; }

            public int PacketsSent5min { get; set; }
            public int PacketsRecv5min { get; set; }
            public int BytesSent5min { get; set; }
            public int BytesRecv5min { get; set; }
            public long TotalPacketsSent { get; set; }
            public long TotalPacketsRecv { get; set; }
            public long TotalBytesSent { get; set; }
            public long TotalBytesRecv { get; set; }
        }

    }
}
