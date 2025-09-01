using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.GemLasers
{
    public class GemLaser : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 4;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.GetDustIDForAI(Projectile))];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
            }
        }
    }
}