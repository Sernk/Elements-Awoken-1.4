using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TimeRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 1f;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.2f, 0.9f, 0.4f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.ai[1]++;
            if (!ModContent.GetInstance<Config>().lowDust || Projectile.ai[1] % 3 == 0)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (Main.rand.Next(2) == 0)
                {
                    num247 = Projectile.velocity.X * 0.5f;
                    num248 = Projectile.velocity.Y * 0.5f;
                }
                int num249 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num247, Projectile.position.Y + 3f + num248) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 75, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                Main.dust[num249].velocity *= 0.2f;
                Main.dust[num249].noGravity = true;
                num249 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num247, Projectile.position.Y + 3f + num248) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num249].velocity *= 0.05f;
            }

            ProjectileUtils.Home(Projectile, 9f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 200);
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 75 }, Projectile.damage, "ranged");
        }
    }
}