using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.ScourgeFighter
{
    public class ScourgeBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BulletDeadeye);
            AIType = ProjectileID.Bullet;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
        }
    }
}