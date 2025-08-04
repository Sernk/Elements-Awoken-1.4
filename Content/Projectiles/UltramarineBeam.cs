using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class UltramarineBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
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
            if (Projectile.ai[0] == 1)
            {
                Projectile.penetrate = 1;
                Projectile.localAI[1] = 10;
                if (Projectile.localAI[0] == 0)
                {
                    Projectile.timeLeft = 60;
                    Projectile.localAI[0]++;
                }
            }
            if (Projectile.localAI[1] >= 10)
            {
                int dustlength = 2;
                for (int i = 0; i < dustlength; i++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * ((float)i * (1 / dustlength));
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(vector33, 1, 1, 135, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[dust].position = vector33;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.05f;
                    Main.dust[dust].noGravity = true;
                }
                if (Main.rand.Next(10) == 0 && Projectile.ai[0] == 0)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 8, ModContent.ProjectileType<UltramarineBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
                }
            }
            return;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 135, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}