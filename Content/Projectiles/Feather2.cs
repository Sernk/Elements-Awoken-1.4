using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Feather2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            //dust
            float num98 = 16f;
            int num99 = 0;
            while ((float)num99 < num98)
            {
                Vector2 vector11 = Vector2.UnitX * 0f;
                vector11 += -Vector2.UnitY.RotatedBy((double)((float)num99 * (6.28318548f / num98)), default(Vector2)) * new Vector2(1f, 4f);
                vector11 = vector11.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                int num100 = Dust.NewDust(Projectile.Center, 0, 0, 137, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num100].scale = 1f;
                Main.dust[num100].noGravity = true;
                Main.dust[num100].position = Projectile.Center + vector11;
                Main.dust[num100].velocity = Projectile.velocity * 0f + vector11.SafeNormalize(Vector2.UnitY) * 1f;
                num99++;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
    }
}