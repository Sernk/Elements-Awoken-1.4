using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class TopazRifle : GemLasersTier2Class
    {
        public override int Damage => 30;
        public override int Marerial => ModContent.ItemType<TopazPistol>();
        public override int AI => 1;
    }
}