using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SoulswordSoul : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 200;
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 100;
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            AIType = ProjectileID.Bullet;
        }
        public override void OnKill(int timeLeft)
        {
            int k;
            for (int i = 0; i < 50; i = k + 1)
            {
                int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 1f);
                Dust dust = Main.dust[num292];
                dust.velocity *= 2f;
                Main.dust[num292].noGravity = true;
                Main.dust[num292].scale = 1.4f;
                k = i;
            }

            int numberProjectiles = 2 + Main.rand.Next(0,2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                value15.X *= 0.25f;
                value15.Y *= 0.25f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<SoulswordSoul2>(), Projectile.damage / 2, 2f, Projectile.owner, 0f, 0f);
            }
        }
        public override void AI()
        {
            int num748 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, 0f, 0f, 0, default(Color), 1f);
            Dust dust = Main.dust[num748];
            dust.velocity *= 0.1f;
            Main.dust[num748].scale = 1.3f;
            Main.dust[num748].noGravity = true;

            Projectile.alpha += 4;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}