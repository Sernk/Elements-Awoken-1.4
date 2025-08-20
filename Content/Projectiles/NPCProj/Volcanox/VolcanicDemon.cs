using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Volcanox
{
    public class VolcanicDemon : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.scale = 1.0f;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 3.14f;
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
            }
            Vector2 vector = Projectile.velocity.SafeNormalize(Vector2.UnitY);
            float num2 = Projectile.ai[0] / 60f;
            float num3 = 2f;
            int num4 = 0;
            while ((float)num4 < num3)
            {
                Dust expr_20B = Dust.NewDustDirect(Projectile.Center, 14, 14, 6, 0f, 0f, 110, default(Color), 1f);
                expr_20B.velocity = vector * 2f;
                expr_20B.position = Projectile.Center + vector.RotatedBy((double)(num2 * 6.28318548f * 2f + (float)num4 / num3 * 6.28318548f), default(Vector2)) * 7f;
                expr_20B.scale = 1f + 0.6f * Main.rand.NextFloat();
                expr_20B.velocity += vector * 3f;
                expr_20B.noGravity = true;
                num4++;
            }

            if (Projectile.timeLeft <= 50)
            {
                if (Main.rand.Next(12) == 0)
                {
                    Projectile.Kill();
                }
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, Projectile.Center) <= 50)
            {
                if (Main.rand.Next(6) == 0)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<VolcanicBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > Main.projFrames[Projectile.type] - 1)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}