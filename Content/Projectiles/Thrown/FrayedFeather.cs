using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class FrayedFeather : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fire Knife");
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= 60f)
            {
                Projectile.alpha += 5;
                Projectile.damage = (int)((double)Projectile.damage * 0.95);
                Projectile.knockBack = (float)((int)((double)Projectile.knockBack * 0.95));
                Projectile.rotation -= 0.3f;
            }
            if (Projectile.localAI[0] < 60f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
            int dust = Dust.NewDust(Projectile.Center - new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.width / 2, Projectile.height/ 2, 21);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 21, Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f);
            }
        }  
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 120);
        }
    }
}