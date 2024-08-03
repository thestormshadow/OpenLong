using Long.Database.Entities;

namespace Long.Kernel.States.Mining
{
    public class MineControl
    {
        private readonly DbMineCtrl mineCtrl;
        private readonly TimeOut timeOut;

        public MineControl(DbMineCtrl ctrl)
        {
            mineCtrl = ctrl;
            timeOut = new TimeOut();
            timeOut.Startup(ctrl.Interval);
        }

        public uint ItemId => mineCtrl.ItemId;
        public uint MapId => mineCtrl.MapId;
        public uint Percent => mineCtrl.Percent;
        public uint Limit => mineCtrl.AmountLimit;

        public async Task<bool> TryPickUpAsync()
        {
            return await ChanceCalcAsync((int)mineCtrl.Percent, 100000);
        }

        public bool IsPickUpAllowed => timeOut.IsTimeOut();

        public void Refresh()
        {
            timeOut.Update();
        }
    }
}
