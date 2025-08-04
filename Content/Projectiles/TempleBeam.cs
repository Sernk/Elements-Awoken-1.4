using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TempleBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 5;
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
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * ((float)i * 0.25f);
                    Projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, 1, 1, 6, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.05f;
                    Main.dust[num448].noGravity = true;
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                Vector2 perturbedSpeed = new Vector2(2f, 2f).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<TempleBall>(), (int)(Projectile.damage * 1.25), 0f, 0);
            }
            return;
        }
    }
}