using System.Drawing;

namespace Long.Shared.Mathematics
{
    public static class Calculations
    {
        public static bool IsTimeRange(long start, long end)
        {
            if (start > end)
            {
                return false;
            }

            DateTime now = DateTime.Now;
            if (start > 10000000000) // specific date
            {
                long current = long.Parse(now.ToString("yyyyMMddHHmmss"));
                return current >= start && current < end;
            }
            if (start > 100000000) // period of months
            {
                long current = long.Parse(now.ToString("MMddHHmmss"));
                return current >= start && current < end;
            }
            if (start > 1000000) // period of days
            {
                long current = long.Parse(now.ToString("ddHHmmss"));
                return current >= start && current < end;
            }
            long currentTime = long.Parse(now.ToString("HHmmss"));
            return currentTime >= start && currentTime < end;
        }

        public static int ChangeAdjustRate(int nData, int divideBy)
        {
            if (divideBy == 0)
            {
                return nData;
            }

            if (nData >= ADJUST_PERCENT)
            {
                return ADJUST_PERCENT + nData % ADJUST_PERCENT / divideBy;
            }

            return nData / divideBy;
        }

        public static int AdjustData(int nData, int nAdjust, int nMaxData = 0)
        {
            return AdjustDataEx(nData, nAdjust, nMaxData);
        }

        public static int AdjustDataEx(int nData, int nAdjust, int nMaxData = 0)
        {
            if (nAdjust == ADJUST_FULL)
            {
                return nMaxData;
            }

            if (nAdjust >= ADJUST_PERCENT)
            {
                return MulDiv(nData, nAdjust - ADJUST_PERCENT, 100);
            }

            if (nAdjust <= ADJUST_SET)
            {
                return -1 * nAdjust + ADJUST_SET;
            }

            return nData + nAdjust;
        }

        public static long AdjustData(long nData, long nAdjust, long nMaxData = 0)
        {
            return AdjustDataEx(nData, nAdjust, nMaxData);
        }

        public static long AdjustDataEx(long nData, long nAdjust, long nMaxData)
        {
            if (nAdjust == ADJUST_FULL)
            {
                return nMaxData;
            }

            if (nAdjust >= ADJUST_PERCENT)
            {
                return MulDiv(nData, nAdjust - ADJUST_PERCENT, 100);
            }

            if (nAdjust <= ADJUST_SET)
            {
                return -1 * nAdjust + ADJUST_SET;
            }

            return nData + nAdjust;
        }

        public static int MulDiv(byte number, byte numerator, byte denominator)
        {
            return number * numerator / denominator;
        }

        public static int MulDiv(short number, short numerator, short denominator)
        {
            return number * numerator / denominator;
        }

        public static int MulDiv(ushort number, ushort numerator, ushort denominator)
        {
            return number * numerator / denominator;
        }

        public static int MulDiv(int number, int numerator, int denominator)
        {
            return (int)((long)number * numerator / denominator);
        }

        public static uint MulDiv(uint number, uint numerator, uint denominator)
        {
            return number * numerator / denominator;
        }

        public static long MulDiv(long number, long numerator, long denominator)
        {
            return number * numerator / denominator;
        }

        public static ulong MulDiv(ulong number, ulong numerator, ulong denominator)
        {
            return number * numerator / denominator;
        }

        public static long CutTrail(long x, long y)
        {
            return x >= y ? x : y;
        }

        public static long CutOverflow(long x, long y)
        {
            return x <= y ? x : y;
        }

