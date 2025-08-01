﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.RadiantMaster
{
    public class RadiantMasterStar : ModProjectile
    {
        private float rotSpeed = 2;
        public float aiTimer = 0;
        private int dist = 0;
        private int orbitDur = 140;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(dist);
            writer.Write(rotSpeed);
            writer.Write(aiTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            dist = reader.ReadInt32();
            rotSpeed = reader.ReadSingle();
            aiTimer = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.scale *= 1.1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)Projectile.ai[1]];
            Player player = Main.LocalPlayer;
            if (rotSpeed > 0 && aiTimer > 0) rotSpeed -= 2 / orbitDur;

            if (dist < 120) dist += 2;
            if (Projectile.localAI[1] == 0) aiTimer += 1f;
            if (aiTimer == orbitDur)
            {
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
                double angle = Math.Atan2(player.position.Y - Projectile.position.Y, player.position.X - Projectile.position.X);
                Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 20f;
            }
            else if (aiTimer < orbitDur)
            {
                Projectile.ai[0] += rotSpeed;
                int distance = dist;
                double rad = Projectile.ai[0] * (Math.PI / 180);
                Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
                Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;

                if (!parent.active) Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Pink, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = Projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
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
            if (aiTimer >= orbitDur)
            {
                while ((float)num148 >= 0f)
                {
                    float num149 = (float)(Projectile.oldPos.Length - num148) / (float)Projectile.oldPos.Length;
                    Color color35 = Color.White;
                    color35 *= 1f - num147 * (float)num148 / 1f;
                    color35.A = (byte)((float)color35.A * (1f - num149));
                    Const.Sb.Draw(texture, vector40 + Projectile.oldPos[num148] - Projectile.position, null, color35, Projectile.oldRot[num148], vector11, Projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                    num148--;
                }
            }
            texture = TextureAssets.Extra[57].Value;
            Const.Sb.Draw(texture, vector40, null, Color.HotPink, 0f, texture.Size() / 2f, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}