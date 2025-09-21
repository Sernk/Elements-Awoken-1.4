using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class ImmolatorBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 90;
            Projectile.scale *= 0.95f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.2f, 0.4f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.5f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 4 : 3 : 2;
            float rotation = MathHelper.ToRadians(360);
            float projSpeed = 4f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ImmolatorBolt>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
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