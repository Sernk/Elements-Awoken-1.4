using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Projectiles.NPCProj.Ancients;
using ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients
{
    [AutoloadBossHead]
    public class AncientAmalgam : ModNPC
    {
        public float originX = 0;
        public float originY = 0;

        public bool canDie = false;
        public float deathTimer = 0;
        bool spawnedHands = false;

        public float[] shootTimer = new float[4];
        public float numRadialProj = 30;
        public int projectileBaseDamage = 200;
        public int invertAttack = 1;
        public Vector2 playerOrigin = new Vector2();

        public bool[] hasInverted = new bool[Main.maxProjectiles];

        public int previousAttackNum = 0;

        public Vector2 toPlayerDash = new Vector2();
        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 144;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1200000;
            NPC.damage = 200;
            NPC.defense = 90;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.scale *= 1.3f;
            NPC.alpha = 255; // starts transparent
            NPC.value = Item.buyPrice(1, 0, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.LunarBoss;
            NPC.buffImmune[BuffType<IceBound>()] = true;
            NPC.buffImmune[BuffType<ExtinctionCurse>()] = true;
            NPC.buffImmune[BuffType<HandsOfDespair>()] = true;
            NPC.buffImmune[BuffType<EndlessTears>()] = true;
            NPC.buffImmune[BuffType<AncientDecay>()] = true;
            NPC.buffImmune[BuffType<SoulInferno>()] = true;
            //npc.buffImmune[ModContent.BuffType<DragonFire>()] = true;
            NPC.buffImmune[BuffType<Discord>()] = true;
            for (int num2 = 0; num2 < 206; num2++)
            {
                NPC.buffImmune[num2] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // 0.n == уменшает изоброжения
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/AncientAmalgamBestiary",
                Scale = 0.8f, // Мини иконка в бестиарии 
                PortraitScale = 0.8f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0;
            value.Position.Y -= 0;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.AncientAmalgam"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 1500000;
            NPC.damage = 250;
            NPC.defense = 120;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 1750000;
                NPC.damage = 300;
                NPC.defense = 130;
            }
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
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.ai[0] < 180 || NPC.alpha > 100) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var _DropNormal = new LeadingConditionRule(new EAIDRC.DropNormal());
            var _DropExpert = new LeadingConditionRule(new EAIDRC.DropAwakened());

            _DropExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemType<AncientsBag>(), 1));
            _DropNormal.OnSuccess(ItemDropRule.Common(ItemType<CrystalAmalgamate>(), minimumDropped:1, maximumDropped:1));
            _DropNormal.OnSuccess(ItemDropRule.Common(ItemType<AncientShard>(), minimumDropped:5, maximumDropped:8));
            npcLoot.Add(_DropExpert);

            _DropNormal.OnSuccess(ItemDropRule.OneFromOptions(1, [.. EAList.AncLot]));
            npcLoot.Add(_DropNormal);
            //npcLoot.Add(ItemDropRule.Common(ItemType<AncientsTrophy>(), 10));
            //npcLoot.Add(ItemDropRule.Common(ItemType<ElderSignet>(), 10)); // 2 по цене одного
            //npcLoot.Add(ItemDropRule.Common(ItemType<AncientsMask>(), 10));
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemType<EpicHealingPotion>();
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);

            // despawn if no players
            if (!P.active || P.dead)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    NPC.localAI[0]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.localAI[0] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.localAI[0] = 0;
            }
            if (NPC.ai[0] < 180)
            {
                if (NPC.ai[0] == 0)
                {
                    originX = P.Center.X;
                    originY = P.Center.Y;

                }
                if (NPC.ai[0] < 60)
                {
                    MoonlordDeathDrama.RequestLight(1f, NPC.Center);
                    NPC.alpha = 255;
                    if (NPC.ai[0] == 59)
                    {
                        SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/NPC/AncientMergeFall"), NPC.position);
                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            if (Main.projectile[i].type == ProjectileType<IzarisShard>() || Main.projectile[i].type == ProjectileType<KirveinShard>() || Main.projectile[i].type == ProjectileType<KrecheusShard>() || Main.projectile[i].type == ProjectileType<XernonShard>() || Main.projectile[i].type == ProjectileType<Content.Projectiles.NPCProj.Ancients.Gores.ShardBase>()) 
                            {
                                Main.projectile[i].Kill();
                            }
                        }
                    }
                }
                else
                {
                    if (!spawnedHands && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.TargetClosest(true);
                        spawnedHands = true;

                        int num = NPC.NewNPC(EAU.NPCs(NPC), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, NPCType<AncientAmalgamFist>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num].ai[0] = -1f;
                        Main.npc[num].ai[1] = (float)NPC.whoAmI;
                        Main.npc[num].target = NPC.target;
                        Main.npc[num].netUpdate = true;
                        num = NPC.NewNPC(EAU.NPCs(NPC), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, NPCType<AncientAmalgamFist>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num].ai[0] = 1f;
                        Main.npc[num].ai[1] = (float)NPC.whoAmI;
                        Main.npc[num].ai[3] = 150f; // ai timer offset so they arent exactly the same
                        Main.npc[num].target = NPC.target;
                        Main.npc[num].netUpdate = true;
                    }
                    NPC.alpha = 0;
                    Vector2 target = new Vector2(originX, originY - 250);
                    Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(target, NPC.Center) > 5)
                    {
                        NPC.velocity = toTarget * 6;
                    }
                    else
                    {
                        NPC.velocity *= 0f;
                    }
                }
                NPC.ai[0]++;
            }
            else
            {
                if (NPC.ai[1] == 0)
                {
                    int attackNum = -1;
                    while (attackNum == previousAttackNum || attackNum == -1)
                    {
                        if (NPC.life > NPC.lifeMax * 0.85f)
                        {
                            attackNum = Main.rand.Next(0, 7); // 0 - 6
                        }
                        else if (NPC.life <= NPC.lifeMax * 0.85f && NPC.life > NPC.lifeMax * 0.5f)
                        {
                            attackNum = Main.rand.Next(1, 7); // 1 - 6
                        }
                        else if (NPC.life <= NPC.lifeMax * 0.5f && NPC.life > NPC.lifeMax * 0.25f)
                        {
                            attackNum = Main.rand.Next(2, 10);// 2 - 9
                        }
                        else if (NPC.life <= NPC.lifeMax * 0.25f)
                        {
                            attackNum = Main.rand.Next(3, 12); // 3 - 11
                            while (attackNum == 4 || attackNum == 9)
                            {
                                attackNum = Main.rand.Next(3, 12); // 3 - 11
                            }
                            if (attackNum == 7)
                            {
                                attackNum = 11;
                            }
                        }
                        else
                        {
                            attackNum = 0;
                            ElementsAwoken.DebugModeText("Error selecting attack"); // this shouldnt occur anymore
                            break;
                        }
                    }
                    if (NPC.localAI[1] == 0)
                    {
                        NPC.localAI[1]++;
                        attackNum = 0;
                    }
                    NPC.ai[2] = attackNum;
                    previousAttackNum = attackNum;

                    invertAttack = Main.rand.Next(2) == 0 ? -1 : 1;
                    playerOrigin = P.Center;
                }
                if (NPC.alpha > 0 && NPC.ai[2] != 3 && NPC.ai[2] != 5 && NPC.ai[2] != 8)
                {
                    NPC.alpha -= 5;
                }
                NPC.ai[1]++;
                if (NPC.ai[2] == 0)
                {
                    float speed = 0.25f;
                    float playerX = P.Center.X;
                    float playerY = P.Center.Y - 250f;
                    Move(P, speed, new Vector2(playerX, playerY));

                    if (NPC.ai[1] > 450)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 1)
                {
                    NPC.velocity = Vector2.Zero;

                    shootTimer[0]--;
                    shootTimer[1]--;
                    if (shootTimer[1] <= 0)
                    {
                        shootTimer[1] = 80;
                        numRadialProj = Main.rand.Next(20, 30);
                    }
                    if (shootTimer[0] <= 0 && shootTimer[1] <= 42)
                    {
                        int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 0.75f) : projectileBaseDamage;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numRadialProj; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numRadialProj - 1))) * 2.5f;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<CrystallineKunaiHostileNG>(), projDamage, 2f, Main.myPlayer);
                        }
                        shootTimer[0] = 14;
                    }
                    if (NPC.ai[1] > 400)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 2)
                {
                    NPC.velocity = Vector2.Zero;

                    // hands spin around
                    NPC.ai[3]++;
                    if (NPC.ai[3] >= 60)
                    {
                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            Projectile oProj = Main.projectile[i];
                            if (Vector2.Distance(oProj.Center, NPC.Center) <= 300 && !hasInverted[i] && oProj.active && !oProj.minion && !ProjectileID.Sets.MinionSacrificable[oProj.type])
                            {
                                oProj.velocity.X *= -1;
                                oProj.velocity.Y *= -1;
                                oProj.friendly = false;
                                oProj.hostile = true;
                                hasInverted[i] = true;
                            }
                            if (!oProj.active)
                            {
                                hasInverted[oProj.whoAmI] = false;
                            }
                        }
                    }
                    if (NPC.ai[1] > 300)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 3)
                {
                    if (NPC.alpha == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie105, NPC.Center);
                    }
                    if (NPC.alpha < 255)
                    {
                        NPC.alpha += 5;
                    }
                    else
                    {
                        NPC.Center = P.Center - new Vector2(0, 1000);
                    }
                    shootTimer[0]--;
                    if (shootTimer[0] <= 0)
                    {
                        float speed = 6f;
                        for (int i = 1; i < 5; i++) // start at 1 so they dont overlap
                        {
                            Projectile.NewProjectile(EAU.NPCs(NPC), playerOrigin.X - 1000 * invertAttack, playerOrigin.Y - i * 300, speed * invertAttack, 0f, ProjectileType<AncientProjection>(), projectileBaseDamage, 0f, 0, Main.rand.Next(4));
                            Projectile.NewProjectile(EAU.NPCs(NPC), playerOrigin.X - 1000 * invertAttack, playerOrigin.Y + i * 300, speed * invertAttack, 0f, ProjectileType<AncientProjection>(), projectileBaseDamage, 0f, 0, Main.rand.Next(4));
                        }
                        Projectile.NewProjectile(EAU.NPCs(NPC), playerOrigin.X - 1000 * invertAttack, playerOrigin.Y, speed * invertAttack, 0f, ProjectileType<AncientProjection>(), projectileBaseDamage, 0f, 0, Main.rand.Next(4));

                        shootTimer[0] = 45;
                    }
                    if (NPC.ai[1] > 300)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 4)
                {
                    float speed = 0.25f;
                    float playerX = P.Center.X;
                    float playerY = P.Center.Y - 450f;
                    Move(P, speed, new Vector2(playerX, playerY));

                    if (Main.rand.Next(5) == 0)
                    {
                        int distance = 500;
                        Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X + Main.rand.NextFloat(-distance, distance), P.Center.Y + Main.rand.NextFloat(-distance, distance), 0f, 0f, ProjectileType<CrystalCluster>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X, P.Center.Y, 0f, 0f, ProjectileType<CrystalCluster>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    if (NPC.ai[1] > 450)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 5)
                {
                    if (NPC.ai[3] == 0)
                    {
                        int dist = 500;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Dust dust = Main.dust[Dust.NewDust(P.Center + offset, 0, 0, 6, 0, 0, 100)];
                        NPC.Center = P.Center + offset;
                        toPlayerDash = P.Center - NPC.Center;

                        for (int i = 0; i < Main.npc.Length; i++)
                        {
                            NPC fist = Main.npc[i];
                            if (fist.type == NPCType<AncientAmalgamFist>() && fist.ai[1] == NPC.whoAmI && fist.active)
                            {
                                int fistPos = fist.ai[0] == 1 ? 90 : -120;
                                fist.position.X = NPC.Center.X + fistPos - fist.width / 2;
                                fist.position.Y = NPC.Center.Y + 140 - fist.height / 2;
                            }
                        }
                    }

                    NPC.ai[3]++;
                    if (NPC.ai[3] >= 75)
                    {
                        if (NPC.alpha < 255)
                        {
                            NPC.alpha += 15;
                        }
                        if (NPC.alpha >= 255)
                        {
                            NPC.ai[3] = 0;
                        }
                    }
                    else
                    {
                        if (NPC.alpha > 0)
                        {
                            NPC.alpha -= 15;
                        }
                        toPlayerDash.Normalize();
                        NPC.velocity = toPlayerDash * 12f;
                    }
                    if (NPC.ai[1] > 300)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 6)
                {
                    if (NPC.ai[3] == 0)
                    {
                        Vector2 target = P.Center + new Vector2(400 * invertAttack, -300);
                        Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                        toTarget.Normalize();
                        if (Vector2.Distance(target, NPC.Center) > 20)
                        {
                            NPC.velocity = toTarget * 16;
                        }
                        else
                        {
                            NPC.ai[3]++;
                        }
                    }
                    if (NPC.ai[3] != 0)
                    {
                        NPC.ai[3]++;
                        shootTimer[0]--;

                        NPC.velocity.X = -8f * invertAttack;
                        NPC.velocity.Y = 0f;

                        if (shootTimer[0] <= 0)
                        {
                            Projectile kunai = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 8f, ProjectileType<CrystallineKunaiHostileExplosive>(), projectileBaseDamage, 2f, 0, 1)];
                            kunai.timeLeft = 60;
                            shootTimer[0] = 15;
                        }
                    }
                    if (NPC.ai[3] >= 90)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                // kunai galaxy
                else if (NPC.ai[2] == 7)
                {
                    NPC.velocity *= 0f;
                    if (NPC.ai[3] == 0)
                    {
                        Vector2 target = P.Center;
                        Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                        toTarget.Normalize();
                        if (Vector2.Distance(target, NPC.Center) > 300)
                        {
                            NPC.velocity = toTarget * 13;
                        }
                        else
                        {
                            NPC.ai[3]++;
                        }
                    }
                    // create projections
                    else if (NPC.ai[3] == 1)
                    {
                        int innerCount = 15;
                        for (int l = 0; l < innerCount; l++)
                        {
                            float distance = 360 / innerCount;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<AncientProjectionSwirl>(), projectileBaseDamage, 5f, 0, l * distance, NPC.whoAmI);
                        }
                        int outerCount = 20;
                        for (int l = 0; l < outerCount; l++)
                        {
                            float distance = 360 / outerCount;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<AncientProjectionSwirl2>(), projectileBaseDamage, 5f, 0, l * distance, NPC.whoAmI);
                        }
                        NPC.ai[3]++;
                    }
                    if (NPC.ai[3] == 2)
                    {
                        shootTimer[0]--;
                        Vector2 offset = new Vector2(400, 0);
                        float rotateSpeed = MathHelper.ToRadians(0.88f);
                        shootTimer[1] += rotateSpeed;

                        if (shootTimer[0] <= 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                            float numProj = 5f;
                            float projOffset = MathHelper.ToRadians(360f) / numProj;
                            for (int i = 0; i < numProj; i++)
                            {
                                Vector2 shootTarget1 = NPC.Center + offset.RotatedBy(shootTimer[1] + (projOffset * i));
                                float rotation = (float)Math.Atan2(NPC.Center.Y - shootTarget1.Y, NPC.Center.X - shootTarget1.X);
                                for (int k = 0; k < 2; k++)
                                {
                                    float Speed = k == 0 ? 7 : 4;
                                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ProjectileType<CrystallineKunaiHostileNG>(), projectileBaseDamage, 0f, 0);
                                }
                            }
                            shootTimer[0] = 12;
                        }
                    }
                    if (NPC.ai[1] > 450)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();

                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            Projectile other = Main.projectile[i];
                            if ((other.type == ProjectileType<AncientProjectionSwirl>() || other.type == ProjectileType<AncientProjectionSwirl2>()) && other.ai[1] == NPC.whoAmI && other.active)
                            {
                                other.Kill();
                            }
                        }
                    }                  
                }
                else if (NPC.ai[2] == 8)
                {
                    NPC.velocity *= 0f;

                    NPC.ai[3]++;
                    int intervalTime = 45;
                    int alphaChangeRate = 5;
                    int xDist = 300;
                    if (NPC.life <= NPC.lifeMax / 4)
                    {
                        intervalTime = 30;
                        alphaChangeRate = 15;
                        xDist = 350;
                    }
                    if (NPC.life <= NPC.lifeMax / 10)
                    {
                        intervalTime = 15;
                        alphaChangeRate = 20;
                        xDist = 450;
                    }
                    if (NPC.ai[3] >= intervalTime)
                    {
                        if (NPC.alpha < 255)
                        {
                            NPC.alpha += alphaChangeRate;
                        }
                        else
                        {
                            NPC.Center = new Vector2(P.Center.X - (Main.rand.Next(2) == 0 ? -xDist : xDist), P.Center.Y + Main.rand.Next(-400, 400));
                            for (int i = 0; i < Main.npc.Length; i++)
                            {
                                NPC fist = Main.npc[i];
                                if (fist.type == NPCType<AncientAmalgamFist>() && fist.ai[1] == NPC.whoAmI && fist.active)
                                {
                                    int fistPos = fist.ai[0] == 1 ? 90 : -120;
                                    fist.position.X = NPC.Center.X + fistPos - fist.width / 2;
                                    fist.position.Y = NPC.Center.Y + 140 - fist.height / 2;
                                }
                            }
                            NPC.ai[3] = 0;
                        }
                    }
                    else
                    {
                        if (NPC.alpha > 0)
                        {
                            NPC.alpha -= alphaChangeRate;
                        }
                    }
                    if (NPC.ai[3] == (int)(intervalTime / 2))
                    {
                        float projSpeed = 12f;
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), ProjectileType<CrystallineKunaiHostileNG>(), projectileBaseDamage, 5f, Main.myPlayer);
                    }
                    if (NPC.ai[1] > 600)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 9)
                {
                    shootTimer[0]--;
                    if (NPC.life <= NPC.lifeMax * 0.35f)
                    {
                        if (Main.rand.Next(50) == 0)
                        {
                            Projectile kunai = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X + Main.rand.Next(-1000, 1000), P.Center.Y - 1500, Main.rand.NextFloat(-2, 2), 9f, ProjectileType<CrystallineKunaiHostileExplosive>(), projectileBaseDamage, 5f, Main.myPlayer)];
                            kunai.timeLeft = 180;
                        }
                    }
                    if (shootTimer[0] <= 0)
                    {
                        float projSpeed = 12f;
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        Projectile kunai = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), ProjectileType<CrystallineKunaiHostileExplosive>(), projectileBaseDamage, 5f, Main.myPlayer)];
                        kunai.timeLeft = 60;
                        shootTimer[0] = 60;
                    }

                    float speed = 0.15f;
                    float playerX = P.Center.X;
                    float playerY = P.Center.Y;
                    Move(P, speed, new Vector2(playerX, playerY));

                    if (NPC.ai[1] > 600)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                else if (NPC.ai[2] == 10)
                {
                    float speed = 0.15f;
                    float playerX = P.Center.X;
                    float playerY = P.Center.Y;
                    Move(P, speed, new Vector2(playerX, playerY));

                    shootTimer[0]--;

                    if (Main.rand.Next(75) == 0)
                    {
                        Projectile kunai = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X + Main.rand.Next(-1000, 1000), P.Center.Y - 1500, Main.rand.NextFloat(-2, 2), 9f, ProjectileType<CrystallineKunaiHostileExplosive>(), projectileBaseDamage, 5f, Main.myPlayer)];
                        kunai.timeLeft = 180;
                    }

                    if (shootTimer[0] <= 0)
                    {
                        float projSpeed = 12f;
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        Projectile kunai = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), ProjectileType<CrystallineKunaiHostileExplosive>(), projectileBaseDamage, 5f, Main.myPlayer)];
                        kunai.timeLeft = 75;
                        shootTimer[0] = 60;
                    }

                    if (Main.rand.Next(10) == 0)
                    {
                        int distance = 500;
                        Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X + Main.rand.NextFloat(-distance, distance), P.Center.Y + Main.rand.NextFloat(-distance, distance), 0f, 0f, ProjectileType<CrystalCluster>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    if (Main.rand.Next(32) == 0)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X, P.Center.Y, 0f, 0f, ProjectileType<CrystalCluster>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    if (NPC.ai[1] > 450)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();
                    }
                }
                // kunai flower
                else if (NPC.ai[2] == 11)
                {
                    NPC.velocity *= 0f;
                    if (NPC.ai[3] == 0)
                    {
                        Vector2 target = P.Center;
                        Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                        toTarget.Normalize();
                        if (Vector2.Distance(target, NPC.Center) > 300)
                        {
                            NPC.velocity = toTarget * 13;
                        }
                        else
                        {
                            NPC.ai[3]++;
                        }
                    }
                    else if (NPC.ai[3] == 1)
                    {
                        int innerCount = 15;
                        for (int l = 0; l < innerCount; l++)
                        {
                            float distance = 360 / innerCount;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<AncientProjectionSwirl>(), projectileBaseDamage, 5f, 0, l * distance, NPC.whoAmI);
                        }
                        int outerCount = 20;
                        for (int l = 0; l < outerCount; l++)
                        {
                            float distance = 360 / outerCount;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<AncientProjectionSwirl2>(), projectileBaseDamage, 5f, 0, l * distance, NPC.whoAmI);
                        }
                        NPC.ai[3]++;
                    }
                    if (NPC.ai[3] == 2)
                    {
                        shootTimer[0]--;
                        Vector2 offset = new Vector2(400, 0);
                        float rotateSpeed = 0.015f;
                        shootTimer[1] += rotateSpeed;
                        shootTimer[2] -= rotateSpeed;

                        float Speed = 7;

                        if (shootTimer[0] <= 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                            float numProj = 5f;
                            float projOffset = MathHelper.ToRadians(360f) / numProj;
                            for (int i = 0; i < numProj; i++)
                            {
                                Vector2 shootTarget1 = NPC.Center + offset.RotatedBy(shootTimer[1] + (projOffset * (float)i) * (Math.PI * 2 / 8));
                                float rotation = (float)Math.Atan2(NPC.Center.Y - shootTarget1.Y, NPC.Center.X - shootTarget1.X);
                                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ProjectileType<CrystallineKunaiHostileNG>(), projectileBaseDamage, 0f, 0);
                            }
                            for (int i = 0; i < numProj; i++)
                            {
                                Vector2 shootTarget1 = NPC.Center + offset.RotatedBy(shootTimer[2] + (projOffset * (float)i) * (Math.PI * 2 / 8));
                                float rotation = (float)Math.Atan2(NPC.Center.Y - shootTarget1.Y, NPC.Center.X - shootTarget1.X);
                                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ProjectileType<CrystallineKunaiHostileNG>(), projectileBaseDamage, 0f, 0);
                            }
                            shootTimer[0] = 12;
                        }
                    }

                    if (NPC.ai[1] > 450)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        ResetShootTimers();

                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            Projectile other = Main.projectile[i];
                            if ((other.type == ProjectileType<AncientProjectionSwirl>() || other.type == ProjectileType<AncientProjectionSwirl2>()) && other.ai[1] == NPC.whoAmI && other.active)
                            {
                                other.Kill();
                            }
                        }
                    }
                }
            }
        }
        private void ResetShootTimers()
        {
            shootTimer[0] = 0;
            shootTimer[1] = 0;
            shootTimer[2] = 0;
            shootTimer[3] = 0;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/NPC/AncientDeath"), NPC.position);

                NPC deathAni = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<AncientAmalgamDeath>())];
                deathAni.Center = NPC.Center;
            }
        }
        private void MoveDirect(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            NPC.velocity = toTarget * moveSpeed;
        }
        private void Move(Player P, float speed, Vector2 target)
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
            float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
            if (MyWorld.awakenedMode) slowSpeed = 0.92f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((NPC.velocity.X < xSign && xSign == 1) || (NPC.velocity.X > xSign && xSign == -1)) NPC.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, NPC.Center.Y) > 1000)
            {
                if ((NPC.velocity.X < ySign && ySign == 1) || (NPC.velocity.X > ySign && ySign == -1)) NPC.velocity.Y *= slowSpeed;
            }
        }
    }
}