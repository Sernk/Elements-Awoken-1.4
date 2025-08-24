using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class EoiteBeam2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
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
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * ((float)i * 0.25f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(vector33, 1, 1, 62, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust].position = vector33;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.01f;
                    Main.dust[dust].velocity *= 0.05f;
                    Main.dust[dust].noGravity = true;
                }
                return;
            }
        }
    }
}