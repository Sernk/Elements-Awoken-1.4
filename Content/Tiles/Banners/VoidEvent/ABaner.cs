using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Banners.VoidEvent
{
    public abstract class ABaner : ModTile
    {
        public virtual int ColorR => 13;
        public virtual int ColorG => 88;
        public virtual int ColorB => 130;
        public abstract int NPCType { get; }
        public virtual int NPCType2 { get; }
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            DustType = -1;
            EAU.DSCursor(Type);
            AddMapEntry(new Color(ColorR, ColorG, ColorB), Language.GetText("MapObject.Banner"));//Banner
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.SceneMetrics.NPCBannerBuff[NPCType] = true;
                Main.SceneMetrics.NPCBannerBuff[NPCType2] = true;
                Main.SceneMetrics.hasBanner = true;
            }
        }
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}