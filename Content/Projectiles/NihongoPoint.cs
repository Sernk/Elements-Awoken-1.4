using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NihongoPoint : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.3f, 0.3f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.5f;
                Main.dust[dust].noGravity = true;
            }

            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 10)
            {
                Projectile.alpha += 20;
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}