        public static long CutRange(long n, long min, long max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static int CutTrail(int x, int y)
        {
            return x >= y ? x : y;
        }

        public static int CutOverflow(int x, int y)
        {
            return x <= y ? x : y;
        }

        public static int CutRange(int n, int min, int max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static short CutTrail(short x, short y)
        {
            return x >= y ? x : y;
        }

        public static short CutOverflow(short x, short y)
        {
            return x <= y ? x : y;
        }

        public static short CutRange(short n, short min, short max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static ulong CutTrail(ulong x, ulong y)
        {
            return x >= y ? x : y;
        }

        public static ulong CutOverflow(ulong x, ulong y)
        {
            return x <= y ? x : y;
        }

        public static ulong CutRange(ulong n, ulong min, ulong max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static uint CutTrail(uint x, uint y)
        {
            return x >= y ? x : y;
        }

        public static uint CutOverflow(uint x, uint y)
        {
            return x <= y ? x : y;
        }

        public static uint CutRange(uint n, uint min, uint max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static ushort CutTrail(ushort x, ushort y)
        {
            return x >= y ? x : y;
        }

        public static ushort CutOverflow(ushort x, ushort y)
        {
            return x <= y ? x : y;
        }

        public static ushort CutRange(ushort n, ushort min, ushort max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static byte CutTrail(byte x, byte y)
        {
            return x >= y ? x : y;
        }

        public static byte CutOverflow(byte x, byte y)
        {
            return x <= y ? x : y;
        }

        public static byte CutRange(byte n, byte min, byte max)
        {
            return n < min ? min : n > max ? max : n;
        }

        public static byte GetDirection(int sourceX, int sourceY, int destX, int destY)
        {
            return GetDirection(new Point(sourceX, sourceY), new Point(destX, destY));
        }

        public static byte GetDirection(Point from, Point to)
        {
            var dir = 0;
            int[] tan = { -241, -41, 41, 241 };
            int deltaX = to.X - from.X;
            int deltaY = to.Y - from.Y;

            if (deltaX == 0)
            {
                if (deltaY > 0)
                {
                    dir = 0;
                }
                else
                {
                    dir = 4;
                }
            }
            else if (deltaY == 0)
            {
                if (deltaX > 0)
                {
                    dir = 6;
                }
                else
                {
                    dir = 2;
                }
            }
            else
            {
                int flag = Math.Abs(deltaX) / deltaX;
                int tempY = deltaY * 100 * flag;
                int i;
                for (i = 0; i < 4; i++)
                {
                    tan[i] *= Math.Abs(deltaX);
                }

                for (i = 0; i < 3; i++)
                {
                    if (tempY >= tan[i] && tempY < tan[i + 1])
                    {
                        break;
                    }
                }

                if (deltaX > 0)
                {
                    if (i == 0)
                    {
                        dir = 5;
                    }
                    else if (i == 1)
                    {
                        dir = 6;
                    }
                    else if (i == 2)
                    {
                        dir = 7;
                    }
                    else if (i == 3)
                    {
                        if (deltaY > 0)
                        {
                            dir = 0;
                        }
                        else
                        {
                            dir = 4;
                        }
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        dir = 1;
                    }
                    else if (i == 1)
                    {
                        dir = 2;
                    }
                    else if (i == 2)
                    {
                        dir = 3;
                    }
                    else if (i == 3)
                    {
                        if (deltaY > 0)
                        {
                            dir = 0;
                        }
                        else
                        {
                            dir = 4;
                        }
                    }
                }
            }

            dir = (dir + 8) % 8;
            return (byte)dir;
        }

        /// <summary> This function returns the direction for a jump or attack. </summary>
        /// <param name="x1">The x coordinate of the destination point.</param>
        /// <param name="y1">The y coordinate of the destination point.</param>
        /// <param name="x2">The x coordinate of the reference point.</param>
        /// <param name="y2">The y coordinate of the reference point.</param>
        public static byte GetDirectionSector(int x1, int y1, int x2, int y2)
        {
            double angle = GetAngle(x1, y1, x2, y2);
            var direction = (byte)(Math.Round(angle / 45.0) % 8);
            return (byte)(direction == 8 ? 0 : direction);
        }

        /// <summary> This function returns the angle for a jump or attack. </summary>
        /// <param name="x1">The x coordinate of the destination point.</param>
        /// <param name="y1">The y coordinate of the destination point.</param>
        /// <param name="x2">The x coordinate of the reference point.</param>
        /// <param name="y2">The y coordinate of the reference point.</param>
        public static double GetAngle(double x1, double y1, double x2, double y2)
        {
            // Declare and initialize local variables:
            double angle = Math.Atan2(y2 - y1, x2 - x1) * RADIAN_TO_DEGREE + 90;
            return angle < 0 ? 270 + (90 - Math.Abs(angle)) : angle;
        }

        /// <summary> This function returns the distance between two objects. </summary>
        /// <param name="x1">The x coordinate of the first object.</param>
        /// <param name="y1">The y coordinate of the first object.</param>
        /// <param name="x2">The x coordinate of the second object.</param>
        /// <param name="y2">The y coordinate of the second object.</param>
        public static int GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }

        public static double GetRadian(int sourceX, int sourceY, int targetX, int targetY)
        {
            if (!(sourceX != targetX || sourceY != targetY))
            {
                return 0f;
            }

            int deltaX = targetX - sourceX;
            int deltaY = targetY - sourceY;
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (!(deltaX <= distance && distance > 0))
            {
                return 0f;
            }

            double radian = Math.Asin(deltaX / distance);
            return deltaY > 0 ? Math.PI / 2 - radian : Math.PI + radian + Math.PI / 2;
        }

        public static bool IsInFan(Point center, Point source, Point target, int width, int range)
        {
            if (width <= 0 || width > 360)
            {
                return false;
            }

            if (center.X == source.X && center.Y == source.Y)
            {
                return false;
            }

            if (target.X == source.X && target.Y == source.Y)
            {
                return false;
            }

            if (GetDistance((ushort)center.X, (ushort)center.Y, (ushort)target.X, (ushort)target.Y) > range)
            {
                return false;
            }

            const double pi = 3.1415926535d;
            double fRadianDelta = pi * width / 180d;
            double fCenterLine = GetRadian(center.X, center.Y, source.X, source.Y);
            double fTargetLine = GetRadian(center.X, center.Y, target.X, target.Y);
            double fDelta = Math.Abs(fCenterLine - fTargetLine);
            if (fDelta <= fRadianDelta || fDelta >= 2 * pi - fRadianDelta)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Return all points on that line. (From TQ)
        /// </summary>
        public static void DDALine(int x0, int y0, int x1, int y1, int nRange, ref List<Point> vctPoint)
        {
            if (x0 == x1 && y0 == y1)
            {
                return;
            }

            var scale = (float)(1.0f * nRange / Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0)));
            x1 = (int)(0.5f + scale * (x1 - x0) + x0);
            y1 = (int)(0.5f + scale * (y1 - y0) + y0);
            DDALineEx(x0, y0, x1, y1, ref vctPoint);
        }

        /// <summary>
        ///     Return all points on that line. (From TQ)
        /// </summary>
        public static void DDALineEx(int x0, int y0, int x1, int y1, ref List<Point> vctPoint)
        {
            if (x0 == x1 && y0 == y1)
            {
                return;
            }

            if (vctPoint == null)
            {
                vctPoint = new List<Point>();
            }

            int dx = x1 - x0;
            int dy = y1 - y0;
            int absDx = Math.Abs(dx);
            int absDy = Math.Abs(dy);
            Point point;
            if (absDx > absDy)
            {
                int _0_5 = absDx * (dy > 0 ? 1 : -1);
                int numerator = dy * 2;
                int denominator = absDx * 2;
                if (dx > 0)
                {
                    // x0 ++
                    for (var i = 1; i <= absDx; i++)
                    {
                        point = new Point { X = x0 + i, Y = y0 + (numerator * i + _0_5) / denominator };
                        vctPoint.Add(point);
                    }
                }
                else if (dx < 0)
                {
                    // x0 --
                    for (var i = 1; i <= absDx; i++)
                    {
                        point = new Point { X = x0 - i, Y = y0 + (numerator * i + _0_5) / denominator };
                        vctPoint.Add(point);
                    }
                }
            }
            else
            {
                int _0_5 = absDy * (dx > 0 ? 1 : -1);
                int numerator = dx * 2;
                int denominator = absDy * 2;
                if (dy > 0)
                {
                    // y0 ++
                    for (var i = 1; i <= absDy; i++)
                    {
                        point = new Point { Y = y0 + i, X = x0 + (numerator * i + _0_5) / denominator };
                        vctPoint.Add(point);
                    }
                }
                else if (dy < 0)
                {
                    // y0 -- 
                    for (var i = 1; i <= absDy; i++)
                    {
                        point = new Point { Y = y0 - i, X = x0 + (numerator * i + _0_5) / denominator };
                        vctPoint.Add(point);
                    }
                }
            }
        }

        public const int ADJUST_PERCENT = 30000;       // ADJUSTÊ±£¬>=30000 ±íÊ¾°Ù·ÖÊý
        public const int ADJUST_SET = -30000;          // ADJUSTÊ±£¬<=-30000 ±íÊ¾µÈÓÚ(-1*num - 30000)
        public const int ADJUST_FULL = short.MinValue; // ADJUSTÊ±£¬== -32768 ±íÊ¾ÌîÂú
        public const int DEFAULT_DEFENCE2 = 10000;     // Êý¾Ý¿âÈ±Ê¡Öµ

        public const double RADIAN_TO_DEGREE = 57.29;
    }
}
