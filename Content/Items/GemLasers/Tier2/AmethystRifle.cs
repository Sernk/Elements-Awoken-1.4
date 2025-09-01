using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class AmethystRifle : GemLasersTier2Class
    {
        public override int Damage => 29;
        public override int Marerial => ModContent.ItemType<AmethystPistol>();
        public override int AI => 0;
    }
}