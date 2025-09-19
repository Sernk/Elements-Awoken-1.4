using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class SaturnRing : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
        }
        public override void AI()
        {
            Projectile.rotation += 0.2f;

            Projectile parent = Main.projectile[(int)Projectile.ai[1]];
            Projectile.Center = parent.Center;

            if (!parent.active) Projectile.Kill();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.immune[Projectile.owner] = 4;
    }
}