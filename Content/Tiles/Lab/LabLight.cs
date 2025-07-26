using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Lab
{
    public class LabLight : ModTile
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
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 200, 200),CreateMapEntryName());
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
            HitSound = SoundID.Tink;
            // NO ITEM DROP
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(15) == 0)
            {
                Lighting.AddLight(new Vector2(i * 16, j * 16), 0.6f, 0.6f, 0.7f);
            }
        }
    }
}