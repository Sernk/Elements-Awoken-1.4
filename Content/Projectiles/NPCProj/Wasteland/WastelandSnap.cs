using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Wasteland
{
    public class WastelandSnap : ModProjectile
    {   	
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 38;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
        }
    }
}