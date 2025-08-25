using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class TheGritP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 150f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 11f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 5f;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[0] > 30)
            {
                Projectile.timeLeft = 50;
            }
        }
    }
}