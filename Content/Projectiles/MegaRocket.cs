using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class MegaRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale *= 1.5f;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            for (int i = 0; i < 2; i++)
            {
                float num253 = 0f;
                float num254 = 0f;
                if (i == 1)
                {
                    num253 = Projectile.velocity.X * 0.5f;
                    num254 = Projectile.velocity.Y * 0.5f;
                }
                int num255 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num253, Projectile.position.Y + 3f + num254) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num255];
                dust.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                dust = Main.dust[num255];
                dust.velocity *= 0.2f;
                Main.dust[num255].noGravity = true;
                num255 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num253, Projectile.position.Y + 3f + num254) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num255].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                dust = Main.dust[num255];
                dust.velocity *= 0.05f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 6, damageType: "ranged");
        }
    }
}