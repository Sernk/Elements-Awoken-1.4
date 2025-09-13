using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class WyvernBreath1 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 3; num121++)
            {
                {
                    Vector2 pos = new Vector2(Projectile.position.X, Projectile.position.Y);
                    int num348 = Dust.NewDust(pos, Projectile.width, Projectile.height, 234, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 1.2f);
                    Main.dust[num348].position = (Main.dust[num348].position + Projectile.Center) / 2f;
                    Main.dust[num348].noGravity = true;
                    Main.dust[num348].velocity *= 0.5f;
                }
            }
            Projectile.velocity.Y += 0.05f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void OnKill(int timeLeft)
        {
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<WyvernBreath2>(), 32, 2f, Projectile.owner, 0f, 0f);
            }
        }
    }
}