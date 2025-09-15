using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Lab
{
    public class LabLightFunctional : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 1;
            //TileObjectData.newTile.Origin = new Point16(1, 0);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName());
            EAU.DSCursor(Type);
            TileObjectData.newTile.CoordinateHeights = [16];
            TileObjectData.addTile(Type);
            HitSound = SoundID.Tink;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.6f;
            g = 0.6f;
            b = 0.7f;
        }
    }
}