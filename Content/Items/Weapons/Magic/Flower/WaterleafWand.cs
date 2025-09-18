using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class WaterleafWand : FlowerClass
    {
        public override int Damage => 13;
        public override int ProjType => ModContent.ProjectileType<WaterleafBall>();
        public override int Materials => ItemID.Waterleaf;
    }
}