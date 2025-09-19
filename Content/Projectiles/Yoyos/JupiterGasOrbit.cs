using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class JupiterGasOrbit : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100000;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            int dustType = 31;
            switch (Main.rand.Next(3))
            {
                case 0: dustType = 31;break;
                case 1:dustType = 32; break;
                case 2: dustType = 102; break;
                default: break;
            }
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].noGravity = true;

            Projectile parent = Main.projectile[(int)Projectile.ai[1]];

            Projectile.ai[0] += 5f; // speed
            int distance = 75;
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            float targetX = parent.Center.X - (int)(Math.Cos(rad) * distance);
            float targetY = parent.Center.Y - (int)(Math.Sin(rad) * distance);

            Vector2 toTarget = new Vector2(targetX - Projectile.Center.X, targetY - Projectile.Center.Y);
            toTarget.Normalize();
            Projectile.velocity += toTarget * 0.3f;
            if (Vector2.Distance(Projectile.Center, parent.Center) >= 100)  Projectile.velocity *= 0.98f;
            if (!parent.active) Projectile.Kill();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) { if (target.realLife == -1) target.immune[Projectile.owner] = 4; }
    }
}