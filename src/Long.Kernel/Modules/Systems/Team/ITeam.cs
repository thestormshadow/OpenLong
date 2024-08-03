using Long.Database.Entities;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.User;
using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Modules.Systems.Team
{
    public interface ITeam
    {
        Character Leader { get; }
        ICollection<Character> Members { get; }

        uint TeamId { get; }
        int MemberCount { get; }

        bool JoinEnable { get; set; }
        bool MoneyEnable { get; set; }
        bool ItemEnable { get; set; }
        bool JewelEnable { get; set; }

        bool IsAutoInvite { get; set; }

        bool AllowTeamVipTeleport { get; }
        void SetVipTeleportLocation(int seconds, DbVipTransPoint vipTransPoint);
        DbVipTransPoint GetTransPoint();

        bool Create();
        //Task<bool> InviteAsync(Character target);
        Task<bool> DismissAsync(Character request, bool disconnect = false);
        Task<bool> LeaveTeamAsync(Character user);
        Task<bool> KickMemberAsync(Character leader, uint idTarget);
        Task<bool> EnterTeamAsync(Character target);
        bool IsLeader(uint id);
        bool IsMember(uint id);
        bool IsTeamWithTutor(uint userId, uint tutorId);
        Task SendShowAsync();
        Task SendAsync(IPacket msg, uint exclude = 0);
        Task SendAsync(string message, Color? color = null)
        {
            return SendAsync(new MsgTalk(TalkChannel.Team, color ?? Color.White, message));
        }
        Task BroadcastMemberLifeAsync(Character user, bool maxLife = false);
        Task AwardMemberExpAsync(uint idKiller, Role target, long exp);
        int FamilyBattlePower(Character user, out uint idProvider);
        Task SyncFamilyBattlePowerAsync();
        Task ProcessAuraAsync();
    }
}
