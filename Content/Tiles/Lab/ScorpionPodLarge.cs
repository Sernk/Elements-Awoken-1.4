using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Lab
{
    public class ScorpionPodLarge : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            Main.tileLighted[Type] = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(98, 214, 177), CreateMapEntryName());
            TileID.Sets.DisableSmartCursor[Type] = true;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.0f;
            g = 0.2f;
            b = 0.5f;
        }
    }
}