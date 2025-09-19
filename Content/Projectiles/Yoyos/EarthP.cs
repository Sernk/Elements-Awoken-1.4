using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class EarthP : ModProjectile
    {
        public float timer = 30;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 145f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 9f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 3f;
        }
    }
}