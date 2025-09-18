using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FizzlerP2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
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
                Projectile.ai[0] = Main.rand.Next(200, 300);
                Projectile.localAI[0]++;
            }
            Projectile.ai[0]--;
            if (Projectile.ai[0] <= 0) Projectile.Kill();
            if (Main.rand.NextBool(2))
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6)];
                dust.velocity *= 0.4f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}