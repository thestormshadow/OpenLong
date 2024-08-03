using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Peerage;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.Shared.Helpers;
using SharpCompress;
using System.Drawing;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgChangeName : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Logger.CreateConsoleLogger("chg_name");

        public const int MAX_CHANGES_PERIOD = 5;
        public const int CHANGE_NAME_PERIOD = 60 * 60 * 24 * 365;
        public const int CHANGE_NAME_COST = 810;

        public enum ChangeNameAction : ushort
        {
            Request,
            Success,
            AlreadyInUse,
            QueryInfo,
            NameError
        }

        public ChangeNameAction Action { get; set; }
        public ushort Param1 { get; set; }
        public ushort Param2 { get; set; }
        public string Name { get; set; } = string.Empty;

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (ChangeNameAction)reader.ReadUInt16();
            Param1 = reader.ReadUInt16();
            Param2 = reader.ReadUInt16();
            Name = reader.ReadString(16);
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgChangeName);
            writer.Write((ushort)Action);
            writer.Write(Param1);
            writer.Write(Param2);
            writer.Write(Name, 16);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Action)
            {
                case ChangeNameAction.QueryInfo:
                    {
                        Param1 = (ushort)Math.Max(0, MAX_CHANGES_PERIOD - user.GetChangeNameRemainingAttempts());
                        Param2 = MAX_CHANGES_PERIOD;

                        if (user.Name.Contains("[Z"))
                        {
                            Action = ChangeNameAction.NameError;
                        }

                        await user.SendAsync(this);
                        break;
                    }

                case ChangeNameAction.NameError:
                case ChangeNameAction.Request:
                    {
                        if (!user.Name.Contains("[Z"))
                        {
                            if (user.GetChangeNameRemainingAttempts() == 0)
                            {
                                await user.SendAsync(StrHaveChangeName, TalkChannel.Talk, Color.White);
                                return;
                            }
                        }

                        if (!RoleManager.IsValidName(Name))
                        {
                            await user.SendMenuMessageAsync(StrChangeNameNoChange);
                            return;
                        }

                        if (await UserRepository.ExistsAsync(Name))
                        {
                            Action = ChangeNameAction.AlreadyInUse;
                            await user.SendAsync(this);
                            return;
                        }

                        if (user.ConquerPoints < CHANGE_NAME_COST)
                        {
                            await user.SendMenuMessageAsync(StrChangeNameEmoneyNotEnough);
                            return;
                        }

                        Name = Name.Replace(" ", "~");
                        await ServerDbContext.UpdateAsync(new DbChangeNameBackup
						{
                            IdUser = user.Identity,
                            OldName = user.Name,
                            NewName = Name,
                            ChangeTime = (uint)UnixTimestamp.Now
                        });

                        logger.Information($"{user.Identity},{user.Name},{Name},{CHANGE_NAME_COST}");

                        string broadcastMessage = string.Format(StrChangeName, user.Name, Name);

                        user.ChangeName(Name);
                        await user.SaveAsync();
                        await user.SpendConquerPointsAsync(CHANGE_NAME_COST);

                        await user.Screen.SynchroScreenAsync();

                        Action = ChangeNameAction.Success;
                        await user.SendAsync(this);
                        await RoleManager.BroadcastWorldMsgAsync(broadcastMessage, TalkChannel.Talk, Color.White);
                        //await client.Character.Nobility.SetRank(user, Name);
                        break;
                    }
            }
        }
    }
}
