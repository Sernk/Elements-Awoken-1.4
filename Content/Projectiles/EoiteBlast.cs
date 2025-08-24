using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class EoiteBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Main.projFrames[Projectile.type] = 3;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);

            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width + 2, Projectile.height + 2, 62, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;      
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}