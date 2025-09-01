using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class TopazBlaster : GemLasersTier3Class
    {
        public override int Damage => 56;
        public override int Marerial => ModContent.ItemType<TopazRifle>();
        public override int AI => 1;
    }
}