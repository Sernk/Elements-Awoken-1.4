using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class TerreneMortar : ModProjectile
    {
        public int shootTimer = 30;
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 42;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 12)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.velocity.X = 0f;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            shootTimer--;
            if (shootTimer <= 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-3, 3), -8, ModContent.ProjectileType<TerreneRock>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                shootTimer = Main.rand.Next(5, 40);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}