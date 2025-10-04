using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.ItemSets.Radia;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.RadiantRain.Enemies
{
    public class RadiantWarrior : ModNPC
    {
        private float aiState
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 50;
            NPC.aiStyle = -1;
            NPC.lifeMax = 7600;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.knockBackResist = 0.5f;
            SpawnModBiomes = [ModContent.GetInstance<RadiantRainBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.RadiantWarrior")]);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -3f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 10;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 15000;
            NPC.damage = 150;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 20000;
                NPC.damage = 200;
                NPC.defense = 65;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (aiState <= 0)
            {
                NPC.frameCounter += Math.Abs(NPC.velocity.X);
                if (NPC.frameCounter > 12 && NPC.frame.Y != frameHeight * 9)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 4)   NPC.frame.Y = 0;
            }
            else
            {
                if (NPC.frame.Y < frameHeight * 4) NPC.frame.Y = frameHeight * 4;
                NPC.frameCounter += 1;
                if (NPC.frameCounter > 8 && NPC.frame.Y != frameHeight * 9)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                    if (NPC.frame.Y == frameHeight * 5 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projDamage = Main.expertMode ? MyWorld.awakenedMode ? 150 : 100 : 75;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X + 30 * NPC.spriteDirection, NPC.Center.Y, 0, 0, ProjectileType<WarriorSlice>(), projDamage, 0f, Main.myPlayer, NPC.whoAmI, 0f)];
                        proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                    }
                }

                if (NPC.frame.Y > frameHeight * 9)   NPC.frame.Y = frameHeight * 9;

                if (NPC.frameCounter > 10)
                {
                    aiState = -90;
                    NPC.frame.Y = 0;
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Radia>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<RadiantKatana>(), 20));
        }
        public override void AI()
        {
            NPC.TargetClosest(false);
            Player P = Main.player[NPC.target];
            if (aiState <= 0)
            {
                if (aiState < 0) aiState++;
                if (Math.Abs(P.Center.X - NPC.Center.X) < 200 && Math.Abs(P.Center.Y - NPC.Center.Y) < 30 && aiState == 0 && (NPC.velocity.Y ==0 || MyWorld.awakenedMode))
                {
                    aiState = 1;
                    NPC.velocity.X += Math.Sign(P.Center.X - NPC.Center.X) * 6;
                    NPC.velocity.Y -= 3f;
                }
                NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
                if (NPC.Bottom.Y - P.Bottom.Y >= 160 && NPC.Bottom.Y - P.Bottom.Y <= 320 && Math.Abs(P.Center.X - NPC.Center.X) < 100 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.Y -= MathHelper.Lerp(10, 14.5f, (NPC.Center.Y - P.Center.Y - 160) / 160);
                }
                CustomAI_3();
            }
            else
            {
                if (NPC.velocity.Y == 0)  NPC.velocity.X *= 0.9f;
            }
        }
        private void CustomAI_3()
        {
            bool flag3 = false;
            if (NPC.velocity.X == 0f)
            {
                flag3 = true;
            }
            if (NPC.justHit)
            {
                flag3 = false;
            }
            int num35 = 60;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = true;

            if (!flag6 & flag7)
            {
                if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
                {
                    flag4 = true;
                }
                if ((NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num35) | flag4)
                {
                    NPC.ai[3] += 1f;
                }
                else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f)
                {
                    NPC.ai[3] -= 1f;
                }
                if (NPC.ai[3] > (float)(num35 * 10))
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.justHit)
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.ai[3] == (float)num35)
                {
                    NPC.netUpdate = true;
                }
            }

            float speed = 0.1f;
            if (MyWorld.awakenedMode) speed = 0.15f;
            if (NPC.velocity.X < -1.5f || NPC.velocity.X > 1.5f)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.88f;
                }
            }
            else if (NPC.velocity.X < 2f && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X > 2f)
                {
                    NPC.velocity.X = 2f;
                }
            }
            else if (NPC.velocity.X > -2f && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X < -2f)
                {
                    NPC.velocity.X = -2f;
                }
            }


            bool flag22 = false;
            if (NPC.velocity.Y == 0f)
            {
                int num161 = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
                int arg_A8FB_0 = (int)NPC.position.X / 16;
                int num162 = (int)(NPC.position.X + (float)NPC.width) / 16;
                for (int num163 = arg_A8FB_0; num163 <= num162; num163++)
                {
                    if (Main.tile[num163, num161] == null)
                    {
                        return;
                    }
                    if (Main.tile[num163, num161].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num163, num161].TileType])
                    {
                        flag22 = true;
                        break;
                    }
                }
            }
            if (NPC.velocity.Y >= 0f)
            {
                StepUpTiles();
            }
            if (flag22)
            {
                int num170 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
                int num171 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                //if (npc.type == 257)
                {
                    num170 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 16) * NPC.direction)) / 16f);
                }
                if (Main.tile[num170, num171] == null)
                {
                    Tile tile = Main.tile[num170, num171];
                    tile = new Tile();
                }
                if (Main.tile[num170, num171 - 1] == null)
                {
                    Tile tile = Main.tile[num170, num171 - 1];
                    tile = new Tile();
                }
                if (Main.tile[num170, num171 - 2] == null)
                {
                    Tile tile = Main.tile[num170, num171 - 2];
                    tile = new Tile();
                }
                if (Main.tile[num170, num171 - 3] == null)
                {
                    Tile tile = Main.tile[num170, num171 - 3];
                    tile = new Tile();
                }
                if (Main.tile[num170, num171 + 1] == null)
                {
                    Tile tile = Main.tile[num170, num171 + 1];
                    tile = new Tile();
                }
                if (Main.tile[num170 + NPC.direction, num171 - 1] == null)
                {
                    Tile tile = Main.tile[num170 + NPC.direction, num171 - 1];
                    tile = new Tile();
                }
                if (Main.tile[num170 + NPC.direction, num171 + 1] == null)
                {
                    Tile tile = Main.tile[num170 + NPC.direction, num171 + 1];
                    tile = new Tile();
                }
                if (Main.tile[num170 - NPC.direction, num171 + 1] == null)
                {
                    Tile tile = Main.tile[num170 - NPC.direction, num171 + 1];
                    tile = new Tile();
                }
                Tile tileNal = Main.tile[num170, num171 + 1];
                tileNal.IsHalfBlock = false;
                if ((Main.tile[num170, num171 - 1].HasUnactuatedTile && (Main.tile[num170, num171 - 1].TileType == 10 || Main.tile[num170, num171 - 1].TileType == 388)) & flag5)
                {
                    NPC.ai[2] += 1f;
                    NPC.ai[3] = 0f;
                    if (NPC.ai[2] >= 60f)
                    {
                        NPC.velocity.X = 0.5f * (float)(-(float)NPC.direction);
                        int num172 = 5;
                        if (Main.tile[num170, num171 - 1].TileType == 388)
                        {
                            num172 = 2;
                        }
                        NPC.ai[1] += (float)num172;

                        NPC.ai[2] = 0f;
                        bool flag23 = false;
                        if (NPC.ai[1] >= 10f)
                        {
                            flag23 = true;
                            NPC.ai[1] = 10f;
                        }
                        WorldGen.KillTile(num170, num171 - 1, true, false, false);
                        if ((Main.netMode != NetmodeID.MultiplayerClient || !flag23) && flag23 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (Main.tile[num170, num171 - 1].TileType == 10)
                            {
                                bool flag24 = WorldGen.OpenDoor(num170, num171 - 1, NPC.direction);
                                if (!flag24)
                                {
                                    NPC.ai[3] = (float)num35;
                                    NPC.netUpdate = true;
                                }
                                if (Main.netMode == 2 & flag24)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 0, (float)num170, (float)(num171 - 1), (float)NPC.direction, 0, 0, 0);
                                }
                            }
                            if (Main.tile[num170, num171 - 1].TileType == 388)
                            {
                                bool flag25 = WorldGen.ShiftTallGate(num170, num171 - 1, false);
                                if (!flag25)
                                {
                                    NPC.ai[3] = (float)num35;
                                    NPC.netUpdate = true;
                                }
                                if (Main.netMode == 2 & flag25)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 4, (float)num170, (float)(num171 - 1), 0f, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    int num173 = NPC.spriteDirection;
                    if ((NPC.velocity.X < 0f && num173 == -1) || (NPC.velocity.X > 0f && num173 == 1))
                    {
                        if (NPC.height >= 32 && Main.tile[num170, num171 - 2].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num170, num171 - 2].TileType])
                        {
                            if (Main.tile[num170, num171 - 3].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num170, num171 - 3].TileType])
                            {
                                NPC.velocity.Y = -8f;
                                NPC.netUpdate = true;
                            }
                            else
                            {
                                NPC.velocity.Y = -7f;
                                NPC.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num170, num171 - 1].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num170, num171 - 1].TileType])
                        {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        }
                        else if (NPC.position.Y + (float)NPC.height - (float)(num171 * 16) > 20f && Main.tile[num170, num171].HasUnactuatedTile && !Main.tile[num170, num171].TopSlope && Main.tileSolid[(int)Main.tile[num170, num171].TileType])
                        {
                            NPC.velocity.Y = -5f;
                            NPC.netUpdate = true;
                        }
                        else if (NPC.directionY < 0 && (!Main.tile[num170, num171 + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num170, num171 + 1].TileType]) && (!Main.tile[num170 + NPC.direction, num171 + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num170 + NPC.direction, num171 + 1].TileType]))
                        {
                            NPC.velocity.Y = -8f;
                            NPC.velocity.X = NPC.velocity.X * 1.5f;
                            NPC.netUpdate = true;
                        }
                        else if (flag5)
                        {
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                        }
                        if ((NPC.velocity.Y == 0f & flag3) && NPC.ai[3] == 1f)
                        {
                            NPC.velocity.Y = -5f;
                        }
                    }
                }
            }
            else if (flag5)
            {
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
            }
        }
        private void StepUpTiles()
        {
            int num164 = 0;
            if (NPC.velocity.X < 0f)
            {
                num164 = -1;
            }
            if (NPC.velocity.X > 0f)
            {
                num164 = 1;
            }
            Vector2 position2 = NPC.position;
            position2.X += NPC.velocity.X;
            int num165 = (int)((position2.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num164)) / 16f);
            int num166 = (int)((position2.Y + (float)NPC.height - 1f) / 16f);
            if (Main.tile[num165, num166] == null)
            {
                Tile tile = Main.tile[num165, num166];
                tile = new Tile();
            }
            if (Main.tile[num165, num166 - 1] == null)
            {
                Tile tile = Main.tile[num165, num166 - 1];
                tile = new Tile();
            }
            if (Main.tile[num165, num166 - 2] == null)
            {
                Tile tile = Main.tile[num165, num166 - 2];
                tile = new Tile();
            }
            if (Main.tile[num165, num166 - 3] == null)
            {
                Tile tile = Main.tile[num165, num166 - 3];
                tile = new Tile();
            }
            if (Main.tile[num165, num166 + 1] == null)
            {
                Tile tile = Main.tile[num165, num166 + 1];
                tile = new Tile();
            }
            if (Main.tile[num165 - num164, num166 - 3] == null)
            {
                Tile tile = Main.tile[num165 - num164, num166 - 3];
                tile = new Tile();
            }
            if ((float)(num165 * 16) < position2.X + (float)NPC.width && (float)(num165 * 16 + 16) > position2.X && ((Main.tile[num165, num166].HasUnactuatedTile && !Main.tile[num165, num166].TopSlope && !Main.tile[num165, num166 - 1].TopSlope && Main.tileSolid[(int)Main.tile[num165, num166].TileType] && !Main.tileSolidTop[(int)Main.tile[num165, num166].TileType]) || (Main.tile[num165, num166 - 1].IsHalfBlock && Main.tile[num165, num166 - 1].HasUnactuatedTile)) && (!Main.tile[num165, num166 - 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num165, num166 - 1].TileType] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 1].TileType] || (Main.tile[num165, num166 - 1].IsHalfBlock && (!Main.tile[num165, num166 - 4].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num165, num166 - 4].TileType] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 4].TileType]))) && (!Main.tile[num165, num166 - 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num165, num166 - 2].TileType] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 2].TileType]) && (!Main.tile[num165, num166 - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num165, num166 - 3].TileType] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 3].TileType]) && (!Main.tile[num165 - num164, num166 - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num165 - num164, num166 - 3].TileType]))
            {
                float num167 = (float)(num166 * 16);
                if (Main.tile[num165, num166].IsHalfBlock)
                {
                    num167 += 8f;
                }
                if (Main.tile[num165, num166 - 1].IsHalfBlock)
                {
                    num167 -= 8f;
                }
                if (num167 < position2.Y + (float)NPC.height)
                {
                    float num168 = position2.Y + (float)NPC.height - num167;
                    float num169 = 16.1f;
                    if (num168 <= num169)
                    {
                        NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num167;
                        NPC.position.Y = num167 - (float)NPC.height;
                        if (num168 < 9f)
                        {
                            NPC.stepSpeed = 1f;
                        }
                        else
                        {
                            NPC.stepSpeed = 2f;
                        }
                    }
                }
            }
            FallThroughPlatforms();
        }
        private void FallThroughPlatforms()
        {
            Player P = Main.player[NPC.target];
            Vector2 platform = NPC.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.TileType] && NPC.Bottom.Y < P.Bottom.Y && platformTile.HasTile) NPC.position.Y += 0.3f;
        }
    }
}
