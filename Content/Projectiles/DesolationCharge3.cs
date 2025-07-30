using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DesolationCharge3 : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minion = true;

            Projectile.penetrate = 5;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desolation");
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight(Projectile.Center, 0.2f, 0.6f, 0.3f);

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AncientGreen>())];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] % 8 == 0)
            {
                Explode();
            }
        }
        public override void OnKill(int timeLeft)
        {
            Explode();
        }
        private void Explode()
        {
            //ProjectileUtils.Explosion(projectile, mod.DustType("AncientGreen"), damageType: "ranged");
        }
    }
}