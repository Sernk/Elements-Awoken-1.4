using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Permafrost;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.NPCProj.Permafrost;
using ElementsAwoken.EASystem.Loot;
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
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.Permafrost
{
    [AutoloadBossHead]
    public class Permafrost : ModNPC
    {
        private int projectileBaseDamage = 50;
        //shockwave
        private int rippleCount = 2;
        private int rippleSize = 15;
        private int rippleSpeed = 30;
        private float distortStrength = 600f;

        private float storePosX = 0;
        private float storePosY = 0;
        private float enrageTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(storePosX);
            writer.Write(storePosY);
            writer.Write(enrageTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            storePosX = reader.ReadSingle();
            storePosY = reader.ReadSingle();
            enrageTimer = reader.ReadSingle();
        }
        private float aiTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float state
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float shockwave
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
            NPC.width = 152;
            NPC.height = 158;
            NPC.lifeMax = 40000;
            NPC.damage = 80;
            NPC.defense = 36;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 20, 0, 0);
            Music = MusicID.Boss3;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffType<IceBound>()] = true;
            NPC.buffImmune[BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // 0.n == уменшает изоброжения
                Scale = 0.77f, // Мини иконка в бестиарии 
                PortraitScale = 0.7f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y -= 22f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.Permafrost"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 65000;
            NPC.damage = 160;
            NPC.defense = 38;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 80000;
                NPC.damage = 190;
                NPC.defense = 42;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }  
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, 180, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule _DropNormal = new LeadingConditionRule(new EAIDRC.DropNormal());
            LeadingConditionRule _DropExpert = new LeadingConditionRule(new EAIDRC.DropAwakened());

            _DropNormal.OnSuccess(ItemDropRule.OneFromOptions(1, [.. EAList.PermLoot]));
            _DropNormal.OnSuccess(ItemDropRule.Common(ItemType<PermafrostTrophy>(), 10));
            _DropNormal.OnSuccess(ItemDropRule.Common(ItemType<PermafrostMask>(), 10));
            _DropExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemType<PermafrostBag>(), 1));
            _DropNormal.OnSuccess(ItemDropRule.Common(ItemType<FrostEssence>(), minimumDropped: 5, maximumDropped: 25));
            npcLoot.Add(_DropNormal);
            npcLoot.Add(_DropExpert);
        }
        public override void OnKill()
        {
            if (!MyWorld.downedPermafrost)
            {
                ElementsAwoken.encounter = 2;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 2 start");
            }
            MyWorld.downedPermafrost = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
            Terraria.Graphics.Effects.Filters.Scene["Shockwave"].Deactivate();
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (state == 4 && Main.netMode == NetmodeID.SinglePlayer)
            {
                if (shockwave == 0)
                {
                    if (!Terraria.Graphics.Effects.Filters.Scene["Shockwave"].IsActive())
                    {
                        Terraria.Graphics.Effects.Filters.Scene.Activate("Shockwave", NPC.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(NPC.Center);
                    }
                }
                else
                {
                    shockwave++;
                    float progress = shockwave / 30f;
                    Terraria.Graphics.Effects.Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
                if (Main.netMode != NetmodeID.Server && Terraria.Graphics.Effects.Filters.Scene["Shockwave"].IsActive() && aiTimer == 180) Terraria.Graphics.Effects.Filters.Scene["Shockwave"].Deactivate();
            }
        }
        public override void AI()
        {
            NPC.TargetClosest(true);

            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);

            Player P = Main.player[NPC.target];
            // despawn
            if (!P.active || P.dead)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    NPC.ai[0]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.ai[0] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.ai[0] = 0;
            }
            if (!P.ZoneSnow)
            {
                if (enrageTimer < 600) enrageTimer++;
            }
            if (P.ZoneSnow)
            {
                if (enrageTimer > 0) enrageTimer--;
            }
            bool enraged = false;
            if (enrageTimer >= 600) enraged = true;

            if (state == 0)
            {
                aiTimer++;
                shootTimer++;
                if (aiTimer < 300) FlyTo(new Vector2(P.Center.X - 200, P.Center.Y - 200), 0.1f, 14f);
                else FlyTo(new Vector2(P.Center.X + 200, P.Center.Y - 200), 0.1f, 14f);
                int shootRate = enraged ? 3 : 6;
                if (shootTimer % shootRate == 0 && shootTimer < 30 && shootTimer >= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    int proj = -1;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 pos = NPC.Center + Main.rand.NextVector2Square(-150, 150);
                        float Speed = 12f;
                        float rotation = (float)Math.Atan2(pos.Y - P.Center.Y, pos.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        proj = Projectile.NewProjectile(EAU.NPCs(NPC), pos, projSpeed, ProjectileType<PermafrostIcicle>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    if (proj != -1)
                    {
                        Projectile ice = Main.projectile[proj];
                        if (!GetInstance<Config>().lowDust)
                        {
                            int numDusts = 20;
                            for (int p = 0; p < numDusts; p++)
                            {
                                Vector2 position = (Vector2.One * new Vector2((float)ice.width / 2f, (float)ice.height) * 0.3f * 0.5f).RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + ice.Center;
                                Vector2 velocity = position - ice.Center;
                                int dust = Dust.NewDust(position + velocity, 0, 0, 135, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 2f;
                            }
                        }
                    }
                }
                if (shootTimer > 75) shootTimer = 0;
                if (aiTimer > 600)
                {
                    aiTimer = 2;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 1)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float speed = 5f;
                    float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 12 : 8 : 6;
                    float rotation = MathHelper.ToRadians(360);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<IceMagic>(), (int)(projectileBaseDamage * 1.2f), 2f, Main.myPlayer);
                    }
                }
                state = aiTimer;
                aiTimer = 0;
                shootTimer = 0;
            }
            else if (state == 2)
            {
                aiTimer++;
                if (shootTimer == 0)
                {
                    Teleport(P.Center - new Vector2(0, 300));
                    NPC.velocity = Vector2.Zero;
                }
                else if (shootTimer > 20)
                {
                    NPC.velocity.Y = 20;
                }
                Tile tileTest = Framing.GetTileSafely((int)(NPC.Bottom.X / 16), (int)(NPC.Bottom.Y / 16));
                shootTimer++;
                if ((tileTest.HasTile && Main.tileSolid[tileTest.TileType] && !TileID.Sets.Platforms[tileTest.TileType] && shootTimer > 30) || shootTimer > 60)
                {
                    shootTimer = 0;

                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(new Vector2(NPC.position.X, NPC.Bottom.Y - 16), NPC.width, 16, 135, 0f, 0f, 100, default(Color), 2.5f)];
                        dust.noGravity = true;
                        if (dust.position.X < NPC.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -6f;
                        else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 6f;
                        dust.velocity.Y = Main.rand.NextFloat(-10, -2);
                    }
                    SoundEngine.PlaySound(SoundID.Item122, NPC.position);
                }
                if (aiTimer > 300)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                    if (NPC.life > NPC.lifeMax * 0.5f) state++;
                    Teleport(P.Center - new Vector2(0, 300));
                    NPC.velocity = Vector2.Zero;
                }
            }
            else if (state == 3)
            {
                FlyTo(new Vector2(P.Center.X, P.Center.Y ), 0.1f, 14f);
                shootTimer--;
                if (shootTimer <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                    float speed = 5f;
                    float numberProjectiles = MyWorld.awakenedMode ? 3 : 2;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        int proj = -1;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);

                            proj = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<HomingIce>(), projectileBaseDamage, 2f, Main.myPlayer);
                        }
                        if (proj != -1)
                        {
                            Projectile ice = Main.projectile[proj];
                            int distance = (int)((NPC.width / 2) * 0.8f);
                            float rad = (MathHelper.ToRadians(360) / numberProjectiles) * i;
                            ice.position.X = NPC.Center.X - (int)(Math.Cos(rad) * distance) - ice.width / 2;
                            ice.position.Y = NPC.Center.Y - (int)(Math.Sin(rad) * distance) - ice.height / 2;
                            if (!GetInstance<Config>().lowDust)
                            {
                                int numDusts = 20;
                                for (int p = 0; p < numDusts; p++)
                                {
                                    Vector2 position = (Vector2.One * new Vector2((float)ice.width / 2f, (float)ice.height) * 0.3f * 0.5f).RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + ice.Center;
                                    Vector2 velocity = position - ice.Center;
                                    int dust = Dust.NewDust(position + velocity, 0, 0, 135, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 2f;
                                }
                            }
                        }
                    }
                    shootTimer = 45;
                    aiTimer++;
                }
                if (aiTimer > 3)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 4)
            {
                aiTimer++;
                if (shockwave == 0)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath10, NPC.position);
                    shockwave = 1;              
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numProj = Main.expertMode ? MyWorld.awakenedMode ? 35 : 25 : 15;
                        float speed = Main.expertMode ? MyWorld.awakenedMode ? 7f : 5f : 3f;
                        for (int i = 0; i < numProj; i++)
                        {
                            Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X + Main.rand.Next(-1000, 1000), NPC.Center.Y - Main.rand.Next(800, 1800), 0, speed, ProjectileType<IceRain>(), projectileBaseDamage, 0f, Main.myPlayer)];
                            proj.rotation = Main.rand.NextFloat((float)Math.PI * 2);
                        }
                    }
                }
                if (aiTimer > 300)
                {
                    shockwave = 0;
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 5)
            {
                float dashSpeed = Main.expertMode ? MyWorld.awakenedMode ? 13 : 10 : 8;
                int tpDist = 700;
                if (NPC.life < NPC.lifeMax * 0.75f) tpDist -= 100;
                if (NPC.life < NPC.lifeMax * 0.5f) tpDist -= 100;
                if (NPC.life < NPC.lifeMax * 0.25f) tpDist -= 100;
                aiTimer++;
                if (shootTimer == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC other = Main.npc[i];
                        if (other.type == NPCType<PermaOrbital>() && other.ai[0] == NPC.whoAmI && other.active)
                        {
                            other.active = false;
                        }
                    }
                    int orbitalcount = Main.expertMode ? MyWorld.awakenedMode ? 11 : 8 : 5;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<PermaOrbital>(), NPC.whoAmI, NPC.whoAmI, l * distance);
                    }
                    shootTimer++;
                }
                if (aiTimer < 90)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(P.Center, NPC.Center) >= 30)
                    {
                        NPC.velocity = toTarget * 0.1f;
                    }
                }
                else if (aiTimer == 90)
                {
                    Teleport(P.Center + new Vector2(tpDist, 0));
                }
                else if (aiTimer > 90 && aiTimer < 210)
                {
                    NPC.velocity.Y = 0;
                    NPC.velocity.X = -dashSpeed;
                }
                else if (aiTimer == 210)
                {
                    Teleport(P.Center + new Vector2(-tpDist, 0));
                }
                else if (aiTimer > 210)
                {
                    NPC.velocity.Y = 0;
                    NPC.velocity.X = dashSpeed;
                }
                if (aiTimer > 330)
                {
                    aiTimer = 6;
                    shootTimer = 0;
                    state = 1;
                    CircularTP(P, 500);
                    NPC.velocity = Vector2.Zero;
                }
            }
            else if (state == 6)
            {
                FlyTo(new Vector2(P.Center.X, P.Center.Y), 0.1f, 14f);
                aiTimer++;
                if (shootTimer == 0)
                {
                    storePosX = P.Center.X + Main.rand.Next(-600, 600);
                    storePosY = P.Center.Y + Main.rand.Next(-300, 300);
                    NPC.netUpdate = true;
                }
                else if (shootTimer < 0)
                {
                    Vector2 storedPos = new Vector2(storePosX, storePosY);

                    Dust dust = Main.dust[Dust.NewDust(storedPos, 2, 2, 135, 0f, 0f, 200, default(Color), 2.5f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.3f;
                    Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                    vector.Normalize();
                    vector *= 3f;
                    dust.velocity = vector;
                    dust.position = storedPos - vector * 15;
                }
                shootTimer--;
                if (shootTimer <= -60 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                    Vector2 storedPos = new Vector2(storePosX, storePosY);
                    float Speed = 12f;
                    float rotation = (float)Math.Atan2(storedPos.Y - P.Center.Y, storedPos.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile.NewProjectile(EAU.NPCs(NPC), storedPos, projSpeed, ProjectileType<HomingIce>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 0;
                }
                if (aiTimer % 120 == 0)
                {
                    CircularTP(P, 500);
                }
                if (aiTimer > 600)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                    CircularTP(P, 650);
                }
            }
            else if (state == 7)
            {
                FlyTo(new Vector2(P.Center.X, P.Center.Y), 0.05f, 3f);

                aiTimer++;
                shootTimer--;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, NPC.Center);

                    Vector2 mouth = new Vector2(NPC.Center.X, NPC.Center.Y + 40);
                    float Speed = 14f;
                    float rotation = (float)Math.Atan2(mouth.Y - P.Center.Y, mouth.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(EAU.NPCs(NPC), mouth, projSpeed, ProjectileType<FrigidBreath>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 6;
                }
                if (aiTimer > 270)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 8)
            {
                aiTimer++;
                if (shootTimer == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    CircularTP(P, 500);
                    NPC.velocity = Vector2.Zero;
                   
                    storePosX = P.Center.X;
                    storePosY = P.Center.Y;
                    float Speed = 24f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center, projSpeed, ProjectileType<PermaDashWarn>(), 0, 0 , Main.myPlayer);
                }
                else if (shootTimer == -30)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    NPC.velocity = toTarget * 20;
                }
                if (shootTimer < -30)
                {
                    NPC.velocity *= 0.99f;
                }
                shootTimer--;
                if (shootTimer <= -100)
                {
                    shootTimer = 0;
                }
                if (aiTimer > 360)
                {
                    aiTimer = 0;
                    shootTimer = -30;
                    state = 0;

                    Teleport(P.Center + new Vector2(0, -300));
                    NPC.velocity = Vector2.Zero;
                }
            }
        }
        private void Teleport(Vector2 toPos)
        {
            SoundEngine.PlaySound(SoundID.Item30, NPC.Center); // 46 // 77 // 104
            for (int k = 0; k < 50; k++)
            {
                Dust d = Main.dust[Dust.NewDust(NPC.Center + (toPos - NPC.Center) * Main.rand.NextFloat() - new Vector2(4, 4), 16, 16, Main.rand.Next(3) == 0 ? 41 : 135)];
                d.noGravity = true;
                d.velocity *= 1.2f;
                if (d.type == 41) d.scale *= 1.8f;
                else d.scale *= 2.8f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ProjectileType<PermafrostTP>(), 0, 0f, Main.myPlayer);
                NPC.Center = toPos;
                NPC.netUpdate = true;
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ProjectileType<PermafrostTP>(), 0, 0f, Main.myPlayer);
            }
        }
        private void CircularTP(Player P, float dist)
        {
            double angle = Main.rand.NextDouble() * 2d * Math.PI;
            Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
            Teleport(P.Center + offset);
            NPC.netUpdate = true;
        }
        private void FlyTo(Vector2 location, float acceleration, float speed)
        {
            float targetX = location.X - NPC.Center.X;
            float targetY = location.Y - NPC.Center.Y;
            float targetPos = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            targetPos = speed / targetPos;
            targetX *= targetPos;
            targetY *= targetPos;
            if (NPC.velocity.X < targetX)
            {
                NPC.velocity.X = NPC.velocity.X + acceleration;
                if (NPC.velocity.X < 0f && targetX > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                }
            }
            else if (NPC.velocity.X > targetX)
            {
                NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.X > 0f && targetX < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                }
            }
            if (NPC.velocity.Y < targetY)
            {
                NPC.velocity.Y = NPC.velocity.Y + acceleration;
                if (NPC.velocity.Y < 0f && targetY > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
                }
            }
            else if (NPC.velocity.Y > targetY)
            {
                NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (NPC.velocity.Y > 0f && targetY < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}