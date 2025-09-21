using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class DanceOfDarknessShade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 200;
            Projectile.penetrate = -1;
            Projectile.alpha = 50;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(61, 49, 91, Projectile.alpha);
        }
        public override void AI()
        {
            Projectile.rotation += 0.5f;

            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 60)
            {
                Projectile.ai[0]++;
            }
            if (Projectile.ai[0] != 0)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0]++;
        }
    }
}