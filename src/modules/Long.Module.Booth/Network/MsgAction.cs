using Long.Kernel.States.User;
using static Long.Kernel.StrRes;

namespace Long.Module.Booth.Network
{
    public static class MsgAction
    {
        public static async Task ProcessAsync(Kernel.Network.Game.Packets.MsgAction msg, Character user)
        {
            switch (msg.Action)
            {
                case Kernel.Network.Game.Packets.MsgAction.ActionType.BoothSpawn:
                    {
                        if (user.Booth != null)
                        {
                            await user.Booth.LeaveMapAsync();
                            user.Booth = null;
                            return;
                        }

                        if (user.Map?.IsBoothEnable() != true) 
                        {
                            await user.SendAsync(StrBoothRegionCantSetup);
                            return;
                        }

                        States.Booth booth;
                        user.Booth = booth = new States.Booth(user);
                        if (!await user.Booth.InitializeAsync())
                        {
                            return;
                        }

                        await user.Booth.EnterMapAsync();
                        await user.SetActionAsync(Kernel.States.Role.EntityAction.Sit);

                        msg.Command = booth.Identity;
                        msg.X = booth.X;
                        msg.Y = booth.Y;
                        await user.SendAsync(msg);
                        break;
                    }

                case Kernel.Network.Game.Packets.MsgAction.ActionType.BoothLeave:
                    {
                        if (user.Booth == null)
                        {
                            return;
                        }

                        await user.Booth.LeaveMapAsync();
                        await user.SetActionAsync(Kernel.States.Role.EntityAction.Stand);
                        await user.Screen.SynchroScreenAsync();
                        user.Booth = null;
                        break;
                    }
            }
        }
    }
}
