using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.RadiantMaster;
using ElementsAwoken.Content.Items.ItemSets.Radia;
using ElementsAwoken.Content.Projectiles.Explosions;
using ElementsAwoken.Content.Projectiles.NPCProj.RadiantMaster;
using ElementsAwoken.EASystem.Loot;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.RadiantRain.Enemies
{
    
    [AutoloadBossHead]

    public class RadiantMaster : ModNPC
    {
        private float despawnTimer = 0;
        private float aiTimer2 = 0;
        private float deathTimerAI = 0;
        private int projectileBaseDamage = 100;
        private float aiTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float aiState
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float deathTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(despawnTimer);
            writer.Write(deathTimer);
            writer.Write(deathTimerAI);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            despawnTimer = reader.ReadSingle();
            deathTimer = reader.ReadSingle();
            deathTimerAI = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 96;
            NPC.aiStyle = -1;
            NPC.lifeMax = 175000;
            NPC.damage = 140;
            NPC.defense = 40;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.knockBackResist = 0f;
            SpawnModBiomes = new int[1] { GetInstance<RadiantRainBiome>().Type };
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.RadiantMaster")
            });
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(175000, balance, bossAdjustment, 450000);
            NPC.damage = (int)EAU.BalanceDamage(140, balance, bossAdjustment, 300);
            NPC.defense = EAU.BalanceDefense(40, 65);
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemType<Items.Consumable.Potions.EpicHealingPotion>();
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter > 4)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 5)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<Majesty>(), ItemType<RadiantBomb>(), ItemType<RadiantBow>(), ItemType<RadiantSword>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<Radia>(), minimumDropped: 4, maximumDropped: 21));
            _AwakenedMode.OnSuccess(ItemDropRule.Common(ItemType<RadiantCrown>()));
            npcLoot.Add(_AwakenedMode);
        }
        public override void OnKill()
        {
            MyWorld.downedRadiantMaster = true;
            for (int l = 0; l < 6; l++)
            {
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center + Main.rand.NextVector2Square(-NPC.width, NPC.width), new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(3)), ProjectileType<RadiantFireball>(), 0, 0, Main.myPlayer, 1);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center + Main.rand.NextVector2Square(-NPC.width, NPC.width), Vector2.Zero, ProjectileType<RadiantMasterDeathExplosion>(), 0, 0, Main.myPlayer);
                SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool CheckDead()
        {
            if (deathTimer < 300)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<RadiantMasterDeath>());
                deathTimer = 1;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.life <= 0) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            NPC.TargetClosest(false);
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1f, 0.2f, 0.5f);
            if (deathTimer > 0)
            {
                NPC.rotation = 0;
                NPC.velocity = Vector2.Zero;
                deathTimer++;
                deathTimerAI--;
                if (deathTimerAI <= 0)
               {
                    Teleport(P.Center + Main.rand.NextVector2Square(-300, 300));
                    if (deathTimer < 120) deathTimerAI = MathHelper.Lerp(60, 5, deathTimer / 300f);
                    else deathTimerAI = MathHelper.Lerp(30, 5, deathTimer / 300f);
                }
                if (deathTimer > 300)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
            else if (!P.active || P.dead)
            {
                despawnTimer++;
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (despawnTimer >= 300)  NPC.active = false;
            }
            else
            {
                despawnTimer = 0;
                if (Main.rainTime < 600)
                {
                    Main.rainTime = 600;
                }
                if (aiState == 0)
                {
                    FlyTo(P.Center, 0.1f, 13f);
                    NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);

                    aiTimer++;
                    shootTimer--;
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, P.Center) > 150)
                    {
                        float Speed = 14f;
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(10));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, projSpeed, ProjectileType<RadiantFireball>(), 0, 0f, Main.myPlayer);
                        shootTimer = 30;
                    }
                    if (aiTimer > 600)
                    {
                        aiState++;
                        aiTimer = 0;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 1)
                {
                    NPC.rotation = NPC.velocity.X * 0.02f;
                    int dashDelay = 15;
                    int dashDuration = 120;
                    int totalDur = dashDelay + dashDuration;
                    aiTimer++;
                    if (aiTimer == 1)
                    {
                        Teleport(P.Center - new Vector2(-500, 0));
                        NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
                        NPC.velocity = Vector2.Zero;
                    }
                    else if (aiTimer > dashDelay && aiTimer < totalDur)
                    {
                        NPC.velocity.X = -12;
                    }
                    else if (aiTimer == totalDur)
                    {
                        Teleport(P.Center - new Vector2(500, 0));
                        NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
                        NPC.velocity = Vector2.Zero;
                    }
                    else if (aiTimer > totalDur + dashDelay && aiTimer < totalDur * 2)
                    {
                        NPC.velocity.X = 12;
                    }
                    else if (aiTimer == totalDur * 2)
                    {
                        Teleport(P.Center - new Vector2(0, 500));
                        NPC.velocity = Vector2.Zero;
                    }
                    else if (aiTimer > totalDur * 2)
                    {
                        NPC.velocity.Y = 12;
                    }
                    if ((aiTimer > totalDur * 2 && NPC.Center.Y > P.Center.Y) || aiTimer > totalDur * 3)
                    {
                        aiState++;
                        aiTimer = 0;
                        shootTimer = 120;
                        aiTimer2 = 300;
                        NPC.rotation = 0;
                    }
                }
                else if (aiState == 2)
                {
                    NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
                    aiTimer++;
                    aiTimer2--;
                    if (aiTimer2 > 0) FlyTo(P.Center - new Vector2(NPC.direction * 400, 300), 0.2f, 13f);
                    shootTimer--;
                    if (aiTimer2 == 0)
                    {
                        NPC.velocity.Y = 0;
                        NPC.velocity.X = NPC.direction * 20;
                    }
                    else if (aiTimer2 < -20)
                    {
                        aiTimer2 = 300;
                    }

                    if (shootTimer < 120)
                    {

                        Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Firework_Pink, 0f, 0f, 200, default(Color), 0.5f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.3f;
                        Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                        vector.Normalize();
                        vector *= 12f;
                        dust.velocity = vector;
                        dust.position = NPC.Center - vector * 15;
                    }
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float Speed = 5f;
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(10));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, projSpeed, ProjectileType<RadiantWhirlwind>(), projectileBaseDamage * 2, 0f, Main.myPlayer);
                        shootTimer = 360;
                    }
                    if (aiTimer > 1100)
                    {
                        aiState++;
                        aiTimer = 0;
                        shootTimer = 0;
                    }
                }
                else if (aiState == 3)
                {
                    NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
                    aiTimer++;
                    shootTimer--;
                    FlyTo(P.Center, 0.1f, 3f);
                    if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int orbitalCount = Main.expertMode ? MyWorld.awakenedMode ? 12 : 8 : 6;
                        for (int l = 0; l < orbitalCount; l++)
                        {
                            int distance = 360 / orbitalCount;
                            Projectile orbital = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<RadiantMasterStar>(), projectileBaseDamage, 0f, Main.myPlayer, l * distance, NPC.whoAmI)];
                            RadiantMasterStar radStar = (RadiantMasterStar)orbital.ModProjectile;
                            radStar.aiTimer = l * -10;
                        }
                        shootTimer = 240;
                    }
                    if (aiTimer > 600)
                    {
                        aiState = 0;
                        aiTimer = 0;
                        shootTimer = 0;
                    }
                }
            }
        }
        private void Teleport(Vector2 toPos)
        {
            SoundEngine.PlaySound(SoundID.Item46, NPC.Center); // 46 // 77 // 104
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int numProj = 5;
                Vector2 distance = (toPos - NPC.Center) / numProj;
                for (int k = 0; k < numProj; k++)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + distance - new Vector2(0, 23), Vector2.Zero, ProjectileType<RadiantTeleport>(), 0, 0f, Main.myPlayer)];
                    proj.spriteDirection = -NPC.spriteDirection;
                    distance += (toPos - NPC.Center) / numProj;
                }
                NPC.Center = toPos;
                NPC.netUpdate = true;
            }
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
    }
}