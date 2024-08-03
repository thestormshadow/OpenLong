using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class Msg2ndPsw : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<Msg2ndPsw>();

        public PasswordRequestType Action { get; set; }
        public uint Password { get; set; }
        public uint NewPassword { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (PasswordRequestType)reader.ReadInt32();
            Password = reader.ReadUInt32();
            NewPassword = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.Msg2ndPsw);
            writer.Write((int)Action);
            writer.Write(Password);
            writer.Write(NewPassword);
            return writer.ToArray();
        }

        public enum PasswordRequestType
        {
            Unknown,
            VerifyPassword,
            ForgotPassword,
            SetNewPassword,
            SendInformation,
            CorrectPassword,
            WrongPassword
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            switch (Action)
            {
                case PasswordRequestType.VerifyPassword:
                    {
                        if (!user.IsUnlocked() && user.CanUnlock2ndPassword())
                        {
                            if (user.SecondaryPassword != NewPassword)
                            {
                                Action = PasswordRequestType.WrongPassword;
                                await user.SendAsync(this);
                                return;
                            }

                            user.UnlockSecondaryPassword();
                            Action = PasswordRequestType.CorrectPassword;
                            await user.SendAsync(this);
                        }
                        else if (!user.IsUnlocked() && !user.CanUnlock2ndPassword())
                        {
                            Action = PasswordRequestType.SendInformation;
                            await user.SendAsync(this);
                            return;
                        }
                        break;
                    }

                case PasswordRequestType.SetNewPassword:
                    {
                        if (user.SecondaryPassword != 0)
                        {
                            return;
                        }

                        user.SecondaryPassword = NewPassword;
                        await user.SaveAsync();

                        await user.SendAsync(new Msg2ndPsw
                        {
                            Password = 0x0101,
                            Action = PasswordRequestType.CorrectPassword
                        });

                        await user.SendAsync(new Msg2ndPsw
                        {
                            Password = 0x1,
                            Action = PasswordRequestType.CorrectPassword
                        });
                        break;
                    }
                default:
                    {
                        logger.Warning("Invalid Msg2ndPsw:Action ({0})\n{1}", Action, PacketDump.Hex(Encode()));
                        break;
                    }
            }
        }
    }
}
