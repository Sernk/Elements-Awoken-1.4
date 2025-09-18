using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class DaybloomWand : FlowerClass
    {
        public override int Damage => 12;
        public override int ProjType => ModContent.ProjectileType<DaybloomSun>();
        public override int Materials => ItemID.Daybloom;
    }
}