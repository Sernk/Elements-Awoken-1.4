using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SWRLRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num246 = 0; num246 < 2; num246++)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (num246 == 1)
                {
                    num247 = Projectile.velocity.X * 0.5f;
                    num248 = Projectile.velocity.Y * 0.5f;
                }
                int num249 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num247, Projectile.position.Y + 3f + num248) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 181, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num249].scale *= 0.8f + (float)Main.rand.Next(10) * 0.1f;
                Main.dust[num249].velocity *= 0.2f;
                Main.dust[num249].noGravity = true;
                num249 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num247, Projectile.position.Y + 3f + num248) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num249].velocity *= 0.05f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 181, damageType: "ranged");
        }
    }
}