using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NapalmCannonP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 2;
        public override void AI()
        {
            if (Projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)Projectile.ai[0]];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = stick.gfxOffY;
                }
                else Projectile.Kill();
            }
            else
            {
                Projectile.velocity.Y += 0.13f;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[0] = target.whoAmI;
                Projectile.ai[1] = 1;
                Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
                Projectile.netUpdate = true;
                Projectile.frame = 1;
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.frame == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.8f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
            }
        }
    }
}