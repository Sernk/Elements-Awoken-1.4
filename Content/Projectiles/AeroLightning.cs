using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AeroLightning : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = 1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -Projectile.velocity.X;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -Projectile.velocity.Y;
            }
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, 1, 1, 226, 0f, 0f, 0, default(Color), 0.75f)];
                dust.position = Projectile.position - Projectile.velocity * ((float)i * 0.25f);
                dust.scale = (float)Main.rand.Next(70, 110) * 0.013f;
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            return;
        }
    }
}