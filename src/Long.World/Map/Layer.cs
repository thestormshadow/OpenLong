using System.Runtime.InteropServices;

namespace Long.World.Map
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct Layer
    {
        public const int IDX_LAYER_NONE = ushort.MaxValue;
        public const int LAYER_TOP = -1;

        public ushort Mask { get; set; }
        public ushort Terrain { get; set; }
        public ushort Altitude { get; set; }

        public override string ToString()
        {
            return $"Mask[{Mask}]Terrain[{Terrain}]Altitude[{Altitude}]";
        }
    }
}
