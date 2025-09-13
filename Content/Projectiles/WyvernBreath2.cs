using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class WyvernBreath2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public int damage = 0;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 3; num121++)
            {
                {
                    Vector2 pos = new Vector2(Projectile.position.X, Projectile.position.Y);
                    int num348 = Dust.NewDust(pos, Projectile.width, Projectile.height, 234, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 1.2f);
                    Main.dust[num348].position = (Main.dust[num348].position + Projectile.Center) / 2f;
                    Main.dust[num348].noGravity = true;
                    Main.dust[num348].velocity *= 0.5f;
                }
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] <= 10)
            {
                Projectile.damage = 0;
            }
            else
            {
                Projectile.damage = 32;
            }
            Projectile.velocity.Y += 0.1f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
    }
}