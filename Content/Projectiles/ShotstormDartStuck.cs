using ElementsAwoken.Content.Buffs.Debuffs;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class ShotstormDartStuck : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)Projectile.ai[0]];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = stick.gfxOffY;

                    Projectile.alpha += 3;
                    if (Projectile.alpha >= 255) Projectile.Kill();
                }
                else Projectile.Kill();
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
             return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = target.whoAmI;
            Projectile.ai[1] = 1;
            Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
            target.AddBuff(BuffType<FastPoison>(), 60);
        }
    }
}