using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.RegarothP
{
    public class RegarothPortal : ModProjectile
    {
        public int laserTimer = 180;

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.95f;
            Projectile.velocity.X *= 0.95f;
            if (Main.rand.Next(4) == 0 && !ModContent.GetInstance<Config>().lowDust)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, Main.rand.Next(2) == 0 ? 135 : 164, 0f, 0f, 100, default, 1)];
                dust.velocity *= 0.2f;
                dust.noGravity = true;
            }
            laserTimer--;
            if (laserTimer <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item33, Projectile.position);

                int type = ModContent.ProjectileType<RegarothBolt>();
                float numberProjectiles = 8;
                float rotation = MathHelper.ToRadians(360);
                float speed = 3f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                    int num1 = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
                laserTimer = 180;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}