using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ElectricBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 6;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Little Buddy");
        }
        public override void AI()
        {
            if (Projectile.localAI[1] == 0)
            {
                Projectile.ai[0] = Projectile.damage;
                Projectile.localAI[1]++;
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 3)
            {
                float numDust = 8;
                for (int l = 0; l < numDust; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / numDust * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1.3f;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.penetrate--; // so it bounces 6 times but can only hit 3 enemies
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.damage += (int)(Projectile.ai[0] * 0.5f);
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 1.5f;
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 229, Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f);
            }
        }
    }
}