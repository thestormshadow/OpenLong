namespace Long.Kernel.States
{
    /// <summary>
    ///     Defines account parameters to be transferred from the account server to the game
    ///     server. Account information is supplied from the account database, and used on
    ///     the game server to transfer authentication and authority level.
    /// </summary>
    public class TransferAuthArgs
    {
        public uint AccountID { get; set; }
        public ushort AuthorityID { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string LastLoginAddress { get; set; }
        public string IPAddress { get; set; }
        public int VIPLevel { get; set; }
    }
}
