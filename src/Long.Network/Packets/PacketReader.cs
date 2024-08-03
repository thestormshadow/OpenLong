using System.Text;

namespace Long.Network.Packets
{
    /// <summary>
    ///     Reader that implements methods for reading bytes from a binary stream reader,
    ///     used to help decode packet structures using TQ Digital's byte ordering rules.
    ///     String processing has been overloaded for supporting TQ's byte-length prefixed
    ///     strings and fixed strings.
    /// </summary>
    public sealed class PacketReader : BinaryReader, IDisposable
    {
        private static readonly Encoding CurrentEncoding = CodePagesEncodingProvider.Instance.GetEncoding(1252);

        /// <summary>
        ///     Instantiates a new instance of <see cref="PacketReader" /> using a supplied
        ///     array of packet bytes. Creates a new binary reader for the derived class
        ///     to read from.
        /// </summary>
        /// <param name="bytes">Packet bytes to be read in</param>
        public PacketReader(byte[] bytes) : base(new MemoryStream(bytes), CurrentEncoding)
        {
        }

        /// <summary>
        ///     Reads a string from the current stream. The string is prefixed with the byte
        ///     length and encoded as an ASCII string. <see cref="EndOfStreamException" /> is
        ///     thrown if the full string cannot be read from the binary reader.
        /// </summary>
        /// <returns>Returns the resulting string from the read.</returns>
        public override string ReadString()
        {
            byte length = ReadByte();
            return ReadString(length).TrimEnd('\0');
        }

        /// <summary>
        ///     Reads a string from the current stream. The string is fixed with a known
        ///     string length before reading from the stream and encoded as an ASCII string.
        ///     <see cref="EndOfStreamException" /> is thrown if the full string cannot be
        ///     read from the binary reader.
        /// </summary>
        /// <param name="fixedLength">Length of the string to be read</param>
        /// <returns>Returns the resulting string from the read.</returns>
        public string ReadString(int fixedLength)
        {
            return CurrentEncoding.GetString(ReadBytes(fixedLength)).TrimEnd('\0');
        }

        /// <summary>
        ///     Reads a list of strings from the current stream. The string list is prefixed
        ///     with the byte amount of strings in the list. Then, each string in the list is
        ///     prefixed with the length of that string and encoded as an ASCII string.
        ///     <see cref="EndOfStreamException" /> is thrown if the full string cannot be read
        ///     from the binary reader.
        /// </summary>
        /// <returns>Returns the resulting list of strings from the read.</returns>
        public List<string> ReadStrings()
        {
            var strings = new List<string>();
            byte amount = ReadByte();
            for (var i = 0; i < amount; i++)
            {
                strings.Add(ReadString());
            }

            return strings;
        }

        #region IDisposable Support

        private bool DisposedValue; // To detect redundant calls

        /// <summary>
        ///     Called from the Dispose method to dispose of class resources once and only
        ///     once using the Disposable design pattern. Calls into the base dispose method
        ///     after disposing of class resources first.
        /// </summary>
        /// <param name="disposing">True if clearing unmanaged and managed resources</param>
        private new void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    BaseStream.Close();
                    BaseStream.Dispose();
                }

                base.Dispose(disposing);
                DisposedValue = true;
            }
        }

        /// <summary>
        ///     Called to dispose the class.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
