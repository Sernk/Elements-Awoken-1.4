using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles.NPCProj.Obsidious;
using ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Obsidious
{
    [AutoloadBossHead]
    public class ObsidiousHuman : ModNPC
    {
        private int projectileBaseDamage = 40;

        private float tpAlphaChangeTimer = 0;
        private float telePosX = 0;
        private float telePosY = 0;

        private float spinAI = 0f;

        private const int tpDuration = 20;
        private float despawnTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float phase
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(spinAI);
            writer.Write(tpAlphaChangeTimer);
            writer.Write(telePosX);
            writer.Write(telePosY);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            spinAI = reader.ReadSingle();
            tpAlphaChangeTimer = reader.ReadSingle();
            telePosX = reader.ReadSingle();
            telePosY = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 48;
            NPC.aiStyle = -1;
            NPC.lifeMax = 50000;
            NPC.damage = 40;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.boss = true;
            NPC.scale = 1f;
            NPC.HitSound = SoundID.NPCHit1;
            //npc.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.Boss1;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ObsidiousTheme");
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 16;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(40000, balance, bossAdjustment, 75000);
            NPC.damage = (int)EAU.BalanceDamage(40, numPlayers, balance, 90);
            NPC.defense = EAU.BalanceDefense(30, 40);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter > 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (phase == 0)
            {
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
            }
            if (phase == 1)
            {
                if (NPC.frame.Y > frameHeight * 7)
                {
                    NPC.frame.Y = 4 * frameHeight;
                }
            }
            if (phase == 2)
            {
                if (NPC.frame.Y > frameHeight * 11)
                {
                    NPC.frame.Y = 8 * frameHeight;
                }
            }
            if (phase == 3)
            {
                if (NPC.frame.Y > frameHeight * 15)
                {
                    NPC.frame.Y = 12 * frameHeight;
                }
            }
            NPC.rotation = NPC.velocity.X * 0.05f;
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ObsidiousTransition>());
            }
        }
        private void Despawn(Player P)
        {
            if (!P.active || P.dead)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    despawnTimer++;
                    if (despawnTimer >= 300)
                    {
                        if (NPC.life > NPC.lifeMax * 0.50f)
                        {
                            Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious, new Color(188, 58, 49));
                        }
                        else
                        {
                            Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious1, new Color(188, 58, 49));
                        }
                        NPC.active = false;
                    }
                }
                else if (!Main.dayTime)
                    despawnTimer = 0;
            }
            if (Main.dayTime)
            {
                despawnTimer++;
                if (despawnTimer >= 300) NPC.active = false;
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 0.5f, 0.5f, 0.5f);
            if (Main.masterMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 7;
                else projectileBaseDamage = 6;
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 26;
                else projectileBaseDamage = 24;
            }
            else projectileBaseDamage = 34;
            Despawn(P);

            //teleport code- takes 20 ticks to return to full alpha
            if (tpAlphaChangeTimer > 0)
            {
                tpAlphaChangeTimer--;
                if (tpAlphaChangeTimer > (int)(tpDuration / 2))
                {
                    NPC.alpha += 26;
                }
                if (tpAlphaChangeTimer == (int)(tpDuration / 2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.position.X = telePosX;
                    NPC.position.Y = telePosY;
                    NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
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
            else NPC.alpha = 0;
            // text and phase changes
            if (NPC.localAI[0] == 0)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious2, new Color(188, 58, 49));
                NPC.localAI[0]++;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.75f && phase == 0) phase++;
            if (NPC.life <= NPC.lifeMax * 0.50f && phase == 1)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious3, new Color(188, 58, 49));
                phase++;
            }
            if (NPC.life <= NPC.lifeMax * 0.25f && phase == 2) phase++;

            shootTimer--;
            aiTimer++; 
            // fire
            if (phase == 0f)
            {
                if (aiTimer < 600f)
                {
                    Move(P, 0.1f);
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousFireCrystal>(), projectileBaseDamage, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        shootTimer = 45;
                    }
                }
                else
                {
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;
                    // shoot in circle
                    Vector2 offset = new Vector2(300, 0);
                    float rotateSpeed = 0.05f;
                    spinAI += rotateSpeed;

                    Vector2 vector = new Vector2(NPC.Center.X, NPC.Center.Y - 102);

                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        Vector2 tpTarget = P.Center + offset.RotatedBy(spinAI * (Math.PI * 2 / 8));
                        Teleport(tpTarget.X, tpTarget.Y);
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousFireCrystalStationary>(), projectileBaseDamage - 2, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        shootTimer = 25;
                    }
                }
                if (aiTimer > 900f)
                {
                    aiTimer = 0f;
                }
            }
            // earth
            else if(phase == 1)
            {
                Move(P, 0.1f);
                if (NPC.localAI[1] == 0)
                {
                    int orbitalCount = 3;
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        int orbital = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousHumanRockOrbital>(), NPC.damage, 0f, Main.myPlayer, l * distance, NPC.whoAmI);
                    }
                    NPC.localAI[1]++;
                }
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item69, NPC.position);
                    float speed = 8f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<ObsidiousRockLarge>(), projectileBaseDamage + 2, 0f, Main.myPlayer);
                    shootTimer = 40;
                }
            }
            // ice
            else if (phase == 2)
            {
                if (aiTimer < 600f)
                {
                    Move(P, 0.1f);
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int orbitalCount = 3;
                        for (int l = 0; l < orbitalCount; l++)
                        {
                            int distance = 360 / orbitalCount;
                            int orbital = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousIceCrystalSpin>(), NPC.damage, 0f, Main.myPlayer, l * distance, NPC.whoAmI);
                        }
                        shootTimer = 45;
                    }
                }
                else
                {
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;
                    if (aiTimer == 600f)
                    {
                        spinAI = 0f;
                        int crystalCount = 2;
                        for (int l = 0; l < crystalCount; l++)
                        {
                            int distance = 360 / crystalCount;
                            int orbital = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousIceBeamCrystal>(), 0, 0f, Main.myPlayer, l * distance, NPC.whoAmI);
                        }
                    }
                    Vector2 offset = new Vector2(400, 0);
                    spinAI += 0.015f;

                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numProj = 2;
                        for (int i = 0; i < numProj; i++)
                        {
                            int damage = aiTimer <= 660 ? 0 : projectileBaseDamage;
                            float projOffset = 360 / numProj;
                            Vector2 shootTarget1 = NPC.Center + offset.RotatedBy(spinAI + (projOffset * i) * (Math.PI * 2 / 8));
                            float rotation = (float)Math.Atan2(NPC.Center.Y - shootTarget1.Y, NPC.Center.X - shootTarget1.X);
                            int proj = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * 10f) * -1), (float)((Math.Sin(rotation) * 10f) * -1), ModContent.ProjectileType<ObsidiousIceBeam>(), projectileBaseDamage, 0f, Main.myPlayer);
                            Main.projectile[proj].timeLeft = (int)((aiTimer - 600));
                        }
                        shootTimer = 3;
                    }
                }
                if (aiTimer > 900f)
                {
                    aiTimer = 0f;
                }
            }
            // purpleness
            else if (phase == 3)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0f;
                if (NPC.localAI[2] == 0)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousRitual>(), NPC.damage, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    NPC.localAI[2]++;
                }
                shootTimer--;
                if (shootTimer == 90)
                {
                    int distance = Main.rand.Next(250, 400);
                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                    {
                        Teleport(P.position.X + distance, P.position.Y - distance);
                    }
                    else if (choice == 1)
                    {
                        Teleport(P.position.X - distance, P.position.Y - distance);
                    }
                    else if (choice == 2)
                    {
                        Teleport(P.position.X + distance, P.position.Y + distance);
                    }
                    else if (choice == 3)
                    {
                        Teleport(P.position.X - distance, P.position.Y + distance);
                    }
                }
                if (shootTimer <= 90 && shootTimer % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    float speed = 8f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<ObsidiousHomingBall>(), projectileBaseDamage - 2, 0f, Main.myPlayer);
                    //npc.netUpdate = true;
                }
                if (shootTimer <= 0)
                {
                    shootTimer = 300;
                    if (NPC.life <= NPC.lifeMax * 0.15f) shootTimer -= 60;
                    if (NPC.life <= NPC.lifeMax * 0.05f) shootTimer -= 30;
                }
            }
        }
        private void Move(Player P, float speed)
        {
            int maxDist = 1000;
            if (Vector2.Distance(P.Center, NPC.Center) >= maxDist)
            {
                float moveSpeed = 8f;
                Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                toTarget.Normalize();
                NPC.velocity = toTarget * moveSpeed;
            }
            else
            {
                NPC.spriteDirection = NPC.direction;

                if (Main.expertMode)
                {
                    speed += 0.05f;
                }
                Vector2 vector75 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float playerX = P.position.X + (float)(P.width / 2) - vector75.X;
                float playerY = P.position.Y + (float)(P.height / 2) - vector75.Y;
                if (NPC.velocity.X < playerX)
                {
                    NPC.velocity.X = NPC.velocity.X + speed;
                    // if (npc.velocity.X < 0f && playerY > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + speed;
                    }
                }
                else if (NPC.velocity.X > playerX)
                {
                    NPC.velocity.X = NPC.velocity.X - speed;
                    //if (npc.velocity.X > 0f && playerX < 0f) // this breaks it for some reason :(
                    {
                        NPC.velocity.X = NPC.velocity.X - speed;
                    }
                }
                if (NPC.velocity.Y < playerY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                    if (NPC.velocity.Y < 0f && playerY > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + speed;
                        return;
                    }
                }
                else if (NPC.velocity.Y > playerY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    if (NPC.velocity.Y > 0f && playerY < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - speed;
                        return;
                    }
                }
            }
        }
        private void Teleport(float posX, float posY)
        {
            tpAlphaChangeTimer = tpDuration;
            telePosX = posX;
            telePosY = posY;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}