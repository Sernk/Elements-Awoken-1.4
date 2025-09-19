using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class SaturnP : ModProjectile
    {
        public bool hasRing = false;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 285f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14.5f;
        }
        public override void AI()
        {
            if (!hasRing)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<SaturnRing>(), Projectile.damage, 0.5f, 0, 0f, Projectile.whoAmI);
                hasRing = true;
            }
        }
    }
}