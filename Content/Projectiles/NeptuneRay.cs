using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NeptuneRay : ModProjectile
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
            Projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ocean's Ray");
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
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * ((float)i * 0.33f);
                    Projectile.alpha = 255;
                    Dust dust = Main.dust[Dust.NewDust(vector33, 1, 1, 221, 0f, 0f, 0, default(Color), 0.75f)];
                    dust.position = vector33;
                    dust.scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    dust.velocity *= 0.05f;
                    dust.noGravity = true;
                }
            }
            return;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numProj = Main.rand.Next(1, 3);
            for (int k = 0; k < numProj; k++)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), target.Center.X + Main.rand.Next(-6,6), target.Top.Y - Main.rand.Next(60,75), 0f, 2, ModContent.ProjectileType<OceansSeashell>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 221, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f)];
                dust.noGravity = true;
            }
        }
    }
}