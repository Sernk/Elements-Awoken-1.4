using ElementsAwoken.Content.NPCs.ItemSets.Carapace;
using ElementsAwoken.Content.Tiles.Banners.VoidEvent;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Tiles.Banners
{
    public class PebleerBanner : ABaner
    {
        public override int NPCType => NPCType<Pebleer>();
        public override int NPCType2 => NPCType<Stoneer>();
    }
}