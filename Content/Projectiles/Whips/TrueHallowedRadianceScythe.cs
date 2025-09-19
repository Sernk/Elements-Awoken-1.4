using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Whips
{
    public class TrueHallowedRadianceScythe : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Hallowed Radiance");
        }
        public override void AI()
        {
            Projectile.rotation += 1f;

            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 73, Projectile.velocity.X, Projectile.velocity.Y, 255, default(Color), 1.8f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noLight = true;
            }
            int hitboxSize = 12;
            if (Collision.SolidCollision(Projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize))
            {
                Projectile.Kill();
            }
        }
    }
}