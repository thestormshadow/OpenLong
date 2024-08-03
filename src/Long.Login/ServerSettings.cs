using Long.Database;
using Long.Network.Security;
using Long.Network.Sockets;
using Microsoft.Extensions.Configuration;

namespace Long.Login
{
    public sealed class ServerSettings
    {
        public ServerSettings(params string[] args)
        {
            new ConfigurationBuilder()
                .AddJsonFile("Config.Login.json")
                .AddCommandLine(args)
                .AddEnvironmentVariables("Login")
                .Build()
                .Bind(this);

            Instance = this;
        }

        public static ServerSettings Instance { get; private set; }

        public DatabaseConfiguration Database { get; set; }
        public Guid[] AllowedServers { get; set; }
        public SocketListenerSettings Login { get; set; }
        public SocketListenerSettings Game { get; set; }

        public class SocketListenerSettings
        {
            public int Port { get; set; }
            public ListenerSettings Listener { get; set; }
            public AesCipher.Settings Encryption { get; set; }
        }
    }
}
