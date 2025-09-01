using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class RubyBlaster : GemLasersTier3Class
    {
        public override int Damage => 60;
        public override int Marerial => ModContent.ItemType<RubyRifle>();
        public override int AI => 5;
    }
}