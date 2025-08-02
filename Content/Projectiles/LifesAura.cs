using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class LifesAura : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 170;
            Projectile.height = 170;
            Projectile.alpha = 150;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 3600;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int maxDist = 85;
            for (int i = 0; i < 30; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, 75, 0, 0, 100)];
                dust.noGravity = true;
            }
            Projectile.ai[0]++;
            if (ProjectileUtils.CountProjectiles(Projectile.type, Projectile.owner) > 1)
            {
                if (ProjectileUtils.HasLeastTimeleft(Projectile.whoAmI))
                {
                    Projectile.alpha += 5;
                    if (Projectile.alpha >= 255)
                    {
                        Projectile.Kill();
                    }
                }
            }
            if (Projectile.timeLeft < 60)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player loopP = Main.player[i];
                if (loopP.active && !loopP.dead)
                {
                    float dist = Vector2.Distance(Projectile.Center, loopP.Center);
                    if (dist < maxDist)
                    {
                        if (Projectile.ai[0] % 10 == 0 && loopP.statLife < loopP.statLifeMax2 && loopP.team == player.team)
                        {
                            int amount = Main.rand.Next(3, 9);
                            loopP.statLife += amount;
                            loopP.HealEffect(amount, true);
                        }
                    }
                }
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && nPC.townNPC)
                {
                    float dist = Vector2.Distance(Projectile.Center, nPC.Center);
                    if (dist < maxDist)
                    {
                        if (Projectile.ai[0] % 10 == 0 && nPC.life < nPC.life)
                        {
                            int amount = Main.rand.Next(3, 9);
                            nPC.life += amount;
                            nPC.HealEffect(amount, true);
                        }
                    }
                }
            }
        }
    }
}