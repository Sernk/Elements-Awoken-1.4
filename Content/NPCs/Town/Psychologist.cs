using ElementsAwoken.Content.Buffs;
using ElementsAwoken.EASystem;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Town
{
    [AutoloadHead]
    public class Psychologist : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Town/Psychologist"; } }
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
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
                .SetBiomeAffection<MushroomBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Wizard, AffectionLevel.Like)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Like)
                .SetNPCAffection(ModContent.NPCType<Storyteller>(), AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Hate);

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
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.TownNPCs.Psychologist"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (MyWorld.downedToySlime && MyWorld.awakenedMode)
            {
                return true;
            }
            return false;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }
        public override List<string> SetNPCNameList() // Drew Sam Alex Dan
        {
            return WorldGen.genRand.Next(4) switch
            {
                0 => [this.GetLocalizedValue("Name.Drew"),],
                1 => [this.GetLocalizedValue("Name.Sam"),],
                2 => [this.GetLocalizedValue("Name.Alex"),],
                3 => [this.GetLocalizedValue("Name.Dan"),],
                _ => [this.GetLocalizedValue("Name.Drew"),]
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
            projType = 712;
            attackDelay = 1;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 4f;
            randomOffset = 1f;
        }
        public override void Load()
        {
            _ = this.GetLocalization("Chat.Say").Value;
            _ = this.GetLocalization("Chat.Say1").Value;
            _ = this.GetLocalization("Chat.Say2").Value;
            _ = this.GetLocalization("Chat.Say3").Value;
            _ = this.GetLocalization("Chat.Say4").Value;
            _ = this.GetLocalization("Chat.Say5").Value;
            _ = this.GetLocalization("Chat.Say6").Value;
            _ = this.GetLocalization("Chat.Say7").Value;
            _ = this.GetLocalization("Chat.Say8").Value;
            _ = this.GetLocalization("Chat.Say9").Value;

            _ = this.GetLocalization("Button").Value;
        }
        public override string GetChat()
        {
            Player player = Main.LocalPlayer;
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();

            string text = "";
            switch (Main.rand.Next(4))
            {
                case 0: text = this.GetLocalization("Chat.Say").Value; break;
                case 1: text = this.GetLocalization("Chat.Say1").Value; break;
                case 2: text = this.GetLocalization("Chat.Say2").Value; break;
                case 3: text = this.GetLocalization("Chat.Say3").Value; break;
                default:
                    return "default";
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                text = this.GetLocalization("Chat.Say4").Value; ;
            }
            if (Main.rand.Next(10) == 0 && MyWorld.downedVolcanox && !MyWorld.downedVoidLeviathan)
            {
                text = this.GetLocalization("Chat.Say5").Value; ;
            }
            if (Main.rand.Next(10) == 0 && modPlayer.sanity < 50)
            {
                text = this.GetLocalization("Chat.Say6").Value; ;
            }
            if (Main.rand.Next(10) == 0 && !Main.dayTime)
            {
                text = this.GetLocalization("Chat.Say7").Value; ;
            }
            if (Main.rand.Next(10) == 0 && NPC.downedMoonlord)
            {
                text = this.GetLocalization("Chat.Say8").Value; ;
            }
            if (Main.bloodMoon)
            {
                text = this.GetLocalization("Chat.Say9").Value; ;
            }
            return text;
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            int price = (int)(Item.buyPrice(0,0,30,0) * GetPriceScale());

            button = Language.GetTextValue($"{this.GetLocalization("Button").Value} ({GetPriceString(price)})");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                SoundEngine.PlaySound(SoundID.Item29, NPC.position);
                Player player = Main.LocalPlayer;
                player.AddBuff(ModContent.BuffType<PsychologistSanity>(), 3600 * 3);
                Main.LocalPlayer.BuyItem((int)(Item.buyPrice(0, 0, 30, 0) * GetPriceScale()), -1);
            }
        }  
        private string GetPriceString(int price)
        {
            string text2 = "";
            int platinum = 0;
            int gold = 0;
            int silver = 0;
            int copper = 0;

            if (price >= 1000000)
            {
                platinum = price / 1000000;
                price -= platinum * 1000000;
            }
            if (price >= 10000)
            {
                gold = price / 10000;
                price -= gold * 10000;
            }
            if (price >= 100)
            {
                silver = price / 100;
                price -= silver * 100;
            }
            if (price >= 1)
            {
                copper = price;
            }
            if (platinum > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        platinum,
                        " ",
                        Lang.inter[15].Value,
                        " "
                });
            }
            if (gold > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        gold,
                        " ",
                        Lang.inter[16].Value,
                        " "
                });
            }
            if (silver > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        silver,
                        " ",
                        Lang.inter[17].Value,
                        " "
                });
            }
            if (copper > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        copper,
                        " ",
                        Lang.inter[18].Value,
                        " "
                });
            }
            return text2;
        }
        public override void AI()
        {
            if (!MyWorld.awakenedMode) NPC.active = false;
        }
        private float GetPriceScale()
        {
            float scale = 0.5f;
            if (NPC.downedSlimeKing) scale += 0.25f;
            if (NPC.downedBoss1) scale += 0.25f;
            if (MyWorld.downedWasteland) scale += 0.25f;
            if (NPC.downedBoss2) scale += 0.25f;
            if (NPC.downedBoss3) scale += 0.5f;
            if (MyWorld.downedInfernace) scale += 0.5f;
            if (Main.hardMode) scale += 4f;
            if (NPC.downedMechBossAny) scale += 2f;
            if (MyWorld.downedScourgeFighter) scale += 1f;
            if (MyWorld.downedRegaroth) scale += 2f;
            if (NPC.downedPlantBoss) scale += 2f;
            if (MyWorld.downedPermafrost) scale += 2f;
            if (MyWorld.downedObsidious) scale += 1f;
            if (NPC.downedFishron) scale += 2f;
            if (MyWorld.downedAqueous) scale += 2f;
            if (NPC.downedMoonlord) scale += 10f;
            if (MyWorld.downedGuardian) scale += 3f;
            if (MyWorld.downedVolcanox) scale += 3f;
            if (MyWorld.downedVoidLeviathan) scale += 3f;
            if (MyWorld.downedAzana || MyWorld.sparedAzana) scale += 3f;
            if (MyWorld.downedAncients) scale += 3f;
            return scale;
        }
    }
}
