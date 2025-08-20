using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Other
{
    public class CreditsStarter : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 120)
            {
                MyWorld.credits = true;
                Projectile.Kill();
            }
        }      
    }
}