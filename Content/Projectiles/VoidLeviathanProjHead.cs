using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class VoidLeviathanProjHead : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Player player10 = Main.player[Projectile.owner];
            if ((int)Main.time % 120 == 0)
            {
                Projectile.netUpdate = true;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;

            float centerY = Projectile.Center.X;
            float centerX = Projectile.Center.Y;
            float num = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float num1 = Main.npc[i].position.X + Main.npc[i].width / 2;
                    float num2 = Main.npc[i].position.Y + Main.npc[i].height / 2;
                    float num3 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num1) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num2);
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
                float speed = 25f;
                Vector2 vector35 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
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
            for (int i = 0; i < 31; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
            target.immune[Projectile.owner] = 3;
        }
    }
}