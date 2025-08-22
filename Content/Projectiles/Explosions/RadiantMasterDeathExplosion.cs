using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Explosions
{
    public class RadiantMasterDeathExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 9)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 6)
                    Projectile.Kill();
            }
            return true;
        }
    }
}