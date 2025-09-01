using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class AmethystBlaster : GemLasersTier3Class
    {
        public override int Damage => 55;
        public override int Marerial => ModContent.ItemType<AmethystRifle>();
        public override int AI => 0;
    }
}