using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AeroStorm : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
            Projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 5)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.ai[0]--;
            Projectile.ai[1]--;
            int spawnWidth = Projectile.width - 4;
            if (Projectile.ai[0] <= 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + Main.rand.Next(-spawnWidth / 2, spawnWidth / 2), Projectile.position.Y + Projectile.height, 0, 10, ProjectileID.RainFriendly, Projectile.damage, 0, Projectile.owner);
                Projectile.ai[0] = 15;
            }
            if (Projectile.ai[1] <= 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + Main.rand.Next(-spawnWidth / 2, spawnWidth / 2), Projectile.position.Y + Projectile.height, Main.rand.NextFloat(-2, 2), 10, ModContent.ProjectileType<AeroLightning>(), Projectile.damage * 2, 0, Projectile.owner);
                Projectile.ai[1] = Main.rand.Next(60, 120);
                Projectile.netUpdate = true;
            }
            if (Projectile.timeLeft <= 100)
            {
                Projectile.alpha += 255 / 60;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
    }
}