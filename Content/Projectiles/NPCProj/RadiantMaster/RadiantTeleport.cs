using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Content.Projectiles.NPCProj.RadiantMaster
{
    public class RadiantTeleport : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 150;

            Projectile.hostile = true;
            Projectile.tileCollide = false;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Master");
            Main.projFrames[Projectile.type] = 6;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[1] != 0)
            {
                Projectile.alpha += 255 / 20;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[1] == 0 && Projectile.ai[0] >= 0)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 2)
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                    if (Projectile.frame == 5)
                        Projectile.ai[1]++;
                }
            }
            return true;
        }
    }
}