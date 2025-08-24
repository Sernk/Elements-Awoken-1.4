using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NeovirtuoShieldBase : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
            if (Projectile.ai[0] == 0)
            {
                int swirlCount = 10;
                int orbital = Projectile.whoAmI;
                Projectile.ai[1] = Projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    if (Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<NeovirtuoShieldSwirl>()] < swirlCount)
                    {
                        //cos = y, sin = x
                        orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<NeovirtuoShieldSwirl>(), Projectile.damage, Projectile.knockBack, Projectile.owner, l, Projectile.whoAmI);
                        orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<NeovirtuoShieldSwirl2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, l, Projectile.whoAmI);
                    }
                }
            }
        }
    }
}