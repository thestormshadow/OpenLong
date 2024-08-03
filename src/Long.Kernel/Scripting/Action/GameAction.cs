using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Shared.Helpers;
using Newtonsoft.Json;
using System.Drawing;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static readonly ILogger logger = Log.ForContext<GameAction>();
        private static readonly ILogger missingLogger = Logger.CreateConsoleLogger("missing_action_types");
        private static readonly ILogger missingAction = Logger.CreateConsoleLogger("missing_action");

        public static async Task<bool> ExecuteActionAsync(uint idAction, Character user, Role role, Item item, params string[] inputs)
        {
            const int _MAX_ACTION_I = 64;
            const int _DEADLOCK_CHECK_I = 5;

            if (idAction == 0)
            {
                return false;
            }

            int actionCount = 0;
            int deadLookCount = 0;
            uint idNext = idAction, idOld = idAction;

            while (idNext > 0)
            {
                if (actionCount++ > _MAX_ACTION_I)
                {
                    logger.Error("Error: too many game action, from: {0}, last action: {1}", idAction, idNext);
                    return false;
                }

                if (idAction == idOld && deadLookCount++ >= _DEADLOCK_CHECK_I)
                {
                    logger.Error("Error: dead loop detected, from: {0}, last action: {1}", idAction, idNext);
                    return false;
                }

                if (idNext != idOld)
                {
                    deadLookCount = 0;
                }

                DbAction action = ScriptManager.GetAction(idNext);
                if (action == null)
                {
                    missingAction.Error($"Missinc Action[{idNext}],{FormatLogString(action, null, user, role, item, inputs)}");
                    return false;
                }

                string param = await FormatParamAsync(action, user, role, item, inputs);
                if (user?.IsPm() == true)
                {
                    await user.SendAsync($"{action.Id}: [{action.IdNext},{action.IdNextfail}]. type[{action.Type}], data[{action.Data}], param:[{param}].",
                        TalkChannel.Action,
                        Color.White);
                }

                bool result = false;
                switch ((TaskActionType)action.Type)
                {
                    #region Action

                    case TaskActionType.ActionMenutext:
                        result = await ExecuteActionMenuTextAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMenulink:
                        result = await ExecuteActionMenuLinkAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMenuedit:
                        result = await ExecuteActionMenuEditAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMenupic:
                        result = await ExecuteActionMenuPicAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMenuMessage:
                        result = await ExecuteActionMenuMessageAsync(action, param, user, role, item, inputs);
                        break;
                    case (TaskActionType)113: // TODO find out what this has to do
                        result = true;
                        break;
                    case TaskActionType.ActionMenucreate:
                        result = await ExecuteActionMenuCreateAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionRand:
                        result = await ExecuteActionMenuRandAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionRandaction:
                        result = await ExecuteActionMenuRandActionAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionChktime:
                        result = await ExecuteActionMenuChkTimeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionPostcmd:
                        result = await ExecuteActionPostcmdAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionBrocastmsg:
                        result = await ExecuteActionBrocastmsgAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSysExecAction:
                        result = await ExecuteActionSysExecActionAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionExecutequery:
                        result = await ExecuteActionExecutequeryAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSysDoSomethingUnknown:
                        result = true;
                        break;
                    case TaskActionType.ActionSysInviteFilter:
                        result = await ExecuteActionSysInviteFilterAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSysInviteTrans:
                        result = await ExecuteActionInviteTransAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSysPathFinding:
                        result = await ExecuteActionSysPathFindingAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionVipFunctionCheck:
                        result = await ExecuteActionVipFunctionCheckAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionDynaGlobalData:
                        result = await ExecuteActionDynaGlobalDataAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Npc

                    case TaskActionType.ActionNpcAttr:
                        result = await ExecuteActionNpcAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcErase:
                        result = await ExecuteActionNpcEraseAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcResetsynowner:
                        result = await ExecuteActionNpcResetsynownerAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcFindNextTable:
                        result = await ExecuteActionNpcFindNextTableAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcFamilyCreate:
                        result = await ExecuteActionNpcFamilyCreateAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcFamilyDestroy:
                        result = await ExecuteActionNpcFamilyDestroyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcFamilyChangeName:
                        result = await ExecuteActionNpcFamilyChangeNameAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionNpcChangePos:
                        result = await ExecuteActionNpcChangePosAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Map

                    case TaskActionType.ActionMapMovenpc:
                        result = await ExecuteActionMapMovenpcAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapMapuser:
                        result = await ExecuteActionMapMapuserAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapBrocastmsg:
                        result = await ExecuteActionMapBrocastmsgAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapDropitem:
                        result = await ExecuteActionMapDropitemAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapSetstatus:
                        result = await ExecuteActionMapSetstatusAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapAttrib:
                        result = await ExecuteActionMapAttribAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapRegionMonster:
                        result = await ExecuteActionMapRegionMonsterAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapDropMultiItems:
                        result = await ExecuteActionMapRandDropItemAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapChangeweather:
                        result = await ExecuteActionMapChangeweatherAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapChangelight:
                        result = await ExecuteActionMapChangelightAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapMapeffect:
                        result = await ExecuteActionMapMapeffectAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapFireworks:
                        result = await ExecuteActionMapFireworksAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMapAbleExp:
                        result = await ExecuteActionMapAbleExpAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Item

                    case TaskActionType.ActionItemAdd:
                    case TaskActionType.ActionItemAdd1:
                    case TaskActionType.ActionItemAdd2:
                        result = await ExecuteActionItemAddAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemDel:
                    case TaskActionType.ActionItemCheck2:
                        result = await ExecuteActionItemDelAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemCheck:
                        result = await ExecuteActionItemCheckAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemHole:
                        result = await ExecuteActionItemHoleAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemMultidel:
                        result = await ExecuteActionItemMultidelAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemMultichk:
                        result = await ExecuteActionItemMultichkAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemLeavespace:
                        result = await ExecuteActionItemLeavespaceAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemUpequipment:
                        result = await ExecuteActionItemUpequipmentAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemEquiptest:
                        result = await ExecuteActionItemEquiptestAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemEquipexist:
                        result = await ExecuteActionItemEquipexistAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemEquipcolor:
                        result = await ExecuteActionItemEquipcolorAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemTransform:
                        result = await ExecuteActionItemTransformAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemCheckrand:
                        result = await ExecuteActionItemCheckrandAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemModify:
                        result = await ExecuteActionItemModifyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemDelAll:
                        result = await ExecuteActionItemDelAllAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemJarCreate:
                        result = await ExecuteActionItemJarCreateAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemJarVerify:
                        result = await ExecuteActionItemJarVerifyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemRefineryAdd:
                        result = await ExecuteActionItemRefineryAddAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemSuperFlag:
                        result = await ExecuteActionItemSuperFlagAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemWeaponRChangeSubtype:
                        result = await ExecuteActionItemWeaponRChangeSubtypeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemAddSpecial:
                        result = await ExecuteActionItemAddSpecialAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Lay Item

                    case TaskActionType.ActionItemRequestlaynpc:
                        result = await ExecuteActionItemRequestlaynpcAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemCountnpc:
                        result = await ExecuteActionItemCountnpcAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemLaynpc:
                        result = await ExecuteActionItemLaynpcAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionItemDelthis:
                        result = await ExecuteActionItemDelthisAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Syndicate

                    case TaskActionType.ActionSynCreate:
                        result = await ExecuteActionSynCreateAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynDestroy:
                        result = await ExecuteActionSynDestroyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynSetAssistant:
                        result = await ExecuteActionSynSetAssistantAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynClearRank:
                        result = await ExecuteActionSynClearRankAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynChangeLeader:
                        result = await ExecuteActionSynChangeLeaderAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynAntagonize:
                        result = await ExecuteActionSynAntagonizeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynClearAntagonize:
                        result = await ExecuteActionSynClearAntagonizeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynAlly:
                        result = await ExecuteActionSynAllyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynClearAlly:
                        result = await ExecuteActionSynClearAllyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynAttr:
                        result = await ExecuteActionSynAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSynChangeName:
                        result = await ExecuteActionSynChangeNameAsync(action, param, user, role, item, inputs);
                        break;

					#endregion

					#region Monster Item

					case TaskActionType.ActionMstDropitem:
						result = await ExecuteActionMstDropitemAsync(action, param, user, role, item, inputs);
						break;

					case TaskActionType.ActionMstTeamReward:
						result = await ExecuteActionMstTeamRewardAsync(action, param, user, role, item, inputs);
						break;

					#endregion

					#region User

					case TaskActionType.ActionUserAttr:
                        result = await ExecuteUserAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserFull:
                        result = await ExecuteUserFullAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserChgmap:
                        result = await ExecuteUserChgMapAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserRecordpoint:
                        result = await ExecuteUserRecordpointAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserHair:
                        result = await ExecuteUserHairAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserChgmaprecord:
                        result = await ExecuteUserChgmaprecordAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserChglinkmap:
                        result = await ExecuteActionUserChglinkmapAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTransform:
                        result = await ExecuteUserTransformAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserIspure:
                        result = await ExecuteActionUserIspureAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTalk:
                        result = await ExecuteActionUserTalkAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserMagicEffect:
                        result = await ExecuteActionUserMagicEffectAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserMagic:
                        result = await ExecuteActionUserMagicAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserWeaponskill:
                        result = await ExecuteActionUserWeaponSkillAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserLog:
                        result = await ExecuteActionUserLogAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserBonus:
                        result = await ExecuteActionUserBonusAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserDivorce:
                        result = await ExecuteActionUserDivorceAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserMarriage:
                        result = await ExecuteActionUserMarriageAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserSex:
                        result = await ExecuteActionUserSexAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserEffect:
                        result = await ExecuteActionUserEffectAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTaskmask:
                        result = await ExecuteActionUserTaskmaskAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserMediaplay:
                        result = await ExecuteActionUserMediaplayAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserCreatemap:
                        result = await ExecuteActionUserCreatemapAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserEnterHome:
                        result = await ExecuteActionUserEnterHomeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserEnterMateHome:
                        result = await ExecuteActionUserEnterMateHomeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserUnlearnMagic:
                        result = await ExecuteActionUserUnlearnMagicAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserRebirth:
                        result = await ExecuteActionUserRebirthAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserWebpage:
                        result = await ExecuteActionUserWebpageAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserBbs:
                        result = await ExecuteActionUserBbsAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserUnlearnSkill:
                        result = await ExecuteActionUserUnlearnSkillAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserDropMagic:
                        result = await ExecuteActionUserDropMagicAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserOpenDialog:
                        result = await ExecuteActionUserOpenDialogAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserPointAllot:
                        result = await ExecuteActionUserFixAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserPlusExp:
                        result = await ExecuteActionUserExpMultiplyAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserWhPassword:
                        result = await ExecuteActionUserWhPasswordAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserSetWhPassword:
                        result = await ExecuteActionUserSetWhPasswordAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserOpeninterface:
                        result = await ExecuteActionUserOpeninterfaceAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTaskManager:
                        result = await ExecuteActionUserTaskManagerAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTaskOpe:
                        result = await ExecuteActionUserTaskOpeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTaskLocaltime:
                        result = await ExecuteActionUserTaskLocaltimeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTaskFind:
                        result = await ExecuteActionUserTaskFindAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserVarCompare:
                        result = await ExecuteActionUserVarCompareAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserVarDefine:
                        result = await ExecuteActionUserVarDefineAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserVarCompareString:
                        result = await ExecuteActionUserVarCompareStringAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserVarDefineString:
                        result = await ExecuteActionUserVarDefineStringAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserVarCalc:
                        result = await ExecuteActionUserVarCalcAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTestEquipment:
                        result = await ExecuteActionUserTestEquipmentAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserDailyStcCompare:
                        result = await ExecuteActionUserDailyStcCompareAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserDailyStcOpe:
                        result = await ExecuteActionUserDailyStcOpeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserExecAction:
                        result = await ExecuteActionUserExecActionAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTestPos:
                        result = true;
                        break; // gotta investigate
                    case TaskActionType.ActionUserStcCompare:
                        result = await ExecuteActionUserStcCompareAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserStcOpe:
                        result = await ExecuteActionUserStcOpeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserActionDefine:
                        result = await ExecuteActionUserCustomMsgAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserSelectToData:
                        result = await ExecuteActionUserSelectToDataAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserStcTimeCheck:
                        result = await ExecuteActionUserStcTimeCheckAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserStcTimeOperation:
                        result = await ExecuteActionUserStcTimeOpeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserAttachStatus:
                        result = await ExecuteActionUserAttachStatusAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserGodTime:
                        result = await ExecuteActionUserGodTimeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserCalExp:
                        result = await ExecuteActionUserCalExpAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserLogEvent:
                        result = await ExecuteActionUserLogAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserTimeToExp:
                        result = await ExecuteActionUserExpballExpAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserPureProfessional:
                        result = await ExecuteActionUserPureProfessionalAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSomethingRelatedToRebirth:
                        result = true;
                        break;
                    case TaskActionType.ActionUserStatusCreate:
                        result = await ExecuteActionUserStatusCreateAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserStatusCheck:
                        result = await ExecuteActionUserStatusCheckAsync(action, param, user, role, item, inputs);
                        break;

                    case TaskActionType.ActionUserAwardTitle:
                        result = await ExecuteActionUserAwardTitleAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Team

                    case TaskActionType.ActionTeamBroadcast:
                        result = await ExecuteActionTeamBroadcastAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamAttr:
                        result = await ExecuteActionTeamAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamLeavespace:
                        result = await ExecuteActionTeamLeavespaceAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamItemAdd:
                        result = await ExecuteActionTeamItemAddAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamItemDel:
                        result = await ExecuteActionTeamItemDelAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamItemCheck:
                        result = await ExecuteActionTeamItemCheckAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamChgmap:
                        result = await ExecuteActionTeamChgmapAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamChkIsleader:
                        result = await ExecuteActionTeamChkIsleaderAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionTeamCreateDynamap:
                        result = await ExecuteActionTeamCreateDynamapAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region General User

                    case TaskActionType.ActionFrozenGrottoEntranceChkDays:
                        result = await ExecuteActionFrozenGrottoEntranceChkDaysAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserCheckHpFull:
                        result = await ExecuteActionUserCheckHpFullAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserCheckHpManaFull:
                        result = await ExecuteActionUserCheckHpManaFullAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserRandTrans:
                        result = await ExecuteActionChgMapSquareAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionOpenShop:
                        result = await ExecuteActionOpenShopAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionCheckUserAttributeLimit:
                        result = await ExecuteActionCheckUserAttributeLimitAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionAttachBuffStatus:
                        result = await ExecuteActionAttachBuffStatusAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionDetachBuffStatuses:
                        result = await ExecuteActionDetachBuffStatusesAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserReturn:
                        result = await ExecuteActionUserReturnAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMouseWaitClick:
                        result = await ExecuteActionMouseWaitClickAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMouseJudgeType:
                        result = await ExecuteActionMouseJudgeTypeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMouseClearStatus:
                        result = await ExecuteActionMouseClearStatusAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionMouseDeleteChosen:
                        result = await ExecuteActionMouseDeleteChosenAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionUserDecLife:
                        result = await ExecuteActionUserDecLifeAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionAchievements:
                        result = await ExecuteActionAchievementsAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionAddProcessActivityTask:
                        result = await ExecuteActionAddProcessActivityTaskAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionAddProcessTaskSchedle:
                        result = await ExecuteActionAddProcessTaskSchedleAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSubclassLearn:
                        result = await ExecuteActionSubclassLearnAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSubclassPromotion:
                        result = await ExecuteActionSubclassPromotionAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionSubclassLevel:
                        result = await ExecuteActionSubclassLevelAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionGeneralLottery:
                        result = await ExecuteActionGeneralLotteryAsync(action, param, user, role, item, inputs);
                        break;

					#endregion

					#region Jiang Hu

					case TaskActionType.ActionJiangHuAttributes:
						result = await ExecuteActionJiangHuAttributesAsync(action, param, user, role, item, inputs);
						break;

					case TaskActionType.ActionJiangHuInscribed:
						result = await ExecuteActionJiangHuInscribedAsync(action, param, user, role, item, inputs);
						break;

					case TaskActionType.ActionJiangHuLevel:
						result = await ExecuteActionJiangHuLevelAsync(action, param, user, role, item, inputs);
						break;
					case TaskActionType.ActionJiangHuExpProtection:
						result = await ExecuteActionJiangHuExpProtectionAsync(action, param, user, role, item, inputs);
						break;

					#endregion

					#region Event

					case TaskActionType.ActionEventSetstatus:
                        result = await ExecuteActionEventSetstatusAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventDelnpcGenid:
                        result = await ExecuteActionEventDelnpcGenidAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventCompare:
                        result = await ExecuteActionEventCompareAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventCompareUnsigned:
                        result = await ExecuteActionEventCompareUnsignedAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventChangeweather:
                        result = await ExecuteActionEventChangeweatherAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventCreatepet:
                        result = await ExecuteActionEventCreatepetAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventCreatenewNpc:
                        result = await ExecuteActionEventCreatenewNpcAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventCountmonster:
                        result = await ExecuteActionEventCountmonsterAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventDeletemonster:
                        result = await ExecuteActionEventDeletemonsterAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventBbs:
                        result = await ExecuteActionEventBbsAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventErase:
                        result = await ExecuteActionEventEraseAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventMapUserChgMap:
                        result = await ExecuteActionEventTeleportAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionEventMapUserExeAction:
                        result = await ExecuteActionEventMassactionAsync(action, param, user, role, item, inputs);
                        break;

					#endregion

					#region Trap

					case TaskActionType.ActionTrapCreate:
						result = await ExecuteActionTrapCreateAsync(action, param, user, role, item, inputs);
						break;
					case TaskActionType.ActionTrapErase:
						result = await ExecuteActionTrapEraseAsync(action, param, user, role, item, inputs);
						break;
					case TaskActionType.ActionTrapCount:
						result = await ExecuteActionTrapCountAsync(action, param, user, role, item, inputs);
						break;
					case TaskActionType.ActionTrapAttr:
						result = await ExecuteActionTrapAttrAsync(action, param, user, role, item, inputs);
						break;
					case TaskActionType.ActionTrapTypeDelete:
						result = await ExecuteActionTrapTypeDeleteAsync(action, param, user, role, item, inputs);
						break;

					#endregion

					#region Hunter

					case TaskActionType.ActionUserCheckPkItem:
                        result = await ExecuteActionUserCheckPkItemAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Family

                    case TaskActionType.ActionFamilyAttr:
                        result = await ExecuteActionFamilyAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionFamilyMemberAttr:
                        result = await ExecuteActionFamilyMemberAttrAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionFamilyWarActivityCheck:
                        result = await ExecuteActionFamilyWarActivityCheckAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionFamilyWarAuthorityCheck:
                        result = await ExecuteActionFamilyWarAuthorityCheckAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionFamilyWarRegisterCheck:
                        result = await ActionFamilyWarRegisterCheckAsync(action, param, user, role, item, inputs);
                        break;

					#endregion

					#region Horse Racing

					case TaskActionType.ActionMountRacingEventReset:
						result = await ExecuteActionMountRacingEventResetAsync(action, param, user, role, item, inputs);
						break;

					#endregion

					#region Event Progress Bar

					case TaskActionType.ActionProgressBar:
						await ExecuteActionProgressBarAsync(action, param, user, role, item, inputs);
						return true;

					#endregion

					#region Capture The Flag

					case TaskActionType.ActionCaptureTheFlagCheck:
						result = await ExecuteActionCaptureTheFlagCheckAsync(action, user, role, item, inputs);
						break;

					case TaskActionType.ActionCaptureTheFlagExit:
						result = await ExecuteActionCaptureTheFlagExitAsync(action, user, role, item, inputs);
						break;

					#endregion

					#region Realm

					case TaskActionType.ActionRealmIsOSUser:
                        result = await ExecuteActionRealmIsOSUserAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionRealmTeleport:
                        result = await ExecuteActionRealmTeleportAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionRealmGoToServer:
                        result = await ExecuteActionRealmGoToServerAsync(action, param, user, role, item, inputs);
                        break;
                    case TaskActionType.ActionRealmTeleport2:
                        result = await ExecuteActionRealmTeleport2Async(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    #region Lua

                    case TaskActionType.ActionLuaLinkMain:
                        result = await ExecuteActionLuaLinkMainAsync(action, param, user, role, item, inputs);
                        break;

                    case TaskActionType.ActionLuaExecute:
                        result = await ExecuteActionLuaExecuteAsync(action, param, user, role, item, inputs);
                        break;

                    #endregion

                    default:
                        {
                            missingLogger.Warning($"Missing type {FormatLogString(action, param, user, role, item, inputs)}");
                            break;
                        }
                }

                idOld = idAction;
                idNext = result ? action.IdNext : action.IdNextfail;
            }
            return true;
        }

        #region Unknown Category
        #endregion

        private static string FormatLogString(DbAction action, string param, Character user, Role role, Item item, string[] input)
        {
            List<string> append = new()
            {
                $"ActionType:{action?.Type}"
            };
            if (user != null)
            {
                append.Add($"User:{user.Identity},{user.Name}");
            }
            if (role != null)
            {
                append.Add($"Role:{role.Identity},{role.Name}");
            }
            if (item != null)
            {
                append.Add($"(Item: type {item.Type},{item.Name};id {item.Identity})");
            }
            if (action != null)
            {
                return $"[{string.Join(',', append.ToArray())}] {action.Id}: [{action.IdNext},{action.IdNextfail}]. type[{action.Type}], data[{action.Data}], param:[{param ?? action.Param}][input:{JsonConvert.SerializeObject(input)}].";
            }
            return $"[{string.Join(',', append.ToArray())}]";
        }

        private static async Task<string> FormatParamAsync(DbAction action, Character user, Role role, Item item, string[] input)
        {
            string result = action.Param;

            result = result.Replace("%user_name", user?.Name ?? StrNone)
                .Replace("%user_id", user?.Identity.ToString() ?? "0")
                .Replace("%user_lev", user?.Level.ToString() ?? "0")
                .Replace("%user_mete", user?.Metempsychosis.ToString() ?? "0")
                .Replace("%user_meto", user?.Metempsychosis.ToString() ?? "0")
                .Replace("%user_mate", user?.MateName ?? StrNone)
                .Replace("%user_pro", user?.Profession.ToString() ?? "0")
                .Replace("%user_map_id", user?.Map?.Identity.ToString() ?? "0")
                .Replace("%user_map_name", user?.Map?.Name ?? StrNone)
                .Replace("%user_map_x", user?.X.ToString() ?? "0")
                .Replace("%user_map_y", user?.Y.ToString() ?? "0")
                .Replace("%map_owner_id", user?.Map?.OwnerIdentity.ToString() ?? "0")
                .Replace("%user_nobility_rank", ((int)(user?.NobilityRank ?? 0)).ToString())
                .Replace("%user_nobility_position", user?.Nobility?.Position.ToString() ?? "0")
                .Replace("%account_id", user?.Client?.AccountIdentity.ToString() ?? "0")
                .Replace("%map_owner_id", user?.Map?.OwnerIdentity.ToString() ?? "0")
                .Replace("%last_add_item_id", user?.LastAddItemIdentity.ToString() ?? "0")
                .Replace("%businessman_days", $"{user?.BusinessManDays ?? 0}")
                .Replace("%user_vip", user?.VipLevel.ToString())
                .Replace("%user_home_id", user?.HomeIdentity.ToString() ?? "0");

            for (int i = 0; i < Math.Min(input.Length, 8); i++)
            {
                result = result.Replace($"%accept{i}", input[i]);
            }

            if (user != null)
            {
                while (result.Contains("%stc("))
                {
                    int start = result.IndexOf("%stc(", StringComparison.InvariantCultureIgnoreCase);
                    string strEvent = "", strStage = "";
                    bool comma = false;
                    for (int i = start + 5; i < result.Length; i++)
                    {
                        if (!comma)
                        {
                            if (result[i] == ',')
                            {
                                comma = true;
                                continue;
                            }

                            strEvent += result[i];
                        }
                        else
                        {
                            if (result[i] == ')')
                            {
                                break;
                            }

                            strStage += result[i];
                        }
                    }

                    uint.TryParse(strEvent, out var stcEvent);
                    uint.TryParse(strStage, out var stcStage);

                    DbStatistic stc = user.Statistic.GetStc(stcEvent, stcStage);
                    result = result.Replace($"%stc({strEvent},{strStage})", stc?.Data.ToString() ?? "0");
                }

                while (result.Contains("%stc_daily("))
                {
                    int start = result.IndexOf("%stc_daily(", StringComparison.InvariantCultureIgnoreCase);
                    string strEvent = "", strStage = "";
                    bool comma = false;
                    for (int i = start + 11; i < result.Length; i++)
                    {
                        if (!comma)
                        {
                            if (result[i] == ',')
                            {
                                comma = true;
                                continue;
                            }

                            strEvent += result[i];
                        }
                        else
                        {
                            if (result[i] == ')')
                            {
                                break;
                            }

                            strStage += result[i];
                        }
                    }

                    uint.TryParse(strEvent, out var stcEvent);
                    uint.TryParse(strStage, out var stcStage);

                    DbStatisticDaily stc = user.Statistic.GetDailyStc(stcEvent, stcStage);
                    result = result.Replace($"%stc_daily({strEvent},{strStage})", stc?.Data.ToString() ?? "0");
                }

                while (result.Contains("%iter_var"))
                {
                    for (int i = Role.MAX_VAR_AMOUNT - 1; i >= 0; i--)
                    {
                        result = result.Replace($"%iter_var_data{i}", user.VarData[i].ToString());
                        result = result.Replace($"%iter_var_str{i}", user.VarString[i]);
                    }
                }

                while (result.Contains("%taskdata"))
                {
                    int start = result.IndexOf("%taskdata(", StringComparison.InvariantCultureIgnoreCase);
                    string taskId = "", taskDataIdx = "";
                    bool comma = false;
                    for (int i = start + 10; i < result.Length; i++)
                    {
                        if (!comma)
                        {
                            if (result[i] == ',')
                            {
                                comma = true;
                                continue;
                            }

                            taskId += result[i];
                        }
                        else
                        {
                            if (result[i] == ')')
                            {
                                break;
                            }

                            taskDataIdx += result[i];
                        }
                    }

                    uint.TryParse(taskId, out var evt);
                    int.TryParse(taskDataIdx, out var idx);

                    int value = user.TaskDetail?.GetData(evt, $"data{idx}") ?? 0;
                    result = ReplaceFirst(result, $"%taskdata({evt},{idx})", value.ToString());
                }

                while (result.Contains("%emoney_card1"))
                {
                    int start = result.IndexOf("%emoney_card1(", StringComparison.InvariantCultureIgnoreCase);
                    string cardTypeString = "";
                    for (int i = start + 14; i < result.Length; i++)
                    {
                        if (result[i] == ')')
                        {
                            break;
                        }

                        cardTypeString += result[i];
                    }

                    uint.TryParse(cardTypeString, out var cardType);
                    result = ReplaceFirst(result, $"%emoney_card1({cardType})", "0");
                }

                while (result.Contains("%emoney_card2"))
                {
                    result = ReplaceFirst(result, $"%emoney_card2", "0");
                }
            }

            if (role != null)
            {
                if (role is BaseNpc npc)
                {
                    result = result.Replace("%data0", npc.GetData("data0").ToString())
                        .Replace("%data1", npc.GetData("data1").ToString())
                        .Replace("%data2", npc.GetData("data2").ToString())
                        .Replace("%data3", npc.GetData("data3").ToString())
                        .Replace("%npc_ownerid", npc.OwnerIdentity.ToString())
                        .Replace("%map_owner_id", role.Map.OwnerIdentity.ToString() ?? "0")
                        .Replace("%id", npc.Identity.ToString())
                        .Replace("%npc_x", npc.X.ToString())
                        .Replace("%npc_y", npc.Y.ToString());
                }

                result = result.Replace("%map_owner_id", role.Map?.OwnerIdentity.ToString());
            }

            if (item != null)
            {
                result = result.Replace("%item_data", item.Identity.ToString())
                    .Replace("%item_name", item.Name)
                    .Replace("%item_type", item.Type.ToString())
                    .Replace("%item_id", item.Identity.ToString());
            }

            while (result.Contains("%random"))
            {
                int start = result.IndexOf("%random(", StringComparison.InvariantCultureIgnoreCase);
                string rateStr = "";
                for (int i = start + 8; i < result.Length; i++)
                {
                    if (result[i] == ')')
                    {
                        break;
                    }
                    rateStr += result[i];
                }

                int rate = int.Parse(rateStr);
                result = ReplaceFirst(result, $"%random({rateStr})", (await NextAsync(rate)).ToString());
            }

            while (result.Contains("%global_dyna_data_str"))
            {
                int start = result.IndexOf("%global_dyna_data_str(", StringComparison.InvariantCultureIgnoreCase);
                string strEvent = "", strNum = "";
                bool comma = false;
                for (int i = start + 21; i < result.Length; i++)
                {
                    if (!comma)
                    {
                        if (result[i] == ',')
                        {
                            comma = true;
                            continue;
                        }

                        strEvent += result[i];
                    }
                    else
                    {
                        if (result[i] == ')')
                        {
                            break;
                        }

                        strNum += result[i];
                    }
                }

                uint.TryParse(strEvent, out var evt);
                int.TryParse(strNum, out var idx);

                var data = await DynamicGlobalDataManager.GetAsync(evt);
                string value = DynamicGlobalDataManager.GetStringData(data, idx);
                result = ReplaceFirst(result, $"%global_dyna_data_str({evt},{idx})", value.ToString());
            }

            while (result.Contains("%global_dyna_data"))
            {
                int start = result.IndexOf("%global_dyna_data(", StringComparison.InvariantCultureIgnoreCase);
                string strEvent = "", strNum = "";
                bool comma = false;
                for (int i = start + 18; i < result.Length; i++)
                {
                    if (!comma)
                    {
                        if (result[i] == ',')
                        {
                            comma = true;
                            continue;
                        }

                        strEvent += result[i];
                    }
                    else
                    {
                        if (result[i] == ')')
                        {
                            break;
                        }

                        strNum += result[i];
                    }
                }

                uint.TryParse(strEvent, out var evt);
                int.TryParse(strNum, out var idx);

                var data = await DynamicGlobalDataManager.GetAsync(evt);
                long value = DynamicGlobalDataManager.GetData(data, idx);
                result = ReplaceFirst(result, $"%global_dyna_data({evt},{idx})", value.ToString());
            }

            while (result.Contains("%sysdatastr"))
            {
                int start = result.IndexOf("%sysdatastr(", StringComparison.InvariantCultureIgnoreCase);
                string strEvent = "", strNum = "";
                bool comma = false;
                for (int i = start + 12; i < result.Length; i++)
                {
                    if (!comma)
                    {
                        if (result[i] == ',')
                        {
                            comma = true;
                            continue;
                        }

                        strEvent += result[i];
                    }
                    else
                    {
                        if (result[i] == ')')
                        {
                            break;
                        }

                        strNum += result[i];
                    }
                }

                uint.TryParse(strEvent, out var evt);
                int.TryParse(strNum, out var idx);

                var data = await DynamicGlobalDataManager.GetAsync(evt);
                string value = DynamicGlobalDataManager.GetStringData(data, idx);
                result = ReplaceFirst(result, $"%sysdatastr({evt},{idx})", value);
            }

            while (result.Contains("%sysdata"))
            {
                int start = result.IndexOf("%sysdata(", StringComparison.InvariantCultureIgnoreCase);
                string strEvent = "", strNum = "";
                bool comma = false;
                for (int i = start + 9; i < result.Length; i++)
                {
                    if (!comma)
                    {
                        if (result[i] == ',')
                        {
                            comma = true;
                            continue;
                        }

                        strEvent += result[i];
                    }
                    else
                    {
                        if (result[i] == ')')
                        {
                            break;
                        }

                        strNum += result[i];
                    }
                }

                uint.TryParse(strEvent, out var evt);
                int.TryParse(strNum, out var idx);

                var data = await DynamicGlobalDataManager.GetAsync(evt);
                long value = DynamicGlobalDataManager.GetData(data, idx);
                result = ReplaceFirst(result, $"%sysdata({evt},{idx})", value.ToString());
            }

            if (result.Contains("%iter_upquality_gem"))
            {
                Item pItem = user?.GetEquipment((Item.ItemPosition)user.Iterator);
                if (pItem != null)
                {
                    result = result.Replace("%iter_upquality_gem", pItem.GetUpQualityGemAmount().ToString());
                }
                else
                {
                    result = result.Replace("%iter_upquality_gem", "0");
                }
            }

            if (result.Contains("%iter_itembound"))
            {
                Item pItem = user?.GetEquipment((Item.ItemPosition)user.Iterator);
                if (pItem != null)
                {
                    result = result.Replace("%iter_itembound", pItem.IsBound ? "1" : "0");
                }
                else
                {
                    result = result.Replace("%iter_itembound", "0");
                }
            }

            if (result.Contains("%iter_uplevel_gem"))
            {
                Item pItem = user?.GetEquipment((Item.ItemPosition)user.Iterator);
                if (pItem != null)
                {
                    result = result.Replace("%iter_uplevel_gem", pItem.GetUpgradeGemAmount().ToString());
                }
                else
                {
                    result = result.Replace("%iter_uplevel_gem", "0");
                }
            }

            result = result.Replace("%name", role?.Name ?? StrNone);

            result = result.Replace("%map_name", user?.Map?.Name ?? role?.Map?.Name ?? StrNone)
                .Replace("%iter_time", UnixTimestamp.Now.ToString())
                .Replace("%%", "%")
                .Replace("%last_del_item_id", user?.LastDelItemIdentity.ToString() ?? "0");
            return result;
        }

        private static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return string.Concat(text.AsSpan(0, pos), replace, text.AsSpan(pos + search.Length));
        }

        public static string[] SplitParam(string param, int count = 0)
        {
            return count > 0
                ? param.Split(new[] { ' ' }, count, StringSplitOptions.RemoveEmptyEntries)
                : param.Split(' ');
        }

        private static string GetParenthesys(string szParam)
        {
            int varIdx = szParam.IndexOf("(", StringComparison.CurrentCulture) + 1;
            int endIdx = szParam.IndexOf(")", StringComparison.CurrentCulture);
            return szParam[varIdx..endIdx];
        }

        private static byte VarId(string szParam)
        {
            int start = szParam.IndexOf("%var(", StringComparison.InvariantCultureIgnoreCase);
            string rateStr = "";
            for (int i = start + 5; i < szParam.Length; i++)
            {
                if (szParam[i] == ')')
                {
                    break;
                }
                rateStr += szParam[i];
            }
            return byte.Parse(rateStr);
        }

        public enum TaskActionType
        {
            // System
            ActionSysFirst = 100,
            ActionMenutext = 101,
            ActionMenulink = 102,
            ActionMenuedit = 103,
            ActionMenupic = 104,
            ActionMenuMessage = 105,
            ActionMenubutton = 110,
            ActionMenulistpart = 111,
            ActionMenuTaskClear = 113,
            ActionMenucreate = 120,
            ActionRand = 121,
            ActionRandaction = 122,
            ActionChktime = 123,
            ActionPostcmd = 124,
            ActionBrocastmsg = 125,
            ActionSysExecAction = 126,
            ActionExecutequery = 127,
            ActionSysDoSomethingUnknown = 128,
            ActionSysInviteFilter = 129,
            ActionSysInviteTrans = 130,
            ActionSysPathFinding = 131,
            ActionNoviceTeaching = 137,
            ActionVipFunctionCheck = 144, // data is flag data << 1UL
            ActionDynaGlobalData = 150,
            ActionSysLimit = 199,

            //NPC
            ActionNpcFirst = 200,
            ActionNpcAttr = 201,
            ActionNpcErase = 205,
            ActionNpcModify = 206,
            ActionNpcResetsynowner = 207,
            ActionNpcFindNextTable = 208,
            ActionNpcFamilyCreate = 218,
            ActionNpcFamilyDestroy = 219,
            ActionNpcFamilyChangeName = 220,
            ActionNpcChangePos = 223,
            ActionNpcLimit = 299,

            // Map
            ActionMapFirst = 300,
            ActionMapMovenpc = 301,
            ActionMapMapuser = 302,
            ActionMapBrocastmsg = 303,
            ActionMapDropitem = 304,
            ActionMapSetstatus = 305,
            ActionMapAttrib = 306,
            ActionMapRegionMonster = 307,
            ActionMapDropMultiItems = 308,
            ActionMapChangeweather = 310,
            ActionMapChangelight = 311,
            ActionMapMapeffect = 312,
            ActionMapFireworks = 314,
            ActionMapFireworks2 = 315,
            ActionMapAbleExp = 332,
            ActionMapLimit = 399,

            // Lay item
            ActionItemonlyFirst = 400,
            ActionItemRequestlaynpc = 401,
            ActionItemCountnpc = 402,
            ActionItemLaynpc = 403,
            ActionItemDelthis = 498,
            ActionItemonlyLimit = 499,

            // Item
            ActionItemFirst = 500,
            ActionItemAdd = 501,
            ActionItemDel = 502,
            ActionItemCheck = 503,
            ActionItemHole = 504,
            ActionItemRepair = 505,
            ActionItemMultidel = 506,
            ActionItemMultichk = 507,
            ActionItemLeavespace = 508,
            ActionItemUpequipment = 509,
            ActionItemEquiptest = 510,
            ActionItemEquipexist = 511,
            ActionItemEquipcolor = 512,
            ActionItemTransform = 513,
            ActionItemCheckrand = 516,
            ActionItemModify = 517,
            ActionItemAdd1 = 518,
            ActionItemDelAll = 519,
            ActionItemJarCreate = 528,
            ActionItemJarVerify = 529,
            ActionItemUnequip = 530,
            ActionItemRefineryAdd = 532,
            ActionItemAdd2 = 542,
            ActionItemCheck2 = 543,
            ActionItemSuperFlag = 544,
            ActionItemWeaponRChangeSubtype = 545,
            ActionItemAddSpecial = 550,
            ActionItemLimit = 599,

            // Dyn NPCs
            ActionNpconlyFirst = 600,
            ActionNpconlyCreatenewPet = 601,
            ActionNpconlyDeletePet = 602,
            ActionNpconlyMagiceffect = 603,
            ActionNpconlyMagiceffect2 = 604,
            ActionNpconlyLimit = 699,

            // Syndicate
            ActionSynFirst = 700,
            ActionSynCreate = 701,
            ActionSynDestroy = 702,
            ActionSynSetAssistant = 705,
            ActionSynClearRank = 706,
            ActionSynChangeLeader = 709,
            ActionSynAntagonize = 711,
            ActionSynClearAntagonize = 712,
            ActionSynAlly = 713,
            ActionSynClearAlly = 714,
            ActionSynAttr = 717,
            ActionSynChangeName = 732,
            ActionSynLimit = 799,

            //Monsters
            ActionMstFirst = 800,
            ActionMstDropitem = 801,
            ActionMstTeamReward = 802,
            ActionMstRefinery = 803,
            ActionMstLimit = 899,

            //User
            ActionUserFirst = 1000,
            ActionUserAttr = 1001,
            ActionUserFull = 1002, // Fill the user attributes. param is the attribute name. life/mana/xp/sp
            ActionUserChgmap = 1003, // Mapid Mapx Mapy savelocation
            ActionUserRecordpoint = 1004, // Records the user location, so he can be teleported back there later.
            ActionUserHair = 1005,
            ActionUserChgmaprecord = 1006,
            ActionUserChglinkmap = 1007,
            ActionUserTransform = 1008,
            ActionUserIspure = 1009,
            ActionUserTalk = 1010,
            ActionUserMagicEffect = 1011,
            ActionUserMagic = 1020,
            ActionUserWeaponskill = 1021,
            ActionUserLog = 1022,
            ActionUserBonus = 1023,
            ActionUserDivorce = 1024,
            ActionUserMarriage = 1025,
            ActionUserSex = 1026,
            ActionUserEffect = 1027,
            ActionUserTaskmask = 1028,
            ActionUserMediaplay = 1029,
            ActionUserSupermanlist = 1030,
            ActionUserAddTitle = 1031,
            ActionUserRemoveTitle = 1032,
            ActionUserCreatemap = 1033,
            ActionUserEnterHome = 1034,
            ActionUserEnterMateHome = 1035,
            ActionUserChkinCard2 = 1036,
            ActionUserChkoutCard2 = 1037,
            ActionUserFlyNeighbor = 1038,
            ActionUserUnlearnMagic = 1039,
            ActionUserRebirth = 1040,
            ActionUserWebpage = 1041,
            ActionUserBbs = 1042,
            ActionUserUnlearnSkill = 1043,
            ActionUserDropMagic = 1044,
            ActionUserFixAttr = 1045,
            ActionUserOpenDialog = 1046,
            ActionUserPointAllot = 1047,
            ActionUserPlusExp = 1048,
            ActionUserDelWpgBadge = 1049,
            ActionUserChkWpgBadge = 1050,
            ActionUserTakestudentexp = 1051,
            ActionUserWhPassword = 1052,
            ActionUserSetWhPassword = 1053,
            ActionUserOpeninterface = 1054,
            ActionUserTaskManager = 1056,
            ActionUserTaskOpe = 1057,
            ActionUserTaskLocaltime = 1058,
            ActionUserTaskFind = 1059,
            ActionUserVarCompare = 1060,
            ActionUserVarDefine = 1061,
            ActionUserVarCompareString = 1062,
            ActionUserVarDefineString = 1063,
            ActionUserVarCalc = 1064,
            ActionUserTestEquipment = 1065,
            ActionUserDailyStcCompare = 1067,
            ActionUserDailyStcOpe = 1068,
            ActionUserExecAction = 1071,
            ActionUserTestPos = 1072,
            ActionUserStcCompare = 1073,
            ActionUserStcOpe = 1074,
            ActionUserActionDefine = 1075,
            ActionUserSelectToData = 1077,
            ActionUserStcTimeOperation = 1080,
            ActionUserStcTimeCheck = 1081,
            ActionUserAttachStatus = 1082,
            ActionUserGodTime = 1083,
            ActionUserCalExp = 1084,
            ActionUserLogEvent = 1085,
            ActionUserTimeToExp = 1086,
            ActionUserPureProfessional = 1094,
            ActionSomethingRelatedToRebirth = 1095,
            ActionUserStatusCreate = 1096,
            ActionUserStatusCheck = 1098,

            //User -> Team
            ActionTeamBroadcast = 1101,
            ActionTeamAttr = 1102,
            ActionTeamLeavespace = 1103,
            ActionTeamItemAdd = 1104,
            ActionTeamItemDel = 1105,
            ActionTeamItemCheck = 1106,
            ActionTeamChgmap = 1107,
            ActionTeamChkIsleader = 1501,
            ActionTeamCreateDynamap = 1520,

            ActionFrozenGrottoEntranceChkDays = 1202,
            ActionUserCheckHpFull = 1203,
            ActionUserCheckHpManaFull = 1204,
            // 1205-1215 > Transfer server actions
            ActionIsChangeServerEnable = 1205,
            ActionUserCheckGuide = 1206,
            ActionUserCheckTradeBuddy = 1207,
            ActionUserHasAuctionItem = 1208,
            ActionUserCheckCard = 1209,
            ActionUserHasMail = 1211,
            ActionUserChangeServer = 1212,
            ActionCheckServerName = 1213,
            ActionIsChangeServerIdle = 1214,
            ActionIsAccountServerNormal = 1215,

            // User -> Events???
            ActionElitePKValidateUser = 1301,
            ActionElitePKUserInscribed = 1302,
            ActionElitePKCheck = 1303,

            ActionTeamPKInscribe = 1311,
            ActionTeamPKExit = 1312,
            ActionTeamPKCheck = 1313,
            ActionTeamPKUnknown1314 = 1314,
            ActionTeamPKUnknown1315 = 1315,

            ActionSkillTeamPKInscribe = 1321,
            ActionSkillTeamPKExit = 1322,
            ActionSkillTeamPKCheck = 1323,
            ActionSkillTeamPKUnknown1324 = 1324,
            ActionSkillTeamPKUnknown1325 = 1325,

            // User -> General
            ActionGeneralLottery = 1508,
            ActionUserRandTrans = 1509,
            ActionUserDecLife = 1510,
            ActionOpenShop = 1511,
            ActionSubclassLearn = 1550,
            ActionSubclassPromotion = 1551,
            ActionSubclassLevel = 1552,
            ActionAchievements = 1554,
            ActionAttachBuffStatus = 1555,
            ActionDetachBuffStatuses = 1556,
            ActionUserReturn = 1557, // data = opt ? ; param iterator index to save the value

            ActionMouseWaitClick = 1650, // 发消息通知客户端点选目标,data=后面操作的action_id，param=[鼠标图片id，对应客户端Cursor.ini的记录]
            ActionMouseJudgeType = 1651, // 判断点选目标的类型 data：1表示点npc，param=‘npc名字’;data：2表示点怪物param=‘怪物id’;data：3表示判断点选玩家性别判断param=‘性别id’ 1男，2女
            ActionMouseClearStatus = 1652, // 清除玩家当前指针选取状态 服务器新增清除玩家当前指针选取状态的action，服务器执行该action后，下发消息给客户端
            ActionMouseDeleteChosen = 1654, // 

			/// <summary>
			/// genuineqi set 3                 >= += == set Talent Status
			/// freecultivateparam              >= += set
			/// </summary>
			ActionJiangHuAttributes = 1705,
			ActionJiangHuInscribed = 1706,
			ActionJiangHuLevel = 1707, // data level to check
			ActionJiangHuExpProtection = 1709,  // param "+= 3600" seconds

			ActionAutoHuntIsActive = 1721,
            ActionCheckUserAttributeLimit = 1723,
            ActionAddProcessActivityTask = 1724,
            ActionAddProcessTaskSchedle = 1725, // Increase the progress of staged tasks (data fill task type)

            ActionUserAwardTitle = 1730, // ACTION:   type=1730，param格式"opt type title savetime", opt可选"check  add  del  time", type 称号类型，title为称号ID，savetime为称号时效(分钟)
            ActionIsMoneyPackForbidden = 1740, // 返回玩家是否禁用金币包，禁用返回true，未禁用返回false

            ActionUserLimit = 1999,

            //Events
            ActionEventFirst = 2000,
            ActionEventSetstatus = 2001,
            ActionEventDelnpcGenid = 2002,
            ActionEventCompare = 2003,
            ActionEventCompareUnsigned = 2004,
            ActionEventChangeweather = 2005,
            ActionEventCreatepet = 2006,
            ActionEventCreatenewNpc = 2007,
            ActionEventCountmonster = 2008,
            ActionEventDeletemonster = 2009,
            ActionEventBbs = 2010,
            ActionEventErase = 2011,
            ActionEventMapUserChgMap = 2012,
            ActionEventMapUserExeAction = 2013,

            ActionEventRegister = 2050,
            ActionEventExit = 2051,
            ActionEventCmd = 2052,
            ActionEventLimit = 2099,

            //Traps
            ActionTrapFirst = 2100,
            ActionTrapCreate = 2101,
            ActionTrapErase = 2102,
            ActionTrapCount = 2103,
            ActionTrapAttr = 2104,
            ActionTrapTypeDelete = 2105,
            ActionTrapLimit = 2199,

            // Detained Item
            ActionDetainFirst = 2200,
            ActionUserCheckPkItem = 2205,
            ActionDetainLimit = 2299,

            //Wanted
            ActionWantedFirst = 3000,
            ActionWantedNext = 3001,
            ActionWantedName = 3002,
            ActionWantedBonuty = 3003,
            ActionWantedNew = 3004,
            ActionWantedOrder = 3005,
            ActionWantedCancel = 3006,
            ActionWantedModifyid = 3007,
            ActionWantedSuperadd = 3008,
            ActionPolicewantedNext = 3010,
            ActionPolicewantedOrder = 3011,
            ActionPolicewantedCheck = 3012,
            ActionWantedLimit = 3099,

            // Family
            ActionFamilyFirst = 3500,
            ActionFamilyAttr = 3501,
            ActionFamilyMemberAttr = 3510,
            ActionFamilyWarActivityCheck = 3521,
            ActionFamilyWarAuthorityCheck = 3523,
            ActionFamilyWarRegisterCheck = 3524,
            ActionFamilyLast = 3599,
            
			ActionMountRacingEventReset = 3601,

			// Progress
			ActionProgressBar = 3701,

			ActionCaptureTheFlagCheck = 3901,
            ActionCaptureTheFlagExit = 3902,

            //Magic
            ActionMagicFirst = 4000,
            ActionMagicAttachstatus = 4001,
            ActionMagicAttack = 4002,
            ActionMagicLimit = 4099,

            ActionRealmIsOSUser = 9001,
            ActionRealmTeleport = 9002,
            ActionRealmGoToServer = 9005,
            ActionRealmTeleport2 = 9006,


            ActionLuaLinkMain = 20001,
            ActionLuaExecute = 20002,
        }

        public enum OpenWindow
        {
            Compose = 1,
            Craft = 2,
            Warehouse = 4,
            ClanWindow = 64,
            DetainRedeem = 336,
            DetainClaim = 337,
            VipWarehouse = 341,
            Breeding = 368,
            PurificationWindow = 455,
            StabilizationWindow = 459,
            TalismanUpgrade = 347,
            GemComposing = 422,
            OpenSockets = 425,
            Blessing = 426,
            TortoiseGemComposing = 438,
            HorseRacingStore = 464,
            EditCharacterName = 489,
            GarmentTrade = 502,
            DegradeEquipment = 506,
            VerifyPassword = 568,
            SetNewPassword = 569,
            ModifyPassword = 570,
            BrowseAuction = 572,
            EmailInbox = 576,
            EmailIcon = 578,
            GiftRanking = 584,
            FriendRequest = 606,
            JiangHuJoinIcon = 618
        }
    }
}
