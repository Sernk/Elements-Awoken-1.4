using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class Sandball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 600;
            AIType = 48;
        }
        public override void OnKill(int timeLeft)
        {
            for (int l = 0; l < 3; l++)
            { 
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width + 2, Projectile.height + 2, 32, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}