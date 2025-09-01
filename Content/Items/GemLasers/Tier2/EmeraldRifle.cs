using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class EmeraldRifle : GemLasersTier2Class
    {
        public override int Damage => 32;
        public override int Marerial => ModContent.ItemType<EmeraldPistol>();
        public override int AI => 3;
    }
}