using Long.Database;
using Long.Network.Security;
using Long.Network.Sockets;
using Microsoft.Extensions.Configuration;

namespace Long.Kernel.Settings
{
    public sealed class GameServerSettings
    {
        public GameServerSettings(string fileName, params string[] args)
        {
            new ConfigurationBuilder()
                .AddJsonFile($"Config.{fileName}.json")
                .AddCommandLine(args)
                .AddEnvironmentVariables(fileName)
                .Build()
                .Bind(this);

            Instance = this;
            IsRealm = "Realm".Equals(fileName);
        }

        public static GameServerSettings Instance { get; private set; }
        public static bool IsRealm { get; private set; }

        public bool CooperatorMode { get; set; }
        public GameServer Game { get; set; }
        public LoginClient Login { get; set; }
        public DatabaseConfiguration Database { get; set; }
        public AesCipher.Settings CrossCipher { get; set; }
        public CrossServer Cross { get; set; }
		public AiServer Ai { get; set; }
		public string[] Modules { get; set; }
        public RealmServer Realm { get; set; }

        public class GameServer
        {
            public uint Id { get; set; }
            public Guid Guid { get; set; }
            public string Name { get; set; }
            public string IPAddress { get; set; }
            public int Port { get; set; }
            public int MaxOnlinePlayers { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public DateTime? ReleaseDate { get; set; }
            public int Processors { get; set; } = 1;

            public ListenerSettings Listener { get; set; }
        }

        public class LoginClient
        {
            public string IPAddress { get; set; }
            public int Port { get; set; }
            public AesCipher.Settings Encryption { get; set; }
        }

        public class CrossServer
        {
            public string IPAddress { get; set; }
            public int ListenPort { get; set; }
            public AesCipher.Settings Encryption { get; set; }
        }

		public class AiServer
		{
			public string IPAddress { get; set; }
			public int Port { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
		}

		public class RealmServer
        {
            public string IPAddress { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public List<RealmGameServer> Servers { get; set; }
        }

        public class RealmGameServer
        {
            public uint ServerID { get; set; }
            public string ServerName { get; set; }
            public string IPAddress { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
