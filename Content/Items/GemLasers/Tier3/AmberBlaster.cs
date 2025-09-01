using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class AmberBlaster : GemLasersTier3Class
    {
        public override int Damage => 59;
        public override int Marerial => ModContent.ItemType<AmberRifle>();
        public override int AI => 4;
    }
}