using Long.Kernel.Managers;
using Long.Kernel.Network.Cross.Server.Packets;
using Long.Kernel.Network.Game.Packets;
using Long.Network.Packets;
using Long.Network.Packets.Cross;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        #region OS

        public RealmManager.ServerConfig OriginServer { get; set; }
        public RealmManager.ServerConfig? CurrentServer { get; set; }

        public bool IsOSUser()
        {
            return OriginServer.ServerId != RealmManager.ServerIdentity;
        }

        public bool IsOSTravelling()
        {
            return CurrentServer.HasValue && CurrentServerID != RealmManager.ServerIdentity;
        }

        public uint OriginalUserId { get; init; }
        public uint RealmUserId { get; set; }

        public uint ServerID => OriginServer.ServerId;
        public uint CurrentServerID => CurrentServer?.ServerId ?? OriginServer.ServerId;
        public string CurrentServerName => CurrentServer?.ServerName ?? OriginServer.ServerName;

        /// <remarks>Must be called at ORIGIN SERVER.</remarks>
        public async Task EnterServerAsync(uint serverID, CrossRequestSwitchExPB response)
        {
            RealmManager.GetServer(serverID, out var config);
            CurrentServer = config;

            RealmUserId = response.NewUserId;

            await LeaveMapAsync();
            await SendAsync(new MsgCrossSwitch
            {
                Action = MsgCrossSwitch.CrossAction.Enter,
                Count = 0,
                ItemInfos = new List<MsgCrossSwitch.CrossItemInfo>(),
                PlayerID = Identity,
                NewUserId = response.NewUserId
            });
            await SendAsync(new MsgName
            {
                Action = MsgName.StringAction.CurrServerName,
                Identity = Identity,
                Strings = new List<string>()
                {
                    CurrentServerName
                }
            });
            
            await OnEnterOSAsync(this);
        }

        public async Task ReturnServerAsync()
        {
            await SendAsync(new MsgCrossSwitch
            {
                PlayerID = RealmUserId,
                Action = MsgCrossSwitch.CrossAction.Enter,
                NewUserId = Identity,
                Count = 0,
                ItemInfos = new List<MsgCrossSwitch.CrossItemInfo>()
            });
            await SendAsync(new MsgCrossSwitch
            {
                PlayerID = Identity,
                Action = MsgCrossSwitch.CrossAction.Exit,
                NewUserId = Identity,
                Count = 0,
                ItemInfos = new List<MsgCrossSwitch.CrossItemInfo>()
            });
            await FlyMapAsync(MapIdentity, X, Y);
            await SendAsync(new MsgName
            {
                Action = MsgName.StringAction.CurrServerName,
                Identity = Identity,
                Strings = new List<string>()
                {
                    CurrentServerName
                }
            });

            await SetPkModeAsync(PkModeType.Capture);

            if (Union != null)
            {
                await SendUnionAsync();
            }

            await OnLeaveOSAsync(this);

            CurrentServer = null;
            RealmUserId = 0;
        }

        public Task SendCrossMsgAsync(IPacket msg)
        {
            return SendCrossMsgAsync(msg.Encode());
        }

        public Task SendCrossMsgAsync(byte[] msg)
        {
            uint serverId;
            uint userId;
            if (OriginServer.ServerId != RealmManager.ServerIdentity)
            {
                serverId = OriginServer.ServerId;
                userId = OriginalUserId;
            }
            else if (CurrentServer.HasValue)
            {
                serverId = CurrentServer.Value.ServerId;
                userId = RealmUserId;
            }
            else
            {
                return Task.CompletedTask;
            }
            return SendOSMsgAsync(new MsgCrossRedirectUserPacketS(userId, msg), serverId);
        }

        #endregion
    }
}
