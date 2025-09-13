using ElementsAwoken.EASystem.EAPlayer;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class NecrosisP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 75;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 27;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 240f)
            {
                Projectile.alpha += 3;
                Projectile.damage = (int)((double)Projectile.damage * 0.95);
                Projectile.knockBack = (float)((int)((double)Projectile.knockBack * 0.95));
            }
            if (Projectile.ai[0] < 240f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 62);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 62, damageType: "thrown");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 300);
        }
    }
}