using Long.Ai.Managers;
using Long.Ai.Sockets.Packets;

namespace Long.Ai.States
{
    public sealed class Character : Role
    {
        public Character(uint identity)
        {
            Identity = identity;
        }

        private int mBattlePower;

        public int Metempsychosis { get; set; }
        public override int BattlePower => mBattlePower;
        public override uint MaxLife { get; }
        public override bool IsAlive => QueryStatus(StatusSet.GHOST) == null;
        public int Silvers { get; set; }
        public int ConquerPoints { get; set; }
        public int Nobility { get; set; }
        public int Syndicate { get; set; }
        public int SyndicatePosition { get; set; }
        public int Family { get; set; }
        public int FamilyPosition { get; set; }

        /// <inheritdoc />
        public override bool IsAttackable(Role attacker)
        {
            if (!IsAlive)
                return false;

            if (protectSecs.IsActive() || !protectSecs.IsTimeOut())
                return false;

            return true;
        }

        public async Task<bool> InitializeAsync(MsgAiPlayerLogin msg)
        {
            
            Name = msg.Data.Name;
            Level = (byte)msg.Data.Level;
            Metempsychosis = msg.Data.Metempsychosis;
            StatusFlag1 = msg.Data.Flag1;
            mBattlePower = msg.Data.BattlePower;
            Life = (uint)msg.Data.Life;
            Silvers = msg.Data.Money;
            ConquerPoints = msg.Data.ConquerPoints;
            Nobility = msg.Data.Nobility;
            Syndicate = msg.Data.Syndicate;
            SyndicatePosition = msg.Data.SyndicatePosition;
            Family = msg.Data.Family;
            FamilyPosition = msg.Data.FamilyPosition;
            MapIdentity = msg.Data.MapId;
            X = msg.Data.X;
            Y = msg.Data.Y;

            if ((Map = MapManager.GetMap(msg.Data.MapId)) == null)
                return false;

            await EnterMapAsync(false);
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            await LeaveMapAsync(false);
            return true;
        }

        #region Protect

        private TimeOut protectSecs = new(10);

        public void SetProtection()
        {
            protectSecs.Startup(10);
        }

        public void ClearProtection()
        {
            protectSecs.Clear();
        }

        #endregion
    }
}
