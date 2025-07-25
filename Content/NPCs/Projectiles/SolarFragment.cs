using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Projectiles
{
    public class SolarFragment : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 26;
            NPC.aiStyle = 99;
            NPC.damage = 60;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.HitSound = null;
            NPC.DeathSound = null;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.alpha = 0;
            NPC.knockBackResist = 0f;
            NPCID.Sets.TrailCacheLength[NPC.type] = 20;
            NPCID.Sets.TrailingMode[NPC.type] = 7;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Solar Fragment");
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
            Color color9 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
            float num66 = 0f;
            float num67 = Main.NPCAddHeight(NPC);
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Vector2 vector39 = NPC.Center - Main.screenPosition;

            vector39 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[NPC.type])) * NPC.scale / 2f;
            vector39 += vector11 * NPC.scale + new Vector2(0f, num66 + num67 + NPC.gfxOffY);
            texture = TextureAssets.Npc[NPC.type].Value;
            Main.spriteBatch.Draw(texture, vector39, new Rectangle?(NPC.frame), NPC.GetAlpha(color9), NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
            float num143 = 1f / (float)NPC.oldPos.Length * 0.7f;
            int num144 = NPC.oldPos.Length - 1;
            while (num144 >= 0f)
            {
                float num145 = (float)(NPC.oldPos.Length - num144) / (float)NPC.oldPos.Length;
                Color color34 = Color.Pink;
                color34 *= 1f - num143 * (float)num144 / 1f;
                color34.A = (byte)((float)color34.A * (1f - num145));
                Main.spriteBatch.Draw(texture, vector39 + NPC.oldPos[num144] - NPC.position, new Rectangle?(), color34, NPC.oldRot[num144], vector11, NPC.scale * MathHelper.Lerp(0.3f, 1.1f, num145), spriteEffects, 0f);
                num144--;
            }
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Rectangle hitbox2 = NPC.Hitbox;
                for (int num67 = 0; num67 < NPC.oldPos.Length; num67 += 3)
                {
                    hitbox2.X = (int)NPC.oldPos[num67].X;

                    hitbox2.Y = (int)NPC.oldPos[num67].Y;
                    for (int i = 0; i < 5; i++)
                    {
                        int num69 = Utils.SelectRandom<int>(Main.rand, new int[]
                        {
                            6,
                            259,
                            158
                        });
                        int num70 = Dust.NewDust(hitbox2.TopLeft(), NPC.width, NPC.height, num69, 2.5f * (float)hit.HitDirection, -2.5f, 0, default(Color), 1f);
                        Main.dust[num70].alpha = 200;
                        Dust dust = Main.dust[num70];
                        dust.velocity *= 2.4f;
                        dust = Main.dust[num70];
                        dust.scale += Main.rand.NextFloat();
                    }
                }
            }
        }
        public override void AI()
        {
            if (NPC.velocity.Y == 0f && NPC.ai[0] == 0f)
            {
                NPC.ai[0] = 1f;
                NPC.ai[1] = 0f;
                NPC.netUpdate = true;
                return;
            }
            if (NPC.ai[0] == 1f)
            {
                NPC.velocity = Vector2.Zero;
                NPC.position = NPC.oldPosition;
                float[] var_9_49F8F_cp_0 = NPC.ai;
                int var_9_49F8F_cp_1 = 1;
                float num244 = var_9_49F8F_cp_0[var_9_49F8F_cp_1];
                var_9_49F8F_cp_0[var_9_49F8F_cp_1] = num244 + 1f;
                if (NPC.ai[1] >= 5f)
                {
                    NPC.HitEffect(0, 9999.0);
                    NPC.active = false;
                }
                return;
            }
            NPC.velocity.Y = NPC.velocity.Y + 0.01f;
            if (NPC.velocity.Y > 12f)
            {
                NPC.velocity.Y = 12f;
            }
            NPC.rotation = NPC.velocity.ToRotation() - 1.57079637f;

            if (NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = 1f;
                for (int i = 0; i < 13; i++)
                {
                    int num1489 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1489].noGravity = true;
                    Main.dust[num1489].fadeIn = 1f;
                    Dust dust = Main.dust[num1489];
                    dust.velocity *= 4f;
                    Main.dust[num1489].noLight = true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (Main.rand.Next(3) < 2)
                {
                    int num1491 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1491].noGravity = true;
                    Dust dust = Main.dust[num1491];
                    dust.velocity *= 0.2f;
                    Main.dust[num1491].fadeIn = 1f;
                    if (Main.rand.Next(6) == 0)
                    {
                        dust = Main.dust[num1491];
                        dust.velocity *= 30f;
                        Main.dust[num1491].noGravity = false;
                        Main.dust[num1491].noLight = true;
                    }
                    else
                    {
                        Main.dust[num1491].velocity = NPC.DirectionFrom(Main.dust[num1491].position) * Main.dust[num1491].velocity.Length();
                    }
                }
            }
            return;

        }

    }
}