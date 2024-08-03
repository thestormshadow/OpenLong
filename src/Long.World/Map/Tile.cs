using System.Runtime.InteropServices;

namespace Long.World.Map
{
    /// <summary>
    ///     Switching to classes increased memory usage by 3x. But it's needed since we need to change the Tile information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct Tile
    {
        private const int MaskRoleCount = 127;

        public short Mask { get; set; }      // The access type for processing the tile.
        public short Elevation { get; set; } // The elevation of the tile on the map.
        public short Surface { get; set; }

        public ushort LayerIndex { get; set; } = Layer.IDX_LAYER_NONE;

        /// <summary>
        ///     This structure encapsulates a tile from the floor's coordinate grid. It contains the tile access information
        ///     and the elevation of the tile. The map's coordinate grid is composed of these tiles. The tile structure
        ///     is not optimized by C#, and thus takes up 24 bits of memory (or 3 bytes).
        /// </summary>
        /// <param name="access">The access type for processing the tile.</param>
        /// <param name="elevation">The elevation of the tile on the map.</param>
        /// <param name="surface"></param>
        public Tile(short elevation, short access, short surface)
        {
            Mask = access;
            Elevation = elevation;
            Surface = surface;
        }

        public bool IsAccessible()
        {
            return Mask != 1;
        }

        public bool IsBoothEnable()
        {
            return (Surface & 0x10) != 0;
        }

        public short GetAltitude()
        {
            return Elevation;
        }

        public int GetLayerAmount(List<List<Layer>> setLayer)
        {
            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                return 1;
            }

            return 1 + setLayer[LayerIndex].Count;
        }

        public Layer GetLayer(List<List<Layer>> setLayer, int index)
        {
            if (index <= 0)
            {
                return default;
            }

            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                return default;
            }

            if (LayerIndex >= setLayer.Count)
            {
                return default;
            }

            if (index >= setLayer[LayerIndex].Count)
            {
                return default;
            }

            return setLayer[LayerIndex][index];
        }

        public void AddLayer(List<List<Layer>> setLayer, Layer layer)
        {
            if (layer.Equals(default))
            {
                return;
            }

            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                for (ushort i = 0; i < setLayer.Count; i++)
                {
                    if (setLayer[i].Count == 0)
                    {
                        LayerIndex = i;
                        setLayer[i].Add(layer);
                        return;
                    }
                }

                LayerIndex = (ushort)setLayer.Count;
                setLayer.Add(new List<Layer> { layer });
            }
            else
            {
                setLayer[LayerIndex].Add(layer);
            }
        }

        public bool DelLayer(List<List<Layer>> setLayer, int index = Layer.LAYER_TOP)
        {
            if (index is not (Layer.LAYER_TOP or > 0))
            {
                return false;
            }

            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                return false;
            }

            if (LayerIndex >= setLayer.Count)
            {
                return false;
            }

            if (!(index == Layer.LAYER_TOP || index < setLayer[LayerIndex].Count))
            {
                return false;
            }

            int idx;
            if (index == Layer.LAYER_TOP)
            {
                idx = setLayer[LayerIndex].Count - 1;
            }
            else
            {
                idx = index - 1;
            }

            Layer layer = setLayer[LayerIndex][idx];
            if (!layer.Equals(default))
            {
                setLayer[LayerIndex].RemoveAt(idx);
            }

            if (setLayer[LayerIndex].Count == 0)
            {
                LayerIndex = Layer.IDX_LAYER_NONE;
            }

            return true;
        }

        public int GetFloorMask(List<List<Layer>> setLayer)
        {
            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                return Mask;
            }

            int amount = setLayer[LayerIndex].Count;
            if (amount > 0)
            {
                return setLayer[LayerIndex][amount - 1].Mask;
            }

            return 1;
        }

        public int GetFloorAttr(List<List<Layer>> setLayer)
        {
            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                return 0;
            }

            int amount = setLayer[LayerIndex].Count;
            if (amount > 0)
            {
                return setLayer[LayerIndex][amount - 1].Terrain;
            }
            return 0;
        }

        public int GetFloorAlt(List<List<Layer>> setLayer)
        {
            if (LayerIndex == Layer.IDX_LAYER_NONE)
            {
                return Elevation / 2;
            }

            int amount = setLayer[LayerIndex].Count;
            if (amount > 0)
            {
                return setLayer[LayerIndex][amount - 1].Altitude;
            }
            return short.MaxValue;
        }
    }
}
