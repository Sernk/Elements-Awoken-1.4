using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Wasteland;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.NPCProj.Wasteland;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.Wasteland
{
    [AutoloadBossHead]
    public class Wasteland : ModNPC
    {
        private int shootTimer = 100;
        private int eggTimer = 75;
        private int jumpUpTimer = 200;
        private int[] superJumpAI = new int[2];

        private int stormTimer = 400;

        private int spoutSpawnTimer = 1000;

        private bool underground = false;
        private int diggingType = 0; // 0 is none, 1 is up, 2 is down
        private int diggingTimer = 60;

        private float digSpeed = 3f;

        private int aiTimer = 0;
        private int acidBallTimer = 200;
        private int jumpSpikeTimer = 0;

        private int projectileBaseDamage = 25;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(shootTimer);
            writer.Write(eggTimer);
            writer.Write(jumpUpTimer);
            writer.Write(superJumpAI[0]);
            writer.Write(superJumpAI[1]);
            writer.Write(stormTimer);
            writer.Write(diggingTimer);
            writer.Write(diggingType);
            writer.Write(acidBallTimer);
            writer.Write(jumpSpikeTimer);
            writer.Write(underground);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            shootTimer = reader.ReadInt32();
            eggTimer = reader.ReadInt32();
            jumpUpTimer = reader.ReadInt32();
            superJumpAI[0] = reader.ReadInt32();
            superJumpAI[1] = reader.ReadInt32();
            stormTimer = reader.ReadInt32();
            diggingTimer = reader.ReadInt32();
            diggingType = reader.ReadInt32();
            acidBallTimer = reader.ReadInt32();
            jumpSpikeTimer = reader.ReadInt32();
            underground = reader.ReadBoolean();
        }
        private float despawnTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public override void BossHeadSlot(ref int index)
        {
            if (underground || diggingType == 2)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/Content/NPCs/Bosses/Wasteland/Wasteland_Head_Boss_Blank");
            }
            else
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/Content/NPCs/Bosses/Wasteland/Wasteland_Head_Boss");
            }
        }
        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 130;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 15;
            NPC.lifeMax = 4300;          
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath36;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPCID.Sets.NeedsExpertScaling[NPC.type] = true;
            Music = MusicID.Boss1;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffType<IceBound>()] = true;
            NPC.buffImmune[BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 2f,
                Direction = -1
            };
            drawModifiers.Position.X += 0f;
            drawModifiers.Position.Y += 10f;
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement(GetInstance<EALocalization>().WastelandBoss),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 5000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 7500;
                NPC.defense = 20;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.direction == 1)
                {
                    NPC.spriteDirection = 1;
                }
                if (NPC.direction == -1)
                {
                    NPC.spriteDirection = -1;
                }
            }
            if (NPC.velocity.Y != 0f || (NPC.direction == -1 && NPC.velocity.X > 0f) || (NPC.direction == 1 && NPC.velocity.X < 0f))
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = NPC.frame.Height * 4;
                return;
            }
            if (NPC.velocity.X == 0f)
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = 0;
                return;
            }
            NPC.frameCounter += (double)Math.Abs(NPC.velocity.X);
            int frameLength = 12;
            if (NPC.frameCounter < frameLength)
            {
                Vector2 snapPos = new Vector2(NPC.Center.X - 52, NPC.Center.Y + 24);
                Vector2 snapPos2 = new Vector2(NPC.Center.X - 14, NPC.Center.Y + 32);
                if (NPC.direction == 1)
                {
                    snapPos.X = NPC.Center.X + 52;
                    snapPos2.X = NPC.Center.X + 14;
                }
                Projectile.NewProjectile(EAU.NPCs(NPC), snapPos.X, snapPos.Y, 0f, 0f, ProjectileType<WastelandSnap>(), 40, 0, Main.myPlayer, NPC.whoAmI);
                Projectile.NewProjectile(EAU.NPCs(NPC), snapPos2.X, snapPos2.Y, 0f, 0f, ProjectileType<WastelandSnap>(), 40, 0, Main.myPlayer, NPC.whoAmI);

                NPC.frame.Y = 0;
                return;
            }
            if (NPC.frameCounter < frameLength * 2)
            {
                NPC.frame.Y = NPC.frame.Height;
                return;
            }
            if (NPC.frameCounter < frameLength * 3)
            {
                NPC.frame.Y = NPC.frame.Height * 2;
                return;
            }
            if (NPC.frameCounter < frameLength * 4)
            {
                NPC.frame.Y = NPC.frame.Height * 3;
                return;
            }

            NPC.frameCounter = 0.0;
            if (aiTimer < 0) NPC.frame.Y = 0;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 32, hit.HitDirection, -1f, 0, default(Color), 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 32, hit.HitDirection, -1f, 0, default(Color), 1f);
                }
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("Wasteland" + i).Type, 1f);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            npcLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.WasLoot]));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemType<WastelandBag>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<WastelandTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<WastelandMask>(), 10));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertEssence>(), minimumDropped: 5, maximumDropped: 20));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ItemType<TheAntidote>()));
            npcLoot.Add(_AwakenedMode);
        }
        public override void OnKill()
        {
            Sandstorm.Happening = false;
            Sandstorm.TimeLeft = 0;
            Sandstorm.IntendedSeverity = 0;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }
        public override int SpawnNPC(int tileX, int tileY)
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            NPC.Center = P.Center + new Vector2(0, 700);

            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = 90000;
            SandstormStuff();
            return base.SpawnNPC(tileX, tileY);
        }
        public override bool PreKill()
        {
            if (aiTimer < 0)
            {
                Main.NewText(GetInstance<EALocalization>().Wasteland,Color.MediumPurple);
                NPC death = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<WastelandDeath>())];
                death.spriteDirection = NPC.spriteDirection;
                death.Center = NPC.Center;
                return false;
            }
            return base.PreKill();
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (aiTimer < 0)
            {
                NPC.immortal = true;
                NPC.dontTakeDamage = true;
                int dryad = NPC.FindFirstNPC(NPCID.Dryad);
                if (dryad >= 0)
                {
                    NPC dryadNPC = Main.npc[dryad];
                    dryadNPC.ai[0] = 0;
                }
                aiTimer++;
                if (NPC.velocity.Y < 0) NPC.velocity.Y *= 0.9f;
                NPC.velocity.X = 0;
                if (aiTimer > -5)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
            else
            {
                if (NPC.life <= NPC.lifeMax * 0.1f && aiTimer >= 0)
                {
                    int dryad = NPC.FindFirstNPC(NPCID.Dryad);
                    if (dryad >= 0)
                    {
                        NPC dryadNPC = Main.npc[dryad];
                        if (Vector2.Distance(dryadNPC.Center, NPC.Center) < 2000)
                        {
                            dryadNPC.alpha = 255;
                            aiTimer = -720;
                            Projectile.NewProjectile(EAU.NPCs(NPC), dryadNPC.Center, Vector2.Zero, ProjectileType<WastelandDryad>(), 0, 0, Main.myPlayer);
                        }
                    }
                }
                if (!P.active || P.dead)
                {
                    NPC.TargetClosest(true);
                    if (!P.active || P.dead)
                    {
                        despawnTimer++;
                        if (despawnTimer >= 300) NPC.active = false;
                    }
                    else
                        despawnTimer = 0;
                }
                bool enraged = !P.ZoneDesert;
                if (enraged) NPC.defense = 30;
                else NPC.defense = 18;

                if (!underground && diggingType == 0) spoutSpawnTimer--;
                if (underground)
                {
                    NPC.immortal = true;
                    NPC.dontTakeDamage = true;
                    NPC.alpha = 255;
                }
                else
                {
                    NPC.immortal = false;
                    NPC.dontTakeDamage = false;
                    NPC.alpha = 0;
                }
                if (spoutSpawnTimer <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numFakes = Main.expertMode ? MyWorld.awakenedMode ? 4 : 3 : 2;
                        for (int k = 0; k < numFakes; k++)
                        {
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center, new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7)), ProjectileType<WastelandDiggingProj>(), 0, 0, Main.myPlayer);
                        }
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center, new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7)), ProjectileType<WastelandDiggingProjReal>(), 0, 0, Main.myPlayer, NPC.whoAmI);
                    }
                    spoutSpawnTimer = 10000;

                    diggingType = 1; 
                    digSpeed = 4.5f;
                    diggingTimer = 120;

                    NPC.noTileCollide = true;
                }
                DetectSpouts();
                if (diggingType != 0)
                {
                    Dig(digSpeed);
                }
                if (Vector2.Distance(P.Center, NPC.Center) >= 1200)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Center = new Vector2(P.Center.X, P.Center.Y + 700);
                        NPC.netUpdate = true;
                    }
                    diggingType = 2;
                    digSpeed = 6f;
                }

                if (!underground && diggingType == 0)
                {
                    aiTimer++;
                    CustomAI_3();
                    NPC.noTileCollide = false;
                    if (aiTimer < 600)
                    {
                        shootTimer--;
                        if (shootTimer <= 0) shootTimer = enraged ? 60 : 100;
                        acidBallTimer--;
                        if (acidBallTimer <= 0) acidBallTimer = 900;
                        eggTimer--;

                        if (eggTimer <= 0 && NPC.CountNPCS(NPCType<WastelandMinion>()) < 10)
                        {
                            NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X + (NPC.direction == -1 ? 40 : -40), (int)NPC.Center.Y + 10, NPCType<WastelandEgg>());
                            eggTimer = 120;
                        }
                        // shoot stingers
                        int under = Main.expertMode ? MyWorld.awakenedMode ? 24 : 18 : 12;
                        if (shootTimer % 6 == 0 && shootTimer <= under)
                        {
                            SoundEngine.PlaySound(SoundID.Item17, NPC.position);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                float speed = 8f;
                                Vector2 tailPos = new Vector2(NPC.Center.X + NPC.direction * 40, NPC.Center.Y - 30);
                                float rotation = (float)Math.Atan2(tailPos.Y - P.Center.Y, tailPos.X - P.Center.X);
                                Projectile.NewProjectile(EAU.NPCs(NPC), tailPos.X, tailPos.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<WastelandStinger>(), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                        }
                        //storm
                        if (Main.expertMode)
                        {
                            stormTimer--;
                            if (stormTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -2), ProjectileType<WastelandStormBolt>(), 6, 0f, Main.myPlayer);
                                }
                                stormTimer = 1800;
                                if (enraged) stormTimer -= 300;
                            }
                        }
                        //acid balls
                        if (MyWorld.awakenedMode)
                        {
                            if (acidBallTimer % 5 == 0 && acidBallTimer <= 20 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                SoundEngine.PlaySound(SoundID.Item48, NPC.position);

                                float speed = 10f;
                                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1) - (acidBallTimer / 5) * 2, ProjectileType<AcidBall>(), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                        }
                        //jump up
                        jumpUpTimer--;
                        if (jumpUpTimer <= 0 || NPC.Center.Y > P.Center.Y + 700)
                        {
                            JumpDust();
                            NPC.velocity.Y = Main.rand.NextFloat(-12, -8);
                            SoundEngine.PlaySound(SoundID.Item69, NPC.position);
                            jumpUpTimer = 350;
                            NPC.netUpdate = true;
                        }
                    }
                    else if (aiTimer >= 600)
                    {
                        if (superJumpAI[1] == 0)
                        {
                            JumpDust();
                            NPC.velocity.Y = Main.rand.NextFloat(-20, -14);
                            SoundEngine.PlaySound(SoundID.Item69, NPC.position);

                            jumpSpikeTimer = 0;
                            superJumpAI[1] = 1;
                            NPC.netUpdate = true;
                        }
                        else if (superJumpAI[1] == 1)
                        {
                            superJumpAI[0]++;
                            if (superJumpAI[0] >= 60 || NPC.velocity.Y > 0)
                            {
                                superJumpAI[1] = 2;
                            }
                        }
                        else if (superJumpAI[1] == 2)
                        {
                            if (!MyWorld.awakenedMode) superJumpAI[1] = Main.rand.Next(3, 5);
                            else superJumpAI[1] = Main.rand.Next(3, 6);
                            if (superJumpAI[1] == 5)
                            {
                                NPC.velocity.Y = 12;
                            }
                            NPC.netUpdate = true;
                        }
                        else if (superJumpAI[1] == 3)
                        {
                            aiTimer = 0;
                            superJumpAI[0] = 0;
                            superJumpAI[1] = 0;
                            jumpUpTimer = 250;
                        }
                        else if (superJumpAI[1] == 4)
                        {
                            jumpSpikeTimer++; // jump up and circle shoot ai

                            NPC.velocity = Vector2.Zero;
                            int timeBetweenSpikes = 15;
                            if (jumpSpikeTimer % timeBetweenSpikes == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                float numberProjectiles = 4;
                                if (Main.expertMode) numberProjectiles = 6;
                                if (MyWorld.awakenedMode) numberProjectiles = 8;
                                float projSpeed = 5.5f;
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedByRandom(MathHelper.ToRadians(360));
                                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<WastelandStinger>(), projectileBaseDamage - 5, 2f, Main.myPlayer);
                                }
                                SoundEngine.PlaySound(SoundID.Item17, NPC.position);
                            }

                            if (jumpSpikeTimer >= timeBetweenSpikes * 3.5f)
                            {
                                aiTimer = 0;
                                superJumpAI[0] = 0;
                                superJumpAI[1] = 0;
                                jumpUpTimer = 250;
                            }
                        }
                        else if (superJumpAI[1] == 5)
                        {
                            if (NPC.velocity.Y == 0f)
                            {
                                JumpDust();

                                Vector2 shockwavePosition = new Vector2(NPC.Center.X, NPC.Bottom.Y);
                                Point shockwavePoint = shockwavePosition.ToTileCoordinates();
                                Tile shockwaveTile = Framing.GetTileSafely((int)shockwavePoint.X, (int)shockwavePoint.Y);
                                if (!Main.tileSolid[shockwaveTile.TileType] && shockwaveTile.HasTile)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (shockwaveTile.HasTile)
                                        {
                                            if (!Main.tileSolid[shockwaveTile.TileType])
                                            {
                                                shockwavePosition -= new Vector2(0f, 16);
                                                shockwavePoint = shockwavePosition.ToTileCoordinates();
                                                shockwaveTile = Framing.GetTileSafely((int)shockwavePosition.X, (int)shockwavePosition.Y);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile.NewProjectile(EAU.NPCs(NPC), shockwavePoint.X * 16 + 8, shockwavePoint.Y * 16 + 8, 0f, 0f, ProjectileType<Shockwave>(), 0, 0f, Main.myPlayer, 24f, 1f);
                                    Projectile.NewProjectile(EAU.NPCs(NPC), shockwavePoint.X * 16 + 8, shockwavePoint.Y * 16 + 8, 0f, 0f, ProjectileType<Shockwave>(), 0, 0f, Main.myPlayer, 24f, -1f);
                                }
                                SoundEngine.PlaySound(SoundID.Item69, NPC.position);

                                aiTimer = 0;
                                superJumpAI[0] = 0;
                                superJumpAI[1] = 0;
                                jumpUpTimer = 250;
                            }
                        }
                    }
                }
            }
        }
        private void JumpDust()
        {
            for (int k = 0; k < 250; k++)
            {
                int dust = Dust.NewDust(new Vector2(NPC.BottomLeft.X, NPC.BottomLeft.Y - 8), NPC.width, 16, 75, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.5f;
                dust = Dust.NewDust(new Vector2(NPC.BottomLeft.X, NPC.BottomLeft.Y - 8), NPC.width, 16, 32, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.5f;
            }
        }
        public static void SandstormStuff()
        {
            Sandstorm.IntendedSeverity = !Sandstorm.Happening ? (Main.rand.Next(3) != 0 ? Main.rand.NextFloat() * 0.3f : 0.0f) : 0.4f + Main.rand.NextFloat();
            if (Main.netMode == 1)
                return;
        }
        private void DetectSpouts()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == ProjectileType<WastelandDiggingSpoutReal>() && proj.ai[0] == NPC.whoAmI)
                {
                    float posX = proj.Center.X;
                    float posY = proj.Center.Y + 300;
                    NPC.Center = new Vector2(posX, posY);

                    if (proj.ai[1] >= 360)
                    {
                        int lifeTime = 0;
                        if (NPC.life < NPC.lifeMax * 0.75)lifeTime = 100;
                        else if (NPC.life < NPC.lifeMax * 0.5)  lifeTime = 200;
                        else if (NPC.life < NPC.lifeMax * 0.25)  lifeTime = 300;
                        spoutSpawnTimer = 1400 - lifeTime;

                        underground = false;
                        diggingType = 2; // 2 is up
                        digSpeed = 3f;
                        diggingTimer = 60;

                        proj.Kill();
                    }
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;
                }
            }
        }

        private void Dig(float digSpeed)
        {
            for (int k = 0; k < 10; k++)
            {
                int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 32);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
            NPC.noTileCollide = true;

            if (diggingType == 1) NPC.velocity.Y = digSpeed;
            else if (diggingType == 2) NPC.velocity.Y = -digSpeed;
            NPC.velocity.X = 0f;

            diggingTimer--;
            if (diggingTimer <= 0)
            {
                if (diggingType == 1)
                {
                    underground = true;
                    diggingType = 0;
                }
                if (diggingType == 2)
                {
                    if (Collision.SolidCollision(NPC.position,NPC.width,NPC.height) == false)
                    {
                        for (int k = 0; k < 500; k++)
                        {
                            int dust2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 32, 0f, 0f, 100, default(Color), 1.5f);
                            Main.dust[dust2].noGravity = true;
                            Main.dust[dust2].velocity *= 1.5f;
                        }
                        NPC.noTileCollide = false;
                        diggingType = 0;
                    }
                    underground = false;
                }
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
            float speed = 0.15f;
            if (MyWorld.awakenedMode) speed = 0.25f;
            if (NPC.velocity.X < -2f || NPC.velocity.X > 2f)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.8f;
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
                Tile tile = Main.tile[num165, num166];
                Tile tile2 = Main.tile[num165, num166 - 1];
                Tile tile3 = Main.tile[num165, num166 - 2];
                Tile tile4 = Main.tile[num165, num166 - 3];
                Tile tile5 = Main.tile[num165, num166 + 1];
                Tile tile6 = Main.tile[num165 - num164, num166 - 3];
                if (Main.tile[num165, num166] == null)
                {
                    tile = new Tile();
                }
                if (Main.tile[num165, num166 - 1] == null)
                {
                    tile2 = new Tile();
                }
                if (Main.tile[num165, num166 - 2] == null)
                {
                    tile3 = new Tile();
                }
                if (Main.tile[num165, num166 - 3] == null)
                {
                    tile4 = new Tile();
                }
                if (Main.tile[num165, num166 + 1] == null)
                {
                    tile5 = new Tile();
                }
                if (Main.tile[num165 - num164, num166 - 3] == null)
                {
                    tile6 = new Tile();
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
            }
            if (flag22)
            {
                int num170 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
                int num171 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                //if (npc.type == 257)
                {
                    num170 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 16) * NPC.direction)) / 16f);
                }
                Tile tile = Main.tile[num170, num171];
                Tile tile1 = Main.tile[num170, num171 - 1];
                Tile tile2 = Main.tile[num170, num171 - 2];
                Tile tile3 = Main.tile[num170, num171 - 3];
                Tile tile4 = Main.tile[num170, num171 + 1];
                Tile tile5 = Main.tile[num170 + NPC.direction, num171 - 1];
                Tile tile6 = Main.tile[num170 + NPC.direction, num171 + 1];
                Tile tile7 = Main.tile[num170 - NPC.direction, num171 + 1];
                Tile tile8 = Main.tile[num170, num171 + 1];
                if (Main.tile[num170, num171] == null)
                {
                    tile1 = new Tile();
                }
                if (Main.tile[num170, num171 - 1] == null)
                {
                    tile1 = new Tile();
                }
                if (Main.tile[num170, num171 - 2] == null)
                {
                   tile2 = new Tile();
                }
                if (Main.tile[num170, num171 - 3] == null)
                {
                    tile3 = new Tile();
                }
                if (Main.tile[num170, num171 + 1] == null)
                {
                    tile4 = new Tile();
                }
                if (Main.tile[num170 + NPC.direction, num171 - 1] == null)
                {
                    tile5 = new Tile();
                }
                if (Main.tile[num170 + NPC.direction, num171 + 1] == null)
                {
                    tile6 = new Tile();
                }
                if (Main.tile[num170 - NPC.direction, num171 + 1] == null)
                {
                    tile7 = new Tile();
                }
                tile8.IsHalfBlock = true; // ???
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
    }
}