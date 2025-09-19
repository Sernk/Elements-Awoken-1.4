using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AstralTear : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 66;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1.3f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Tear");
        }
        public override void AI()
        {
            Projectile.velocity.X = 0f;
            Projectile.velocity.Y = 0f;
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 173);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
            }
        }
    }
}