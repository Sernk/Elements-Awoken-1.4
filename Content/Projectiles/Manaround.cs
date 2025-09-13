using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ManaRound : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 4)
            {
                float numDust = 4;
                for (int l = 0; l < numDust; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / numDust * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234, Projectile.oldVelocity.X * 0.15f, Projectile.oldVelocity.Y * 0.15f)]; ;
                dust.noLight = true;
            }
        }
    }
}