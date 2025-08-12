using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Infernace;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.NPCs.Prompts;
using ElementsAwoken.Content.Projectiles.NPCProj.Infernace;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.Infernace
{
    [AutoloadBossHead]
    public class Infernace : ModNPC
    {
        private const int tpDuration = 30;
        private int projectileBaseDamage = 37;

        private int furosiaState = 0;
        private int dropNum = 0;
        private float monsterDropAI = 0;
        private float monolithTimer = 0f;

        private float telePosX = 0;
        private float telePosY = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(furosiaState);
            writer.Write(dropNum);
            writer.Write(monsterDropAI);
            writer.Write(monolithTimer);

            writer.Write(telePosX);
            writer.Write(telePosY);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            furosiaState = reader.ReadInt32();
            dropNum = reader.ReadInt32();
            monsterDropAI = reader.ReadSingle();
            monolithTimer = reader.ReadSingle();

            telePosX = reader.ReadSingle();
            telePosY = reader.ReadSingle();
        }
        private float despawnTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float tpAlphaChangeTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 90;
            NPC.lifeMax = 8500;
            NPC.damage = 25;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.alpha = 255;
            NPC.scale = 1.3f;
            NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 7, 50, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.Plantera;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffType<IceBound>()] = true;
            NPC.buffImmune[BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/InfernaceBestiary",
                Scale = 1f, // Мини иконка в бестиарии
                PortraitScale = 0.7f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y += 0f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 30;
            NPC.lifeMax = 12000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 15000;
                NPC.damage = 45;
                NPC.defense = 30;
            }
        }
        public override bool CheckDead()
        {
            if (aiTimer > -1 && !MyWorld.downedInfernace)
            {
                int duration = 270 * 4;
                aiTimer = -duration;
                shootTimer = NPC.AnyNPCs(NPCType<Furosia>()) ? 1 : 0;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            NPC healer = null;
            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (other.ai[1] == NPC.whoAmI && other.active && other.type == NPCType<HealingHearth>())
                {
                    healer = other;
                }
            }
            if (healer != null)
            {
                if (healer.active)
                {
                    if (Vector2.Distance(NPC.Center, healer.Center) < 600)
                    {
                        Texture2D texture = Request<Texture2D>("ElementsAwoken/Content/NPCs/Bosses/Infernace/InfernaceHealer").Value;

                        Vector2 position = NPC.Center;
                        Vector2 mountedCenter = healer.Center;
                        Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
                        Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
                        float num1 = (float)texture.Height;
                        Vector2 vector2_4 = mountedCenter - position;
                        float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                        bool flag = true;
                        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                            flag = false;
                        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                            flag = false;
                        while (flag)
                        {
                            if ((double)vector2_4.Length() < (double)num1 + 1.0)
                            {
                                flag = false;
                            }
                            else
                            {
                                Vector2 vector2_1 = vector2_4;
                                vector2_1.Normalize();
                                position += vector2_1 * num1;
                                vector2_4 = mountedCenter - position;
                                EAU.Sb.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                    }
                }
            }
            return true;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            ++NPC.frameCounter;
            if (NPC.frameCounter >= 30.0)
                NPC.frameCounter = 0.0;
            NPC.frame.Y = frameHeight * (int)(NPC.frameCounter / 6.0);

            NPC.rotation = NPC.velocity.X * 0.1f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.InfernaceBestiary"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.InfeLoot]));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemType<InfernaceBag>(), 1));
            npcLoot.Add(ItemDropRule.Common(ItemType<FireEssence>(), 1, 5, 22));
        }
        public override void OnKill()
        {
            if (!MyWorld.downedInfernace)
            {
                ElementsAwoken.encounter = 1;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 1 start");
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int centerX = (int)NPC.Center.X / 16;
                int centerY = (int)NPC.Center.Y / 16;
                int boxWidth = NPC.width / 2 / 16 + 1;
                for (int tileX = centerX - boxWidth; tileX <= centerX + boxWidth; tileX++)
                {
                    for (int tileY = centerY - boxWidth; tileY <= centerY + boxWidth; tileY++)
                    {
                        if ((tileX == centerX - boxWidth || tileX == centerX + boxWidth || tileY == centerY - boxWidth || tileY == centerY + boxWidth) && !Main.tile[tileX, tileY].HasTile)
                        {
                            Tile tile = Main.tile[tileX, tileY];
                            Main.tile[tileX, tileY].TileType = TileID.HellstoneBrick;
                            tile.HasTile = true;
                        }
                        Tile tile2 = Main.tile[tileX, tileY];
                        tile2.LiquidType = LiquidID.Lava; //= false;
                        Main.tile[tileX, tileY].LiquidAmount = 0;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendTileSquare(-1, tileX, tileY, 1, TileChangeType.None);
                        }
                        else
                        {
                            WorldGen.SquareTileFrame(tileX, tileY, true);
                        }
                    }
                }
            }
            MyWorld.downedInfernace = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.alpha > 100) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            var e = GetInstance<EALocalization>();
            if (tpAlphaChangeTimer > 0)
            {
                tpAlphaChangeTimer--;
                if (tpAlphaChangeTimer > (int)(tpDuration / 2))
                {
                    NPC.alpha += 26;
                }
                if (tpAlphaChangeTimer == (int)(tpDuration / 2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.position.X = telePosX - NPC.width / 2;
                    NPC.position.Y = telePosY - NPC.height / 2;
                    NPC.netUpdate = true;
                }
                if (tpAlphaChangeTimer < (int)(tpDuration / 2))
                {
                    NPC.alpha -= 26;
                    if (NPC.alpha <= 0)
                    {
                        tpAlphaChangeTimer = 0;
                    }
                }
            }
            if (aiTimer < 0 && shootTimer < 0)
            {
                if (aiTimer == -300)
                {
                    NPC.dontTakeDamage = true;
                    NPC.netUpdate = true;
                    NPC.Center = P.Center + new Vector2(0, -300);
                    NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<InfernaceSpawner>(), NPC.whoAmI, -1);
                    NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<InfernaceSpawner>(), NPC.whoAmI, 1);
                    aiTimer++;
                }
                NPC.alpha--;
                if (NPC.alpha <= 0)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    NPC.dontTakeDamage = false;
                    NPC.netUpdate = false;
                }
            }
            else if (aiTimer < 0)
            {
                int duration = 270 * 4;
                NPC.velocity *= 0.9f;
                if (aiTimer == -duration)
                {
                    Projectile wife = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X - 200, NPC.Center.Y, 0f, 0f, ProjectileType<InfernaceWifeSoul>(), 0, 0f, Main.myPlayer)];
                    wife.ai[1] = shootTimer;
                    if (shootTimer == 0) Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X - 300, NPC.Center.Y, 0f, 0f, ProjectileType<FurosiaSoul>(), 0, 0f, Main.myPlayer);
                }
                aiTimer++;
                if (aiTimer > -2)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<InfernaceSoul>(), 0, 0f, Main.myPlayer);
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
            else
            {
                #region despawning
                if (!P.active || P.dead || !P.ZoneUnderworldHeight)
                {
                    NPC.TargetClosest(true);
                    if (!P.active || P.dead || !P.ZoneUnderworldHeight) despawnTimer++;
                    else despawnTimer = 0;
                }
                if (despawnTimer >= 300) NPC.active = false;
                #endregion

                if ((NPC.life > NPC.lifeMax * 0.75f && aiTimer > 1060f) ||
                    (NPC.life <= NPC.lifeMax * 0.75f && NPC.life > NPC.lifeMax * 0.45f && aiTimer > 1660f) ||
                    (NPC.life <= NPC.lifeMax * 0.45f && aiTimer > 1900f)) aiTimer = 0f;

                if (NPC.life <= NPC.lifeMax * 0.25f && MyWorld.awakenedMode)
                {
                    monolithTimer--;
                    if (monolithTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 monolithPos = P.Bottom;

                        Point monolithPoint = monolithPos.ToTileCoordinates();
                        for (int j = monolithPoint.Y; j < Main.maxTilesY; j++)
                        {
                            Tile newTile = Framing.GetTileSafely(monolithPoint.X, j);
                            if (newTile.HasTile && Main.tileSolid[newTile.TileType])
                            {
                                monolithPoint = new Point(monolithPoint.X, j);
                                monolithPos = new Vector2(monolithPoint.X * 16, monolithPoint.Y * 16);
                                break;
                            }
                        }
                        Projectile.NewProjectile(EAU.NPCs(NPC), monolithPos.X, monolithPos.Y, 0f, 0f, ProjectileType<InfernalMonolithSpawn>(), projectileBaseDamage + 20, 0f, Main.myPlayer);
                        monolithTimer = (int)(MathHelper.Lerp(120, 500, (float)NPC.life / (float)(NPC.lifeMax * 0.25f)));
                    }
                }
                if (NPC.life <= NPC.lifeMax * 0.65f && MyWorld.awakenedMode && dropNum == 0)
                {
                    EnterDroppingAI();
                    dropNum++;
                }
                if (NPC.life <= NPC.lifeMax * 0.15f)
                {
                    bool canDrop = dropNum == 0;
                    if (MyWorld.awakenedMode) canDrop = dropNum == 1;
                    if (canDrop)
                    {
                        Main.NewText(e.Infernace, Color.Orange);
                        EnterDroppingAI();
                        dropNum++;
                    }
                }
                #region furosia 
                if (P.active && Main.expertMode)
                {
                    bool validLife = NPC.life <= NPC.lifeMax * 0.5f;
                    if (MyWorld.awakenedMode) validLife = NPC.life <= NPC.lifeMax * 0.75f;
                    if (validLife && furosiaState == 0)
                    {
                        Furosia furosiaNPC = (Furosia)Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Furosia>())].ModNPC;
                        //furosiaNPC.dashAI = 30;
                        Main.NewText(e.Infernace1, Color.Orange);
                        furosiaState++;
                    }
                    if (furosiaState == 1 && !NPC.AnyNPCs(NPCType<Furosia>()))
                    {
                        Main.NewText(e.Infernace2, Color.Orange);
                        SoundEngine.PlaySound(SoundID.NPCDeath10, NPC.position);
                        furosiaState++;
                    }
                }               
                #endregion

                if (NPC.life < NPC.lifeMax / 2 && NPC.localAI[0] == 0 && MyWorld.awakenedMode)
                {
                    NPC healer = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<HealingHearth>())];
                    healer.ai[1] = NPC.whoAmI;
                    NPC.localAI[0]++;
                }
                aiTimer++;
                shootTimer--;
                if (monsterDropAI <= 0)
                {
                    NPC.color = default(Color);

                    float movementSpeed = Main.expertMode ? 3 : 2.5f;
                    if (MyWorld.awakenedMode) movementSpeed = 3.5f;
                    if (aiTimer < 700f)
                    {
                        MoveDirect(P, movementSpeed);

                        int tpTimer = (int)(aiTimer - Math.Floor(aiTimer / 300f) * 300) + 1;
                        if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer <= 0f)
                        {
                            Spike(P, 10f, projectileBaseDamage);
                            shootTimer = Main.expertMode ? 90 : 130f;
                            if (MyWorld.awakenedMode) shootTimer = 70;
                        }
                        //tp dust
                        int maxdusts = 20;
                        if (tpTimer >= 280f && tpTimer % 5 == 0 && !GetInstance<Config>().lowDust)
                        {
                            for (int i = 0; i < maxdusts; i++)
                            {
                                float dustDistance = 100;
                                float dustSpeed = 10;
                                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                Dust vortex = Dust.NewDustPerfect(NPC.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                                vortex.noGravity = true;
                            }
                        }
                        if (tpTimer >= 300)
                        {
                            int distance = 200 + Main.rand.Next(0, 200);
                            int choice = Main.rand.Next(4);
                            if (choice == 0) Teleport(P.position.X + distance, P.position.Y - distance);
                            else if (choice == 1) Teleport(P.position.X - distance, P.position.Y - distance);
                            else if (choice == 2) Teleport(P.position.X + distance, P.position.Y + distance);
                            else if (choice == 3) Teleport(P.position.X - distance, P.position.Y + distance);
                        }
                    }
                    if (aiTimer >= 700f && aiTimer <= 1060)
                    {
                        if (aiTimer == 700) shootTimer = 0;

                        if (aiTimer >= 700 + tpDuration / 2) NPC.velocity = new Vector2(0, -6);

                        if (aiTimer == 700) Teleport(P.position.X + 300, P.position.Y + 300);
                        if (aiTimer == 880) Teleport(P.position.X - 300, P.position.Y + 300);
                        if (aiTimer == 1060) Teleport(P.position.X, P.position.Y - 300);
                        float projSpeedX = aiTimer < 880f ? -5f : 5f;
                        if (shootTimer <= 0f && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            SoundEngine.PlaySound(SoundID.Item13, NPC.position);
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, projSpeedX, -1, ProjectileType<InfernaceFire>(), projectileBaseDamage, 0f, Main.myPlayer);
                            shootTimer = Main.rand.Next(15, 35);
                        }
                    }
                    if (aiTimer > 1060f && aiTimer <= 1660)
                    {
                        if (aiTimer == 1061) shootTimer = 30;
                        NPC.velocity = Vector2.Zero;
                        if (shootTimer <= 0f && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int damage = Main.expertMode ? (int)(projectileBaseDamage * 1.3f) : (int)(projectileBaseDamage * 0.8f);
                            if (MyWorld.awakenedMode) damage = (int)(projectileBaseDamage * 1.6f);

                            float speed = Main.expertMode ? 8f : 6f;
                            if (MyWorld.awakenedMode) speed = 10f;
                            Waves(P, speed, damage, 4);
                            shootTimer = Main.rand.Next(50, 80);
                            NPC.netUpdate = true;
                        }
                    }
                    if (aiTimer > 1660)
                    {
                        int tpTimer = (int)(aiTimer - 1660);
                        float speed = Main.expertMode ? 5f : 4f;
                        if (MyWorld.awakenedMode) speed = 7f;

                        if (tpTimer == 1)
                        {
                            Teleport(P.position.X + 500, P.position.Y + 500);
                        }
                        if (tpTimer >= 1 + tpDuration / 2 && tpTimer < 120)
                        {
                            NPC.velocity.X = -speed;
                            NPC.velocity.Y = -speed;
                        }
                        if (tpTimer == 120)
                        {
                            Teleport(P.position.X - 500, P.position.Y + 500);
                        }
                        if (tpTimer >= 120 + tpDuration / 2 && tpTimer < 240)
                        {
                            NPC.velocity.X = speed;
                            NPC.velocity.Y = -speed;
                        }
                    }
                }
                else
                {
                    NPC.velocity = Vector2.Zero;
                    float scaleValue = (float)(Math.Sin(monsterDropAI / 17) + 1) / 2f;
                    int r = 255;
                    int g = (int)MathHelper.Lerp(60, 255, scaleValue);
                    int b = (int)MathHelper.Lerp(20, 255, scaleValue);
                    NPC.color = new Color(r, g, b);
                    monsterDropAI--;
                    if (monsterDropAI > 750)
                    {
                        int modNum = Main.expertMode ? 45 : 60;
                        if (MyWorld.awakenedMode) modNum = 20;
                        if (monsterDropAI % modNum == 0)
                        {
                            NPC monster = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, GetDropIDs()[Main.rand.Next(GetDropIDs().Count)])];
                            monster.velocity = new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
                            monster.SpawnedFromStatue = true;
                        }
                    }
                    if ((AnyDropNPCs() || monsterDropAI > 750) && monsterDropAI > 0)
                    {
                        NPC.immortal = true;
                        NPC.dontTakeDamage = true;
                    }
                    else if ((!AnyDropNPCs() && monsterDropAI <= 750) || monsterDropAI <= 0)
                    {
                        ExitDroppingAI();
                    }
                }
            }
            Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0.4f) / 255f, ((255 - NPC.alpha) * 0.1f) / 255f, ((255 - NPC.alpha) * 0f) / 255f);
            if ((aiTimer < 0 && shootTimer > 0) || aiTimer < 0)
            {
                int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }
        private List<int> GetDropIDs()
        {
            List<int> idList = new List<int>()
            {
                NPCID.LavaSlime,
                NPCID.Hellbat
            };
            return idList;
        }
        private bool AnyDropNPCs()
        {
            for (var i = 0; i < GetDropIDs().Count; i++)
            {
                if (NPC.AnyNPCs(GetDropIDs()[i])) return true;
            }
            return false;
        }
        private void ExitDroppingAI()
        {
            NPC.immortal = false;
            NPC.dontTakeDamage = false;
            monsterDropAI = 0;
        }
        private void EnterDroppingAI()
        {
            monsterDropAI = 1200;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (GetDropIDs().Contains(other.type))
                {
                    other.active = false;
                }
            }
        }
        private void MoveDirect(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            NPC.velocity = toTarget * moveSpeed;
        }
        private void Spike(Player P, float speed, int damage)
        {
            SoundEngine.PlaySound(SoundID.Item20, NPC.position);
            float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
            Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<InfernaceSpike>(), damage, 0f, Main.myPlayer)];
            proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
        }
        private void Waves(Player P, float speed, int damage, int numberProj)
        {
            for (int k = 0; k < numberProj; k++)
            {
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(15));
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * perturbedSpeed.X) * -1), (float)((Math.Sin(rotation) * perturbedSpeed.Y) * -1), ProjectileType<InfernaceWave>(), damage, 0f, Main.myPlayer)];
                proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
            }
        }
        private void Teleport(float posX, float posY)
        {
            tpAlphaChangeTimer = tpDuration;
            telePosX = posX;
            telePosY = posY;
        }
    }
}