using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AncientStar2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            AIType = ProjectileID.Starfury;
            Projectile.scale = 1f;
        }
        public override bool PreKill(int timeLeft)
        {
            Projectile.type = ProjectileID.Starfury;
            return true;
        }
    }
}