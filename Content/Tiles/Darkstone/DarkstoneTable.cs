using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class DarkstoneTable : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileTable[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            AddMapEntry(new Color(51, 51, 51), CreateMapEntryName());
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            EAU.DSCursor(Type);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);
            DustType = 6;
            MinPick = 20;
        }
    }
}