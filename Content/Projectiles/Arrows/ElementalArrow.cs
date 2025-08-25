using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class ElementalArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 242);
                Main.dust[dust].scale *= 0.8f;
                Main.dust[dust].noGravity = true;
                int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
                Main.dust[dust1].scale *= 0.8f;
                Main.dust[dust1].noGravity = true;
                int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229);
                Main.dust[dust2].scale *= 0.8f;
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 197);
                Main.dust[dust3].scale *= 0.8f;
                Main.dust[dust3].noGravity = true;
            }
        }
    }
}