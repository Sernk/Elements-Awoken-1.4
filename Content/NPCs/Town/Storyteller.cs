using ElementsAwoken.Content.Items.BossSummons;
using ElementsAwoken.Content.Items.Placeable.Drives;
using ElementsAwoken.Content.Items.Storyteller;
using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.Content.Projectiles.Thrown;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static ElementsAwoken.ElementsAwoken;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Town
{
    [AutoloadHead]
    public class Storyteller : ModNPC
    {
        public float shadowTimer = 0f;
        public float shadowAlpha = 0.25f;
        public int shadowDirection = 0;
        public int shadowAI = 0;
        int Slot = 0;
        public override string Texture
        {
            get
            {
                return "ElementsAwoken/Content/NPCs/Town/Storyteller";
            }
        }
        public override void Load()
        {
            _ = this.GetLocalization("ButtonText.Text").Value;
            _ = this.GetLocalization("ButtonText.Text1").Value;
            _ = this.GetLocalization("ButtonText.Text2").Value;

            _ = this.GetLocalization("Story.Story").Value;
            _ = this.GetLocalization("Story.Story_1").Value;
            _ = this.GetLocalization("Story.Story1").Value;
            _ = this.GetLocalization("Story.Story1_1").Value;
            _ = this.GetLocalization("Story.Story2").Value;
            _ = this.GetLocalization("Story.Story2_1").Value;
            _ = this.GetLocalization("Story.Story3").Value;
            _ = this.GetLocalization("Story.Story3_1").Value;
            _ = this.GetLocalization("Story.Story4").Value;
            _ = this.GetLocalization("Story.Story4_1").Value;
            _ = this.GetLocalization("Story.Story5").Value;
            _ = this.GetLocalization("Story.Story5_1").Value;
            _ = this.GetLocalization("Story.Story6").Value;
            _ = this.GetLocalization("Story.Story6_1").Value;
            _ = this.GetLocalization("Story.Story7").Value;
            _ = this.GetLocalization("Story.Story7_1").Value;
            _ = this.GetLocalization("Story.Story8").Value;
            _ = this.GetLocalization("Story.Story9").Value;
            _ = this.GetLocalization("Story.Story9_1").Value;
            _ = this.GetLocalization("Story.Story10").Value;
            _ = this.GetLocalization("Story.Story10_1").Value;
            _ = this.GetLocalization("Story.Story11").Value;
            _ = this.GetLocalization("Story.Story11_1").Value;
            _ = this.GetLocalization("Story.Story12").Value;
            _ = this.GetLocalization("Story.Story12_1").Value;
            _ = this.GetLocalization("Story.Story13").Value;
            _ = this.GetLocalization("Story.Story13_1").Value;
            _ = this.GetLocalization("Story.Story14").Value;
            _ = this.GetLocalization("Story.Story15").Value;
            _ = this.GetLocalization("Story.Story15_1").Value;
            _ = this.GetLocalization("Story.Story16").Value;
            _ = this.GetLocalization("Story.Story16_1").Value;
            _ = this.GetLocalization("Story.Story17").Value;
            _ = this.GetLocalization("Story.Story17_1").Value;
            _ = this.GetLocalization("Story.Story18").Value;
            _ = this.GetLocalization("Story.Story18_1").Value;
            _ = this.GetLocalization("Story.Story19").Value;
            _ = this.GetLocalization("Story.Story19_1").Value;
            _ = this.GetLocalization("Story.Story20").Value;
            _ = this.GetLocalization("Story.Story21").Value;
            _ = this.GetLocalization("Story.Story21_1").Value;
            _ = this.GetLocalization("Story.Story22").Value;
            _ = this.GetLocalization("Story.Story22_1").Value;
            _ = this.GetLocalization("Story.Story23").Value;
            _ = this.GetLocalization("Story.Story23_1").Value;
            _ = this.GetLocalization("Story.Story24").Value;
            _ = this.GetLocalization("Story.Story24_1").Value;
            _ = this.GetLocalization("Story.Story25").Value;
            _ = this.GetLocalization("Story.Story26").Value;
            _ = this.GetLocalization("Story.Story26_1").Value;

            _ = this.GetLocalization("Chat.Chat").Value;
            _ = this.GetLocalization("Chat.Chat1").Value;
            _ = this.GetLocalization("Chat.Chat2").Value;
            _ = this.GetLocalization("Chat.Chat3").Value;
            _ = this.GetLocalization("Chat.Chat4").Value;
            _ = this.GetLocalization("Chat.Chat4_1").Value;
            _ = this.GetLocalization("Chat.Chat4_2").Value;
            _ = this.GetLocalization("Chat.Chat5").Value;
            _ = this.GetLocalization("Chat.Chat6").Value;
            _ = this.GetLocalization("Chat.Chat7").Value;
            _ = this.GetLocalization("Chat.Chat8").Value;
            _ = this.GetLocalization("Chat.Chat9").Value;
            _ = this.GetLocalization("Chat.Chat9_1").Value;
            _ = this.GetLocalization("Chat.Chat10").Value;
            _ = this.GetLocalization("Chat.Chat11").Value;
            _ = this.GetLocalization("Chat.Chat11_1").Value;
            _ = this.GetLocalization("Chat.Chat12").Value;
            _ = this.GetLocalization("Chat.Chat13").Value;
            _ = this.GetLocalization("Chat.Chat13_1").Value;
            _ = this.GetLocalization("Chat.Chat14").Value;
            _ = this.GetLocalization("Chat.Chat15").Value;
            _ = this.GetLocalization("Chat.Chat16").Value;
            _ = this.GetLocalization("Chat.Chat16_1").Value;
            _ = this.GetLocalization("Chat.Chat17").Value;
            _ = this.GetLocalization("Chat.Chat18").Value;
            _ = this.GetLocalization("Chat.Chat19").Value;
            _ = this.GetLocalization("Chat.Chat20").Value;
            _ = this.GetLocalization("Chat.Chat21").Value;
            _ = this.GetLocalization("Chat.Chat22").Value;
            _ = this.GetLocalization("Chat.Chat22_1").Value;
            _ = this.GetLocalization("Chat.Chat23").Value;
            _ = this.GetLocalization("Chat.Chat23_1").Value;
            _ = this.GetLocalization("Chat.Chat24").Value;
            _ = this.GetLocalization("Chat.Chat25").Value;
            _ = this.GetLocalization("Chat.Chat26").Value;
            _ = this.GetLocalization("Chat.Chat27").Value;
            _ = this.GetLocalization("Chat.Chat28").Value;
            _ = this.GetLocalization("Chat.Chat29").Value;
            _ = this.GetLocalization("Chat.Chat30").Value;
            _ = this.GetLocalization("Chat.Chat31").Value;

            _ = this.GetLocalization("Content_1_4.BossD").Value;
            _ = this.GetLocalization("Content_1_4.BossD_1").Value;
            _ = this.GetLocalization("Content_1_4.BossQ").Value;
            _ = this.GetLocalization("Content_1_4.BossQ_1").Value;
            _ = this.GetLocalization("Content_1_4.BossE").Value;
            _ = this.GetLocalization("Content_1_4.BossE_1").Value;
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 30;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            AnimationType = NPCID.Merchant;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Like)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Wizard, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Clothier, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like);

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
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.TownNPCs.Storyteller"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return true;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            if (MyWorld.downedAncients)
            {
                return false;
            }
            return true;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ProjectileID.RottenEgg)
            {
                modifiers.SourceDamage += 80 + Main.rand.Next(-2, 2);
                if (NPC.life < NPC.lifeMax / 5)
                {
                    NPC.life = NPC.lifeMax;
                    float projSpeed = 12f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - Main.player[projectile.owner].Center.Y, NPC.Center.X - Main.player[projectile.owner].Center.X);
                    Projectile attack = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1) - 2f, ProjectileType<CrystallineKunai>(), 1200, 5f, 0)];
                    attack.friendly = false;
                    attack.hostile = true;
                }
            }
            base.ModifyHitByProjectile(projectile, ref modifiers);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (MyWorld.downedAzana)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, NPC.height * 0.5f);
                for (int k = 0; k < 4; k++)
                {
                    Vector2 drawPos = NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY) + new Vector2(0, -2);
                    float multipler = 1;
                    if (k == 1 || k == 3)
                    {
                        multipler = 1.5f;
                    }
                    int switchDir = 1;
                    if (k == 0 || k == 2)
                    {
                        switchDir = -1;
                    }
                    drawPos.X += shadowTimer * multipler * switchDir;
                    Color color = NPC.GetAlpha(drawColor) * shadowAlpha;
                    Texture2D texture = Request<Texture2D>("ElementsAwoken/Content/NPCs/Town/StorytellerShadow" + k).Value;
                    SpriteEffects effects = NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    EAU.Sb.Draw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, effects, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            if (MyWorld.downedAzana || MyWorld.sparedAzana)
            {
                if (shadowDirection == 0)
                {
                    shadowTimer -= 1;
                }
                else
                {
                    shadowTimer += 1;
                }
                if (shadowTimer <= -10)
                {
                    shadowDirection = 1;
                }
                else if (shadowTimer >= 10)
                {
                    shadowDirection = 0;
                }

                shadowAI--;
                if (shadowAI < 300)
                {
                    shadowAlpha += 0.005f;
                    if (shadowAlpha > 0.25f)
                    {
                        shadowAlpha = 0.25f;
                    }
                }
                else
                {
                    shadowAlpha -= 0.005f;
                    if (shadowAlpha < 0f)
                    {
                        shadowAlpha = 0f;
                    }
                }
                if (shadowAI <= 0)
                {
                    shadowAI = Main.rand.Next(300, 900);
                }
            }
            else
            {
                shadowAlpha = 0f;
            }
        }
        public override List<string> SetNPCNameList()
        {
            if (Main.rand.Next(300) == 0)
            {
                return [this.GetLocalizedValue("Name.Amadis"),];
            }
            return WorldGen.genRand.Next(4) switch
            {
                0 => [this.GetLocalizedValue("Name.Neivirk"),],
                1 => [this.GetLocalizedValue("Name.Herckeus"),],
                2 => [this.GetLocalizedValue("Name.Nornex"),],
                3 => [this.GetLocalizedValue("Name.Zairis"),],
                _ => [this.GetLocalizedValue("Name.default"),],
            };
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 2;
            randExtraCooldown = 20;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileType<CrystallineKunai>();
            attackDelay = 1;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 15f;
            randomOffset = 2f;
        }
        public string buttonText = "";
        public int buttonMode = 0;
        public int buttonPressed = 0;
        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (buttonMode == 0)
            {
                if (!MyWorld.downedAzana && !MyWorld.sparedAzana)
                {
                    buttonText = this.GetLocalization("ButtonText.Text").Value; ;
                }
                else
                {
                    buttonText = this.GetLocalization("ButtonText.Text1").Value; ;
                    buttonPressed = 0;
                }
            }
            if (buttonMode == 1)
            {
                buttonText = this.GetLocalization("ButtonText.Text2").Value; ;
                buttonPressed = 1;
            }
            button = buttonText;
            if (NPC.downedBoss1)
            {
                button2 = Language.GetTextValue("LegacyInterface.28"); ;
            }
        }
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.player[Main.myPlayer];
            if (firstButton)
            {
                if (buttonMode == 0)
                {
                    if (!MyWorld.downedAzana && !MyWorld.sparedAzana)
                    {
                        Item item = new Item(); // to get the name of the item
                        bool toySlime = MyWorld.downedToySlime;
                        bool kingSlime = NPC.downedSlimeKing;
                        bool eyeOfCthulhu = NPC.downedBoss1;
                        bool wasteland = MyWorld.downedWasteland;
                        bool eaterOrBrain = NPC.downedBoss2;
                        bool queenBee = NPC.downedQueenBee;
                        bool skeletron = NPC.downedBoss3;
                        bool deerclops = NPC.downedDeerclops;
                        bool infernace = MyWorld.downedInfernace;
                        bool wallOfFlesh = Main.hardMode;
                        bool queen = NPC.downedQueenSlime;
                        bool destroyer = NPC.downedMechBoss1;
                        bool twins = NPC.downedMechBoss2;
                        bool skeletronPrime = NPC.downedMechBoss3;
                        bool scourgeFighter = MyWorld.downedScourgeFighter;
                        bool regaroth = MyWorld.downedRegaroth;
                        bool plantera = NPC.downedPlantBoss;
                        bool golem = NPC.downedGolemBoss;
                        bool permafrost = MyWorld.downedPermafrost;
                        bool obsidious = MyWorld.downedObsidious;
                        bool dukeFishron = NPC.downedFishron;
                        bool empress = NPC.downedEmpressOfLight;
                        bool aqueous = MyWorld.downedAqueous;
                        bool theGuardian = MyWorld.downedGuardian;
                        bool cultist = NPC.downedAncientCultist;
                        bool moonLord = NPC.downedMoonlord;
                        bool volcanox = MyWorld.downedVolcanox;
                        bool voidLeviathan = MyWorld.downedVoidLeviathan;
                        bool azana = MyWorld.downedAzana;

                        /*if (!toySlime)
                        {
                            Main.npcChatText = "Back then, when the dreaded frost moon rose, a slime filled with toys has happened to appear. A 'Large Slimeball' is enough to summon it.";
                            Main.npcChatCornerItem = mod.ItemType("ToySlimeSummon");
                        }
                        else */if (!kingSlime)
                        {
                            item.SetDefaults(ItemID.SlimeCrown);
                            string q = this.GetLocalization("Story.Story").Value + item.Name + this.GetLocalization("Story.Story_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.SlimeCrown;
                        }
                        else if (!eyeOfCthulhu)
                        {
                            item.SetDefaults(ItemID.SuspiciousLookingEye);
                            string q = this.GetLocalization("Story.Story1").Value + item.Name + this.GetLocalization("Story.Story1_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.SuspiciousLookingEye;
                        }
                        else if (!wasteland)
                        {
                            item.SetDefaults(ItemType<WastelandSummon>());
                            string q = this.GetLocalization("Story.Story2").Value + item.Name + this.GetLocalization("Story.Story2_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<WastelandSummon>();
                        }
                        else if (!eaterOrBrain)
                        {
                            if(WorldGen.crimson)
                            {
                                item.SetDefaults(ItemID.BloodySpine);
                                string q = this.GetLocalization("Story.Story3").Value + item.Name + this.GetLocalization("Story.Story3_1").Value;
                                Main.npcChatText = q;
                                Main.npcChatCornerItem = ItemID.BloodySpine;
                            }
                            else
                            {
                                item.SetDefaults(ItemID.WormFood);
                                string q = this.GetLocalization("Story.Story4").Value + item.Name + this.GetLocalization("Story.Story4_1").Value;
                                Main.npcChatText = q;
                                Main.npcChatCornerItem = ItemID.WormFood;
                            }
                        }
                        else if (!queenBee)
                        {
                            item.SetDefaults(ItemID.Abeemination);
                            string q = this.GetLocalization("Story.Story5").Value + item.Name + this.GetLocalization("Story.Story5_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.Abeemination;
                        }
                        else if (!skeletron)
                        {
                            item.SetDefaults(ItemID.ClothierVoodooDoll);
                            string q = this.GetLocalization("Story.Story6").Value + item.Name + this.GetLocalization("Story.Story6_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.ClothierVoodooDoll;
                        }
                        else if(!deerclops)
                        {
                            item.SetDefaults(ItemID.DeerThing);
                            string q = this.GetLocalization("Content_1_4.BossD").Value + item.Name + this.GetLocalization("Content_1_4.BossD_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.DeerThing;
                        }
                        else if (!infernace)
                        {
                            item.SetDefaults(ItemType<InfernaceSummon>());
                            string q = this.GetLocalization("Story.Story7").Value + item.Name + this.GetLocalization("Story.Story7_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<InfernaceSummon>();
                        }
                        else if (!wallOfFlesh)
                        {
                            item.SetDefaults(ItemID.GuideVoodooDoll);
                            Main.npcChatText = this.GetLocalization("Story.Story8").Value;
                            Main.npcChatCornerItem = ItemID.GuideVoodooDoll;
                        }
                        else if (!queen)
                        {
                            item.SetDefaults(ItemID.QueenSlimeCrystal);
                            string q = this.GetLocalization("Content_1_4.BossQ").Value + item.Name + this.GetLocalization("Content_1_4.BossQ_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.QueenSlimeCrystal;
                        }
                        else if (!twins)
                        {
                            item.SetDefaults(ItemID.MechanicalEye);
                            string q = this.GetLocalization("Story.Story9").Value + item.Name + this.GetLocalization("Story.Story9_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.MechanicalEye;
                        }
                        else if (!destroyer)
                        {
                            item.SetDefaults(ItemID.MechanicalWorm);
                            string q = this.GetLocalization("Story.Story10").Value + item.Name + this.GetLocalization("Story.Story10_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.MechanicalWorm;
                        }
                        else if (!skeletronPrime)
                        {
                            item.SetDefaults(ItemID.MechanicalSkull);
                            string q = this.GetLocalization("Story.Story11").Value + item.Name + this.GetLocalization("Story.Story11_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.MechanicalSkull;
                        }
                        else if (!scourgeFighter)
                        {
                            item.SetDefaults(ItemType<ScourgeFighterSummon>());
                            string q = this.GetLocalization("Story.Story12").Value + item.Name + this.GetLocalization("Story.Story12_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<ScourgeFighterSummon>();
                        }
                        else if (!regaroth)
                        {
                            item.SetDefaults(ItemType<RegarothSummon>());
                            string q = this.GetLocalization("Story.Story13").Value + item.Name + this.GetLocalization("Story.Story13_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<RegarothSummon>();
                        }
                        else if (!plantera)
                        {
                            Main.npcChatText = this.GetLocalization("Story.Story14").Value;
                        }
                        else if (!golem)
                        {
                            item.SetDefaults(ItemID.LihzahrdPowerCell);
                            string q = this.GetLocalization("Story.Story15").Value + item.Name + this.GetLocalization("Story.Story15_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.LihzahrdPowerCell;
                        }
                        else if (!permafrost)
                        {
                            item.SetDefaults(ItemType<PermafrostSummon>());
                            string q = this.GetLocalization("Story.Story16").Value + item.Name + this.GetLocalization("Story.Story16_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<PermafrostSummon>();
                        }
                        /*else if (!celestial)
                        {
                            Main.npcChatText = "The celestial forces once created a weaker amalgamation of their powers. That amalgamation sees a lot of things as a threat, so you better get a 'Stone of the Stars' and take it's sight in return.";
                            Main.npcChatCornerItem = ItemType<CelestialSummon>();
                        }*/
                        else if (!obsidious)
                        {
                            item.SetDefaults(ItemType<ObsidiousSummon>());
                            string q = this.GetLocalization("Story.Story17").Value + item.Name + this.GetLocalization("Story.Story17_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<ObsidiousSummon>();
                        }
                        else if (!dukeFishron)
                        {
                            item.SetDefaults(ItemID.TruffleWorm);
                            string q = this.GetLocalization("Story.Story18").Value + item.Name + this.GetLocalization("Story.Story18_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.TruffleWorm;
                        }
                        else if (!empress)
                        {
                            item.SetDefaults(ItemID.EmpressButterfly);
                            string q = this.GetLocalization("Content_1_4.BossE").Value + item.Name + this.GetLocalization("Content_1_4.BossE_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.EmpressButterfly;
                        }
                        else if (!aqueous)
                        {
                            item.SetDefaults(ItemType<AqueousSummon>());
                            string q = this.GetLocalization("Story.Story19").Value + item.Name + this.GetLocalization("Story.Story19_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<AqueousSummon>();
                        }
                        else if (!cultist)
                        {
                            Main.npcChatText = this.GetLocalization("Story.Story20").Value;
                        }
                        else if (!moonLord)
                        {
                            item.SetDefaults(ItemID.CelestialSigil);
                            string q = this.GetLocalization("Story.Story21").Value + item.Name + this.GetLocalization("Story.Story21_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemID.CelestialSigil;
                        }
                        else if (!volcanox)
                        {
                            item.SetDefaults(ItemType<VolcanoxSummon>());
                            string q = this.GetLocalization("Story.Story22").Value + item.Name + this.GetLocalization("Story.Story22_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<VolcanoxSummon>();
                        }
                        else if (!voidLeviathan)
                        {
                            item.SetDefaults(ItemType<VoidLeviathanSummon>());
                            string q = this.GetLocalization("Story.Story23").Value + item.Name + this.GetLocalization("Story.Story23_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<VoidLeviathanSummon>();
                        }
                        else if (!azana)
                        {
                            item.SetDefaults(ItemType<AzanaSummon>());
                            string q = this.GetLocalization("Story.Story24").Value + item.Name + this.GetLocalization("Story.Story24_1").Value;
                            Main.npcChatText = q;
                            Main.npcChatCornerItem = ItemType<AzanaSummon>();
                        }
                        else
                        {
                            Main.npcChatText = this.GetLocalization("Story.Story25").Value; // never actually displayed anymore
                        }
                    }
                    else
                    {
                        string q = this.GetLocalization("Story.Story26").Value + " " + Main.worldName + " " + this.GetLocalization("Story.Story26_1").Value;
                        Main.npcChatText = q;
                        buttonMode = 1;
                    }
                }
                if (buttonPressed == 1)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer) SpawnAncients(player);
                    else
                    {
                        ModPacket netMessage = GetPacket();
                        netMessage.Write(true);
                        netMessage.Send();
                    }

                }
            }
            if (!firstButton)
            {
                shopName = "StorytellerShop";
                // shop button only appears after EoC   
                /*
                if (NPC.downedBoss1)
                {
                    shop = true;
                }
                else
                {
                    Main.npcChatText = "Uh, sorry i dont have anything to sell. Defeat a boss first";
                }
                */
            }
        }
        public override void AddShops()
        {
            // Предметы добавляются начиная с сверху и идут к последнему предмету который нижний
            NPCShop shop = new(Type, "StorytellerShop");

            #region Weapons
            Condition Boss1 = new (EALocalization.BossName(1 ),      () => NPC.downedBoss1    );
            Condition Boss2 = new (EALocalization.BossName(2 ),      () => NPC.downedBoss2    );
            Condition Boss3 = new (EALocalization.BossName(3 ),      () => NPC.downedBoss3    );
            Condition HardM = new (EALocalization.BossName(4 ),      () => Main.hardMode      );
            Condition MBoss = new (EALocalization.BossName(5 ),      () => NPC.downedMechBoss1);
            Condition MBos2 = new (EALocalization.BossName(6 ),      () => NPC.downedMechBoss2);
            Condition MBos3 = new (EALocalization.BossName(7 ),      () => NPC.downedMechBoss3);
            Condition BossP = new (EALocalization.BossName(8 ),      () => NPC.downedPlantBoss);
            Condition BossG = new (EALocalization.BossName(9 ),      () => NPC.downedGolemBoss);
            Condition BossF = new (EALocalization.BossName(10),      () => NPC.downedFishron  );
            Condition BossM = new (EALocalization.BossName(11),      () => NPC.downedMoonlord );

            shop.Add(new Item(ItemType<Sanguine>())             { value = 200000  },     Boss1);
            shop.Add(new Item(ItemType<Wormer>())               { value = 250000  },     Boss2);
            shop.Add(new Item(ItemType<Soulsword>())            { value = 300000  },     Boss3);
            shop.Add(new Item(ItemType<Nihongo>())              { value = 300000  },     HardM);
            shop.Add(new Item(ItemType<ForeverSword>())         { value = 500000  },     MBoss);
            shop.Add(new Item(ItemType<SearingBlaze>())         { value = 500000  },     MBos2);
            shop.Add(new Item(ItemType<MasterSword>())          { value = 500000  },     MBos3);
            shop.Add(new Item(ItemType<Gladiolus>())            { value = 650000  },     BossP);
            shop.Add(new Item(ItemType<Mjolnir>())              { value = 750000  },     BossG);
            shop.Add(new Item(ItemType<PoseidonsTrident>())     { value = 1000000 },     BossF);
            shop.Add(new Item(ItemType<EmptyGauntlet>())        { value = 1250000 },     BossM);
            #endregion
            #region Skip 9slot
            while (Slot < 9)
            {
                shop.Add(ItemType<ModItem>()); 
                Slot++;
            }
            #endregion
            #region Drive
            Condition EABossWas = new(EALocalization.MBossName(1 ), () => MyWorld.downedWasteland     );
            Condition EABossInf = new(EALocalization.MBossName(2 ), () => MyWorld.downedInfernace     );
            Condition EABossSco = new(EALocalization.MBossName(3 ), () => MyWorld.downedScourgeFighter);
            Condition EABossReg = new(EALocalization.MBossName(4 ), () => MyWorld.downedRegaroth      );
            Condition EABossObs = new(EALocalization.MBossName(5 ), () => MyWorld.downedObsidious     );
            Condition EABossPer = new(EALocalization.MBossName(6 ), () => MyWorld.downedPermafrost    );
            Condition EABossAqu = new(EALocalization.MBossName(7 ), () => MyWorld.downedAqueous       );
            Condition EABossGua = new(EALocalization.MBossName(8 ), () => MyWorld.downedGuardian      );
            Condition EABossVol = new(EALocalization.MBossName(9 ), () => MyWorld.downedVolcanox      );
            Condition EABossVoi = new(EALocalization.MBossName(10), () => MyWorld.downedVoidLeviathan );
            Condition EABossAza = new(EALocalization.MBossName(11), () => MyWorld.downedAzana         );

            shop.Add(new Item(ItemType<WastelandDrive>())       { value = 10000 }, EABossWas);
            shop.Add(new Item(ItemType<InfernaceDrive>())       { value = 10000 }, EABossInf);
            shop.Add(new Item(ItemType<ScourgeFighterDrive>())  { value = 10000 }, EABossSco);
            shop.Add(new Item(ItemType<RegarothDrive>())        { value = 10000 }, EABossReg);
            shop.Add(new Item(ItemType<ObsidiousDrive>())       { value = 10000 }, EABossObs);
            shop.Add(new Item(ItemType<PermafrostDrive>())      { value = 10000 }, EABossPer);
            shop.Add(new Item(ItemType<AqueousDrive>())         { value = 10000 }, EABossAqu);
            shop.Add(new Item(ItemType<GuardianDrive>())        { value = 10000 }, EABossGua);
            shop.Add(new Item(ItemType<VolcanoxDrive>())        { value = 10000 }, EABossVol);
            shop.Add(new Item(ItemType<VoidLeviathanDrive>())   { value = 10000 }, EABossVoi);
            shop.Add(new Item(ItemType<AzanaDrive>())           { value = 10000 }, EABossAza);
            #endregion

            shop.Register();
        }
        private ModPacket GetPacket()
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)ElementsAwokenMessageType.Storyteller);
            packet.Write(NPC.whoAmI);
            return packet;
        }
        public void HandlePacket(BinaryReader reader)
        {
            Player player = Main.LocalPlayer;
            SpawnAncients(player);
        }
        private void SpawnAncients(Player player)
        {
            var n = EAU.NPCs(NPC);
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                NPC.active = false;
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<Izaris>());
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<Kirvein>());
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<Krecheus>());
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<Xernon>());
                NPC.NewNPC(n, (int)player.Center.X, (int)player.Center.Y - 300, NPCType<ShardBase>(), 0, 0, 0, 0, 0, player.whoAmI);
            }
            else
            {
                Console.WriteLine("Spawning Ancients");
                NPC.SimpleStrikeNPC(9999, 0, false, 0f, DamageClass.Default, false, 0, false);

                NPC npc1 = Main.npc[NPC.NewNPC(n, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Izaris>(), 0, 0, 0, 0, 0, player.whoAmI)];
                NPC npc2 = Main.npc[NPC.NewNPC(n, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Kirvein>(), 0, 0, 0, 0, 0, player.whoAmI)];
                NPC npc3 = Main.npc[NPC.NewNPC(n, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Kirvein>(), 0, 0, 0, 0, 0, player.whoAmI)];
                NPC npc4 = Main.npc[NPC.NewNPC(n, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Xernon>(), 0, 0, 0, 0, 0, player.whoAmI)];
                NPC npc5 = Main.npc[NPC.NewNPC(n, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<ShardBase>(), 0, 0, 0, 0, 0, player.whoAmI)];
                NPC.netUpdate = true;
                //Projectile.NewProjectile(player.Center.X, player.Center.Y - 300, 0f, 0f, mod.ProjectileType("ShardBase"), 0, 0f, Main.myPlayer, Main.myPlayer);
            }
        }
        public override string GetChat()
        {
            buttonMode = 0;

            if (MyWorld.downedAzana) 
            {
                return Main.rand.Next(3) switch
                {
                    0 => this.GetLocalization("Chat.Chat").Value,
                    1 => this.GetLocalization("Chat.Chat1").Value,
                    2 => this.GetLocalization("Chat.Chat2").Value,
                    _ => this.GetLocalization("Chat.Chat3").Value
                };
            }

            Player player = Main.player[Main.myPlayer];
            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            int merchant = NPC.FindFirstNPC(NPCID.Merchant);
            int guide = NPC.FindFirstNPC(NPCID.Guide);
            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            int armsDealer = NPC.FindFirstNPC(NPCID.ArmsDealer);
            int nurse = NPC.FindFirstNPC(NPCID.Nurse);
            int truffle = NPC.FindFirstNPC(NPCID.Truffle);
            if (Main.rand.Next(249) == 0)
            {
                return this.GetLocalization("Chat.Chat4").Value + " " + player.name + " " + this.GetLocalization("Chat.Chat4_1").Value + " " + player.name + this.GetLocalization("Chat.Chat4_2").Value;
            }
            if (Main.hardMode && Main.rand.Next(30) == 0)
            {
                return this.GetLocalization("Chat.Chat5").Value;
            }
            if (Main.bloodMoon && Main.rand.Next(5) == 0)
            {
                return this.GetLocalization("Chat.Chat6").Value;
            }
            if (Main.pumpkinMoon && Main.rand.Next(5) == 0)
            {
                return this.GetLocalization("Chat.Chat7").Value;
            }
            if (Main.snowMoon && Main.rand.Next(5) == 0)
            {
                return this.GetLocalization("Chat.Chat8").Value;
            }
            if (partyGirl >= 0 && Main.rand.Next(5) == 0)
            {
                return this.GetLocalization("Chat.Chat9").Value + " " + Main.npc[partyGirl].GivenName +  " " + this.GetLocalization("Chat.Chat9_1").Value;
            }
            if (dryad >= 0 && Main.rand.Next(10) == 0)
            {
                return Main.npc[dryad].GivenName + " " + this.GetLocalization("Chat.Chat10").Value;
            }
            if (truffle >= 0 && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat11").Value + " " + Main.npc[truffle].GivenName + this.GetLocalization("Chat.Chat11_1").Value;
            }
            if (armsDealer >= 0 && Main.rand.Next(5) == 0)
            {
                return Main.npc[armsDealer].GivenName + this.GetLocalization("Chat.Chat12").Value;
            }
            if (guide >= 0 && Main.rand.Next(5) == 0)
            {
                return this.GetLocalization("Chat.Chat13").Value + " " + Main.npc[guide].GivenName + " " + this.GetLocalization("Chat.Chat13_1").Value;
            }
            if (ElementsAwoken.bossChecklistEnabled && Main.rand.Next(20) == 0)
            {
                return this.GetLocalization("Chat.Chat14").Value;
            }
            if (NPC.downedMoonlord && Main.rand.Next(15) == 0)
            {
                return this.GetLocalization("Chat.Chat15").Value;
            }
            if (!NPC.downedBoss1 && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat16").Value + " " + Main.npc[merchant].GivenName + " " + this.GetLocalization("Chat.Chat16_1").Value;
            }
            if (NPC.downedBoss1 && !MyWorld.downedWasteland && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat17").Value;
            }
            if (NPC.downedBoss3 && !Main.hardMode && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat18").Value;
            }
            if (NPC.downedMoonlord && !MyWorld.downedVoidLeviathan && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat19").Value;
            }
            if (NPC.downedMoonlord && !MyWorld.downedVoidLeviathan && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat20").Value;
            }
            if (NPC.downedMoonlord && !MyWorld.downedVolcanox && Main.rand.Next(10) == 0)
            {
                return this.GetLocalization("Chat.Chat21").Value;
            }
            if (player.statLife <= (player.statLifeMax2 / 5) && Main.rand.Next(5) == 0)
            {
                if (nurse >= 0)
                {
                    return this.GetLocalization("Chat.Chat22").Value + " " + player.name + " " + this.GetLocalization("Chat.Chat22_1").Value + " " + Main.npc[nurse].GivenName;
                }
                else
                {
                    return this.GetLocalization("Chat.Chat23").Value + " " + player.name + this.GetLocalization("Chat.Chat23_1").Value;
                }
            }

            return Main.rand.Next(8) switch
            {
                0 => this.GetLocalization("Chat.Chat24").Value,
                1 => this.GetLocalization("Chat.Chat25").Value,
                2 => this.GetLocalization("Chat.Chat26").Value,
                3 => this.GetLocalization("Chat.Chat27").Value,
                4 => this.GetLocalization("Chat.Chat28").Value,
                5 => this.GetLocalization("Chat.Chat29").Value,
                6 => this.GetLocalization("Chat.Chat30").Value,
                7 => this.GetLocalization("Chat.Chat31").Value,
                _ => this.GetLocalization("Chat.Chat3").Value,
            };
        }
    }
}