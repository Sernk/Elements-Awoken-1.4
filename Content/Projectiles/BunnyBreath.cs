using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BunnyBreath : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bunny Breath");
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.16f;

            for (int i = 0; i < 2; i++)
            {
                int num113 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.5f);
                Dust dust = Main.dust[num113];
                dust.velocity *= 0.1f;
                dust = Main.dust[num113];
                dust.velocity += Projectile.velocity * 0.2f;
                Main.dust[num113].position.X = Projectile.position.X + (float)(Projectile.width / 2) + 4f + (float)Main.rand.Next(-2, 3);
                Main.dust[num113].position.Y = Projectile.position.Y + (float)(Projectile.height / 2) + (float)Main.rand.Next(-2, 3);
                Main.dust[num113].noGravity = true;
            }
        }
    }
}
