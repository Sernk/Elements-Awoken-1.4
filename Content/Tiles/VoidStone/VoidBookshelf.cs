using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.VoidStone
{
    public class VoidBookshelf : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.CoordinateHeights = [ 16, 16, 16, 16 ];
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(51, 51, 51), CreateMapEntryName());
            EAU.DSCursor(Type);
            AdjTiles = new int[] { TileID.Bookcases };
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}