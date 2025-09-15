using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class DarkstoneChandelier : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(51, 51, 51), CreateMapEntryName());
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 0.2f;
            b = 0.0f;
        }
    }
}