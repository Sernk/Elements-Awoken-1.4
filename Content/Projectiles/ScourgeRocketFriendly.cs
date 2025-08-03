using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ScourgeRocketFriendly : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 4)
            {
                for (int num255 = 0; num255 < 2; num255++)
                {
                    float num256 = 0f;
                    float num257 = 0f;
                    if (num255 == 1)
                    {
                        num256 = Projectile.velocity.X * 0.5f;
                        num257 = Projectile.velocity.Y * 0.5f;
                    }
                    Vector2 position71 = new Vector2(Projectile.position.X + 3f + num256, Projectile.position.Y + 3f + num257) - Projectile.velocity * 0.5f;
                    int width67 = Projectile.width - 8;
                    int height67 = Projectile.height - 8;
                    int num258 = Dust.NewDust(position71, width67, height67, Const.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                    Dust dust = Main.dust[num258];
                    dust.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                    dust = Main.dust[num258];
                    dust.velocity *= 0.2f;
                    Main.dust[num258].noGravity = true;
                    Vector2 position72 = new Vector2(Projectile.position.X + 3f + num256, Projectile.position.Y + 3f + num257) - Projectile.velocity * 0.5f;
                    int width68 = Projectile.width - 8;
                    int height68 = Projectile.height - 8;
                    num258 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num258].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                    dust = Main.dust[num258];
                    dust.velocity *= 0.05f;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, Const.PinkFlame, damageType: "ranged");
        }
    }
}