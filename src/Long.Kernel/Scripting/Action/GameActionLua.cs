using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionLuaLinkMainAsync(DbAction action, string param, Character user, Role role, Item item, string[] input)
        {
            string[] splitParams = SplitParam(param);
            string script;

            if (splitParams[0].Equals("LinkMonsterMain"))
            {
                return true;
            }
            if (splitParams.Length > 1)
            {
                script = $"{splitParams[0]}({string.Join(',', splitParams[1..(splitParams.Length)])})";
            }
            else
            {
                script = $"{splitParams[0]}()";
            }
            LuaScriptManager.Run(user, role, item, input, script);
            return true;
        }

        private static async Task<bool> ExecuteActionLuaExecuteAsync(DbAction action, string param, Character user, Role role, Item item, string[] input)
        {
            string[] splitParams = SplitParam(param, 2);
            if (user != null)
            {
                LuaScriptManager.Run(user, role, item, input, $"return {splitParams[0]}({user.Identity})");
            }
            else
            {
                LuaScriptManager.Run(user, role, item, input, $"return {splitParams[0]}({splitParams[1]})");
            }
            return true;
        }
		private static async Task<bool> ExecuteActionNPCLuaExecuteAsync(DbAction action, string param, Character user, Role role, Item item, string[] input)
		{
			string function = string.Empty;
			string[] splitParams = SplitParam(param, 2);
			if (user != null)
			{
				if (input == null || input.Length == 0 || input[0] == "")
				{
					function += $"{splitParams[0]}({user.Identity}";
					for (int i = 1; i < splitParams.Length; i++)
					{
						function += $",{splitParams[i]}";
					}
					LuaScriptManager.Run(user, role, item, input, $"{function})");
				}
                else
                {
                    function += $"{splitParams[0]}({user.Identity}";
                    for (int i = 0; i < input.Length; i++)
                    {
						function += $",{input[i]}";
					}
					LuaScriptManager.Run(user, role, item, input, $"{function})");
				}					
			}
			else
			{
				LuaScriptManager.Run(user, role, item, input, $"{splitParams[0]}({splitParams[1]})");
			}
			return true;
		}
		
	}
}
