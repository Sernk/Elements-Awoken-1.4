using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BlossomSpore : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1.3f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.94f;
            Projectile.velocity.Y *= 0.94f;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] <= 60)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 273);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noLight = true;
                }
            }
            else
            {
                Projectile.alpha += 2;
                if (Projectile.alpha > 250)
                {
                    Projectile.Kill();
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > Main.projFrames[Projectile.type] - 1)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}