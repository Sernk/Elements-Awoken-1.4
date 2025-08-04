using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SolusP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public float aiTimer = 0f;
        public Vector2 initialMouse = new Vector2(0,0);
        public int initialDir = 1;
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {        
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
                Main.dust[dust].velocity *= 0.06f;
                Main.dust[dust].scale *= 1.5f;
                Main.dust[dust].position -= Projectile.velocity / 6f * i;
                Main.dust[dust].noGravity = true;
            }

            if (Projectile.localAI[0] == 0)
            {
                initialMouse = Main.MouseWorld;
                initialDir = Main.player[Projectile.owner].direction;
                Projectile.localAI[0]++;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] <= 20)
            {
                float xSpeed = 0.35f;
                Projectile.velocity.X += initialDir == 1 ? -xSpeed : xSpeed;
                Projectile.velocity.Y -= 1f;
            }
            else if (Projectile.ai[0] > 20 && Projectile.ai[0] < 75)
            {
                Projectile.velocity *= 0.9f;
            }
            else if (Projectile.ai[0] == 75)
            {
                Vector2 toTarget = new Vector2(initialMouse.X - Projectile.Center.X, initialMouse.Y - Projectile.Center.Y);
                toTarget.Normalize();
                Projectile.velocity = toTarget * 15f;
            }
            else
            {
                float centerY = Projectile.Center.X;
                float centerX = Projectile.Center.Y;
                float maxDist = 400f;
                bool home = false;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                    {
                        float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                        float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                        float num3 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num2);
                        if (num3 < maxDist)
                        {
                            maxDist = num3;
                            centerY = num1;
                            centerX = num2;
                            home = true;
                        }
                    }
                }
                if (home)
                {
                    float speed = 15f;
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
        }
    }
}