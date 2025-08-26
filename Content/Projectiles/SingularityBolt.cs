using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SingularityBolt : ModProjectile
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
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.3f) / 255f, ((255 - Projectile.alpha) * 0.4f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 242);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;
            int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 197);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].scale = 1.2f;
            int dust3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
            Main.dust[dust3].noGravity = true;
            Main.dust[dust3].scale = 1.2f;
            int dust4 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229);
            Main.dust[dust4].noGravity = true;
            Main.dust[dust4].scale = 1.2f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 0)
            {
                int swirlCount = 5;
                int orbital = Projectile.whoAmI;
                Projectile.ai[1] = Projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    //cos = y, sin = x
                    int distance = 59;
                    orbital = Projectile.NewProjectile(EAU.Proj(Projectile), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<SingularityBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner, l * distance, target.whoAmI);

                }
                Projectile.ai[0] = 1;
            }
        }
    }
}