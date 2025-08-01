using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class AquaticArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.arrow = true;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Lighting.AddLight((int)Projectile.Center.X, (int)Projectile.Center.Y, 0f, 0.1f, 0.5f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Dust dust = Main.dust[Dust.NewDust(Projectile.Center, 4, 4, 111)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] >= 20)
            {
                float numberProjectiles = 6;
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AquaticArrow2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
                Projectile.Kill();
            }
        }
    }
}