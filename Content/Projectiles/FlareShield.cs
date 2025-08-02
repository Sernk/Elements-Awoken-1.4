using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FlareShield : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10000;
        }
        public override void AI()
        {
            Player P = Main.player[Projectile.owner];

            Projectile.position.X = P.position.X;
            Projectile.position.Y = P.position.Y;

            int maxDist = 100;
            for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, 6, 0, 0, 100)];
                dust.noGravity = true;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - npc.Center.X, Projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity -= toTarget * 0.3f;
                }
            }
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.hostile && Vector2.Distance(proj.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - proj.Center.X, Projectile.Center.Y - proj.Center.Y);
                    toTarget.Normalize();
                    proj.velocity -= toTarget;
                }
            }
            if (P.FindBuffIndex(ModContent.BuffType<Buffs.Other.FlareShield>()) == -1 || P.dead)
            {
                Projectile.active = false;
            }
        }
    }
}