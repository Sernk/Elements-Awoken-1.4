using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AtaxiaCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 420;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = (int)Projectile.ai[1];
            return true;
        }
        public override void AI()
        {
            Projectile.rotation -= 0.2f;

            Player player = Main.player[Projectile.owner];

            Projectile.ai[0] += 5f;
            float distance = Projectile.localAI[0];
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            Projectile.Center = player.Center- new Vector2((int)(Math.Cos(rad) * distance) - Projectile.width / 2, (int)(Math.Sin(rad) * distance) - Projectile.height / 2);

            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AncientRed>());
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.2f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AncientRed>())];
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
    }
}