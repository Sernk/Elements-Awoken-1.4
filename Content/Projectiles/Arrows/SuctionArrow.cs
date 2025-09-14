using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class SuctionArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 400;
            Projectile.penetrate = -1;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)Projectile.ai[0]];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = stick.gfxOffY;
                    Projectile.localAI[0]++;
                }
                else Projectile.Kill();
            }
            else
            {
                Projectile.velocity.Y += 0.13f;
            }
        }
        private void DeleteOldest(NPC target)
        {
            int lowestTimeLeftID = Projectile.whoAmI;
            int numStuck = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != Projectile.whoAmI && currentProjectile.active && currentProjectile.owner == Main.myPlayer && currentProjectile.type == Projectile.type && currentProjectile.ai[1] == 1 && currentProjectile.ai[0] == target.whoAmI)
                {
                    if (currentProjectile.timeLeft < Main.projectile[lowestTimeLeftID].timeLeft) lowestTimeLeftID = currentProjectile.whoAmI;
                    numStuck++;
                    if (numStuck > 3)
                        break;
                }
            }
            if (numStuck > 3) Main.projectile[lowestTimeLeftID].Kill();
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] == 1 && Projectile.localAI[0] % 60 != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[0] = target.whoAmI;
                Projectile.ai[1] = 1;
                Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
                Projectile.netUpdate = true;
                DeleteOldest(target);
            }
        }
    }
}