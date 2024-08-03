using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Module.Trade.Network;
using Long.Shared;

namespace Long.Module.Trade.States
{
    public sealed class TradeBuddy
    {
        private readonly Character user;
        private readonly DbBusiness business;

        public TradeBuddy(Character user, DbBusiness business)
        {
            this.user = user;
            this.business = business;
        }

        public async Task<bool> InitializeAsync()
        {
            uint targetUserId = user.Identity == business.UserId ? business.BusinessId : business.UserId;
            DbUser targetUser = await UserRepository.FindByIdentityAsync(targetUserId);
            if (targetUser == null)
            {
                return false;
            }

            TargetId = targetUser.Identity;
            TargetMesh = targetUser.Mesh;
            TargetName = targetUser.Name;
            TargetLevel = targetUser.Level;
            return true;
        }

        public void Initialize(Character targetUser)
        {
            TargetId = targetUser.Identity;
            TargetMesh = targetUser.Mesh;
            TargetName = targetUser.Name;
            TargetLevel = targetUser.Level;
        }

        public uint TargetId { get; private set; }
        public uint TargetMesh { get; private set; }
        public string TargetName { get; private set; }
        public byte TargetLevel { get; private set; }

        public int RemainingMinutes => (int)(!IsValidPartnership() ? (UnixTimestamp.ToDateTime(business.Date) - DateTime.Now).TotalMinutes : 0);

        public Character Target => RoleManager.GetUser(TargetId);

        public Task SendStatusAsync()
        {
            return user.SendAsync(new MsgTradeBuddy
            {
                Name = TargetName,
                Action = MsgTradeBuddy.TradeBuddyAction.AddPartner,
                IsOnline = Target != null,
                HoursLeft = RemainingMinutes,
                Identity = TargetId,
                Level = ((ushort)(TargetMesh % 10000000 / 10000))
            });
        }

        public Task SendStatusAsync(bool isOnline)
        {
            return user.SendAsync(new MsgTradeBuddy
            {
                Name = TargetName,
                Action = isOnline ? MsgTradeBuddy.TradeBuddyAction.SetUserOnline : MsgTradeBuddy.TradeBuddyAction.SetUserOffline,
                IsOnline = isOnline,
                HoursLeft = RemainingMinutes,
                Identity = TargetId,
                Level = ((ushort)(TargetMesh % 10000000 / 10000))
            });
        }

        public bool IsValidPartnership()
        {
            return UnixTimestamp.ToDateTime(business.Date) < DateTime.Now;
        }

        public static implicit operator DbBusiness(TradeBuddy tradeBuddy)
        {
            return tradeBuddy.business;
        }
    }
}
