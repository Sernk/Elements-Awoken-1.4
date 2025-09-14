using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles
{
    public class Altar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileTable[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            //TileObjectData.newTile.Width = 3;
            //TileObjectData.newTile.Height = 2;
            AddMapEntry(new Color(141, 11, 156), CreateMapEntryName());
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            EAU.DSCursor(Type);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
            AdjTiles = new int[] { TileID.DemonAltar };
        }
    }
}