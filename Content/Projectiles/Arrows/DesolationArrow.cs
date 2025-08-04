using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class DesolationArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 600;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AncientGreen>(), 0f, 0f, 100, default(Color), 1f)];
                dust.noGravity = true;
            }
            if (Projectile.ai[0] > 0)
            {
                Projectile.velocity.Y += Projectile.ai[0];
            }
        }
        public override void OnKill(int timeLeft)
        {
            int numberProjectiles = Main.rand.Next(1,4);
            if (Projectile.ai[1] > 0)
            {
                numberProjectiles = (int)Projectile.ai[1];
            }
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-1.2f, 1.2f), Main.rand.NextFloat(-1.2f, 1.2f), ModContent.ProjectileType<DesolationShard>(), (int)(Projectile.damage * 0.75f), 2f, Projectile.owner, 0f, 0f);
            }
        }
    }
}