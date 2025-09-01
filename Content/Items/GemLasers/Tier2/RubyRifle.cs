using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class RubyRifle : GemLasersTier2Class
    {
        public override int Damage => 34;
        public override int Marerial => ModContent.ItemType<RubyPistol>();
        public override int AI => 5;
    }
}