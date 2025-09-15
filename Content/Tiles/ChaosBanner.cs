using ElementsAwoken.Content.Buffs.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles
{
    public class ChaosBanner : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            AddMapEntry(new Color(217, 137, 85));
            EAU.DSCursor(Type);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16,16,16 };
            TileObjectData.addTile(Type);
			AnimationFrameHeight = 36;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            if (closer && !player.dead && player.active)
            {
                player.AddBuff(ModContent.BuffType<ChaosBannerBuff>(), 60);
            }
        }
    }
}