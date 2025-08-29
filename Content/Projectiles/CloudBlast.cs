using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CloudBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 100;
            Projectile.light = 0.75f;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 16);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 0.8f;
            Main.dust[dust].velocity *= 0.1f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            Projectile.alpha = (int)Projectile.localAI[0] * 2;

            int hitboxSize = 16;
            if (Collision.SolidCollision(Projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize))
            {
                Projectile.Kill();
            }
        }
    }
}