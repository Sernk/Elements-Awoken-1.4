using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class SapphireRifle : GemLasersTier2Class
    {
        public override int Damage => 31;
        public override int Marerial => ModContent.ItemType<SapphirePistol>();
        public override int AI => 2;
    }
}