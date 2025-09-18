using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DubstepWave : ModProjectile
    {
        public int dustType = 60;
        public bool invert = false;

        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dubstep");
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                dustType = Main.rand.Next(219, 224); // Main.rand.Next(59, 64)
                invert = Main.rand.Next(2) == 0 ? true : false;
                Projectile.localAI[0]++;
            }
            int dustLength = 4;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.scale *= 0.7f;
            }

            float rotateIntensity = 6f;
            if (invert)
            {
                rotateIntensity *= -1;
            }
            float waveTime = 8f;
            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0) // this part is to fix the offset (it is still slightlyyyy offset)
            {
                if (Projectile.ai[0] > waveTime * 0.5f)
                {
                    Projectile.ai[0] = 0;
                    Projectile.ai[1] = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (Projectile.ai[0] <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
                if (Projectile.ai[0] >= waveTime * 2)
                {
                    Projectile.ai[0] = 0;
                }
            }
        }
    }
}