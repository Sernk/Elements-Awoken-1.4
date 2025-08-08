using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian
{
    public class GuardianTargeter : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 62;
            Projectile.height = 62;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.scale = 0.7f;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.2f, 0.4f);
            Projectile.rotation += 0.05f;
            if (Projectile.alpha > 0)  Projectile.alpha -= 255 / 30;

            int innerCircle = 18;
            Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(innerCircle * 0.5f, innerCircle * 0.5f);
            Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
            dust.noGravity = true;
            dust.velocity = Projectile.velocity * -0.2f;

            Player P = Main.player[(int)Projectile.ai[1]];
            if (!P.active)Projectile.Kill();

            float speed = MathHelper.Lerp(4f,7.5f, Vector2.Distance(P.Center, Projectile.Center) / 200);
            float speedScale = Main.expertMode ? 1.5f : 1;
            if (MyWorld.awakenedMode) speedScale = 2f;
            speed *= speedScale;
            double angle = Math.Atan2(P.Center.Y - Projectile.Center.Y, P.Center.X - Projectile.Center.X);
            Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
        }
    }
}