using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Blank : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.alpha = 255;
            Projectile.damage = 0;
            Projectile.timeLeft = 1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blank");
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
        }
    }
}