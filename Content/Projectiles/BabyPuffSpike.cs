using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BabyPuffSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.aiStyle = 1;
            Projectile.penetrate = 1;
        }
        public override void AI()
        {
            Projectile.alpha -= 50;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 5f)
            {
                Projectile.ai[0] = 5f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 21, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}