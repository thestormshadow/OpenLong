using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Scripting.Action
{
	public partial class GameAction
	{
		private static async Task<bool> ExecuteActionMstDropitemAsync(DbAction action, string param, Character user, Role role,	 Item item, params string[] inputs)
		{
			if (role == null || !(role is Monster monster))
				return false;

			string[] splitParam = SplitParam(param, 2);
			if (splitParam.Length < 2)
				return false;

			string ope = splitParam[0];
			uint data = uint.Parse(splitParam[1]);

			var percent = 100;
			if (splitParam.Length >= 3)
				percent = int.Parse(splitParam[2]);

			var flag = 0;
			if (splitParam.Length >= 4)
				flag = int.Parse(splitParam[3]);

			if (ope.Equals("dropitem"))
			{
				int quality = (int)(data % 10);
				if (Item.IsEquipment(data) && quality > 5)
				{
					ServerStatisticManager.DropQualityItem(quality);
				}
				else if (data == Item.TYPE_METEOR)
				{
					ServerStatisticManager.DropMeteor();
				}
				else if (data == Item.TYPE_DRAGONBALL)
				{
					ServerStatisticManager.DropDragonBall();

					if (user != null)
					{
						await monster.SendEffectAsync(user, "darcue");
					}
					else
					{
						await monster.SendEffectAsync("darcue", false);
					}
				}
				else if (Item.IsGem(data))
				{
					ServerStatisticManager.DropGem((Item.SocketGem)(data % 1000));
				}
				await monster.DropItemAsync(data, user, (MapItem.DropMode)flag);
				return true;
			}

			if (ope.Equals("dropmoney"))
			{
				percent %= 100;
				var dwMoneyDrop = (uint)(data * (percent + await NextAsync(100 - percent)) / 100);
				if (dwMoneyDrop <= 0)
					return false;
				uint idUser = user?.Identity ?? 0u;
				await monster.DropMoneyAsync(dwMoneyDrop, idUser, (MapItem.DropMode)flag);
				return true;
			}
			return false;
		}
		private static async Task<bool> ExecuteActionMstTeamRewardAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
		{
			if (user == null)
			{
				return false;
			}

			string[] splitParams = SplitParam(param, 3);
			if (splitParams.Length < 3)
			{
				logger.Warning($"{action.Id} invalid number of params: unknown unknown study_points");
				return false;
			}

			int u0 = int.Parse(splitParams[0]);
			int u1 = int.Parse(splitParams[1]);
			int studyPoints = int.Parse(splitParams[2]);

			if (studyPoints > 0)
			{
				if (user.Team != null)
				{
					foreach (var member in user.Team.Members)
					{
						if (member.Identity != user.Identity)
						{
							if (member.MapIdentity != user.MapIdentity || user.GetDistance(member) > Screen.VIEW_SIZE * 2)
							{
								continue;
							}
						}
						await member.AwardCultivationAsync(studyPoints);
					}
				}
				else
				{
					await user.AwardCultivationAsync(studyPoints);
				}
			}
			return true;
		}
	}
}
