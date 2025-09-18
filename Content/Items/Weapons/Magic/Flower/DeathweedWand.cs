using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class DeathweedWand : FlowerClass
    {
        public override int Damage => 6;
        public override int ProjType => ModContent.ProjectileType<DeathweedBall>();
        public override int Materials => ItemID.Deathweed;
    }
}
