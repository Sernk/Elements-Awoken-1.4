using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Aqueous
{
    public class WaterKnife : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 10)
            {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 111);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1f;
                    Main.dust[dust].noGravity = true;
            }
        }
    }
}