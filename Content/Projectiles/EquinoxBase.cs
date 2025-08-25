using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class EquinoxBase : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                int swirlCount = 2;
                int orbital = Projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 16;
                    int proj = ModContent.ProjectileType<EquinoxSwirl>();
                    orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, proj, Projectile.damage, Projectile.knockBack, Projectile.owner, l * distance, Projectile.whoAmI);
                        
                }
                Projectile.ai[0] = 1;
            }
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 242);
                Main.dust[dust].scale *= 0.8f;
                Main.dust[dust].noGravity = true;
                int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
                Main.dust[dust1].scale *= 0.8f;
                Main.dust[dust1].noGravity = true;
                int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229);
                Main.dust[dust2].scale *= 0.5f;
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 197);
                Main.dust[dust3].scale *= 0.5f;
                Main.dust[dust3].noGravity = true;
            }
        }
    }
}