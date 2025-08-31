using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan.ElderShadeWyrm
{
    [AutoloadBossHead]
    class ElderShadeWyrmHead : ElderShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/ElderShadeWyrm/ElderShadeWyrmHead"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            NPC.width = 68;
            NPC.height = 88;
            NPC.damage = 50;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.scale = 1.1f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.npcSlots = 1f;
            NPC.netAlways = true;
            SpawnModBiomes = new int[1] { GetInstance<Emptiness>().Type };
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.8f,
                PortraitScale = 0.7f,
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/ElderShadeWyrmBestiary"
            };
            value.Position.X += 40f;
            value.Position.Y += 20f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            var EALocalization = GetInstance<EALocalization>();
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement(EALocalization.ElderShadeWyrm),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 65000;
            NPC.damage = 90;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 75000;
                NPC.damage = 150;
                NPC.defense = 15;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }

        public override void Init()
        {
            base.Init();
            head = true;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                modifiers.SourceDamage.Base = 20;
            }
        }
        public override void CustomBehavior()
        {
            Player P = Main.player[NPC.target];
            MyPlayer modPlayer = P.GetModPlayer<MyPlayer>();
            if (!NPC.AnyNPCs(NPCType<VoidLeviathanHead>())) NPC.active = false;
        }
    }

    class ElderShadeWyrmBody : ElderShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/ElderShadeWyrm/ElderShadeWyrmBody"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            NPC.width = 38;
            NPC.height = 50;

            NPC.damage = 150;
            NPC.defense = 90;
            NPC.knockBackResist = 0.0f;

            NPC.scale = 1.1f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;

            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 175;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 200;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }


        private int attackCounter;
        private int projectileBaseDamage = 150;
        public int bodyNum = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void CustomBehavior()
        {
            Player P = Main.player[NPC.target];

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num = Main.expertMode ? 2 : 3;
                if (MyWorld.awakenedMode) num = 1;
                if (bodyNum % num == 0)
                {
                    if (aiTimer == 1100 + 10 * bodyNum)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                            if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 2f);

                            float speedMult = Main.expertMode ? 9f : 7f;
                            if (MyWorld.awakenedMode) speedMult = 12f;

                            Vector2 projSpeed = new Vector2(0, 1).RotatedBy(NPC.rotation + MathHelper.ToRadians(k == 0 ? 90 : 270));
                            projSpeed.Normalize();
                            projSpeed *= speedMult;

                            Projectile bolt = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, projSpeed, ProjectileType<VoidBolt>(), projDamage, 0f, 0)];
                            bolt.Name = "Elder Shade Wyrm Bolt";
                            bolt.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                        SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                    }
                }
            }
        }
    }

    class ElderShadeWyrmTail : ElderShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/ElderShadeWyrm/ElderShadeWyrmTail"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            NPC.width = 48;
            NPC.height = 40;

            NPC.damage = 150;
            NPC.defense = 90;
            NPC.knockBackResist = 0.0f;

            NPC.scale = 1.1f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;

            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 175;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 200;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    // I made this 2nd base class to limit code repetition.
    public abstract class ElderShadeWyrm : ElderShadeWyrmAI
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 40000;

            NPCsGLOBAL.ImmuneAllEABuffs(NPC);
            // all vanilla buffs
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elder Shade Wyrm");
        }

        public override void Init()
        {
            wormLength = 20;
            tailType = NPCType<ElderShadeWyrmTail>();
            bodyType = NPCType<ElderShadeWyrmBody>();
            headType = NPCType<ElderShadeWyrmHead>();
            speed = 10f;
            if (MyWorld.awakenedMode) speed = 25f;
            else if (Main.expertMode) speed = 20f;
            turnSpeed =  0.4f;
            if (MyWorld.awakenedMode) turnSpeed = 0.8f;
            else if (Main.expertMode) turnSpeed = 0.6f;
            flies = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }

    public abstract class ElderShadeWyrmAI : ModNPC
    {
        /* ai[0] = follower
		 * ai[1] = following
		 * ai[2] = distanceFromTail
		 * ai[3] = head
		 */
        public bool head;
        public bool tail;
        public int wormLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool flies = false;
        public bool directional = false;
        public float speed;
        public float turnSpeed;
        public bool tooFar = false;


        public float aiTimer = 0;
        private float wanderTimer = 0;
        private float wanderX = 0;
        private float wanderY = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
            writer.Write(wanderTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
            wanderTimer = reader.ReadSingle();
        }

        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC headNPC = Main.npc[(int)NPC.ai[3]];
            if (NPC.localAI[1] == 0f)
            {
                NPC.localAI[1] = 1f;
                Init();
            }

            aiTimer++;
            if (aiTimer > 1100 + wormLength * 10) aiTimer = 0;
            //worm stuff down
            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (!head && NPC.timeLeft < 300)
            {
                NPC.timeLeft = 300;
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
            {
                NPC.timeLeft = 300;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!tail && NPC.ai[0] == 0f)
                {
                    if (head)
                    {
                        NPC.ai[3] = (float)NPC.whoAmI;
                        NPC.realLife = NPC.whoAmI;
                        NPC.ai[2] = wormLength;
                        NPC.ai[0] = (float)NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), bodyType, NPC.whoAmI);
                    }
                    else if (NPC.ai[2] > 0f)
                    {
                        NPC.ai[0] = (float)NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), NPC.type, NPC.whoAmI);
                        ElderShadeWyrmBody bodyNPC = (ElderShadeWyrmBody)NPC.ModNPC;
                        ElderShadeWyrmBody newBodyNPC = (ElderShadeWyrmBody)Main.npc[(int)NPC.ai[0]].ModNPC;
                        newBodyNPC.bodyNum = bodyNPC.bodyNum + 1;
                    }
                    else
                    {
                        NPC.ai[0] = (float)NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), tailType, NPC.whoAmI);
                    }
                    Main.npc[(int)NPC.ai[0]].ai[3] = NPC.ai[3];
                    Main.npc[(int)NPC.ai[0]].realLife = NPC.realLife;
                    Main.npc[(int)NPC.ai[0]].ai[1] = (float)NPC.whoAmI;
                    Main.npc[(int)NPC.ai[0]].ai[2] = NPC.ai[2] - 1f;
                    NPC.netUpdate = true;
                }
                if (!head && (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[1]].type != headType && Main.npc[(int)NPC.ai[1]].type != bodyType))
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
                if (!tail && (!Main.npc[(int)NPC.ai[0]].active || Main.npc[(int)NPC.ai[0]].type != bodyType && Main.npc[(int)NPC.ai[0]].type != tailType))
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
                if (!NPC.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            int num180 = (int)(NPC.position.X / 16f) - 1;
            int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num182 = (int)(NPC.position.Y / 16f) - 1;
            int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            bool collision = flies;
            if (!collision)
            {
                for (int num184 = num180; num184 < num181; num184++)
                {
                    for (int num185 = num182; num185 < num183; num185++)
                    {
                        if (Main.tile[num184, num185] != null && (Main.tile[num184, num185].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num184, num185].TileType] || Main.tileSolidTop[(int)Main.tile[num184, num185].TileType] && Main.tile[num184, num185].TileFrameY == 0) || Main.tile[num184, num185].LiquidAmount > 64))
                        {
                            Vector2 vector17;
                            vector17.X = (float)(num184 * 16);
                            vector17.Y = (float)(num185 * 16);
                            if (NPC.position.X + (float)NPC.width > vector17.X && NPC.position.X < vector17.X + 16f && NPC.position.Y + (float)NPC.height > vector17.Y && NPC.position.Y < vector17.Y + 16f)
                            {
                                collision = true;
                                if (Main.rand.NextBool(100) && NPC.behindTiles && Main.tile[num184, num185].HasUnactuatedTile)
                                {
                                    WorldGen.KillTile(num184, num185, true, true, false);
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].TileType == 2)
                                {
                                    ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].TileType;
                                }
                            }
                        }
                    }
                }
            }
            if ((!collision) && head)
            {
                Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int num186 = 1000;
                bool flag19 = true;
                for (int num187 = 0; num187 < 255; num187++)
                {
                    if (Main.player[num187].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186, (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            flag19 = false;
                            break;
                        }
                    }
                }
                if (flag19)
                {
                    collision = true;
                }
            }
            if (directional)
            {
                if (NPC.velocity.X < 0f)
                {
                    NPC.spriteDirection = 1;
                }
                else if (NPC.velocity.X > 0f)
                {
                    NPC.spriteDirection = -1;
                }
            }
            float speedAI = speed;
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float targetX = P.Center.X;
            float targetY = P.Center.Y;
            if (aiTimer >= 300 && aiTimer <= 1100)
            {
                speedAI *= 0.4f;
                if (wanderX != 0 && wanderY != 0)
                {
                    targetX = wanderX;
                    targetY = wanderY;
                }
            }
            targetX = (float)((int)(targetX / 16f) * 16);
            targetY = (float)((int)(targetY / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            targetX -= vector18.X;
            targetY -= vector18.Y;
            float num193 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    targetX = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
                    targetY = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)Math.Atan2((double)targetY, (double)targetX) + 1.57f;
                num193 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                int num194 = NPC.width;
                num193 = (num193 - (float)num194) / num193;
                targetX *= num193;
                targetY *= num193;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + targetX;
                NPC.position.Y = NPC.position.Y + targetY;
                if (directional)
                {
                    if (targetX < 0f)
                    {
                        NPC.spriteDirection = 1;
                    }
                    if (targetX > 0f)
                    {
                        NPC.spriteDirection = -1;
                    }
                }
            }
            else
            {
                if (!collision)
                {
                    NPC.TargetClosest(true);
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.velocity.Y > speedAI)
                    {
                        NPC.velocity.Y = speedAI;
                    }
                    if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)speedAI * 0.4)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - turnSpeed * 1.1f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X + turnSpeed * 1.1f;
                        }
                    }
                    else if (NPC.velocity.Y == speedAI)
                    {
                        if (NPC.velocity.X < targetX)
                        {
                            NPC.velocity.X = NPC.velocity.X + turnSpeed;
                        }
                        else if (NPC.velocity.X > targetX)
                        {
                            NPC.velocity.X = NPC.velocity.X - turnSpeed;
                        }
                    }
                    else if (NPC.velocity.Y > 4f)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + turnSpeed * 0.9f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - turnSpeed * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
                    {
                        float num195 = num193 / 40f;
                        if (num195 < 10f)
                        {
                            num195 = 10f;
                        }
                        if (num195 > 20f)
                        {
                            num195 = 20f;
                        }
                        NPC.soundDelay = (int)num195;
                        SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                    }
                    num193 = (float)System.Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                    float num196 = System.Math.Abs(targetX);
                    float num197 = System.Math.Abs(targetY);
                    float num198 = speedAI / num193;
                    targetX *= num198;
                    targetY *= num198;
                    if (ShouldRun())
                    {
                        bool flag20 = true;
                        for (int num199 = 0; num199 < 255; num199++)
                        {
                            if (Main.player[num199].active && !Main.player[num199].dead && Main.player[num199].ZoneCorrupt)
                            {
                                flag20 = false;
                            }
                        }
                        if (flag20)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient && (double)(NPC.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
                            {
                                NPC.active = false;
                                int num200 = (int)NPC.ai[0];
                                while (num200 > 0 && num200 < 200 && Main.npc[num200].active && Main.npc[num200].aiStyle == NPC.aiStyle)
                                {
                                    int num201 = (int)Main.npc[num200].ai[0];
                                    Main.npc[num200].active = false;
                                    NPC.life = 0;
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendData(23, -1, -1, null, num200, 0f, 0f, 0f, 0, 0, 0);
                                    }
                                    num200 = num201;
                                }
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(23, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            targetX = 0f;
                            targetY = speedAI;
                        }
                    }

                    if (aiTimer >= 300 && aiTimer <= 1100)
                    {
                        Wander(P);                      
                    }
                    if (aiTimer <= 1100)
                    {
                        int xTurnSpeedScale = 1;
                        if (MathHelper.Distance(NPC.Center.X, P.Center.X) > 3000)
                        {
                            xTurnSpeedScale = 5;
                            targetX *= 15f;
                        }
                        if (NPC.velocity.X > 0f && targetX > 0f || NPC.velocity.X < 0f && targetX < 0f || NPC.velocity.Y > 0f && targetY > 0f || NPC.velocity.Y < 0f && targetY < 0f)
                        {
                            if (NPC.velocity.X < targetX)
                            {
                                NPC.velocity.X = NPC.velocity.X + turnSpeed * xTurnSpeedScale;
                            }
                            else
                            {
                                if (NPC.velocity.X > targetX)
                                {
                                    NPC.velocity.X = NPC.velocity.X - turnSpeed * xTurnSpeedScale;
                                }
                            }
                            if (NPC.velocity.Y < targetY)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + turnSpeed;
                            }
                            else
                            {
                                if (NPC.velocity.Y > targetY)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - turnSpeed;
                                }
                            }
                            if ((double)System.Math.Abs(targetY) < (double)speedAI * 0.2 && (NPC.velocity.X > 0f && targetX < 0f || NPC.velocity.X < 0f && targetX > 0f))
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + turnSpeed * 2f;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - turnSpeed * 2f;
                                }
                            }
                            if ((double)System.Math.Abs(targetX) < (double)speedAI * 0.2 && (NPC.velocity.Y > 0f && targetY < 0f || NPC.velocity.Y < 0f && targetY > 0f))
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + turnSpeed * 2f;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - turnSpeed * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num196 > num197)
                            {
                                if (NPC.velocity.X < targetX)
                                {
                                    NPC.velocity.X = NPC.velocity.X + turnSpeed * 1.1f;
                                }
                                else if (NPC.velocity.X > targetX)
                                {
                                    NPC.velocity.X = NPC.velocity.X - turnSpeed * 1.1f;
                                }
                                if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)speedAI * 0.5)
                                {
                                    if (NPC.velocity.Y > 0f)
                                    {
                                        NPC.velocity.Y = NPC.velocity.Y + turnSpeed;
                                    }
                                    else
                                    {
                                        NPC.velocity.Y = NPC.velocity.Y - turnSpeed;
                                    }
                                }
                            }
                            else
                            {
                                if (NPC.velocity.Y < targetY)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + turnSpeed * 1.1f;
                                }
                                else if (NPC.velocity.Y > targetY)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - turnSpeed * 1.1f;
                                }
                                if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)speedAI * 0.5)
                                {
                                    if (NPC.velocity.X > 0f)
                                    {
                                        NPC.velocity.X = NPC.velocity.X + turnSpeed;
                                    }
                                    else
                                    {
                                        NPC.velocity.X = NPC.velocity.X - turnSpeed;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.96f;
                    }
                }
                NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
                if (head)
                {
                    if (collision)
                    {
                        if (NPC.localAI[0] != 1f)
                        {
                            NPC.netUpdate = true;
                        }
                        NPC.localAI[0] = 1f;
                    }
                    else
                    {
                        if (NPC.localAI[0] != 0f)
                        {
                            NPC.netUpdate = true;
                        }
                        NPC.localAI[0] = 0f;
                    }
                    if ((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f || NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f || NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f || NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f) && !NPC.justHit)
                    {
                        NPC.netUpdate = true;
                        return;
                    }
                }
            }
            CustomBehavior();
        }
        private void Wander(Player P)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                int dust = Dust.NewDust(new Vector2(wanderX, wanderY), 16, 16, DustID.Firework_Pink);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (wanderX == 0 || wanderY == 0 || Vector2.Distance(new Vector2(wanderX, wanderY), P.Center) > 1600)
            {
                wanderX = P.Center.X + Main.rand.Next(-800, 800);
                wanderY = P.Center.Y + Main.rand.Next(-800, 800);
                NPC.netUpdate = true;
            }
            wanderTimer++;
            if (wanderTimer >= 180 || Vector2.Distance(new Vector2(wanderX, wanderY), NPC.Center) < 40)
            {
                wanderX = P.Center.X + Main.rand.Next(-800, 800);
                wanderY = P.Center.Y + Main.rand.Next(-800, 800);
                NPC.netUpdate = true;
                wanderTimer = 0;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<ExtinctionCurse>(), 90, true);
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            float damage = modifiers.SourceDamage.Base;
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = (int)(damage * 0.1f);
            }
            else if (projectile.penetrate == -1)
            {
                damage = (int)(damage * 0.3f);
            }
            else if (projectile.maxPenetrate > 10)
            {
                damage = (int)(damage * 0.3f);
            }
            else if (projectile.maxPenetrate > 6)
            {
                damage = (int)(damage * 0.5f);
            }
            else if (projectile.maxPenetrate > 3)
            {
                damage = (int)(damage * 0.8f);
            }
        }
        public virtual void Init()
        {
        }

        public virtual bool ShouldRun()
        {
            return false;
        }

        public virtual void CustomBehavior()
        {
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.alpha > 150) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return head ? (bool?)null : false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
            Vector2 origin = (new Vector2(texture.Width * 0.5f, frameHeight * 0.5f)); 
            Color color = NPC.GetAlpha(drawColor);
            Main.spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, color, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}