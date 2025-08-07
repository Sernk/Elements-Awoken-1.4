using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan.ElderShadeWyrm;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan.Minions;
using ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan
{
    [AutoloadBossHead]
    class VoidLeviathanHead : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/VoidLeviathanHead"; } }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            var EALocalization = GetInstance<EALocalization>();
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement(EALocalization.VoidLeviathan),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.6f,
                PortraitScale = 0.5f,
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/VoidLeviathanBestiary"
            };
            value.Position.X += 40f;
            value.Position.Y += 20f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 108;
            NPC.height = 134;
            NPC.damage = 250;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.value = Item.buyPrice(1, 50, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.LunarBoss;
            SpawnModBiomes = new int[1] { GetInstance<Emptiness>().Type };
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VoidLeviathanTheme");
            NPC.netAlways = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 700000;
            NPC.damage = 350;
            NPC.defense = 56;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 850000;
                NPC.damage = 450;
                NPC.defense = 67;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {            
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            var _AwakenedModeEssence = new LeadingConditionRule(new EAIDRC.DropVoidEssenceAwakened());
            var _AwakenedModeExpert = new LeadingConditionRule(new EAIDRC.DropVoidEssenceExpert());

            npcLoot.Add(ItemDropRule.OneFromOptions(1, [..ListItems.LeviLoot]));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemType<VoidLeviathanBag>(), 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<VoidLeviathanMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<VoidLeviathanTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<VoidLeviathanHeart>()));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ItemType<AbyssalMatter>()));
            npcLoot.Add(_AwakenedMode);
            _AwakenedModeEssence.OnSuccess(ItemDropRule.Common(ItemType<VoidEssence>(), minimumDropped: 8, maximumDropped: 20));
            npcLoot.Add(_AwakenedModeEssence);            
            _AwakenedModeExpert.OnSuccess(ItemDropRule.Common(ItemType<VoidEssence>(), minimumDropped: 5, maximumDropped: 13));
            npcLoot.Add(_AwakenedModeExpert);
        }
        public override void OnKill()
        {
            if (!MyWorld.downedVoidLeviathan)
            {
                ElementsAwoken.encounter = 3;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 3 start");
                Main.NewText(GetInstance<EALocalization>().VoidLeviathanHead, new Color(235, 70, 106));
            }
            if (MyWorld.voidLeviathanKills < 3)
            {
                MyWorld.genVoidite = true;
            }
            MyWorld.voidLeviathanKills++;
            MyWorld.downedVoidLeviathan = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }  
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemType<EpicHealingPotion>();
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
        private int projectileBaseDamage = 80;

        private float attackCounter;
        private float strikeCircleTimer;
        private float roarTimer;
        private float despawnTimer = 0;
        public float orbTimer = 0;
        private int spawnNPCs = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(strikeCircleTimer);
            writer.Write(roarTimer);
            writer.Write(despawnTimer);
            writer.Write(orbTimer);
            writer.Write(spawnNPCs);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadSingle();
            strikeCircleTimer = reader.ReadSingle();
            roarTimer = reader.ReadSingle();
            despawnTimer = reader.ReadSingle();
            orbTimer = reader.ReadSingle();
            spawnNPCs = reader.ReadInt32();
        }
        public override void CustomBehavior()
        {
            Player P = Main.player[NPC.target];
            MyPlayer modPlayer = P.GetModPlayer<MyPlayer>();
            var s = NPC.GetSource_FromThis();
            if (P.dead)
            {
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                despawnTimer++;
                if (despawnTimer >= 300)
                {
                    NPC.active = false;
                }
            }
            roarTimer--;
            if (roarTimer <= 0)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath10, NPC.position);
                roarTimer = 300 + Main.rand.Next(1, 400);
            }
            int maxDist = 3000;
            if (Main.netMode == 0)
            {
                P.AddBuff(BuffType<BehemothGaze>(), 20, true);
                float dist = Vector2.Distance(NPC.Center, P.Center);
                if (dist > maxDist) modPlayer.behemothGazeTimer++;
                else modPlayer.behemothGazeTimer = 0;
                modPlayer.leviathanDist = (int)dist;
            }
            else
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player loopP = Main.player[i];
                    MyPlayer modLoopP = P.GetModPlayer<MyPlayer>();
                    if (loopP.active && !loopP.dead)
                    {
                        loopP.AddBuff(BuffType<BehemothGaze>(), 20, true);
                        float dist = Vector2.Distance(NPC.Center, loopP.Center);
                        if (dist > maxDist) modLoopP.behemothGazeTimer++;
                        else modLoopP.behemothGazeTimer = 0;
                        modLoopP.leviathanDist = (int)dist;
                    }
                }
            }
            if (spawnNPCs == 0)
            {
                int soulCount = Main.expertMode ? 4 : 3;
                if (MyWorld.awakenedMode) soulCount = 7;
                for (int l = 0; l < soulCount; l++)
                {
                    int distance = 360 / soulCount;
                    NPC soul = Main.npc[NPC.NewNPC(s, (int)(NPC.Center.X + (Math.Sin(l * distance) * 150)), (int)(NPC.Center.Y + (Math.Cos(l * distance) * 150)), NPCType<BarrenOrbital>(), NPC.whoAmI, l * distance, NPC.whoAmI)];
                    soul.ai[2] += 60 * l;
                }
                spawnNPCs++;
            }
            strikeCircleTimer++;
            if (strikeCircleTimer >= 500 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int numProj = Main.expertMode ? 12 : 8;
                int dist = Main.expertMode ? 1400 : 1600;
                if (MyWorld.awakenedMode)
                {
                    numProj = 16;
                    dist = 1200;
                }
                int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.8f);
                for (float l = 0; l < numProj; l++)
                {
                    Vector2 projPos = P.Center + new Vector2(0, dist).RotatedBy(l * (Math.PI * 2f / numProj));
                    Vector2 projVel = P.Center - projPos;
                    projVel.Normalize();
                    projVel *= 8f;
                    Projectile strike = Main.projectile[Projectile.NewProjectile(s, projPos.X, projPos.Y, projVel.X, projVel.Y, ProjectileType<VoidStrike>(), projDamage, 6f, 0)];
                    strike.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                strikeCircleTimer = 0;
            }
            attackCounter++;
            int num = 600;
            if (NPC.life <= NPC.lifeMax * 0.9f) num -= 25;
            if (NPC.life <= NPC.lifeMax * 0.7f) num -= 25;
            if (NPC.life <= NPC.lifeMax * 0.5f) num -= 25;
            if (NPC.life <= NPC.lifeMax * 0.3f) num -= 75;
            if (NPC.life <= NPC.lifeMax * 0.1f) num -= 75;
            if (attackCounter % num == 0)
            {
                int numSouls = Main.expertMode ? 3 : 1;
                if (MyWorld.awakenedMode) numSouls = 5;
                int distance = 600;
                for (float l = 0; l < numSouls; l++)
                {
                    Vector2 soulPos = new Vector2(P.Center.X + Main.rand.Next(-distance, distance), P.Center.Y + Main.rand.Next(-distance, distance));
                    NPC soul = Main.npc[NPC.NewNPC(s, (int)soulPos.X, (int)soulPos.Y, NPCType<BarrenSoul>(), NPC.whoAmI)];
                    soul.ai[2] -= 30 * l;
                }
            }
            if (aiTimer > 600 && aiTimer <= 1020)
            {
                if (aiTimer % 45 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                    if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 2f);

                    Projectile rune = Main.projectile[Projectile.NewProjectile(s, P.Center.X + Main.rand.Next(-600, 600), P.Center.Y + Main.rand.Next(-600, 600), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), ProjectileType<VoidRunes>(), projDamage, 6f, Main.myPlayer)];
                    rune.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<VoidLeviathanOrb>())) orbTimer++;
            if (NPC.life <= NPC.lifeMax * 0.3f)
            {
                int vulnerableTime = Main.expertMode ? 1200 : 1800;
                if (MyWorld.awakenedMode) vulnerableTime = 1050;
                if (orbTimer > vulnerableTime)
                {
                    int numOrbs = Main.expertMode ? 2 : 1;
                    if (MyWorld.awakenedMode) numOrbs = 3;
                    int i = 0;
                    int tries = 0;
                    float[] otherPosX = new float[numOrbs];
                    float[] otherPosY = new float[numOrbs];
                    while (i < numOrbs)
                    {
                        int maxOrbDistance = 1500;

                        Vector2 orbPos = new Vector2(P.Center.X + Main.rand.Next(-maxOrbDistance, maxOrbDistance), 2700 + Main.rand.Next(-maxOrbDistance, maxOrbDistance));
                        Vector2 worldMapSize = new Vector2(Main.maxTilesX * 16, Main.maxTilesY * 16);
                        if (orbPos.X < 0) orbPos.X = Main.rand.Next(100, maxOrbDistance / 2);
                        else if (orbPos.X > worldMapSize.X) orbPos.X = worldMapSize.X- Main.rand.Next(200, maxOrbDistance / 2);
                        if (orbPos.Y < 0) orbPos.Y = Main.rand.Next(100, maxOrbDistance / 2);
                        else if (orbPos.Y > worldMapSize.Y) orbPos.Y = worldMapSize.Y - Main.rand.Next(200, maxOrbDistance / 2);

                        bool tooCloseToOthers = false;
                        for (int k = 0; k < otherPosX.Length; k++)
                        {
                            if (otherPosX[k] != 0)
                            {
                                if (Vector2.Distance(new Vector2(otherPosX[k], otherPosY[k]), orbPos) < 600) tooCloseToOthers = true;
                            }
                        }
                        Tile orbTile = Framing.GetTileSafely((int)(orbPos.X / 16), (int)(orbPos.Y / 16));
                        if ((!orbTile.HasTile && !tooCloseToOthers) || tries > 1000)
                        {
                            NPC.NewNPC(s, (int)orbPos.X, (int)orbPos.Y, NPCType<VoidLeviathanOrb>(), NPC.whoAmI);
                            for (int k = 0; k < otherPosX.Length; k++)
                            {
                                if (otherPosX[k] == 0)
                                {
                                    otherPosX[k] = orbPos.X;
                                    otherPosY[k] = orbPos.Y;
                                    break;
                                }
                            }
                            i++;
                        }
                        tries++;
                    }
                    orbTimer = 0;
                }
                if (spawnNPCs == 1)
                {
                    NPC.NewNPC(s, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<ElderShadeWyrmHead>(), NPC.whoAmI);
                    spawnNPCs++;
                }
                else if (spawnNPCs == 2 && NPC.life <= NPC.lifeMax * 0.2f && Main.expertMode)
                {
                    NPC.NewNPC(s, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<ElderShadeWyrmHead>(), NPC.whoAmI);
                    spawnNPCs++;
                }
                else if (spawnNPCs == 3 && NPC.life <= NPC.lifeMax * 0.1f && MyWorld.awakenedMode)
                {
                    NPC.NewNPC(s, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<ElderShadeWyrmHead>(), NPC.whoAmI);
                    spawnNPCs++;
                }
            }
        }
    }
    [AutoloadBossHead]
    class VoidLeviathanBody : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/VoidLeviathanBody"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 80;
            NPC.height = 68;
            NPC.damage = 150;
            NPC.knockBackResist = 0.0f;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 175;
            NPC.defense = 56;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 200;
                NPC.defense = 67;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }

        private int projectileBaseDamage = 100;
        public int bodyNum = 0;

        public override void CustomBehavior()
        {
            Player P = Main.player[NPC.target];
            NPC headNPC = Main.npc[(int)NPC.ai[3]];
            int num = Main.expertMode ? 3 : 5;
            if (MyWorld.awakenedMode) num = 2;
            if (bodyNum % num == 0 && headNPC.life > headNPC.lifeMax * 0.3f)
            {
                if (aiTimer == 1440 + betweenShots * bodyNum && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.3f) : projectileBaseDamage;
                    if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.5f);

                    float speedMult = Main.expertMode ? 8f : 6f;
                    if (MyWorld.awakenedMode) speedMult = 10f;
                    for (int k = 0; k < 2; k++)
                    {
                        Vector2 projSpeed = new Vector2(0, 1).RotatedBy(NPC.rotation + MathHelper.ToRadians(k == 0 ? 90 : 270));
                        projSpeed.Normalize();
                        projSpeed *= speedMult;
                        Projectile bolt = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, projSpeed, ProjectileType<VoidBolt>(), projDamage, 0f, Main.myPlayer)];
                    }
                    SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                }
            }
        }
    }
    [AutoloadBossHead]
    class VoidLeviathanTail : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/VoidLeviathanTail"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 80;
            NPC.height = 100;
            NPC.damage = 150;
            NPC.defense = 10;
            NPC.knockBackResist = 0.0f;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
            NPC.takenDamageMultiplier = 3;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 175;
            NPC.defense = 20;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 200;
                NPC.defense = 30;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        private int attackCounter;
        private int projectileBaseDamage = 150;

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

            if (Vector2.Distance(P.Center, NPC.Center) < 600)
            {
                attackCounter++;

                if (attackCounter >= 20 && Main.netMode != NetmodeID.MultiplayerClient && !NPC.dontTakeDamage)
                {
                    int numProj = Main.expertMode ? 8 : 4;
                    if (MyWorld.awakenedMode) numProj = 16;
                    for (float l = 0; l < numProj; l++)
                    {
                        Vector2 projPos = NPC.Center + new Vector2(0, 2).RotatedBy(l * (Math.PI * 2f / numProj));
                        Vector2 projVel = projPos - NPC.Center;
                        projVel.Normalize();
                        projVel *= 8f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), projPos.X, projPos.Y, projVel.X, projVel.Y, ProjectileType<VoidSpine>(), projectileBaseDamage, 6f, Main.myPlayer);
                    }
                    attackCounter = 0;
                }
            }
            else attackCounter = -90;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }
    public abstract class VoidLeviathan : VoidLeviathanAI
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 500000;
            NPC.defense = 50;
            NPC.scale = 1.1f;

            NPC.aiStyle = -1;

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;

            NPCsGLOBAL.ImmuneAllEABuffs(NPC);
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void Init()
        {
            wormLength = 60;
            tailType = NPCType<VoidLeviathanTail>();
            bodyType = NPCType<VoidLeviathanBody>();
            headType = NPCType<VoidLeviathanHead>();
            speed = 40f;
            if (MyWorld.awakenedMode) speed = 50f;
            else if (Main.expertMode) speed = 60f;
            turnSpeed =  0.6f;
            if (MyWorld.awakenedMode) turnSpeed = 0.8f;
            else if (Main.expertMode) turnSpeed = 0.6f;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }

    public abstract class VoidLeviathanAI : ModNPC
    {
        public bool head;
        public bool tail;
        public int wormLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool directional = false;
        public float speed;
        public float turnSpeed;

        public float aiTimer = 0;
        private float wanderTimer = 0;
        private float wanderX = 0;
        private float wanderY = 0;
        private int circleNum = 0;

        public const int betweenShots = 4;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
            writer.Write(wanderTimer);
            writer.Write(wanderX);
            writer.Write(wanderY);
            writer.Write(circleNum);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
            wanderTimer = reader.ReadSingle();
            wanderX = reader.ReadSingle();
            wanderY = reader.ReadSingle();
            circleNum = reader.ReadInt32();
        }

        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC headNPC = Main.npc[(int)NPC.ai[3]];
            var s = NPC.GetSource_FromThis();
            if (NPC.localAI[1] == 0f)
            {
                NPC.localAI[1] = 1f;
                Init();
            }
            int phase = 0;
            if (aiTimer > 600 && aiTimer <= 1020) phase = 1;
            if (aiTimer > 1020 && aiTimer <= 1440) phase = 2;
            if (aiTimer > 1440) phase = 3;

            if (headNPC.life <= headNPC.lifeMax * 0.3f && headNPC.life != 0)
            {
                if (NPC.localAI[3] == 0)
                {
                    bool doGore = true;
                    if (!tail && !head)
                    {
                        VoidLeviathanBody vleviBody = (VoidLeviathanBody)NPC.ModNPC;
                        int modNum = 4;
                        if (GetInstance<Config>().lowDust) modNum = 2;
                        if (vleviBody.bodyNum % modNum == 0) doGore = false;
                    }
                    if (doGore)
                    {
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VoidLeviathanHead").Type, 1f);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VoidLeviathanBody").Type, 1f);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VoidLeviathanTail").Type, 1f);
                        NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
                        NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
                        NPC.width = 50;
                        NPC.height = 50;
                        NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
                        NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
                    }
                    if (head)
                    {
                        VoidLeviathanHead vlevi = (VoidLeviathanHead)NPC.ModNPC;
                        vlevi.orbTimer = 1800;
                    }
                    NPC.localAI[3]++;
                }
                if (NPC.AnyNPCs(NPCType<VoidLeviathanOrb>()))
                {
                    NPC.immortal = true;
                    NPC.dontTakeDamage = true;
                    NPC.alpha = 200;
                    if (head)
                    {
                        aiTimer = 620;
                        Wander(P);
                    }
                }
                else
                {
                    NPC.immortal = false;
                    NPC.dontTakeDamage = false;
                    NPC.alpha = 90;
                    if (head)
                    {
                        aiTimer = 0;
                    }
                }
                if (!tail)
                {
                    NPC.defense = 20;
                    if (MyWorld.awakenedMode) NPC.defense = 30;
                }
                else
                {
                    NPC.defense = 0;
                }
                NPC.HitSound = SoundID.NPCHit49;
            }
            else aiTimer++;
            if (aiTimer > 1440 + wormLength * betweenShots) aiTimer = 0;


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
                        NPC.ai[0] = (float)NPC.NewNPC(s,(int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), bodyType, NPC.whoAmI);
                    }
                    else if (NPC.ai[2] > 0f)
                    {
                        NPC.ai[0] = (float)NPC.NewNPC(s,(int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), NPC.type, NPC.whoAmI);
                        VoidLeviathanBody bodyNPC = (VoidLeviathanBody)NPC.ModNPC;
                        VoidLeviathanBody newBodyNPC = (VoidLeviathanBody)Main.npc[(int)NPC.ai[0]].ModNPC;
                        newBodyNPC.bodyNum = bodyNPC.bodyNum + 1;
                    }
                    else
                    {
                        NPC.ai[0] = (float)NPC.NewNPC(s,(int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), tailType, NPC.whoAmI);
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
            float turnSpeedAI = turnSpeed;
            if (Main.dayTime)
            {
                speedAI += 20;
                turnSpeedAI += 0.2f;
                if (NPC.localAI[2] == 0)
                {
                    NPC.damage += 50;
                    NPC.localAI[2]++;
                }
            }
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float targetX = P.Center.X;
            float targetY = P.Center.Y;
            if (phase == 1 || phase == 2)
            {
                speedAI *= 0.2f;
                turnSpeedAI *= 1.2f;
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

                num193 = (float)System.Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                float num196 = System.Math.Abs(targetX);
                float num197 = System.Math.Abs(targetY);
                float num198 = speedAI / num193;
                targetX *= num198;
                targetY *= num198;

                if (headNPC.life > NPC.lifeMax * 0.3f)
                    if (phase == 1)
                    {
                        Wander(P);
                    }
                if (phase == 2)
                {
                    Circle(P);
                }
                if (phase < 3)
                {
                    float yTurnSpeedScale = 0.3f;
                    float xTurnSpeedScale = 1f;
                    if (MathHelper.Distance(NPC.Center.Y, P.Center.Y) > 600 || phase != 0) yTurnSpeedScale = 1f; // to stop him turning so hard onto the player
                    if (MathHelper.Distance(NPC.Center.Y, P.Center.Y) > 900 && MathHelper.Distance(NPC.Center.Y, P.Center.Y) < 3000 && phase == 0) targetY *= 0.2f; // to stop him going turbo speed
                    if (MathHelper.Distance(NPC.Center.X, P.Center.X) > 3000)
                    {
                        xTurnSpeedScale = 5;
                        targetX *= 15f;
                    }
                    if (MathHelper.Distance(NPC.Center.X, P.Center.X) > 2500) if ((NPC.velocity.X > targetX && NPC.velocity.X > 0) || NPC.velocity.X < targetX && NPC.velocity.X < 0) NPC.velocity.X *= 0.5f;      
                    if (NPC.velocity.X > 0f && targetX > 0f || NPC.velocity.X < 0f && targetX < 0f || NPC.velocity.Y > 0f && targetY > 0f || NPC.velocity.Y < 0f && targetY < 0f)
                    {
                        if (NPC.velocity.X < targetX)
                        {
                            NPC.velocity.X = NPC.velocity.X + turnSpeedAI * xTurnSpeedScale;
                        }
                        else
                        {
                            if (NPC.velocity.X > targetX)
                            {
                                NPC.velocity.X = NPC.velocity.X - turnSpeedAI * xTurnSpeedScale;
                            }
                        }
                        if (NPC.velocity.Y < targetY)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + turnSpeedAI * yTurnSpeedScale;
                        }
                        else
                        {
                            if (NPC.velocity.Y > targetY)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - turnSpeedAI * yTurnSpeedScale;
                            }
                        }
                        if ((double)System.Math.Abs(targetY) < (double)speedAI * 0.2 && (NPC.velocity.X > 0f && targetX < 0f || NPC.velocity.X < 0f && targetX > 0f))
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + turnSpeedAI * 2f;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - turnSpeedAI * 2f;
                            }
                        }
                        if ((double)System.Math.Abs(targetX) < (double)speedAI * 0.2 && (NPC.velocity.Y > 0f && targetY < 0f || NPC.velocity.Y < 0f && targetY > 0f))
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + turnSpeedAI * 2f;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - turnSpeedAI * 2f;
                            }
                        }
                    }
                    else
                    {
                        if (num196 > num197)
                        {
                            if (NPC.velocity.X < targetX)
                            {
                                NPC.velocity.X = NPC.velocity.X + turnSpeedAI * 1.1f;
                            }
                            else if (NPC.velocity.X > targetX)
                            {
                                NPC.velocity.X = NPC.velocity.X - turnSpeedAI * 1.1f;
                            }
                            if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)speedAI * 0.5)
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + turnSpeedAI;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - turnSpeedAI;
                                }
                            }
                        }
                        else
                        {
                            if (NPC.velocity.Y < targetY)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + turnSpeedAI * 1.1f;
                            }
                            else if (NPC.velocity.Y > targetY)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - turnSpeedAI * 1.1f;
                            }
                            if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)speedAI * 0.5)
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + turnSpeedAI;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - turnSpeedAI;
                                }
                            }

                        }
                    }
                }
                else
                {
                    NPC.velocity *= 0.96f;
                }

                NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
                if (head)
                {

                    if (NPC.localAI[0] != 1f)
                    {
                        NPC.netUpdate = true;
                    }
                    NPC.localAI[0] = 1f;

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
        private void Circle(Player P)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                int dust = Dust.NewDust(new Vector2(wanderX, wanderY), 16, 16, DustID.Firework_Pink);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (Vector2.Distance(new Vector2(wanderX, wanderY), NPC.Center) < 40 || (wanderX == 0 || wanderY == 0) || Vector2.Distance(new Vector2(wanderX, wanderY), P.Center) > 1600)
            {
                int numPoints = 180;
                Vector2 circlePos = P.Center + new Vector2(0, 600).RotatedBy(circleNum * (Math.PI * 2f / numPoints));
                wanderX = circlePos.X;
                wanderY = circlePos.Y;
                circleNum++;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC headNPC = Main.npc[(int)NPC.ai[3]];
            if (headNPC.life <= headNPC.lifeMax * 0.3f)
            {
                NPC.frame.Y = frameHeight;
                if (NPC.alpha > 150) NPC.frame.Y = frameHeight * 2;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<ExtinctionCurse>(), 300, true);
        }
        public virtual void Init() { }
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
            else
            {
                if (projectile.penetrate > projectile.maxPenetrate * 0.8f) damage = (int)MathHelper.Lerp(0, damage * 0.7f, (float)projectile.penetrate / (float)projectile.maxPenetrate);
                else damage = (int)MathHelper.Lerp(0, damage * 0.3f, (float)projectile.penetrate / (float)projectile.maxPenetrate);
            }
        }
        public virtual bool ShouldRun()
        {
            return false;
        }
        public virtual void CustomBehavior() { }
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
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            NPC headNPC = Main.npc[(int)NPC.ai[3]];
            if (headNPC.life > headNPC.lifeMax * 0.3f)
            {
                Texture2D texture = Request<Texture2D>("ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/Glow/" + GetType().Name + "_Glow").Value;
                Rectangle frame = new Rectangle(0, texture.Height * NPC.frame.Y, texture.Width, texture.Height);
                Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f); 
                SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), frame, new Color(255, 255, 255, 0), NPC.rotation, origin, NPC.scale, effects, 0.0f);
            }
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}