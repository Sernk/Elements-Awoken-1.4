using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.Banners.VoidEvent
{
    public class VoidKnightBanner : ABaner
    {
        public override int ColorR => 13;
        public override int ColorG => 88;
        public override int ColorB => 130;
        public override int NPCType => ModContent.NPCType<VoidKnight>();
    }
}