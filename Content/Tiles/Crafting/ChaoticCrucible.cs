using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Crafting
{
    public class ChaoticCrucible : ModTile
    {
        public override void SetStaticDefaults()
        {
			Main.tileLighted[Type] = true;
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            AddMapEntry(new Color(217, 137, 85), CreateMapEntryName());
            TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);
			AdjTiles = new int[] { TileID.Furnaces, ModContent.TileType<ElementalForge>(), };
        }
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.8f;
            g = 0.3f;
            b = 0.6f;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.NextBool(20))
            {
                Vector2 position = new Vector2(i * 16, j * 16 - 16);
                Dust dust = Main.dust[Dust.NewDust(position, 8, 2, 127)];
                dust.velocity.Y = Main.rand.NextFloat(-2, -0.2f);
                dust.fadeIn = 1.5f;
                dust.scale = 0.2f;
                dust.noGravity = true;
            }
        }
    }
}