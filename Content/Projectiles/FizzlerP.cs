using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FizzlerP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.ai[0] = Main.rand.Next(20, 30);
                Projectile.localAI[0]++;
            }
            Projectile.ai[0]--;
            if (Projectile.ai[0] <= 0)
            {
                float numProj = Main.rand.Next(2, 4);
                for (int i = 0; i < numProj; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(7));
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FizzlerP2>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner);
                }
                Projectile.Kill();
            }
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6)];
                    dust.velocity *= 0.4f;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
    }
}