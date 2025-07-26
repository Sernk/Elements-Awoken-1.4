using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Other
{
    public class StarstruckP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.tileCollide = false;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 60000;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hands of Despair");
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = (int)Projectile.ai[0];
            return true;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.8f, 0.2f, 0.6f);
            Projectile.velocity *= 0.995f;
            Projectile.rotation += Projectile.velocity.X * 0.2f;
            Projectile.ai[1]++;
            if (Projectile.ai[1] > 20)
            {
                Projectile.alpha += 255 / 120;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
    }
}