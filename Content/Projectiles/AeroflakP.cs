using ElementsAwoken.Content.Projectiles.Explosions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AeroflakP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.localAI[0] == 0)
            {
                Projectile.ai[0] = Main.rand.NextFloat((float)Math.PI * 2);
                Projectile.localAI[0]++;
            }
            Projectile.ai[0] += 0.02f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.noGravity && Vector2.Distance(nPC.Center, Projectile.Center) < 100 && nPC.CanBeChasedBy(Projectile, false) && Collision.CanHitLine(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    Explosion();
                    int numDusts = 40;
                    for (int k = 0; k < numDusts; k++)
                    {
                        Vector2 position = (Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(k - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                        Vector2 velocity = position - Projectile.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, 229, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 9f;
                    }
                    Projectile.Kill();
                    break;
                }
            }
            if (Projectile.oldPos[Projectile.oldPos.Length - 1] == Projectile.position && Projectile.ai[1] != 0) Projectile.Kill();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[1] = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1;
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] != 0) return false;
            return base.CanHitNPC(target);
        }
        public override bool ShouldUpdatePosition()
        {
            if (Projectile.ai[1] != 0) return false;
            return base.ShouldUpdatePosition();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                if (Projectile.oldPos[k] == Projectile.position && Projectile.ai[1] != 0) continue;
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);

                float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(85, 44, 156), (float)k / (float)Projectile.oldPos.Length) * scale;

                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }

            Texture2D star = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/AeroflakHead").Value;
            Vector2 starOrigin = new Vector2(star.Width * 0.5f, star.Height * 0.5f);
            Vector2 starPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            if (Projectile.ai[1] == 0) Main.spriteBatch.Draw(star, starPos, null, Color.White, Projectile.ai[0], starOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        private void Explosion()
        {
            Projectile exp = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<AeroflakExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f)];
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                int dustID = 229;
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                int dustID2 = 229;
                dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 6f;
            }
            int num373 = Gore.NewGore(EAU.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(EAU.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(EAU.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(EAU.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}