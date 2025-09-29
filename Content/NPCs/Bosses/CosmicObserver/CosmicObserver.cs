using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.CosmicObserver;
using ElementsAwoken.Content.Projectiles.NPCProj.CosmicObserver;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.Loot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.CosmicObserver
{
    [AutoloadBossHead]
    public class CosmicObserver : ModNPC
    {
        private int projectileBaseDamage = 20;
        private const float beamChargeMax = 300f;
        private int targetLaserFrame = 0;
        private float desiredX = 0;
        private float desiredY = 0;
        private int moveAI = 1;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(desiredX);
            writer.Write(desiredY);
            writer.Write(targetLaserFrame);
            writer.Write(moveAI);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            desiredX = reader.ReadInt32();
            desiredY = reader.ReadInt32();
            targetLaserFrame = reader.ReadInt32();
            moveAI = reader.ReadInt32();
        }
        private float shootTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float storeRot
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float beamCharge
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 104;
            NPC.height = 104;
            NPC.aiStyle = -1;
            NPC.lifeMax = 5500;
            NPC.damage = 40;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.scale = 1.2f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPCID.Sets.TrailCacheLength[NPC.type] = 15;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 1f, // Мини иконка в бестиарии
                PortraitScale = 0.6f, // При нажатии на иконку в бестиарии
            };
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 70;
            NPC.lifeMax = 12000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 17500;
                NPC.damage = 90;
                NPC.defense = 30;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.life >= NPC.lifeMax * 0.8f)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.life >= NPC.lifeMax * 0.65f)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.life >= NPC.lifeMax * 0.45f)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.life >= NPC.lifeMax * 0.3f && MyWorld.awakenedMode)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            if (aiTimer % 9 == 0)
            {
                targetLaserFrame++;
                if (targetLaserFrame > 3)
                {
                    targetLaserFrame = 0;
                }
            }
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.CosmicObserverBestiary"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            var _AwakenedModeEssence = new LeadingConditionRule(new EAIDRC.DropAwakened());
            var _AwakenedModeExpert = new LeadingConditionRule(new EAIDRC.DropExpert());
            var _AwakenedModeNormal = new LeadingConditionRule(new EAIDRC.DropNormal());

            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsMasterMode(), ModContent.ItemType<CosmicObserverRelicItem>()));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmicGlass>()));
            npcLoot.Add(_AwakenedMode);
            _AwakenedModeEssence.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmicShard>(), minimumDropped: 17, maximumDropped: 26));
            npcLoot.Add(_AwakenedModeEssence);
            _AwakenedModeExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmicShard>(), minimumDropped: 12, maximumDropped: 19));
            npcLoot.Add(_AwakenedModeExpert);
            _AwakenedModeNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmicShard>(), minimumDropped: 8, maximumDropped: 12));
            npcLoot.Add(_AwakenedModeNormal);
        }
        public override void OnKill()
        {
            MyWorld.downedCosmicObserver = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); 
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("CosmicObserver" + i).Type, NPC.scale);
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (aiTimer >= 1400)
            {
                Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/CosmicObserver/ObserverTarget").Value;
                Texture2D backTexture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/CosmicObserver/ObserverTarget1").Value;

                Vector2 position = NPC.Center;
                Player P = Main.player[NPC.target];
                Vector2 mountedCenter = P.MountedCenter;
                int height = 34;
                Rectangle? sourceRectangle = new Rectangle(0, height * targetLaserFrame, texture.Width, height);
                Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)height * 0.5f);
                float num1 = (float)height;
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
                        Tile t = Main.tile[position.ToTileCoordinates().X, position.ToTileCoordinates().Y];
                        if (Main.tileSolid[t.TileType] && t.HasTile)
                        {
                            return;
                        }
                        Main.spriteBatch.Draw(backTexture, position - Main.screenPosition, new Rectangle?(), Color.White * (beamCharge / beamChargeMax), rotation, origin, 1f, SpriteEffects.None, 0.0f);
                        Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    }
                }
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            #region despawning
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    NPC.localAI[3]++;
                }
            }
            if (NPC.localAI[3] >= 300)
            {
                NPC.active = false;
            }
            #endregion
            Lighting.AddLight(NPC.Center, 0.2f, 1.4f, 0.2f);
            shootTimer--;
            aiTimer++;
            if (MyWorld.awakenedMode && NPC.life < NPC.lifeMax * 0.25f)
            {
                aiTimer++;
            }
            if (MyWorld.awakenedMode)
            {
                if (NPC.life > NPC.lifeMax * 0.45f)
                {
                    if (aiTimer >= 1200)
                    {
                        aiTimer = 0;
                    }
                }
                else if (NPC.life > NPC.lifeMax * 0.3f)
                {
                    if (aiTimer >= 1400)
                    {
                        aiTimer = 0;
                    }
                }
                else
                {
                    // set in the beam
                }
            }
            else if (Main.expertMode)
            {
                if (NPC.life > NPC.lifeMax / 2)
                {
                    if (aiTimer >= 1200)
                    {
                        aiTimer = 0;
                    }
                }
                else
                {
                    if (aiTimer >= 1400)
                    {
                        aiTimer = 0;
                    }
                }
            }
            else
            {
                if (aiTimer >= 1200)
                {
                    aiTimer = 0;
                }
            }
            if (aiTimer <= 600)
            {
                Vector2 floatCenter = new Vector2(desiredX, desiredY);
                 if (Vector2.Distance(P.Center, NPC.Center) > 700)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    NPC.velocity = toTarget * 5f;
                }
                else if (Vector2.Distance(P.Center, NPC.Center) > 500)
                {
                    Move(P, 0.02f, P.Center);
                }
                else
                {
                    float moveSpeed = 0.2f;
                    Vector2 vector149 = floatCenter - NPC.Center;
                    if (vector149.Length() < 60f)
                    {
                        moveSpeed = 0.12f;
                    }
                    if (vector149.Length() < 40f)
                    {
                        moveSpeed = 0.06f;
                    }
                    if (vector149.Length() > 20f)
                    {
                        if (Math.Abs(floatCenter.X - NPC.Center.X) > 20f)
                        {
                            NPC.velocity.X = NPC.velocity.X + moveSpeed * (float)Math.Sign(floatCenter.X - NPC.Center.X);
                        }
                        if (Math.Abs(floatCenter.Y - NPC.Center.Y) > 10f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + moveSpeed * (float)Math.Sign(floatCenter.Y - NPC.Center.Y);
                        }
                    }
                    else if (NPC.velocity.Length() > 2f)
                    {
                        NPC.velocity *= 0.96f;
                    }
                    if (Math.Abs(NPC.velocity.Y) < 1f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                    }
                    float maxSpeed = 15f;
                    if (NPC.velocity.Length() > maxSpeed)
                    {
                        NPC.velocity = Vector2.Normalize(NPC.velocity) * maxSpeed;
                    }
                }
                if (Vector2.Distance(P.Center, NPC.Center) > 300)
                {
                    if (Vector2.Distance(floatCenter, NPC.Center) > 150) 
                    {
                        desiredX = NPC.Center.X;
                        desiredY = NPC.Center.Y;
                    }
                }
                if (ModContent.GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(floatCenter, 2, 2, 6)];
                    dust.noGravity = true;
                }    
            }
            if (aiTimer == 600)
            {
                NPC.localAI[0] = 0;
            }
            if (aiTimer > 600 && aiTimer <= 1200)
            {

                int numProj = 8;
                int shootDelay = 5;
                if (shootTimer == numProj * shootDelay + 1)
                {
                    storeRot = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                }               
                if (shootTimer > numProj * shootDelay)
                {
                    Vector2 target = P.Center + new Vector2(600f * moveAI, -75);
                    if (moveAI == 0) moveAI = -1;
                    if (MathHelper.Distance(target.X, NPC.Center.X) <= 20)
                    {
                        moveAI *= -1;
                    }

                    Move(P, 0.1f, target);
                }
                if (shootTimer <= numProj * shootDelay)
                {
                    NPC.velocity.X *= 0.9f;
                    NPC.velocity.Y *= 0.9f;

                    if (NPC.localAI[0] < numProj && shootTimer % shootDelay == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                        Vector2 projSpeed = new Vector2((float)((Math.Cos(storeRot) * 10) * -1), (float)((Math.Sin(storeRot) * 10) * -1));
                        float rotation = MathHelper.ToRadians(5);
                        float amount = NPC.direction == -1 ? NPC.localAI[0] - numProj / 2 : -(NPC.localAI[0] - numProj / 2); // to make it from down to up
                        Vector2 perturbedSpeed = new Vector2(projSpeed.X, projSpeed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount));
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ObserverShard>(), projectileBaseDamage, 3f, Main.myPlayer);
                        NPC.localAI[0]++;
                    }
                }
                if (NPC.localAI[0] >= numProj)
                {
                    NPC.localAI[0] = 0;
                    shootTimer = 120f + numProj * shootDelay;
                }
            }
            if (aiTimer > 1200 && aiTimer < 1400) // spin
            {
                NPC.rotation += 0.2f;

                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 perturbedSpeed = new Vector2(7f, 7f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ObserverShard>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 5;
                }
                if (MyWorld.awakenedMode)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];
                        if (proj.active && proj.friendly && Vector2.Distance(proj.Center, NPC.Center) < 150)
                        {
                            proj.Kill();
                            for (int d = 0; d < 10; d++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(proj.position, proj.width, proj.height, 220, proj.oldVelocity.X, proj.oldVelocity.Y, 100, default(Color), 1.8f)];
                                dust.noGravity = true;
                                dust.velocity *= 0.5f;
                            }
                        }
                    }
                }
            }
            else
            {
                NPC.rotation = NPC.velocity.X * 0.1f;
            }
            if (aiTimer >= 1400)
            {
                beamCharge++;
                if (beamCharge >= beamChargeMax && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item113, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (P.Center.Y + P.velocity.Y * 2), NPC.Center.X - (P.Center.X + P.velocity.X * 2));
                    if (Collision.CanHit(NPC.Center, 2, 2, P.Center, 2, 2))
                    {
                        P.immune = false;
                    }
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * 4f) * -1), (float)((Math.Sin(rotation) * 4f) * -1), ModContent.ProjectileType<ObserverBeam>(), projectileBaseDamage * 5, 0f, Main.myPlayer, 0, NPC.whoAmI);
                    beamCharge = 0;
                    aiTimer = 0;
                }
            }
            if (NPC.localAI[1] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.TargetClosest(true);
                NPC.localAI[1] = 1;
                NPC hand = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<CosmicObserverHand>(), 0, 0f, 0f, 0f, 0f, 255)];
                hand.ai[0] = -1f;
                hand.ai[1] = (float)NPC.whoAmI;
                hand.target = NPC.target;
                hand.netUpdate = true;
                hand = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<CosmicObserverHand>(), 0, 0f, 0f, 0f, 0f, 255)];
                hand.ai[0] = 1f;
                hand.ai[1] = (float)NPC.whoAmI;
                hand.target = NPC.target;
                hand.netUpdate = true;
            }
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - NPC.Center;
            if (Main.expertMode) speed *= 1.05f;
            if (MyWorld.awakenedMode) speed *= 1.05f;

            if (NPC.velocity.X < desiredVelocity.X)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + speed;
                }
            }
            else if (NPC.velocity.X > desiredVelocity.X)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - speed;
                }
            }
            if (NPC.velocity.Y < desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed;
                if (NPC.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                    return;
                }
            }
            else if (NPC.velocity.Y > desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.96f : 0.98f;
            if (MyWorld.awakenedMode) slowSpeed = 0.94f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((NPC.velocity.X < xSign && xSign == 1) || (NPC.velocity.X > xSign && xSign == -1)) NPC.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, NPC.Center.Y) > 1000)
            {
                if ((NPC.velocity.X < ySign && ySign == 1) || (NPC.velocity.X > ySign && ySign == -1)) NPC.velocity.Y *= slowSpeed;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}