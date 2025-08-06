using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class BiomeTileCounterSystem : ModSystem
    {
        public static int _lizardTiles = 0;

        public override void ResetNearbyTileEffects()
        {
            _lizardTiles = 0;
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            _lizardTiles = tileCounts[TileID.LihzahrdBrick];
        }
    }
}
