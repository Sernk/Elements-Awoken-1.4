using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BountyP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 1f;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] < 30)
            {
                Projectile.velocity *= 0.9f;
            }
            if (Projectile.localAI[1] == 30)
            {
                Vector2 toTarget = new Vector2(Projectile.ai[0] - Projectile.Center.X, Projectile.ai[1] - Projectile.Center.Y);
                toTarget.Normalize();
                Projectile.velocity = toTarget * 15f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, EAU.PinkFlame, damageType: "melee");
        }
    }
}