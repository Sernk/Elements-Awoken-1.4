using ElementsAwoken.Content.Items.GemLasers.Tier2;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier3
{
    public class SapphireBlaster : GemLasersTier3Class
    {
        public override int Damage => 57;
        public override int Marerial => ModContent.ItemType<SapphireRifle>();
        public override int AI => 2;
    }
}