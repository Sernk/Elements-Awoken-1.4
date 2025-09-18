using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class MatrixLaser : ModProjectile
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
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Quantum");
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
                for (int num447 = 0; num447 < 4; num447++)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector33 = Projectile.position;
                        vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
                        Projectile.alpha = 255;
                        int dust = Dust.NewDust(vector33, 1, 1, ModContent.DustType<Dusts.Matrix>(), 0f, 0f, 0, default(Color), 0.75f);
                        Main.dust[dust].position = vector33;
                        Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                        Main.dust[dust].velocity *= 0.05f;
                        Main.dust[dust].noGravity = true;
                    }
                }
                return;
            }
        }
    }
}