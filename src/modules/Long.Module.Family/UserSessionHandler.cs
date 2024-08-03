using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using System.Drawing;
using static Long.Kernel.StrRes;

namespace Long.Module.Family
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public async Task OnUserLoginCompleteAsync(Character user)
        {
            user.Family = ModuleManager.FamilyManager.FindByUser(user.Identity);
            if (user.Family == null)
            {
                if (user.MateIdentity != 0)
                {
                    IFamily family = ModuleManager.FamilyManager.FindByUser(user.MateIdentity);
                    IFamilyMember mateFamily = family?.GetMember(user.MateIdentity);
                    if (mateFamily == null || mateFamily.Rank == IFamily.FamilyRank.Spouse)
                    {
                        return;
                    }

                    if (!await family.AppendMemberAsync(null, user, IFamily.FamilyRank.Spouse))
                    {
                        return;
                    }
                }
            }
            else
            {
                await user.Family.SendFamilyAsync(user);
                await user.Family.SendRelationsAsync(user);
            }

            if (user.Family == null)
            {
                return;
            }

            var war = ModuleManager.FamilyManager.FamilyWar;
            if (war == null)
            {
                return;
            }

            if (user.Family.Challenge != 0)
            {
                GameMap map = MapManager.GetMap(user.Family.Challenge);
                if (map == null)
                {
                    return;
                }

                await user.SendAsync(string.Format(StrPrepareToChallengeFamilyLogin, map.Name), TalkChannel.Talk, Color.White);
            }

            if (user.Family.Occupy != 0)
            {
                GameMap map = MapManager.GetMap(user.Family.Occupy);
                if (map == null)
                {
                    return;
                }

                if (war.GetChallengersByNpc(user.Family.Occupy).Count == 0)
                {
                    return;
                }

                await user.SendAsync(string.Format(StrPrepareToDefendFamilyLogin, map.Name), TalkChannel.Talk, Color.White);
            }
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
