using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Barrier : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Barrier");
        }
        public override void AI()
        {
            bool fade = false;
            if (ProjectileUtils.CountProjectiles(Projectile.type, Projectile.owner) > 3)
            {
                if (ProjectileUtils.HasLeastTimeleft(Projectile.whoAmI))
                {
                    fade = true;
                }
            }
            if (Projectile.timeLeft <= 60) fade = true;
            if (fade)
            {
                Projectile.alpha += 255 / 60;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            int maxDist = 75;
            for (int i = 0; i < 12; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, 205, 0, 0, 100,default,0.8f)];
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(Projectile.Center - dust.position);
                dust.fadeIn = 0.3f;
                dust.velocity *= Main.rand.NextFloat(-2,0);
                if (Main.rand.NextBool(10)) dust.velocity *= 3f;
            }
            if (Main.rand.NextBool(3))
            {
                Dust d = Main.dust[Dust.NewDust(Projectile.Center, 0, 0, 205, 0, 0, 100)];
                d.noGravity = true;
                d.scale *= 0.5f;
                d.velocity = Main.rand.NextVector2CircularEdge(maxDist / 8, maxDist / 8);
                d.fadeIn = 1.5f;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - npc.Center.X, Projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity -= toTarget * 2f;
                }
            }
        }
    }
}