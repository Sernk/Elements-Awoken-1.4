using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.InfinityGauntlet
{
    public class FirePortal : ModProjectile
    {
        public int shootTimer = 0;
        public override void SetDefaults()
        {
            Projectile.scale = 1.0f;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            shootTimer--;
            if (shootTimer <= 0)
            {
                float randAi0 = Main.rand.Next(10, 80) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    randAi0 *= -1f;
                }
                float randAi1 = Main.rand.Next(10, 80) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    randAi1 *= -1f;
                }
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-10, 10) * .25f, Main.rand.Next(-10, 10) * .25f, ModContent.ProjectileType<InfinityFireTentacle>(), 100, 0, Projectile.owner, randAi0, randAi1);
                shootTimer = 10;
            }
        }
    }
}