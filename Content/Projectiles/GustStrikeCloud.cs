using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class GustStrikeCloud : ModProjectile
    {
        public int shootTimer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gust Strike");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[1]++;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.alpha > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                shootTimer = Main.rand.Next(20, 180);
                Projectile.localAI[0]++;
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.ai[0] += (float)(Math.PI / 10);
            for (int j = 0; j < 2; j++)
            {
                int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
                for (int i = 0; i < dustLength; i++)
                {
                    float Y = ((float)Math.Sin(Projectile.ai[0]) * 5) * (j == 0 ? 1 : -1) + (j == 0 ? 1 : -1) * 10;
                    Vector2 dustPos = new Vector2(Y, 0);
                    dustPos = dustPos.RotatedBy((double)Projectile.rotation, default(Vector2));

                    Dust dust = Main.dust[Dust.NewDust(Projectile.Center + dustPos - Vector2.One * 5f, 2, 2, 31)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                    dust.alpha = Projectile.alpha;
                }
            }
            shootTimer--;
            if (shootTimer <= 0)
            {
                for (int l = 0; l < 200; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height) && Vector2.Distance(Projectile.Center, nPC.Center) <= 300)
                    {
                        float projSpeed = 8f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                        float speedX = nPC.Center.X - Projectile.Center.X;
                        float speedY = nPC.Center.Y - Projectile.Center.Y;
                        float num406 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                        num406 = projSpeed / num406;
                        speedX *= num406;
                        speedY *= num406;

                        SoundEngine.PlaySound(SoundID.Item60.WithPitchOffset(-0.2f), Projectile.position);
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speedX, speedY, ModContent.ProjectileType<GustStrikeLightning>(), Projectile.damage / 2, 0f, Projectile.owner);
                        shootTimer = Main.rand.Next(20, 180);
                        return;
                    }
                }
            }
            if (Projectile.ai[1] != 0)
            {
                Projectile.alpha += 7;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }

            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]);
                EAU.Sb.Draw(tex, drawPos, rectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}