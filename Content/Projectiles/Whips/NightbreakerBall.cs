using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Whips
{
	public class NightbreakerBall : ModProjectile
	{
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
		{
			Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
            Projectile.timeLeft = 150;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
		}
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 14)];
                dust.velocity *= 0.4f;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
                dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 27)];
                dust.velocity *= 0.4f;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}