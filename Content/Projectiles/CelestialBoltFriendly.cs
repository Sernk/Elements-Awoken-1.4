using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CelestialBoltFriendly : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {        
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);

            int dustType = 6;
            switch ((int)Projectile.ai[1])
            {
                case 0:
                    dustType = 6;
                    break;
                case 1:
                    dustType = 197;
                    break;
                case 2:
                    dustType = 229;
                    break;
                case 3:
                    dustType = 242;
                    break;
                default: break;
            }
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}