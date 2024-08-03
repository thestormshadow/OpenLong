using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.States.User;
using static Long.Module.Peerage.Network.MsgNobility;
using Long.Module.Peerage.Network;
using Long.Kernel.Managers;

namespace Long.Module.Peerage.States
{
    public sealed class Nobility : INobility
    {
        private readonly Character user;

        private NobilityRank rank;

        public Nobility(Character user)
        {
            this.user = user;
        }

        public NobilityRank Rank => user.IsOSUser() ? rank : ModuleManager.NobilityManager.GetRanking(user.Identity);

        public void SetRank(NobilityRank rank)
        {
            if (user.IsOSUser())
            {
                this.rank = rank;
            }
        }

        public int Position => ModuleManager.NobilityManager.GetPosition(user.Identity);

        public ulong Donation
        {
            get => user.Donation;
            set => user.Donation = value;
        }

        public async Task InitializeAsync()
        {
            await BroadcastAsync();
        }

        public async Task BroadcastAsync()
        {
            MsgNobility msg = new()
            {
                Action = NobilityAction.Info,
                DataLow = user.Identity
            };
            msg.Strings.Add($"{user.Identity} {Donation} {(int)Rank:d} {Position}");
			await user.BroadcastRoomMsgAsync(msg, true);
        }
    }
}
