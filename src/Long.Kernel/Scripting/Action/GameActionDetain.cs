using Long.Database.Entities;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States;
using static Long.Kernel.Network.Game.Packets.MsgAction;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionUserCheckPkItemAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (param.Equals("target"))
            {
                await user.SendAsync(new MsgAction
                {
                    Action = ActionType.ClientDialog,
                    X = user.X,
                    Y = user.Y,
                    Identity = user.Identity,
                    Data = 336
                });
                return true;
            }

            if (param.Equals("hunter"))
            {
                await user.SendAsync(new MsgAction
                {
                    Action = ActionType.ClientDialog,
                    X = user.X,
                    Y = user.Y,
                    Identity = user.Identity,
                    Data = 337
                });
                return true;
            }

            return false;
        }
    }
}
