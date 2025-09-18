using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Particles : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 150;
        }
        public override void AI()
        {
            int fadeout = 40;
            if (Projectile.ai[0] > 10)
            {
                Projectile.ai[1]++;
                if (Projectile.ai[1] > fadeout) Projectile.Kill();
            }
            int num = (int)(3 * (1-(Projectile.ai[1] / (fadeout /2))));
            if (num >= 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateDust();
                }
            }
            else if (Main.rand.NextBool(3))  CreateDust();
        }
        private void CreateDust()
        {
            Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135)];
            dust.velocity = Vector2.Zero;
            dust.noGravity = true;
            dust.scale = 0.5f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] > 10) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 1;
            Projectile.ai[0]++;
        }
    }
}