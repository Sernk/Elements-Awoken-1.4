using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class StarLunar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 24;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            int dustID = 6;
            if (Projectile.ai[0] == 1) dustID = 242;
            else if (Projectile.ai[0] == 2) dustID = 197;
            else if (Projectile.ai[0] == 3) dustID = 229;
            else if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                for (int i = 0; i < 13; i++)
                {
                    int num1493 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1493].noGravity = true;
                    Main.dust[num1493].fadeIn = 1f;
                    Dust dust = Main.dust[num1493];
                    dust.velocity *= 4f;
                    Main.dust[num1493].noLight = true;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (Main.rand.Next(10 - (int)Math.Min(7f, Projectile.velocity.Length())) < 1)
                {
                    int num1495 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1495].noGravity = true;
                    Dust dust = Main.dust[num1495];
                    dust.velocity *= 0.2f;
                    Main.dust[num1495].fadeIn = 0.4f;
                    if (Main.rand.Next(6) == 0)
                    {
                        dust = Main.dust[num1495];
                        dust.velocity *= 5f;
                        Main.dust[num1495].noLight = true;
                    }
                    else
                    {
                        Main.dust[num1495].velocity = Projectile.DirectionFrom(Main.dust[num1495].position) * Main.dust[num1495].velocity.Length();
                    }
                }
            }

            Lighting.AddLight(Projectile.Center, 0.2f, 0.4f, 0.6f);

            float centerY = Projectile.Center.X;
            float centerX = Projectile.Center.Y;
            float maxDist = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                    float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                    float num3 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num2);
                    if (num3 < maxDist)
                    {
                        maxDist = num3;
                        centerY = num1;
                        centerX = num2;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 8f;
                Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num4 = centerY - vector35.X;
                float num5 = centerX - vector35.Y;
                float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                num6 = speed / num6;
                num4 *= num6;
                num5 *= num6;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num4) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num5) / 21f;
                return;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(TextureAssets.Projectile[Projectile.type].Value.Width / 2), (float)(TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] / 2));

            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 vector40 = Projectile.Center - Main.screenPosition;
            vector40 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[Projectile.type])) * Projectile.scale / 2f;
            vector40 += vector11 * Projectile.scale + new Vector2(0f, Projectile.gfxOffY);
            float num147 = 1f / (float)Projectile.oldPos.Length * 1.1f;
            int num148 = Projectile.oldPos.Length - 1;
            while ((float)num148 >= 0f)
            {
                float num149 = (float)(Projectile.oldPos.Length - num148) / (float)Projectile.oldPos.Length;
                Color color35 = new Color(255, 123, 0);
                if (Projectile.ai[0] == 1) color35 = new Color(255, 69, 233);
                if (Projectile.ai[0] == 2) color35 = new Color(61, 236, 255);
                if (Projectile.ai[0] == 3) color35 = new Color(56, 255, 172);
                color35 *= 1f - num147 * (float)num148 / 1f;
                color35.A = (byte)((float)color35.A * (1f - num149));
                Main.spriteBatch.Draw(texture, vector40 + Projectile.oldPos[num148] - Projectile.position, null, color35, Projectile.oldRot[num148], vector11, Projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                num148--;
            }
            texture = TextureAssets.Extra[57].Value;
            Color starColor = new Color(255, 123, 0);
            if (Projectile.ai[0] == 1) starColor = new Color(255, 69, 233);
            if (Projectile.ai[0] == 2) starColor = new Color(61, 236, 255);
            if (Projectile.ai[0] == 3) starColor = new Color(56, 255, 172);

            Main.spriteBatch.Draw(texture, vector40, null, starColor, 0f, texture.Size() / 2f, Projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            int dustID = 6;
            if (Projectile.ai[0] == 1) dustID = 242;
            else if (Projectile.ai[0] == 2) dustID = 197;
            else if (Projectile.ai[0] == 3) dustID = 229;

            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(Projectile.Center, 0, 0, dustID, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = Projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
            }

            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-5f,5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<StarLunarFragment>(), (int)(Projectile.damage * 0.8f), 2f, Projectile.owner, Projectile.ai[0], 0f);
            }
        }
    }
}