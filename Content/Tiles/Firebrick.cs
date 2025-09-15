using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles
{
    public class Firebrick : ModTile
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
            HitSound = SoundID.Tink;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) 
        {
            r = 0.25f;
            g = 0f;
            b = 0.05f;
        }
    }
}