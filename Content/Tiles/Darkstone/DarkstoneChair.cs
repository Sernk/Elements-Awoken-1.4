using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class DarkstoneChair : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.CoordinateHeights = [16, 18];
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2; 
            TileObjectData.newTile.StyleMultiplier = 2; 
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
            AddMapEntry(new Color(51, 51, 51), CreateMapEntryName());
            EAU.DSCursor(Type);
            AdjTiles = new int[] { TileID.Chairs };
        }
    }
}