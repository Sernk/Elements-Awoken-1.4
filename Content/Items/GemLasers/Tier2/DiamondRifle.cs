using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class DiamondRifle : GemLasersTier2Class
    {
        public override int Damage => 35;
        public override int Marerial => ModContent.ItemType<DiamondPistol>();
        public override int AI => 6;     
    }
}