using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SkyFuryRain : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RainFriendly);
            AIType = ProjectileID.RainFriendly;
            Projectile.scale = 1f;
        }
        public override void AI()         
        {
            Projectile.velocity.X *= 0f;
        }
    }
}