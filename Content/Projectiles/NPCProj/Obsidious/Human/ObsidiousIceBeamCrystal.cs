using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousIceBeamCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
        }
        public override void AI()
        {
            Lighting.AddLight((int)Projectile.Center.X, (int)Projectile.Center.Y, 0.5f, 0.2f, 0.2f);

            NPC parent = Main.npc[(int)Projectile.ai[1]];
            Vector2 direction = parent.Center - Projectile.Center;
            Projectile.rotation = direction.ToRotation() + 0.785f;
            Projectile.rotation += MathHelper.ToRadians(45);

            Projectile.ai[0] += 0.85f; // speed
            int distance = 50;
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
            Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;
            if (!parent.active || parent.ai[3] < 600 || parent.ai[1] != 2)
            {
                Projectile.Kill();
            }
        }
    }
}