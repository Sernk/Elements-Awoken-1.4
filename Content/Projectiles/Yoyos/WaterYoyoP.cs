using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class WaterYoyoP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 380f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 16.5f;
        }
        public override void AI()
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<WaterYoyoWater>(), Projectile.damage, 5f, Projectile.owner, 0f, 0f);
        }
    }
}