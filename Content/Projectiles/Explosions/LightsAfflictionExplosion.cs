using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Explosions
{
    public class LightsAfflictionExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.5f;
            Main.dust[dust].velocity *= 1f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 6)
                    Projectile.Kill();
            }
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 8;
        }
    }
}