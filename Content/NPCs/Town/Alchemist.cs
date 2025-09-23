using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.NPCs.Town;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Town
{
    [AutoloadHead]
    public class Alchemist : ModNPC
    {
        private int Slot = 0;
        public override string Texture
        {
            get
            {
                return "ElementsAwoken/Content/NPCs/Town/Alchemist";
            }
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 40;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 30;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            AnimationType = NPCID.Wizard;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love)
                .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Wizard, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Hate)
                .SetNPCAffection(ModContent.NPCType<Storyteller>(), AffectionLevel.Hate);

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
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
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.TownNPCs.Alchemist"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (NPC.downedBoss2)
            {
                return true;
            }
            return false;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }
        public override List<string> SetNPCNameList()
        {
            return WorldGen.genRand.Next(4) switch
            {
                0 => [this.GetLocalizedValue("Name.Saul"),],
                1 => [this.GetLocalizedValue("Name.Darius"),],
                2 => [this.GetLocalizedValue("Name.Eliseo"),],
                3 => [this.GetLocalizedValue("Name.Cadmus"),],
                _ => [this.GetLocalizedValue("Name.Saul"),]
            };
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 30;
            knockback = 4f;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 6;
            randExtraCooldown = 10;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<SuperStinkPotion>();
            attackDelay = 1;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 10f;
            randomOffset = 2f;
        }

        const int NumberShops = 4;
        static int shopNumber = 0;
        public string text = "";
        public string shopText = "";
        public override void Load()
        {
            _ = this.GetLocalization("Chat.Say").Value;
            _ = this.GetLocalization("Chat.Say1").Value;
            _ = this.GetLocalization("Chat.Say2").Value;
            _ = this.GetLocalization("Chat.Say3").Value;
            _ = this.GetLocalization("Chat.Say4").Value;
            _ = this.GetLocalization("Chat.Say5").Value;
            _ = this.GetLocalization("Chat.Say6").Value; // Health and Mana
            _ = this.GetLocalization("Chat.Say7").Value; // Combat 
            _ = this.GetLocalization("Chat.Say8").Value; // Other
            _ = this.GetLocalization("Chat.Say9").Value; // Transmutation
            _ = this.GetLocalization("Chat.Say10").Value;

            _ = this.GetLocalization("ButtonName.Name").Value;
            _ = this.GetLocalization("ButtonName.Name1").Value;
            _ = this.GetLocalization("ButtonName.Name2").Value;
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(6))
            {
                case 0: text = this.GetLocalization("Chat.Say").Value; break;
                case 1: text = this.GetLocalization("Chat.Say1").Value; break;
                case 2: text = this.GetLocalization("Chat.Say2").Value; break;
                case 3: text = this.GetLocalization("Chat.Say3").Value; break;
                case 4: text = this.GetLocalization("Chat.Say4").Value; break;
                case 5: text = this.GetLocalization("Chat.Say5").Value; break;
                default:
                    return "default";
            }
            string addText = "";

            if (ModContent.GetInstance<Config>().alchemistPotions)
            {
                if (shopNumber == 0)
                {
                    shopText = this.GetLocalization("Chat.Say6").Value; ;
                }
                if (shopNumber == 1)
                {
                    shopText = this.GetLocalization("Chat.Say7").Value; ;
                }
                if (shopNumber == 2)
                {
                    shopText = this.GetLocalization("Chat.Say8").Value; ;
                }
                if (shopNumber == 3)
                {
                    shopText = this.GetLocalization("Chat.Say9").Value; ;
                }
                addText = $"\n\n{this.GetLocalization("Chat.Say10").Value} {shopText}";
            }
            return text + addText;
        }
        // Multiple Shops code. multishop
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            if (shopNumber == 3) button = this.GetLocalization("ButtonName.Name").Value;
            if (ModContent.GetInstance<Config>().alchemistPotions) button2 = this.GetLocalization("ButtonName.Name1").Value;
            else button2 = this.GetLocalization("ButtonName.Name2").Value;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                if (ModContent.GetInstance<Config>().alchemistPotions)
                {
                    if (shopNumber < 3) shopName = "AlchemistShop";
                    else
                    {
                        Main.playerInventory = true;
                        Main.npcChatText = "";
                        ModContent.GetInstance<ElementsAwoken>().AlchemistUserInterface.SetState(new EASystem.UI.AlchemistUI());
                    }
                }
                else
                {
                    shopName = "AlchemistShop";
                }
            }
            else
            {
                if (ModContent.GetInstance<Config>().alchemistPotions)
                {
                    shopNumber = shopNumber + 1 % NumberShops;
                    if (shopNumber > 3)
                    {
                        shopNumber = 0;
                    }
                    if (shopNumber == 0)
                    {
                        shopText = this.GetLocalization("Chat.Say6").Value;
                    }
                    if (shopNumber == 1)
                    {
                        shopText = this.GetLocalization("Chat.Say7").Value;
                    }
                    if (shopNumber == 2)
                    {
                        shopText = this.GetLocalization("Chat.Say8").Value;
                    }
                    if (shopNumber == 3)
                    {
                        shopText = this.GetLocalization("Chat.Say9").Value;
                    }
                    Main.npcChatText = text + $"\n\n{this.GetLocalization("Chat.Say10").Value} {shopText}";
                }
                else
                {
                    Main.playerInventory = true;
                    Main.npcChatText = "";
                    ModContent.GetInstance<ElementsAwoken>().AlchemistUserInterface.SetState(new EASystem.UI.AlchemistUI());
                }
            }
        }
        public override void AddShops()
        {
            Condition IfEnabled = new(EALocalization.ShopNeme(), () => ModContent.GetInstance<Config>().alchemistPotions);
            Condition ShopName0 = new(EALocalization.ShopNeme(0), () => shopNumber == 0);
            Condition ShopName1 = new(EALocalization.ShopNeme(1), () => shopNumber == 1);
            Condition ShopName2 = new(EALocalization.ShopNeme(2), () => shopNumber == 2);
            Condition BossName13 = new(EALocalization.BossName(13), () => NPC.downedSlimeKing);
            Condition BossName1 = new(EALocalization.BossName(1), () => NPC.downedBoss1);
            Condition BossName2 = new(EALocalization.BossName(2), () => NPC.downedBoss2);
            Condition BossName14 = new(EALocalization.BossName(14), () => NPC.downedQueenBee);
            Condition BossName3 = new(EALocalization.BossName(3), () => NPC.downedBoss3);
            Condition BossName4 = new(EALocalization.BossName(4), () => Main.hardMode);
            Condition AnyMechBoss = new(EALocalization.DownedMechBossAny, () => NPC.downedMechBossAny);
            Condition BossName12 = new(EALocalization.BossName(12), () => NPC.downedAncientCultist);
            Condition BossName11 = new(EALocalization.BossName(11), () => NPC.downedMoonlord);

            Condition SavedAngler = new(EALocalization.IWSM(), () => NPC.savedAngler);
            Condition WoroldEvilCo = new(EALocalization.IsWoroldCorruption, () => WorldGen.crimson);
            Condition WoroldEvilCr = new(EALocalization.IsWoroldCrimson, () => WorldGen.crimson == false);
            Condition MoonPhaseFull = new(EALocalization.MoonFull, () => Main.GetMoonPhase() == MoonPhase.Full);

            NPCShop shop = new(Type, "AlchemistShop");

            #region Shop0
            #region Healing Potions
            shop.Add(new Item(ItemID.LesserHealingPotion) { value = 7500 }, IfEnabled, ShopName0);
            shop.Add(new Item(ItemID.LesserHealingPotion) { value = 10000 }, IfEnabled, ShopName0, BossName2);
            shop.Add(new Item(ItemID.GreaterHealingPotion) { value = 75000 }, IfEnabled, ShopName0, BossName4);
            shop.Add(new Item(ItemID.SuperHealingPotion) { value = 100000 }, IfEnabled, ShopName0, BossName12);
            shop.Add(new Item(ModContent.ItemType<EpicHealingPotion>()) { value = 200000 }, IfEnabled, ShopName0, BossName11);
            #endregion

            #region Mana Potions
            shop.Add(new Item(ItemID.LesserManaPotion) { value = 7500 }, IfEnabled, ShopName0);
            shop.Add(new Item(ItemID.ManaPotion) { value = 10000 }, IfEnabled, ShopName0, BossName2);
            shop.Add(new Item(ItemID.GreaterManaPotion) { value = 75000 }, IfEnabled, ShopName0, BossName4);
            shop.Add(new Item(ItemID.SuperManaPotion) { value = 100000 }, IfEnabled, ShopName0, AnyMechBoss);
            #endregion

            #region Other
            shop.Add(new Item(ItemID.LesserRestorationPotion) { value = 10000 }, IfEnabled, ShopName0, BossName1);
            shop.Add(new Item(ItemID.RestorationPotion) { value = 20000 }, IfEnabled, ShopName0, BossName1);
            shop.Add(new Item(ItemID.RegenerationPotion) { value = 10000 }, IfEnabled, ShopName0, BossName1);
            shop.Add(new Item(ItemID.ManaRegenerationPotion) { value = 10000 }, IfEnabled, ShopName0, BossName1);
            #endregion
            #endregion

            #region Shop1
            #region Defense
            shop.Add(new Item(ItemID.IronskinPotion) { value = 10000 }, IfEnabled, ShopName1, BossName1);
            shop.Add(new Item(ModContent.ItemType<DemonPhilter>()) { value = 30000 }, IfEnabled, ShopName1, BossName2);
            shop.Add(new Item(ModContent.ItemType<HellFury>()) { value = 40000 }, IfEnabled, ShopName1, BossName4);
            shop.Add(new Item(ModContent.ItemType<CelestialEmpowerment>()) { value = 100000 }, IfEnabled, ShopName1, BossName11);
            #endregion

            #region Other2
            shop.Add(new Item(ItemID.ArcheryPotion) { value = 10000 }, IfEnabled, ShopName1);
            shop.Add(new Item(ItemID.EndurancePotion) { value = 10000 }, IfEnabled, ShopName1);
            shop.Add(new Item(ItemID.ThornsPotion) { value = 10000 }, IfEnabled, ShopName1);
            shop.Add(new Item(ItemID.TitanPotion) { value = 10000 }, IfEnabled, ShopName1);
            shop.Add(new Item(ItemID.SwiftnessPotion) { value = 10000 }, IfEnabled, ShopName1);

            shop.Add(new Item(ItemID.AmmoReservationPotion) { value = 10000 }, IfEnabled, ShopName1, BossName2);

            shop.Add(new Item(ItemID.SummoningPotion) { value = 10000 }, IfEnabled, ShopName1, BossName3);
            shop.Add(new Item(ItemID.GravitationPotion) { value = 10000 }, IfEnabled, ShopName1, BossName3);
            shop.Add(new Item(ItemID.HeartreachPotion) { value = 10000 }, IfEnabled, ShopName1, BossName3);
            shop.Add(new Item(ItemID.MagicPowerPotion) { value = 10000 }, IfEnabled, ShopName1, BossName3);
            shop.Add(new Item(ItemID.WrathPotion) { value = 10000 }, IfEnabled, ShopName1, BossName3);

            shop.Add(new Item(ItemID.InfernoPotion) { value = 30000 }, IfEnabled, ShopName1, BossName4);
            shop.Add(new Item(ItemID.RagePotion) { value = 30000 }, IfEnabled, ShopName1, BossName4);
            shop.Add(new Item(ItemID.LifeforcePotion) { value = 30000 }, IfEnabled, ShopName1, BossName4);
            #endregion
            #endregion

            #region Shop2
            #region Other 2
            shop.Add(new Item(ItemID.RecallPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.WormholePotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.InvisibilityPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.BuilderPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.FeatherfallPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.HunterPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.TrapsightPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.MiningPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.NightOwlPotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.ShinePotion) { value = 10000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ItemID.ThornsPotion) { value = 10000 }, IfEnabled, ShopName2);
            #endregion

            #region IWS
            shop.Add(new Item(ItemID.CratePotion) { value = 10000 }, IfEnabled, ShopName2, SavedAngler);
            shop.Add(new Item(ItemID.FishingPotion) { value = 10000 }, IfEnabled, ShopName2, SavedAngler);
            shop.Add(new Item(ItemID.FlipperPotion) { value = 10000 }, IfEnabled, ShopName2, SavedAngler);
            shop.Add(new Item(ItemID.SonarPotion) { value = 10000 }, IfEnabled, ShopName2, SavedAngler);
            shop.Add(new Item(ItemID.GillsPotion) { value = 10000 }, IfEnabled, ShopName2, SavedAngler);
            #endregion

            #region Boss 3
            shop.Add(new Item(ItemID.BattlePotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ItemID.CalmingPotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ItemID.GravitationPotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ItemID.HeartreachPotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ItemID.ObsidianSkinPotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ItemID.WarmthPotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ItemID.SpelunkerPotion) { value = 10000 }, IfEnabled, ShopName2, BossName3);
           

            shop.Add(new Item(ItemID.TeleportationPotion) { value = 50000 }, IfEnabled, ShopName2, AnyMechBoss);
            shop.Add(new Item(ItemID.GenderChangePotion) { value = 100000 }, IfEnabled, ShopName2);
            shop.Add(new Item(ModContent.ItemType<ChaosPotion>()) { value = 50000 }, IfEnabled, ShopName2, BossName3);
            shop.Add(new Item(ModContent.ItemType<HavocPotion>()) { value = 100000 }, IfEnabled, ShopName2, AnyMechBoss);
            shop.Add(new Item(ModContent.ItemType<CalamityPotion>()) { value = 250000 }, IfEnabled, ShopName2, BossName11);
            #endregion
            #endregion

            #region Shop3
            shop.Add(new Item(ItemID.DaybloomSeeds) { value = Item.buyPrice(0, 0, 5, 0) });
            shop.Add(new Item(ItemID.MoonglowSeeds) { value = Item.buyPrice(0, 0, 5, 0) });
            shop.Add(new Item(ItemID.BlinkrootSeeds) { value = Item.buyPrice(0, 0, 5, 0) });
            shop.Add(new Item(ItemID.DeathweedSeeds) { value = Item.buyPrice(0, 0, 5, 0) });
            shop.Add(new Item(ItemID.WaterleafSeeds) { value = Item.buyPrice(0, 0, 5, 0) });
            shop.Add(new Item(ItemID.ShiverthornSeeds) { value = Item.buyPrice(0, 0, 5, 0) });
            shop.Add(new Item(ItemID.FireblossomSeeds) { value = Item.buyPrice(0, 0, 5, 0) });

            while (Slot < 3)
            {
                shop.Add(ModContent.ItemType<ModItem>());
                Slot++;
            }

            shop.Add(ItemID.DayBloomPlanterBox, BossName13);
            shop.Add(ItemID.MoonglowPlanterBox, BossName14);
            shop.Add(ItemID.BlinkrootPlanterBox, BossName1);
            shop.Add(ItemID.CorruptPlanterBox, BossName2, WoroldEvilCo);
            shop.Add(ItemID.CrimsonPlanterBox, BossName2, WoroldEvilCr);
            shop.Add(ItemID.WaterleafPlanterBox, BossName3);
            shop.Add(ItemID.ShiverthornPlanterBox, BossName3);
            shop.Add(ItemID.FireBlossomPlanterBox, BossName4);

            shop.Add(ModContent.ItemType<AlchemistsTimer>(), MoonPhaseFull);
            #endregion
            shop.Register();
        }
    }
}