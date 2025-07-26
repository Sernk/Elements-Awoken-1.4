using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class EAProjectileType : GlobalProjectile
    {
        public bool whip = false;
        public int whipAliveTime = 30;
        public int specialPenetrate = -2;
        public EAProjectileType()
        {
            whip = false;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.SolarWhipSword)
            {
                whip = true;
                whipAliveTime = 30;
            }
        }
    }
}
