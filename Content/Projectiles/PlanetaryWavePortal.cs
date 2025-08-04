using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PlanetaryWavePortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center - new Vector2(0, 70);

            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 220, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
            Projectile.localAI[1]++;
            int numProj = 5;
            if (Projectile.localAI[0] < numProj && Projectile.localAI[1] % 3 == 0)
            {
                float speedX = Projectile.ai[0];
                float speedY = Projectile.ai[1];
                float rotation = MathHelper.ToRadians(1.75f);
                float amount = (Projectile.localAI[0] - numProj / 2);
                float amount2 = player.direction == -1 ? amount - 3 : -amount + 3;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount2));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<PlanetarySpike>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                Projectile.localAI[0]++;
            }
            if (Projectile.localAI[0] >= numProj)
            {
                Projectile.alpha += 30;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 220, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 360);
        }       
    }
}