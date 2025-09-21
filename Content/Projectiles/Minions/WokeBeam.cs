using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class WokeBeam : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -Projectile.velocity.X;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -Projectile.velocity.Y;
            }

            int dustlength = 4;
            for (int i = 0; i < dustlength; i++)
            {
                Vector2 vector33 = Projectile.position;
                vector33 -= Projectile.velocity * ((float)i * (1 / dustlength));
                Projectile.alpha = 255;
                int num448 = Dust.NewDust(vector33, 1, 1, 219, 0f, 0f, 0, default(Color), 0.75f);
                Main.dust[num448].position = vector33;
                Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                Main.dust[num448].velocity *= 0.05f;
                Main.dust[num448].noGravity = true;
            }
            return;
        }
    }
}