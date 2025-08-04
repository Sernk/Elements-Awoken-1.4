using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class IceMistSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180, false);
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.3f) / 255f, ((255 - Projectile.alpha) * 0.4f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 0.8f;
            Main.dust[dust].velocity *= 0.1f;
        }
    }
}