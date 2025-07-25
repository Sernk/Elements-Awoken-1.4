using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ChargeRifleHalf : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charge Rifle");
        }
        public override void AI()
        {
            for (int l = 0; l < 5; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 220)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}
