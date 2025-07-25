using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles
{
    public class Voidite : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSpelunker[Type] = true;
            TileID.Sets.Ore[Type] = true;
            Main.tileOreFinderPriority[Type] = 1500;
            AddMapEntry(new Color(255, 127, 227), CreateMapEntryName());
            MineResist = 2f;
            //dustType = 6;
            MinPick = 225;
            HitSound = SoundID.Tink;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(500) == 0)
            {
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.t_Slime, 0f, 0f, 0, default(Color), 1.0f); // PinkFlame
            }
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.25f;
            g = 0f;
            b = 0.2f;
        }
    }
}