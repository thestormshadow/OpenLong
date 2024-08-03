using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Newtonsoft.Json;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgVipUserHandle : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgVipUserHandle>();

        public int Mode { get; set; }
        public int Location { get; set; }
        public int Countdown { get; set; }
        public string Name { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = reader.ReadInt32();
            Location = reader.ReadInt32();
            Countdown = reader.ReadInt32();
            Name = reader.ReadString();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgVipUserHandle);
            writer.Write(Mode);
            writer.Write(Location);
            writer.Write(Countdown);
            writer.Write(Name);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            const int vipTeleportCountdown = 30;

            Character user = client.Character;

            DbVipTransPoint vipTransPoint = await VipTransPointRepository.GetAsync((uint)Location);
            if (vipTransPoint == null)
            {
                logger.Warning("Invalid Transpoint {0} {1}.\n{2}", Mode, Location, JsonConvert.SerializeObject(this));
                return;
            }

            if (user.Map.IsTeleportDisable() || user.Map.IsPrisionMap())
            {
                return;
            }

            if (Mode == 0) // Self
            {
                if (vipTransPoint.Type == 1)
                {
                    // city
                    if (!user.CanUseVipCityTeleport())
                    {
                        return;
                    }
                    user.UseVipCityPortal();
                }
                else
                {
                    // location
                    if (!user.CanUseVipPortal())
                    {
                        return;
                    }
                    user.UseVipPortal();
                }
            }
            else if (Mode == 1) // Team
            {
                if (user.Team == null || !user.Team.AllowTeamVipTeleport)
                {
                    return;
                }

                if (vipTransPoint.Type == 1)
                {
                    // city
                    if (!user.CanUseVipTeamCityTeleport())
                    {
                        return;
                    }
                    user.UseVipTeamCityPortal();
                }
                else
                {
                    // location
                    if (!user.CanUseVipTeamPortal())
                    {
                        return;
                    }
                    user.UseVipTeamPortal();
                }

                user.Team.SetVipTeleportLocation(vipTeleportCountdown, vipTransPoint);

                await user.Team.SendAsync(new MsgVipUserHandle
                {
                    Mode = 2,
                    Location = Location,
                    Countdown = vipTeleportCountdown,
                    Name = user.Name
                }, user.Identity);
            }
            else if (Mode == 3) // Teammate
            {
                if (user.Team == null)
                {
                    return;
                }

                vipTransPoint = user.Team.GetTransPoint();
                if (vipTransPoint == null)
                {
                    return;
                }
            }

            await user.SavePositionAsync(vipTransPoint.MapId, vipTransPoint.MapX, vipTransPoint.MapY);
            await user.FlyMapAsync(vipTransPoint.MapId, vipTransPoint.MapX, vipTransPoint.MapY);
            await user.SendAsync(this);
        }
    }
}
