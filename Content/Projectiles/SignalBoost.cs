using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SignalBoost : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 1f;
            Projectile.scale *= 0.7f;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.localAI[1]++;

            Projectile.scale = Projectile.localAI[1] * 0.03f;

            if (Projectile.scale <= 0.7f)
            {
                Projectile.scale = 0.7f;
            }
            if (Projectile.scale >= 2f)
            {
                Projectile.scale = 2f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && !target.boss)
            {
                Vector2 knockBack = (target.Center - Projectile.Center);
                knockBack.Normalize();
                float kbMulti = Projectile.localAI[1] * 0.12f;
                if (kbMulti <= 1.5f)
                {
                    kbMulti = 1.5f;
                }
                if (kbMulti >= 6.5f)
                {
                    kbMulti = 6.5f;
                }
                target.velocity = (target.velocity + knockBack) * kbMulti;
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, EAU.PinkFlame, damageType: "magic");
        }
    }
}