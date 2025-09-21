using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class ExlitureEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.penetrate = 1;
            Projectile.minion = true;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Main.projFrames[Projectile.type] = 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 12)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            for (int num121 = 0; num121 < 3; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
            }

            float centerY = Projectile.Center.X;
            float centerX = Projectile.Center.Y;
            float num = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                    float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                    float num3 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num2);
                    if (num3 < num)
                    {
                        num = num3;
                        centerY = num1;
                        centerX = num2;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 3f;
                Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num4 = centerY - vector35.X;
                float num5 = centerX - vector35.Y;
                float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                num6 = speed / num6;
                num4 *= num6;
                num5 *= num6;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num4) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num5) / 21f;
                return;
            }

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 5;
        }
        public override void OnKill(int timeLeft)
        {
            Gore gore = Main.gore[Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, Projectile.velocity, Mod.Find<ModGore>("ExlitureEye1").Type)];
            Gore gore2 = Main.gore[Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, Projectile.velocity, Mod.Find<ModGore>("ExlitureEye2").Type)];
            gore.timeLeft = 60;
            gore2.timeLeft = 60;
        }
    }
}