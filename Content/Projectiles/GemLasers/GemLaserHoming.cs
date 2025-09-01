using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.GemLasers
{
    public class GemLaserHoming : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.GetDustIDForAI(Projectile))];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
            }

            float targetX = Projectile.Center.X;
            float targetY = Projectile.Center.Y;
            float closestEntity = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(Projectile.Center.X - nPC.Center.X) + Math.Abs(Projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity)
                    {
                        closestEntity = dist;
                        targetX = nPC.Center.X;
                        targetY = nPC.Center.Y;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 7f;
                float goToX = targetX - Projectile.Center.X;
                float goToY = targetY - Projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                dist = speed / dist;
                goToX *= dist;
                goToY *= dist;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + goToX) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + goToY) / 21f;
                return;
            }
        }
    }
}