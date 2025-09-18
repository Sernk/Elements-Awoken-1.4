using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CrimsonShadeHoming : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Shade");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 127, 0f, 0f, 0, default(Color), 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.5f;
            Main.dust[dust].velocity += Projectile.velocity * 0.1f;

            // to minimise the loops required and cause lag
            if (Projectile.ai[0] == 0)
            {
                float num = 400f;
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
                            Projectile.ai[0] = Main.npc[i].whoAmI;
                        }
                    }
                }
            }
            else
            {
                NPC target = Main.npc[(int)Projectile.ai[0]];
                if (target.active)
                {
                    float speed = 7f;
                    float num4 = target.Center.X - Projectile.Center.X;
                    float num5 = target.Center.Y - Projectile.Center.Y;
                    float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                    num6 = speed / num6;
                    num4 *= num6;
                    num5 *= num6;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num4) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num5) / 21f;
                }
                else
                {
                    Projectile.ai[0] = 0;
                }
            }
        }
    }
}