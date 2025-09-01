using ElementsAwoken.Content.Items.GemLasers.Tier1;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier2
{
    public class AmberRifle : GemLasersTier2Class
    {
        public override int Damage => 33;
        public override int Marerial => ModContent.ItemType<AmberPistol>();
        public override int AI => 4;
    }
}