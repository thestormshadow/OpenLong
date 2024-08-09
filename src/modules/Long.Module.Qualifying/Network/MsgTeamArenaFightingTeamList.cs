using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Qualifying.Network.States;
using Long.Module.Qualifying.States.TeamQualifier;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgTeamArenaFightingTeamList : MsgBase<GameClient>
    {
        public int Page { get; set; }
        public int Mode { get; set; }
        public int Count { get; set; }
        public int TeamsCount { get; set; }
        public int Unknown20 { get; set; }
        public int PageCount { get; set; }
        public List<TeamFightListStruct> TeamFightList { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Page = reader.ReadInt32();
            Mode = reader.ReadInt32();
            Count = reader.ReadInt32();
            TeamsCount = reader.ReadInt32();
            Unknown20 = reader.ReadInt32();
            PageCount = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgTeamArenaFightingTeamList);
            writer.Write(Page);
            writer.Write(Mode);
            writer.Write(Count);
            writer.Write(TeamsCount);
            //writer.Write(Unknown20);
            writer.Write(TeamFightList.Count);
            foreach (var o in TeamFightList)
            {
                writer.Write(o.LeaderId0);
                writer.Write(o.LeaderName0, 16);
                writer.Write(o.Count0);

                writer.Write(o.LeaderId1);
                writer.Write(o.LeaderName1, 16);
                writer.Write(o.Count1);
            }
            return writer.ToArray();
        }

        public struct TeamFightListStruct
        {
            public uint LeaderId0 { get; set; }
            public string LeaderName0 { get; set; }
            public int Count0 { get; set; }

            public uint LeaderId1 { get; set; }
            public string LeaderName1 { get; set; }
            public int Count1 { get; set; }
        }

        public override Task ProcessAsync(GameClient client)
        {
            MsgTeamArenaFightingTeamList msg = CreateMsg(Page);
            if (msg == null)
            {
                return Task.CompletedTask;
            }

            return client.SendAsync(msg);
        }

        public static MsgTeamArenaFightingTeamList CreateMsg(int page = 0)
        {
            var qualifier = EventManager.GetEvent<TeamArenaQualifier>();
            List<TeamArenaQualifierMatch> fights = qualifier?.QueryMatches((page - 1) * 6, 6);
            if (fights == null)
            {
                return null;
            }

            var msg = new MsgTeamArenaFightingTeamList
            {
                Mode = 6,
                Page = page,
                Count = qualifier.MatchCount,
                TeamsCount = qualifier.TeamsOnQueue
            };

            foreach (TeamArenaQualifierMatch fight in fights)
            {
                msg.TeamFightList.Add(new TeamFightListStruct
                {
                    LeaderId0 = fight.TeamId1,
                    LeaderName0 = fight.Company1.Name,
                    Count0 = fight.Company1.Participants.Count,

                    LeaderId1 = fight.TeamId2,
                    LeaderName1 = fight.Company2.Name,
                    Count1 = fight.Company2.Participants.Count,
                });
            }

            return msg;
        }
    }
}
