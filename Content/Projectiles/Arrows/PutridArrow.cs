using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class PutridArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 600;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Putrid Arrow");
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.velocity.Y += 0.06f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 46, 0f, 0f, 150, default(Color), 1f)];
                dust.noGravity = true;
            }
            if (Projectile.ai[0] > 0)
            {
                Projectile.velocity.Y += Projectile.ai[0];
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (Main.rand.NextBool(3))
            {
                for (int i = 0; i < Main.rand.Next(1,4); i++)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center, Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(6)) * 0.5f, ProjectileType<PutridGoop>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
                }
            }
        }
    }
}