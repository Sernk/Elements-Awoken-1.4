using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Fireblast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] == 12f)
            {
                Projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)Projectile.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
                    int num9 = Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].position = Projectile.Center + vector3;
                    Main.dust[num9].velocity = Projectile.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                for (int m = 0; m < 1; m++)
                {
                    Vector2 value = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num10 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num10].velocity *= 0.1f;
                    Main.dust[num10].position = Projectile.Center + value * (float)Projectile.width / 2f;
                    Main.dust[num10].fadeIn = 0.9f;
                }
            }
            if (Main.rand.Next(32) == 0)
            {
                for (int n = 0; n < 1; n++)
                {
                    Vector2 value2 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num11 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 155, default(Color), 0.8f);
                    Main.dust[num11].velocity *= 0.3f;
                    Main.dust[num11].position = Projectile.Center + value2 * (float)Projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num11].fadeIn = 1.4f;
                    }
                }
            }
            if (Main.rand.Next(2) == 0)
            {
                for (int num12 = 0; num12 < 2; num12++)
                {
                    Vector2 value3 = -Vector2.UnitX.RotatedByRandom(0.78539818525314331).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num13 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 1.2f);
                    Main.dust[num13].velocity *= 0.3f;
                    Main.dust[num13].noGravity = true;
                    Main.dust[num13].position = Projectile.Center + value3 * (float)Projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num13].fadeIn = 1.4f;
                    }
                }
            }

            Lighting.AddLight(Projectile.Center,0.9f, 0.2f, 0.2f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

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
                float speed = 12f;
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
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 6, damageType: "magic");
        }
    }
}