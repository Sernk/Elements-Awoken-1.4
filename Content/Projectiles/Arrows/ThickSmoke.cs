using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class ThickSmoke : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.97f;
            Projectile.velocity.Y *= 0.97f;

            Projectile.rotation += 0.1f;

            float aliveTime = 60;
            Projectile.scale -= 0.5f / aliveTime;
            Projectile.ai[0]++;
            if (Projectile.ai[0] <= aliveTime - 20)
            {
                if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(4)== 0)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 54);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                }
            }
            else
            {
                Projectile.alpha += 255 / 20;
                if (Projectile.alpha >= 255)
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