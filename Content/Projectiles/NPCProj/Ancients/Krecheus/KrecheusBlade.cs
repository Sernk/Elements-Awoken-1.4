using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000000;
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)Projectile.ai[1]];

            Projectile.localAI[0]++;

            if (Projectile.localAI[0] == 5)
            {
                for (int k = 0; k < 80; k++)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AncientRed>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                }               
            }

            Vector2 direction = parent.Center - Projectile.Center;
            Projectile.rotation = direction.ToRotation() + 0.785f;
            Projectile.rotation += MathHelper.ToRadians(45);

            Projectile.ai[0] += 2f; // speed
            int distance = 100;
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
            Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;

            if (parent.active == false)
            {
                Projectile.Kill();
            }
        }
    }
}