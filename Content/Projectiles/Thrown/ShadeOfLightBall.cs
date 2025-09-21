using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class ShadeOfLightBall : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {

                Vector2 pos = new Vector2(Projectile.position.X, Projectile.position.Y);
                int num348 = Dust.NewDust(pos, Projectile.width, Projectile.height, 254, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 1.2f);
                Main.dust[num348].position = (Main.dust[num348].position + Projectile.Center) / 2f;
                Main.dust[num348].noGravity = true;
                Main.dust[num348].velocity *= 0.5f;

            }
            Projectile.velocity.Y += 0.01f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 254, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}