using System.Drawing;
using static System.Math;

namespace Long.Shared.Mathematics
{
    public class Bresenham
    {
        private Bresenham() { }

        public static List<Point> Calculate(int x0, int y0, int x1, int y1)
        {
            List<Point> points = new();

            int dx = Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2; /* error value e_xy */

            for (; ; )
            {  /* loop */
                points.Add(new Point(x0, y0));
                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                e2 = 2 * err;
                if (e2 >= dy) { err += dy; x0 += sx; } /* e_xy+e_x > 0 */
                if (e2 <= dx) { err += dx; y0 += sy; } /* e_xy+e_y < 0 */
            }

            return points;
        }

        public static List<Point> CalculateThick(int x0, int y0, int x1, int y1, double wd)
        {
            int dx = Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx - dy, e2, x2, y2;                          /* error value e_xy */
            double ed = dx + dy == 0 ? 1 : Sqrt((float)dx * dx + (float)dy * dy);
            List<Point> points = new();
            for (wd = (wd + 1) / 2; ;)
            {                                   /* pixel loop */
                points.Add(new Point(x0, y0));
                e2 = err; x2 = x0;
                if (2 * e2 >= -dx)
                {                                           /* x step */
                    for (e2 += dy, y2 = y0; e2 < ed * wd && (y1 != y2 || dx > dy); e2 += dx)
                    {
                        points.Add(new Point(x0, y2 += sy));
                    }
                    if (x0 == x1)
                    {
                        break;
                    }

                    e2 = err; err -= dy; x0 += sx;
                }
                if (2 * e2 <= dy)
                {                                            /* y step */
                    for (e2 = dx - e2; e2 < ed * wd && (x1 != x2 || dx < dy); e2 += dy)
                    {
                        points.Add(new Point(x2 += sx, y0));
                    }
                    if (y0 == y1)
                    {
                        break;
                    }

                    err += dx; y0 += sy;
                }
            }
            return points;
        }
    }
}
