using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class InfernoCloud : ModProjectile
    {
        public int shootTimer = 5;
        public int shootTimer2 = 5;
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
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
            shootTimer--;
            shootTimer2--;
            if (shootTimer <= 0)
            {
                int rand = Main.rand.Next(-20, 20);
                int rand2 = Main.rand.Next(-2, 2);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + rand, Projectile.Center.Y, rand2, 10, ModContent.ProjectileType<GreekFire>(), Projectile.damage, 0, Projectile.owner);
                shootTimer = 15;
            }
            if (shootTimer2 <= 0)
            {
                int rand = Main.rand.Next(-20, 20);
                int rand2 = Main.rand.Next(-2, 2);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + rand, Projectile.Center.Y, rand2, 10, ModContent.ProjectileType<InfernoLightning>(), Projectile.damage * 2, 0, Projectile.owner);
                shootTimer2 = 30 + Main.rand.Next(0, 90);
            }
            if (Projectile.timeLeft <= 100)
            {
                Projectile.alpha += 3;
            }
        }
    }
}