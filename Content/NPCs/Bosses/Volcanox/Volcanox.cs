using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.Consumable.StatIncreases;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.NPCProj.Volcanox;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Volcanox
{
    [AutoloadBossHead]
    public class Volcanox : ModNPC
    {
        public int changeLocationTimer = 0;
        public float vectorX = 0f;
        public float vectorY = 0f;

        public float burstTimer = 10f;
        public float shootCooldown = 10f;
        public float shootTimer = 0f;
        public float minionTimer = 0f;

        public float strikeTimer = 0f;

        public int ringAttack = 0;

        public bool enraged = false;

        public int projectileBaseDamage = 0;
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 150;
            NPC.lifeMax = 200000;
            NPC.damage = 130;
            NPC.defense = 75;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.Boss2;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<ExtinctionCurse>()] = true;
            NPC.buffImmune[ModContent.BuffType<HandsOfDespair>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.buffImmune[ModContent.BuffType<AncientDecay>()] = true;
            NPC.buffImmune[ModContent.BuffType<SoulInferno>()] = true;
            //npc.buffImmune[ModContent.BuffType<DragonFire>()] = true;
            NPC.buffImmune[ModContent.BuffType<Discord>()] = true;
            for (int num2 = 0; num2 < 206; num2++)
            {
                NPC.buffImmune[num2] = true;
            }
            NPC.aiStyle = -1;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 10;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // 0.n == уменшает изоброжения
                Scale = 0.65f, // Мини иконка в бестиарии 
                PortraitScale = 0.65f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0;
            value.Position.Y -= 28f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.VolcanoxBoss"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(200000, balance, bossAdjustment, 450000);
            NPC.damage = (int)EAU.BalanceDamage(130, balance, bossAdjustment, 200);
            NPC.defense = EAU.BalanceDefense(75, 90);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule _DropNormal = new(new EAIDRC.DropNormal());
            LeadingConditionRule _DropExpert = new(new EAIDRC.DropAwakened());
            LeadingConditionRule _DropFruit =  new(new EAIDRC.AwakenedModeActive());

            _DropNormal.OnSuccess(ItemDropRule.OneFromOptions(1, [.. EAList.VolLoot]));
            _DropExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<VolcanoxBag>(), 1));
            _DropExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsMasterMode(), ModContent.ItemType<VolcanoxRelicItem>(), 1));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<VolcanoxTrophy>(), 10));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<VolcanoxMask>(), 10));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Pyroplasm>(), minimumDropped: 5, maximumDropped: 40));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<VolcanicStone>(), minimumDropped: 6, maximumDropped: 20));

            _DropFruit.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ExtraAcc>()));

            npcLoot.Add(_DropNormal);
            npcLoot.Add(_DropExpert);
            npcLoot.Add(_DropFruit);
        }
        public override void OnKill()
        {
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
                            Tile t = Main.tile[tileX, tileY];
                            Main.tile[tileX, tileY].TileType = TileID.HellstoneBrick;
                            t.HasTile = true;
                        }
                        Tile ti = Main.tile[tileX, tileY];
                        ti.LiquidType = LiquidID.Lava;
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
            MyWorld.downedVolcanox = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.life > NPC.lifeMax * 0.5f)
            {
                if (NPC.frameCounter > 6)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 4)  // so it doesnt go over
                {
                    NPC.frame.Y = 0;
                }
            }
            if (NPC.life < NPC.lifeMax * 0.5f)
            {
                if (NPC.frameCounter > 6)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 9)  // so it doesnt go over
                {
                    NPC.frame.Y = frameHeight * 5;
                }
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (Main.masterMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 11;
                else projectileBaseDamage = 8;
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 48;
                else projectileBaseDamage = 48;
            }
            else projectileBaseDamage = 60;
            if (!P.ZoneUnderworldHeight)
            {
                enraged = true;
            }
            if (P.ZoneUnderworldHeight)
            {
                enraged = false;
            }
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
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
            Lighting.AddLight(NPC.Center, 2f, 2f, 2f);
            NPC.TargetClosest(true);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num728 = 6000;
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)num728)
                {
                    NPC.active = false;
                    NPC.life = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            // Set the correct rotation for this NPC.
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

            if (NPC.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.localAI[0] = 1f;
                int hook = NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VolcanoxHook>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                hook = NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VolcanoxHook>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                hook = NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<VolcanoxHook>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
            }
            if (NPC.life <= NPC.lifeMax * 0.25f && NPC.localAI[2] == 0)
            {
                NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SoulOfInfernace>());
                Main.NewText(ModContent.GetInstance<EALocalization>().Volcanox, Color.Orange.R, Color.Orange.G, Color.Orange.B);
                NPC.localAI[2]++;
            }

            #region movement
            int[] array2 = new int[3];
            float num730 = 0f;
            float num731 = 0f;
            int num732 = 0;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<VolcanoxHook>())
                {
                    num730 += Main.npc[i].Center.X;
                    num731 += Main.npc[i].Center.Y;
                    array2[num732] = i;
                    num732++;
                    if (num732 > 2)
                    {
                        break;
                    }
                }
            }
            num730 /= (float)num732;
            num731 /= (float)num732;
            float num734 = 2.5f;
            float speed = 0.05f;
            float speedMultiplier = 4f;
            if (NPC.life < NPC.lifeMax / 2)
            {
                num734 = 5f;
                speed = 0.10f;
                NPC.HitSound = SoundID.NPCHit1;
            }
            if (NPC.life < NPC.lifeMax / 4)
            {
                num734 = 7f;
            }
            if (Main.expertMode)
            {
                num734 += 1f;
                num734 *= 1.1f;
                speed *= 1.2f;
            }
            Vector2 vector91 = new Vector2(num730, num731);
            float targetX = Main.player[NPC.target].Center.X - vector91.X;
            float targetY = Main.player[NPC.target].Center.Y - vector91.Y;
            if (!P.active)
            {
                targetY *= -1f;
                targetX *= -1f;
                num734 += 8f;
            }
            float num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            int num739 = 500;
            if (Main.expertMode)
            {
                num739 += 150;
            }
            if (num738 >= (float)num739)
            {
                num738 = (float)num739 / num738;
                targetX *= num738;
                targetY *= num738;
            }
            num730 += targetX;
            num731 += targetY;
            vector91 = new Vector2(NPC.Center.X, NPC.Center.Y);
            targetX = num730 - vector91.X;
            targetY = num731 - vector91.Y;
            num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            if (enraged)
            {
                NPC.damage += 50;
                NPC.defense = 60;
                speedMultiplier += 3f;
            }
            if (num738 < num734)
            {
                targetX = NPC.velocity.X;
                targetY = NPC.velocity.Y;
            }
            else
            {
                num738 = num734 / num738;
                targetX *= num738 * speedMultiplier; // MULTIPLY SPEED HERE
                targetY *= num738 * speedMultiplier;
            }
            if (NPC.velocity.X < targetX)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X < 0f && targetX > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + speed * 2f;
                }
            }
            else if (NPC.velocity.X > targetX)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X > 0f && targetX < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - speed * 2f;
                }
            }
            if (NPC.velocity.Y < targetY)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed;
                if (NPC.velocity.Y < 0f && targetY > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed * 2f;
                }
            }
            else if (NPC.velocity.Y > targetY)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && targetY < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed * 2f;
                }
            }
            Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num740 = Main.player[NPC.target].Center.X - vector92.X;
            float num741 = Main.player[NPC.target].Center.Y - vector92.Y;
            NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
            #endregion

            NPC.ai[2]++;
            if (NPC.ai[2] >= 1500f)
            {
                NPC.ai[2] = 0f;
            }
            strikeTimer--;
            if (strikeTimer <= 0)
            {
                float posX = P.Center.X + Main.rand.Next(-20, 20);
                float posY = P.Center.Y + 1000;
                Projectile.NewProjectile(EAU.NPCs(NPC), posX, posY, 0f, -15f, ModContent.ProjectileType<VolcanicDemon>(), projectileBaseDamage, 0f, Main.myPlayer);
                float posX2 = P.Center.X + Main.rand.Next(-20, 20);
                float posY2 = P.Center.Y - 1000;
                Projectile.NewProjectile(EAU.NPCs(NPC), posX2, posY2, 0f, 15f, ModContent.ProjectileType<VolcanicDemon>(), projectileBaseDamage, 0f, Main.myPlayer);
                strikeTimer = 100;
            }
            if (NPC.life > NPC.lifeMax / 2)
            {
                shootTimer += 1f;
                if (NPC.life < NPC.lifeMax * 0.8)
                {
                    shootTimer += 1f;
                }
                if (NPC.life < NPC.lifeMax * 0.6)
                {
                    shootTimer += 1f;
                }
                if (Main.expertMode)
                {
                    shootTimer += 1f;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer > 80f) // firing
                {
                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        float Speed = 15f;
                        if (Main.expertMode)
                        {
                            Speed += 2f;
                        }
                        int type = ModContent.ProjectileType<VolcanoxBolt>();
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        int proj = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    shootTimer = 0f;
                }
            }
            else
            {
                shootCooldown--;
                burstTimer--;
                if (shootCooldown <= 0)
                {
                    shootCooldown = 80f;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && burstTimer <= 0f && shootCooldown <= 30)
                {
                    float projSpeed = 10f;
                    int damage = projectileBaseDamage - 2;
                    int type = ModContent.ProjectileType<VolcanoxBolt>();
                    if (Main.expertMode)
                    {
                        projSpeed += 5f;
                    }
                    if (Main.rand.Next(6) == 0)
                    {
                        projSpeed += 5f;
                        damage += 25;
                        type = ModContent.ProjectileType<VolcanoxBlast>();
                    }
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), type, damage, 0f, Main.myPlayer);

                    burstTimer = 6f;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] >= 750 && NPC.ai[2] <= 1000)
                {
                    ringAttack--;
                    if (ringAttack <= 0)
                    {
                        int type = ModContent.ProjectileType<VolcanicDebris>();
                        int projDamage = projectileBaseDamage;
                        float numberProjectiles = Main.expertMode ? 12 : 8;
                        float rotation = MathHelper.ToRadians(360);
                        float projSpeed = 4f;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, projDamage, 2f, Main.myPlayer);
                        }
                        SoundEngine.PlaySound(SoundID.Zombie93, NPC.position);
                        ringAttack = 60;
                    }
                }

                // increase stats when low health:
                NPC.defense = 25;
                NPC.damage = Main.expertMode ? 250 : 175;
                if (MyWorld.awakenedMode)
                {
                    NPC.damage = 275;
                }
                int tentacleType = ModContent.NPCType<VolcanoxTentacle>();
                int numTentacles = 10;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (NPC.localAI[0] == 1f)
                    {
                        NPC.localAI[0] = 2f;
                        for (int k = 0; k < numTentacles; k++)
                        {
                            NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, tentacleType, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        // create extra tentacles
                        if (Main.expertMode)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, tentacleType, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                            }
                        }
                    }
                }
                // SPORE TIME
                minionTimer += 1f;
                if ((double)NPC.life < (double)NPC.lifeMax * 0.2)
                {
                    minionTimer += 1f;
                }
                if (minionTimer >= 350f)
                {
                    NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Firefly>());
                    minionTimer = 0f;
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
