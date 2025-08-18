using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousHumanRockOrbital : ModProjectile
    {
        int type = 0;
        int rotType = 0;
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(0, 3);
                rotType = Main.rand.Next(0, 1);
                Projectile.localAI[0] = 1;
            }
            if (rotType == 0)
            {
                Projectile.rotation += 3;
            }
            if (rotType == 1)
            {
                Projectile.rotation -= 3;
            }

            NPC parent = Main.npc[(int)Projectile.ai[1]];
            if (parent != Main.npc[0])
            {
                Projectile.ai[0] += 3f; // speed
                int distance = 35;
                double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
                Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
                Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;
            }
            if (!parent.active || parent.ai[1] != 1)
            {
                Projectile.Kill();
            }
        }
    }
}
    