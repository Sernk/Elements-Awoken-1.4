﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AstralStar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1200;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                for (int i = 0; i < 13; i++)
                {
                    int num1493 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 261, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
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
                    int num1495 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 261, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
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
            Projectile.velocity *= 0.995f;
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
                Color color35 = Color.White;
                color35 *= 1f - num147 * (float)num148 / 1f;
                color35.A = (byte)((float)color35.A * (1f - num149));
                Main.spriteBatch.Draw(texture, vector40 + Projectile.oldPos[num148] - Projectile.position, null, color35, Projectile.oldRot[num148], vector11, Projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                num148--;
            }
            texture = TextureAssets.Extra[57].Value;
            Main.spriteBatch.Draw(texture, vector40, null, Color.White, 0f, texture.Size() / 2f, Projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(Projectile.Center, 0, 0, 66, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = Projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
            }
        }
    }
}