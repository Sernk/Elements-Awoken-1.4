using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SansNote : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snote");
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.X * 0.1f;
            Projectile.spriteDirection = -Projectile.direction;
            if (Main.rand.Next(3) == 0)
            {
                int num274 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0f, 0f, 80, default(Color), 1f);
                Main.dust[num274].noGravity = true;
                Dust dust = Main.dust[num274];
                dust.velocity *= 0.2f;
            }

            ProjectileUtils.Home(Projectile, 5f);

            ProjectileUtils.PushOtherEntities(Projectile);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
            if (Projectile.velocity.Y != oldVelocity.Y)  Projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 0);
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.immune[Projectile.owner] = 10;
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 229, Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f);
            }
        }
    }
}