using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BloodbathDashP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.97f;

            Dust dust = Main.dust[Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 5, Projectile.velocity.X * 0.6f, Projectile.velocity.Y * 0.6f, 130, default(Color), 2f)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;

            ProjectileUtils.Home(Projectile, 6f);
        }
    }
}