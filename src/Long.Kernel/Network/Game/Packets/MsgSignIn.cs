using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSignIn : MsgBase<GameClient>
    {
        public enum MsgSignInType
        {
            Sign,
            Delay,
            Claim,
            Display,
        }

        public MsgSignInType Action { get; set; }
        public byte RewardAmount { get; set; }
        public ushort CanFillSignInTimes { get; set; }
        public uint SignInDays { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (MsgSignInType)reader.ReadByte(); // 4
            RewardAmount = reader.ReadByte();
            CanFillSignInTimes = reader.ReadUInt16();
            SignInDays = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgSignIn);
            writer.Write((byte)Action); // 4
            writer.Write(RewardAmount);
            writer.Write(CanFillSignInTimes);
            writer.Write(SignInDays);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user.IsOSTravelling())
            {
                return;
            }

            switch (Action)
            {
                case MsgSignInType.Sign:
                    {
                        if (await user.SignIn.SignInAsync())
                        {
                            await user.SignIn.SendAsync();
                        }
                        break;
                    }
                case MsgSignInType.Delay:
                    {
                        if (await user.SignIn.LateSignInAsync())
                        {
                            await user.SignIn.SendAsync();
                        }
                        break;
                    }
                case MsgSignInType.Claim:
                    {
                        if (await user.SignIn.GetAccmulateRewardAsync())
                        {
                            await user.SignIn.SendAsync();
                            await user.SignIn.SaveAsync();
                        }
                        break;
                    }
                case MsgSignInType.Display:
                    {
                        await user.SignIn.SendAsync();
                        break;
                    }
            }
        }
    }
}
