using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AzanaBlood : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Main.rand.NextBool() ? 127: 5, 0f, 0f, 0, default(Color), 2.75f)];
                if (dust.type == 5) dust.scale *= 0.5f;
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
        }
    }
}