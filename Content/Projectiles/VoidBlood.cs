using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class VoidBlood : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = true;
            Projectile.tileCollide = true;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Voidblood");
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dustType = Main.rand.Next(3) < 2 ? 5 : 127;
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, 1, 1, dustType)];
                dust.scale = Main.rand.NextFloat(0.3f,1.3f);
                dust.noGravity = true;
                if (Projectile.ai[0] == 0) dust.velocity *= 0.05f;
                else dust.velocity *= 1.5f;
            }
            if (Projectile.ai[0] != 1)
            {
                Projectile.velocity.Y += 0.05f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.position.Y -= 6; // to elevate slightly above the ground
            Projectile.ai[0] = 1;
            return false;
        }
    }
}