using Long.Kernel.Network.Game.Packets;

namespace Long.Kernel.States.User
{
    public partial class Character
    {

        private int actionLastTick = 0;
        private Queue<int> actionTime = new ();

        public bool IsMagicAtkCheat(uint idTarget, int targetX, int targetY, int timestamp)
        {
            Role target = Map?.QueryAroundRole(this, idTarget);
            if (target == null)
            {
                return false;
            }

            if (timestamp - actionLastTick < 100)
            {
                return true;
            }

            const int testSize = 8;
            actionTime.Enqueue(timestamp);

            if (actionTime.Count > testSize)
            {
                actionTime.Dequeue();
            }

            if (actionTime.Count != testSize)
            {
                return false;
            }

            var setActionTime = actionTime.ToArray();
            List<int> deltaTime = new();
            for (int i = 1; i < testSize; i++)
            {
                deltaTime.Add(setActionTime[i] - setActionTime[i - 1]);
            }

            const int suspectTime = 20;
            int suspectCount = 0;
            for (int i = 1; i < testSize - 1; i++)
            {
                var abs = Math.Abs(deltaTime[0] - deltaTime[i]);
                if (abs <= suspectTime)
                {
                    suspectCount++;
                }
            }

            if (suspectCount >= testSize - 2)
            {
                return true;
            }

            if (targetX == 0 || targetY == 0)
            {
                return true;
            }

            return false;
        }

        public async Task DoCheaterPunishAsync()
        {
            const uint cheaterPrison = 6010;
            const ushort posX = 34,
                         posY = 74;

            if (MapIdentity == cheaterPrison)
            {
                return;
            }

            await SavePositionAsync(cheaterPrison, posX, posY);
            await FlyMapAsync(cheaterPrison, posX, posY);
            await SendAsync(new MsgCheatingProgram(Identity, StrCheatingProgram));
        }

    }
}
