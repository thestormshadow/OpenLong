namespace Long.World.Map
{
    public sealed class TerrainObjectPart
    {
        public TerrainObjectPart()
        {
            Identity = Interlocked.Increment(ref currentIdentity);
        }

        public int Identity { get; }

        public List<Layer> Layers { get; } = new();

        public string AniFile { get; set; }
        public string AniTitle { get; set; }

        public int PosOffsetX { get; set; }
        public int PosOffsetY { get; set; }

        public int FrameInterval { get; set; }

        public int SizeBaseCX { get; set; }
        public int SizeBaseCY { get; set; }

        public int Thickness { get; set; }

        public int PosSceneOffsetX { get; set; }
        public int PosSceneOffsetY { get; set; }

        public int Height { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Layer GetLayer(int x, int y)
        {
            int baseSize = SizeBaseCX;
            if (SizeBaseCX > 1 && SizeBaseCY == 1)
            {
                baseSize = SizeBaseCY;
            }
            int index = GameMapData.Pos2Index(x, y, baseSize);
            if (index < 0 || index >= Layers.Count)
            {
                return default;
            }

            return Layers[index];
        }

        private static int currentIdentity;

        public override string ToString()
        {
            return $"X:{X},Y{Y},CX:{SizeBaseCX},CY{SizeBaseCY}";
        }
    }
}
