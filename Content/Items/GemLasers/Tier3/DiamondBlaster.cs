using ElementsAwoken.Content.Items.GemLasers;
using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class DiamondBlaster : GemLasersTier3Class
    {
        public override int Damage => 61;
        public override int Marerial => ModContent.ItemType<DiamondRifle>();
        public override int AI => 6;
    }
}