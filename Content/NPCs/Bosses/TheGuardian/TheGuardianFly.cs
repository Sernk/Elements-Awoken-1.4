using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.TheGuardian;
using ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian;
using ElementsAwoken.Utilities;
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

namespace ElementsAwoken.Content.NPCs.Bosses.TheGuardian
{
    [AutoloadBossHead]
    public class TheGuardianFly : ModNPC
    {
        private int projectileBaseDamage = 65;
        private int moveAI = 1;
        private float shootTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float minionTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float shootTimer2
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(moveAI);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            moveAI = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            NPC.width = 252;
            NPC.height = 152;
            NPC.aiStyle = -1;
            NPC.lifeMax = 115000;
            NPC.damage = 120;
            NPC.defense = 45;
            NPC.knockBackResist = 0f;
            NPC.scale = 1.2f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 55, 0, 0);
            Music = MusicID.Boss4;
            for (int num2 = 0; num2 < 206; num2++)
            {
                NPC.buffImmune[num2] = true;
            }
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            //bossBag = mod.ItemType("GuardianBag");
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.7f, // Мини иконка в бестиарии
                PortraitScale = 0.8f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y -= 35f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.TheGuardianFly"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 200000;
            NPC.damage = 150;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 250000;
                NPC.damage = 200;
                NPC.defense = 60;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 5)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.TGuaLoot]));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<GuardianBag>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGuardianTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGuardianMask>(), 10));
        }
        public override void OnKill()
        {
            MyWorld.downedGuardian = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
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
                    NPC.localAI[0]++;
                }
            }
            if (Main.dayTime)
            {
                NPC.localAI[0]++;
            }
            if (NPC.localAI[0] >= 300)
            {
                NPC.active = false;
            }
            #endregion
            shootTimer--; 
            minionTimer--;
            aiTimer++;
            shootTimer2--; // portals & infernoballs
            if (NPC.life >= NPC.lifeMax * 0.5f)
            {
                if (aiTimer > 1700f)
                {
                    aiTimer = 0f;
                }
            }
            if (NPC.life <= NPC.lifeMax * 0.5f)
            {
                if (aiTimer > 2500f)
                {
                    aiTimer = 0f;
                }
            }
            // minions
            if (minionTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GuardianProbe>());
                minionTimer = Main.rand.Next(300, 1200);
                NPC.netUpdate = true;
            }

            //attack 1- flys left and right of the player and leaves shooting orbs 
            if (aiTimer <= 500)
            {
                Vector2 target = P.Center + new Vector2(400 * moveAI, -300);
                if (Vector2.Distance(target,NPC.Center) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.2f, target);

                if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer <= 25 && shootTimer % 5 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<GuardianOrb>(), projectileBaseDamage, 0f, Main.myPlayer);
                }
                if (shootTimer <= 0) shootTimer = 75;
            }

            //attack 2- flys above the player and drops fire/lasers
            if (aiTimer >= 500 && aiTimer <= 800)
            {
                Move(P, 0.09f, P.Center - new Vector2(0, 400), 1f);
                if (NPC.life >= NPC.lifeMax * 0.5f)
                {
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        //Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                        int proj = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, Main.rand.NextFloat(-2, 2), 8f, ModContent.ProjectileType<GuardianFire>(), projectileBaseDamage, 0f, Main.myPlayer);
                        Projectile fire = Main.projectile[proj];
                        fire.timeLeft = 120;
                        shootTimer = 12;
                    }
                }
                if (NPC.life <= NPC.lifeMax * 0.5f)
                {
                    if (shootTimer <= 30 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int maxdusts = 10;
                        for (int i = 0; i < maxdusts; i++)
                        {
                            float dustDistance = 100;
                            float dustSpeed = 15;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y + 40) + offset, 6, velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                        }
                    }
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item122, NPC.position);
                        for (int i = 0; i < 6; i++)
                        {
                            int proj = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.position.X + 140 + 6.6f * i, NPC.Bottom.Y, 0f, 9f, ModContent.ProjectileType<GuardianBeam>(), projectileBaseDamage, 0f, Main.myPlayer);
                            Projectile Beam = Main.projectile[proj];
                            Beam.timeLeft = 75;
                        }
                        shootTimer = 50;
                    }

                }
            }
            // so the guardian wont instantly shoot the player in the face 
            if (aiTimer == 800)
            {
                shootTimer = 100;
            }
            //attack 3 - throws multiple sticky grenades at the player
            if (aiTimer >= 800 && aiTimer <= 1200)
            {
                Move(P, 0.1f, P.Center - new Vector2(0, 300));

                if (shootTimer <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    Bolts(P, 18f, projectileBaseDamage - 20, Main.rand.Next(4, 6), 13);
                    shootTimer = Main.rand.Next(30, 80);
                }
            }
            //attack 4 - fireball cluster
            if (aiTimer >= 1200 && aiTimer <= 1700)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    for (int i = 0; i < 6; i++)
                    {
                        float speed = 16 + Main.rand.NextFloat(-2, 2);
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<GuardianFireball>(), projectileBaseDamage - 10, 0f, 0);
                    }
                    shootTimer = 50;
                }
            }

            //attack 5 - shoots a fast exploding bolt at the player 
            if (NPC.life <= NPC.lifeMax * 0.5f && aiTimer >= 1700f && aiTimer <= 2000f)
            {
                Move(P, 0.1f, P.Center - new Vector2(0, 300));
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float Speed = 20f;
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<GuardianStrike>(), projectileBaseDamage + 20, 0f, Main.myPlayer, 0, 1);
                    shootTimer = 100;
                }
            }

            //attack 6 - releases shots in a circle
            if (NPC.life <= NPC.lifeMax * 0.5f && aiTimer >= 2000f && aiTimer <= 2500f)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<GuardianShot>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 5;
                }
            }

            // portals and infernoballs
            if (NPC.life <= NPC.lifeMax * 0.5f)
            {
                // infernoballs
                if (shootTimer2 == 200 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int direction = 1;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            direction = 1;
                            break;
                        case 1:
                            direction = -1;
                            break;
                        default: break;
                    }
                    float speed = 12f;
                    int damage = projectileBaseDamage - 20;
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X - 1300 * direction + i * 50, P.Center.Y - i * 200, speed * direction, 0f, ModContent.ProjectileType<GuardianInfernoball>(), damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X - 1300 * direction + i * 50, P.Center.Y + i * 200, speed * direction, 0f, ModContent.ProjectileType<GuardianInfernoball>(), damage, 0f, Main.myPlayer);
                    }
                }
                // portal
                if (shootTimer2 <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int minDist = 400;
                    int vectorX = Main.rand.Next(-800, 800);
                    int vectorY = Main.rand.Next(-800, 800);
                    if (vectorX < minDist && vectorX > 0)
                    {
                        vectorX = minDist;
                    }
                    else if (vectorX > -minDist && vectorX < 0)
                    {
                        vectorX = -minDist;
                    }
                    if (vectorY < minDist && vectorY > 0)
                    {
                        vectorY = minDist;
                    }
                    else if (vectorY > -minDist && vectorX < 0)
                    {
                        vectorY = -minDist;
                    }
                    Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X + vectorX, P.Center.Y + vectorY, 0f, 0f, ModContent.ProjectileType<GuardianPortal>(), projectileBaseDamage * 2, 0f, Main.myPlayer);
                    shootTimer2 = 400;
                }
            }

            // strikes coming up from underneath
            if (NPC.life <= NPC.lifeMax * 0.2f)
            {
                if (Main.rand.Next(14) == 0)
                {
                    float posX = P.Center.X + Main.rand.Next(5000) - 3000;
                    float posY = P.Center.Y + 1000;
                    Projectile.NewProjectile(EAU.NPCs(NPC), posX, posY, 0f, -10f, ModContent.ProjectileType<GuardianStrike>(), projectileBaseDamage, 0f, Main.myPlayer);
                }
            }

            // create circle
            if (NPC.life <= NPC.lifeMax * 0.2f && NPC.localAI[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.localAI[1]++;
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<GuardianCircle>(), NPC.damage, 0f, Main.myPlayer, 0, NPC.whoAmI);
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/NPCs/Bosses/TheGuardian/" + GetType().Name + "_Glow").Value;
            Rectangle frame = new Rectangle(0, NPC.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[NPC.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[NPC.type]) * 0.5f);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY) + new Vector2(0, 11), frame, new Color(255, 255, 255, 0), NPC.rotation, origin, NPC.scale, effects, 0.0f);
        }
        private void Move(Player P, float speed, Vector2 target, float slowScale = 0.99f)
        {
            Vector2 desiredVelocity = target - NPC.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;
            if (Vector2.Distance(P.Center, NPC.Center) >= 2500) speed = 2;

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
            float slowSpeed = Main.expertMode ? slowScale * 0.97f : slowScale;
            if (MyWorld.awakenedMode) slowSpeed = slowScale * 0.95f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((NPC.velocity.X < xSign && xSign == 1) || (NPC.velocity.X > xSign && xSign == -1)) NPC.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, NPC.Center.Y) > 1000)
            {
                if ((NPC.velocity.X < ySign && ySign == 1) || (NPC.velocity.X > ySign && ySign == -1)) NPC.velocity.Y *= slowSpeed;
            }
        }
        private void Bolts(Player P, float speed, int damage, int numberProj, int angle)
        {
            float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<GuardianStickyBolt>(), damage, 0f, 0);
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
