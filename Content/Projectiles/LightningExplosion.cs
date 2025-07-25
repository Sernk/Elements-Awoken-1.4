using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class LightningExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lightning");
        }
        public override void AI()
        {
            for (int num134 = 0; num134 < 10; num134++)
            {
                float x = Projectile.position.X - Projectile.velocity.X / 10f * (float)num134;
                float y = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num134;
                int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 15, 0f, 0f, 0, default(Color), 1.25f);
                Main.dust[num135].alpha = Projectile.alpha;
                Main.dust[num135].position.X = x;
                Main.dust[num135].position.Y = y;
                Main.dust[num135].velocity *= 0f;
                Main.dust[num135].noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
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
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
    }
}