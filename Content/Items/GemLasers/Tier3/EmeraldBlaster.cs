using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class EmeraldBlaster : GemLasersTier3Class
    {
        public override int Damage => 58;
        public override int Marerial => ModContent.ItemType<EmeraldRifle>();
        public override int AI => 3;
    }
}