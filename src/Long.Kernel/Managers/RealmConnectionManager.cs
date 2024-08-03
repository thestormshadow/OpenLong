using Long.Kernel.Network.Cross.Client;
using Long.Kernel.Network.Cross.Server.Packets;
using Long.Kernel.Settings;
using Long.Network.Packets;
using Long.Network.Packets.Cross;
using Long.Network.Sockets;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public sealed class RealmConnectionManager
    {
        private static readonly ILogger logger = Log.ForContext<RealmConnectionManager>();

        private static readonly ConcurrentDictionary<uint, TcpServerActor> connectedServers = new();

        public static bool NoRealmServer { get; set; }

        /// <summary>
        /// Not null only if not a realm.
        /// </summary>
        public static CrossClientSession RealmSession { get; set; }

        public static bool AddConnection(uint serverId, TcpServerActor actor)
        {
            return connectedServers.TryAdd(serverId, actor);
        }

        public static bool RemoveConnection(uint serverId)
        {
            return connectedServers.TryRemove(serverId, out _);
        }

        public static bool IsServerConnected(uint serverId)
        {
            return connectedServers.TryGetValue(serverId, out var client) && client.Socket.Connected;
        }

        public static Task BroadcastToServersAsync(IPacket msg, uint ignoreServerId = 0)
        {
            return BroadcastToServersAsync(msg.Encode(), ignoreServerId);
        }

        public static async Task BroadcastToServersAsync(byte[] msg, uint ignoreServerId = 0)
        {
            if (!GameServerSettings.IsRealm)
            {
                if (RealmSession?.Actor?.Socket.Connected == true)
                {
                    await RealmSession.Actor.SendAsync(msg);
                }
                return;
            }
            foreach (var server in connectedServers)
            {
                if (server.Key != ignoreServerId)
                {
                    await server.Value.SendAsync(msg);
                }
            }
        }

        public static async Task BroadcastRealmMsgAsync(IPacket msg, uint userID = 0)
        {
            var broadcastMsg = new MsgCrossRedirectUserPacketS(userID, msg.Encode());            
            if (RealmSession?.Actor?.Socket.Connected == true)
            {
                await RealmSession.Actor.SendAsync(broadcastMsg);
            }

            foreach (var server in connectedServers.Values)
            {
                await server.SendAsync(broadcastMsg);
            }
        }

        public static Task SendOSMsgAsync(IPacket msg, uint serverID)
        {
            return SendOSMsgAsync(msg.Encode(), serverID);
        }

        public static Task SendOSMsgAsync(byte[] msg, uint serverID)
        {
            if (RealmSession?.ServerID == serverID)
            {
                return RealmSession.Actor.SendAsync(msg);
            }
            if (connectedServers.TryGetValue(serverID, out var server))
            {
                return server.SendAsync(msg);
            }
#if DEBUG
            logger.Debug("Msg has invalid server ID {0}\n" + PacketDump.Hex(msg), serverID);
#endif
            return Task.CompletedTask;
        }

        public static async Task PingServersAsync()
        {
            if (RealmSession?.Actor != null)
            {
                await RealmSession.Actor.SendAsync(new MsgCrossRealmActionS
                {
                    Data = new()
                    {
                        Action = (uint)CrossRealmAction.Ping,
                        ServerID = RealmManager.ServerIdentity,
                        Command = (uint)UnixTimestamp.Now,
                        Strings = new()
                    }
                });
            }

            foreach (var server in connectedServers.Values)
            {
                await server.SendAsync(new MsgCrossRealmActionS
                {
                    Data = new()
                    {
                        Action = (uint)CrossRealmAction.Ping,
                        ServerID = RealmManager.ServerIdentity,
                        Command = (uint)UnixTimestamp.Now,
                        Strings = new()
                    }
                });
            }
        }

        public static async Task ConnectToRealmAsync()
        {
            logger.Information("Connecting to realm server...");
            var settings = GameServerSettings.Instance.Realm;
            var connection = new CrossClientSession
            {
                ServerID = RealmManager.RealmIdentity,
                Username = settings.Username,
                Password = settings.Password,
            };
            if (!await connection.ConnectToAsync(settings.IPAddress, settings.Port))
            {
                return;
            }
            RealmSession = connection;
        }

        public static async Task ConnectToServerAsync(uint serverId)
        {
            if (!RealmManager.GetServer(serverId, out var server))
            {
                logger.Error("ConnectToServerAsync Server {0} not found.", serverId);
                return;
            }

            if (IsServerConnected(serverId))
            {
                logger.Error("ConnectToServerAsync Server {0} is already connected.", server.ServerName);
                return;
            }

            var settings = GameServerSettings.Instance.Realm;
            GameServerSettings.RealmGameServer gameServer = settings.Servers.FirstOrDefault(x => x.ServerID == serverId);
            if (gameServer == null)
            {
                logger.Error("ConnectToServerAsync Could not find server {0} {1} connect info", server.ServerId, server.ServerName);
                return;
            }

            logger.Information("Connecting to server {0} on {1}:{2}...", server.ServerName);
        }

        public static class RealmGlobalFunctions
        {
            public static Task BroadcastRealmMsgAsync(IPacket msg, uint userID = 0) => RealmConnectionManager.BroadcastRealmMsgAsync(msg, userID);
            public static Task BroadcastToServersAsync(IPacket msg, uint ignoreServerId = 0) => RealmConnectionManager.BroadcastToServersAsync(msg, ignoreServerId);
            public static Task BroadcastToServersAsync(byte[] msg, uint ignoreServerId = 0) => RealmConnectionManager.BroadcastToServersAsync(msg, ignoreServerId);
            public static Task SendOSMsgAsync(byte[] msg, uint serverID) => RealmConnectionManager.SendOSMsgAsync(msg, serverID);
            public static Task SendOSMsgAsync(IPacket msg, uint serverID) => RealmConnectionManager.SendOSMsgAsync(msg, serverID);
        }
    }
}
