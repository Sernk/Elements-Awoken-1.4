using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class VioletEdgeBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 300;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 180);
        }       
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
            for (int i = 0; i < 2; i++)
            {
                int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Const.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0f;
            }
            Projectile.localAI[1] += 1f;

            if (Projectile.localAI[1] <= 90f)
            {
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
                    float speed = 8f;
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
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 62, damageType: "ranged");
        }
    }
}