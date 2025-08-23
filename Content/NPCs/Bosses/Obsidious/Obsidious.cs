using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Obsidious;
using ElementsAwoken.Content.Projectiles.NPCProj.Obsidious;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Obsidious
{
    [AutoloadBossHead]
    public class Obsidious : ModNPC
    {
        private float spinAI = 0f;
        private float shootCooldown = 0; // for a multiple burst

        private int projectileBaseDamage = 50;
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
            writer.Write(shootCooldown);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            spinAI = reader.ReadSingle();
            shootCooldown = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            NPC.width = 222;
            NPC.height = 254;
            NPC.aiStyle = -1;
            NPC.lifeMax = 75000;
            NPC.damage = 75;
            NPC.defense = 55;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 20, 0, 0);
            Music = MusicID.Plantera;
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
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // 0.n == уменшает изоброжения
                Scale = 0.55f, // Мини иконка в бестиарии 
                PortraitScale = 0.55f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y -= 50f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.ObsidiousBoss"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 90;
            NPC.lifeMax = 100000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 150000;
                NPC.damage = 110;
                NPC.defense = 65;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (phase != 4)
            {
                if (phase == 0)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                if (phase == 1)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                if (phase == 2)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                if (phase == 3)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 6)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule _DropNormal = new LeadingConditionRule(new EAIDRC.DropNormal());
            LeadingConditionRule _DropExpert = new LeadingConditionRule(new EAIDRC.DropAwakened());

            _DropNormal.OnSuccess(ItemDropRule.OneFromOptions(1, [.. EAList.ObsiLoot]));
            _DropExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<ObsidiousBag>(), 1));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousTrophy>(), 10));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CrystallineCluster>(), 10));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousWings>(), 10));
            npcLoot.Add(_DropNormal);
            npcLoot.Add(_DropExpert);

            var RobeDrop = new LeadingConditionRule(new EAIDRC.DropRobe());
            RobeDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousMask>()));
            RobeDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousRobes>()));
            RobeDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousPants>()));
            npcLoot.Add(RobeDrop);
        }
        public override void OnKill()
        {
            Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious10, new Color(188, 58, 49));
            MyWorld.downedObsidious = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
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
                        if (phase == 4)
                        {
                            Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious5, new Color(188, 58, 49));
                        }
                        else
                        {
                            Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious6, new Color(188, 58, 49));
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
            Lighting.AddLight(NPC.Center, 0.5f, 0.5f, 0.5f);
            Player P = Main.player[NPC.target];
            var e = ModContent.GetInstance<EALocalization>();
            Despawn(P);

            if (NPC.localAI[1] == 0)
            {
                NPC.localAI[1]++;
                if (!ModContent.GetInstance<Config>().lowDust)
                {
                    for (int k = 0; k < 50; k++)
                    {
                        int dust = Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, 6, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                        int dust1 = Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, 75, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                        Main.dust[dust1].noGravity = true;
                        Main.dust[dust1].scale = 1f + Main.rand.Next(10) * 0.1f;
                        int dust2 = Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, 135, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                        Main.dust[dust2].noGravity = true;
                        Main.dust[dust2].scale = 1f + Main.rand.Next(10) * 0.1f;
                        int dust3 = Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, EAU.PinkFlame, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                        Main.dust[dust3].noGravity = true;
                        Main.dust[dust3].scale = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.75f && NPC.localAI[1] == 0)
            {
                Main.NewText(e.Obsidious7, new Color(188, 58, 49));
                NPC.localAI[1]++;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.25f && NPC.localAI[1] == 1)
            {
                Main.NewText(e.Obsidious8, new Color(188, 58, 49));
                phase = 4;
                NPC.localAI[1]++;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.1f && NPC.localAI[1] == 2)
            {
                Main.NewText(e.Obsidious9, new Color(188, 58, 49));
                NPC.localAI[1]++;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.50f && phase != 4)
            {
                if (!ModContent.GetInstance<Config>().lowDust)
                {
                    int dustType = 6;
                    switch ((int)phase)
                    {
                        case 0:
                            dustType = 6;
                            break;
                        case 1:
                            dustType = 75;
                            break;
                        case 2:
                            dustType = 135;
                            break;
                        case 3:
                            dustType = EAU.PinkFlame;
                            break;
                        default: break;
                    }
                    Vector2 leftEye = new Vector2(NPC.Center.X - 14, NPC.Center.Y - 96);
                    Vector2 rightEye = new Vector2(NPC.Center.X + 14, NPC.Center.Y - 96);
                    int dust = Dust.NewDust(leftEye, 6, 6, dustType, NPC.velocity.X * 0.5f, 12f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                    dust = Dust.NewDust(rightEye, 6, 6, dustType, NPC.velocity.X * 0.5f, 12f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                }
            } 
            shootTimer--;
            shootCooldown--;
            if (shootCooldown <= 0)
            {
                shootCooldown = 80;
            }
            aiTimer++;
            //fire
            if (phase == 0)
            {
                if (aiTimer <= 120)
                {
                    NPC.velocity = Vector2.Zero;
                    if (aiTimer == 100)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y + 60, 0, 0, ModContent.ProjectileType<ObsidiousFirePortal>(), projectileBaseDamage, 1, Main.myPlayer);
                    }
                }
                else
                {
                    Move(P, 4f);
                    if (NPC.life <= NPC.lifeMax * 0.5f)
                    {
                        if (shootTimer == 150 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X, P.Center.Y, 0, 0, ModContent.ProjectileType<ObsidiousTargetCrystalCenter>(), 0, 0, Main.myPlayer, 0f, P.whoAmI); // lonng name
                        }
                        if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int k = 0; k < Main.maxProjectiles; k++)
                            {
                                Projectile other = Main.projectile[k];
                                if (other.type == ModContent.ProjectileType<ObsidiousTargetCrystalCenter>() && other.active)
                                {
                                    int numberProjectiles = 8 + Main.rand.Next(0,4);
                                    SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                                    float rotation = (float)Math.Atan2(NPC.Center.Y - other.Center.Y, NPC.Center.X - other.Center.X);
                                    for (int i = 0; i < numberProjectiles; i++)
                                    {
                                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * 12f) * -1), (float)((Math.Sin(rotation) * 12f) * -1)).RotatedByRandom(MathHelper.ToRadians(10));
                                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ObsidiousFireBeam>(), projectileBaseDamage + 30, 0f, Main.myPlayer, 0f, 0f);
                                    }
                                }
                            }
                            shootTimer = 300;
                        }
                    }
                }
                if (aiTimer >= 900)
                {
                    phase++;
                    aiTimer = 0;
                }
            }
            //earth
            if (phase == 1)
            {
                Move(P, 4f);
                if (aiTimer == 10)
                {
                    //Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<ObsidiousRockEffect>(), projectileBaseDamage, 1, Main.myPlayer);
                }
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item69, NPC.position);
                    float speed = 8f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<ObsidiousRockLarge>(), projectileBaseDamage + 20, 0f, Main.myPlayer);
                    shootTimer = Main.rand.Next(20, 60);
                }
                int rand = NPC.life <= NPC.lifeMax * 0.5f ? 12 : 14;
                if (Main.rand.Next(rand) == 0)
                {
                    int damage = 30;
                    float posX = NPC.Center.X + Main.rand.Next(5000) - 3000;
                    float posY = NPC.Center.Y + 1000;
                    Projectile.NewProjectile(EAU.NPCs(NPC), posX, posY, 0f, -10f, ModContent.ProjectileType<ObsidiousRockNoCollide>(), damage, 0f, Main.myPlayer);
                }
                if (aiTimer >= 900)
                {
                    phase++;
                    aiTimer = 0;
                }
            }
            //ice
            if (phase == 2)
            {
                Move(P, 4f);
                if (shootTimer <= 0 && shootCooldown <= 24 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float speed = 6f;
                    int type = ModContent.ProjectileType<ObsidiousIceLaser>();
                    SoundEngine.PlaySound(SoundID.Item33, NPC.position);

                    float rotation = (float)Math.Atan2(NPC.Center.Y - 92 - P.Center.Y, NPC.Center.X - 12 - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X - 12, NPC.Center.Y - 92, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, projectileBaseDamage, 0f, Main.myPlayer);

                    float rotation2 = (float)Math.Atan2(NPC.Center.Y - 92 - P.Center.Y, NPC.Center.X + 12 - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X + 12, NPC.Center.Y - 92, (float)((Math.Cos(rotation2) * speed) * -1), (float)((Math.Sin(rotation2) * speed) * -1), type, projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 6;
                }
                if (aiTimer >= 900)
                {
                    phase++;
                    aiTimer = 0;
                }
            }
            //shadowflame
            if (phase == 3)
            {
                Move(P, 4f);
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item103, NPC.position);
                    float speed = 5f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y - 80, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<ObsidiousHomingBall>(), projectileBaseDamage + 20, 0f, Main.myPlayer);
                    shootTimer = Main.rand.Next(8, 40);
                    NPC.netUpdate = true;
                }
                if (aiTimer >= 900)
                {
                    phase = 0;
                    aiTimer = 0;
                }
            }
            // all/beams
            if (phase == 4)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0f;
                if (aiTimer <= 300)
                {
                    NPC.immortal = true;
                    NPC.dontTakeDamage = true;
                    
                    Vector2 offset = new Vector2(400, 0);
                    spinAI += 0.01f;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numProj = 4;
                        for (int i = 0; i < numProj; i++)
                        {
                            int damage = aiTimer <= 60 ? 0 : projectileBaseDamage;
                            float projOffset = 360 / numProj;
                            Vector2 shootTarget1 = NPC.Center + offset.RotatedBy(spinAI + (projOffset * i) * (Math.PI * 2 / 8));
                            float rotation = (float)Math.Atan2(NPC.Center.Y - shootTarget1.Y, NPC.Center.X - shootTarget1.X);
                            int proj = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * 10f) * -1), (float)((Math.Sin(rotation) * 10f) * -1), ModContent.ProjectileType<ObsidiousBeam>(), damage, 0f, Main.myPlayer, 0, i);
                            Main.projectile[proj].timeLeft = (int)aiTimer;
                        }
                        shootTimer = 4;
                    }
                }
                else
                {
                    NPC.immortal = false;
                    NPC.dontTakeDamage = false;
                }
                if (aiTimer >= (NPC.life <= NPC.lifeMax * 0.1f ? 400 : 600))
                {
                    aiTimer = 0;
                }
            }
            if (NPC.localAI[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.TargetClosest(true);
                NPC.localAI[0]++;
                int num = NPC.NewNPC(EAU.NPCs(NPC), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<ObsidiousHand>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[num].ai[0] = -1f;
                Main.npc[num].ai[1] = (float)NPC.whoAmI;
                Main.npc[num].target = NPC.target;
                Main.npc[num].netUpdate = true;
                num = NPC.NewNPC(EAU.NPCs(NPC), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<ObsidiousHand>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[num].ai[0] = 1f;
                Main.npc[num].ai[1] = (float)NPC.whoAmI;
                Main.npc[num].ai[3] = 150f; // ai timer offset so they arent exactly the same
                Main.npc[num].target = NPC.target;
                Main.npc[num].netUpdate = true;
                NPC.netUpdate = true;
            }
        }
        private void Move(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, NPC.Center) >= 30)
            {
                NPC.velocity = toTarget * moveSpeed;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (aiTimer <= 300 && phase == 4)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                Texture2D tex = ModContent.Request<Texture2D>("Terraria/Images/Misc/Perlin").Value;

                var center = NPC.Center - Main.screenPosition;
                float intensity = 0f;
                DrawData drawData = new DrawData(tex, center - new Vector2(0, 10), new Rectangle(0, 0, 500, 500), Color.White, NPC.rotation, new Vector2(250f, 250f), NPC.scale * (1f + intensity * 0.05f), SpriteEffects.None, 0);
                GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + intensity * 0.5f));
                GameShaders.Misc["ForceField"].Apply(drawData);
                drawData.Draw(Main.spriteBatch);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return;
            }
            Terraria.Graphics.Effects.Filters.Scene["Nebula"].GetShader().UseIntensity(0f).UseProgress(0f); // why is this here
        }
    }
}
