using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ViridiumExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }
        public override void AI()
        {
            for (int i = 0; i < 6; i++)
            {
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width * 2, Projectile.height * 2);
                Dust circle = Dust.NewDustPerfect(position, 222, Vector2.Zero);
                circle.noGravity = true;
            }
            for (int i = 0; i < 2; i++)
            {
                float randomSpeed = 0;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        randomSpeed = Main.rand.Next(10, 40);
                        break;
                    case 1:
                        randomSpeed = Main.rand.Next(-40, -10);
                        break;
                    default: break;
                }
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 133, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dust].velocity.X = randomSpeed;
                Main.dust[dust].velocity.Y = randomSpeed;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}