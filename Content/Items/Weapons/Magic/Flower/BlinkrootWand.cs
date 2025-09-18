using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class BlinkrootWand : FlowerClass
    {
        public override int Damage => 13;
        public override int ProjType => ModContent.ProjectileType<BlinkrootDirt>();
        public override int Materials => ItemID.Blinkroot;
    }
}