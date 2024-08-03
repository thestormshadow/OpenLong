using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.Mining;
using Long.Kernel.States.User;

namespace Long.Kernel.Managers
{
    public class MineManager
    {
        private static readonly ILogger logger = Log.ForContext<MineManager>();
        private static readonly List<MineControl> mMineControl = new();

        public static async Task<bool> InitializeAsync()
        {
            logger.Information("Initializing Mine Controller");

            foreach (DbMineCtrl ctrl in await MineCtrlRepository.GetAsync())
            {
                if (ctrl.ItemId == 0)
                {
                    continue;
                }

                DbItemtype it = ItemManager.GetItemtype(ctrl.ItemId);
                if (it == null)
                {
                    logger.Warning($"Could not find {ctrl.ItemId} for mining {ctrl.Id}");
                    continue;
                }

                mMineControl.Add(new MineControl(ctrl));
            }

            return true;
        }

        public static async Task<uint> MineAsync(uint mapId, Character target)
        {
            IOrderedEnumerable<MineControl> mapPool = mMineControl.Where(x => x.MapId == mapId).OrderBy(x => x.Percent);
            foreach (MineControl ctrl in mapPool)
            {
                if (ctrl.IsPickUpAllowed && await ctrl.TryPickUpAsync())
                {
                    if (target.UserPackage.MultiCheckItem(ctrl.ItemId, ctrl.ItemId,
                                                          (int)ctrl.Limit)) // user has reached limit
                    {
                        continue;
                    }

                    ctrl.Refresh();
                    return ctrl.ItemId;
                }
            }
            return 0;
        }
    }
}
