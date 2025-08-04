using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SkeleSkull : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = (int)(Projectile.ai[1] - 1);
            return true;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = Main.rand.Next(1, 4);
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 1.5f;

            ProjectileUtils.Home(Projectile, 6f);
        }
    }
}