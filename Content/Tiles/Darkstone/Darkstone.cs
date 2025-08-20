using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class Darkstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true; 
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(51, 51, 51), CreateMapEntryName());
            MineResist = 2f;
            MinPick = 30;
            HitSound = SoundID.Tink;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(300) == 0)
            {
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 54, 0f, 0f, 0, default(Color), 1.0f);
            }
        }
    }
}