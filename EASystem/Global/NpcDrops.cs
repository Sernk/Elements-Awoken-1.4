using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Accessories.Emblems;
using ElementsAwoken.Content.Items.Accessories.Teeth;
using ElementsAwoken.Content.Items.Armor.Vanity;
using ElementsAwoken.Content.Items.Artifacts.Materials;
using ElementsAwoken.Content.Items.BossDrops.zVanilla;
using ElementsAwoken.Content.Items.Donator.Crow;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Other;
using ElementsAwoken.Content.Items.Pets;
using ElementsAwoken.Content.Items.Tech.Generators;
using ElementsAwoken.Content.Items.Weapons.Magic;
using ElementsAwoken.Content.Items.Weapons.Melee;
using ElementsAwoken.Content.Items.Weapons.Ranged;
using ElementsAwoken.Content.NPCs.InfernoSpirit;
using ElementsAwoken.Content.NPCs.ItemSets.Mortemite;
using ElementsAwoken.Content.NPCs.ItemSets.ToySlime;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Loot.BiomeConditions;
using static ElementsAwoken.EASystem.Loot.EAIDRC;
using static ElementsAwoken.EASystem.Loot.InMultipleConditionByMode;
using static ElementsAwoken.EAUtilities.EAList;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.EASystem.Global
{
    public class NpcDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule FromStatue = new(new NPCSpawnedFromStatue());
            IItemDropRuleCondition[] conds;

            LeadingConditionRule Awakened;
            LeadingConditionRule Expert;
            LeadingConditionRule Normal; 

            if (npc.type == NPCID.KingSlime) npcLoot.Add(ItemDropRule.Common(ItemType<NinjaKatana>(), 3));
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<TearsOfSorrow>(), 3));
                npcLoot.Add(ItemDropRule.Common(ItemType<LensFragment>(), 1, 5, 15));
            }
            if (npc.type == NPCID.EaterofWorldsHead) npcLoot.Add(ItemDropRule.Common(ItemType<TheEater>(), 22));
            if (npc.type == NPCID.BrainofCthulhu) npcLoot.Add(ItemDropRule.Common(ItemType<SpinalSplayer>(), 3));
            if (npc.type == NPCID.SkeletronHead) npcLoot.Add(ItemDropRule.Common(ItemType<SkeletronFist>(), 3));
            if (npc.type == NPCID.Spazmatism) npcLoot.Add(ItemDropRule.Common(ItemType<Retinasm>(), 3));
            if (npc.type == NPCID.Retinazer) npcLoot.Add(ItemDropRule.Common(ItemType<Retinasm>(), 3));
            if (npc.type == NPCID.TheDestroyer) npcLoot.Add(ItemDropRule.Common(ItemType<TheDestroyer>(), 3));
            //if (npc.type == NPCID.SkeletronPrime) npcLoot.Add(ItemDropRule.Common(ItemType<PrimeCannon>(), 3));
            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<CelestialIdol>(), 6));
                npcLoot.Add(ItemDropRule.Common(ItemType<Shockstorm>(), 6));
                npcLoot.Add(ItemDropRule.Common(ItemType<Frosthail>(), 6));
                npcLoot.Add(ItemDropRule.Common(ItemType<Fireblast>(), 6));
                npcLoot.Add(ItemDropRule.Common(ItemType<Starthrower>(), 6));
                npcLoot.Add(ItemDropRule.Common(ItemType<EldritchKeepsake>(), 10));
            }
            if (npc.type == NPCID.TheDestroyer) npcLoot.Add(ItemDropRule.Common(ItemType<GreekFire>(), 9));
            if (EAList.VB.Contains(npc.type))
            {
                conds = [new NPCSpawnedFromStatue()];
                Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
                Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
                Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
                Awakened.OnSuccess(ItemDropRule.Common(ItemType<Awakener>(), 150)); npcLoot.Add(Awakened);
                Expert.OnSuccess(ItemDropRule.Common(ItemType<Awakener>(), 200)); npcLoot.Add(Expert);
                Normal.OnSuccess(ItemDropRule.Common(ItemType<Awakener>(), 250)); npcLoot.Add(Normal);
            }
            if (npc.type == NPCID.DukeFishron) npcLoot.Add(ItemDropRule.Common(ItemType<RoyalScale>(), 1, 5, 15));
            if (npc.type == NPCID.UndeadViking) npcLoot.Add(ItemDropRule.Common(ItemType<Warhorn>(), 15));
            if (npc.type == NPCID.Shark)
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<Warhorn>(), 15));
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<BabyShark>(), 15));
                npcLoot.Add(FromStatue); 
            }
            if (npc.type >= NPCID.Salamander && npc.type <= NPCID.Salamander9)
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<AxolotlMask>(), 15)); npcLoot.Add(FromStatue);
            }
            if ((npc.type == NPCID.Penguin || npc.type == NPCID.CrimsonPenguin || npc.type == NPCID.CorruptPenguin || npc.type == NPCID.PenguinBlack))
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<PenguinFeather>(), 3)); npcLoot.Add(FromStatue);
            }
            if (npc.type == NPCID.Mothron) npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroWhip>(), 3));
            if (npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing) npcLoot.Add(ItemDropRule.Common(ItemType<BrokenHeroWhip>(), 249));
            if (npc.type == NPCID.Golem) npcLoot.Add(ItemDropRule.Common(ItemType<SunFragment>(), 1, 10, 30));
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<DemonicFleshClump>(), 1, 10, 30));
                npcLoot.Add(ItemDropRule.Common(ItemType<ThrowerEmblem>(), 7));
            }
            if (npc.type == NPCID.GiantWormHead) npcLoot.Add(ItemDropRule.Common(ItemType<DiggerTooth>(), 10));
            if (npc.type == NPCID.DuneSplicerHead) npcLoot.Add(ItemDropRule.Common(ItemType<TombCrawlerTooth>(), 10));
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<MysticLeaf>(), 1, 1, 3));
                npcLoot.Add(ItemDropRule.Common(ItemType<MagicalHerbs>(), 1, 3, 8));
                npcLoot.Add(ItemDropRule.Common(ItemType<CrematedChaos>(), 8));
            }
            if (npc.type == NPCID.Tim)
            {
                conds = [new NPCSpawnedFromStatue()];
                Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
                Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
                Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
                Awakened.OnSuccess(ItemDropRule.Common(ItemType<IllusiveCharm>(), 1)); npcLoot.Add(Awakened);
                Expert.OnSuccess(ItemDropRule.Common(ItemType<IllusiveCharm>(), 2)); npcLoot.Add(Expert);
                Normal.OnSuccess(ItemDropRule.Common(ItemType<IllusiveCharm>(), 3)); npcLoot.Add(Normal);
            }
            if (npc.type == NPCID.FireImp)
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<ImpEar>(), 1, 1, 2)); npcLoot.Add(FromStatue);
            }
            if (npc.type == NPCID.LavaSlime)
            {
                conds = [new NPCSpawnedFromStatue()];
                Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
                Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
                Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
                Awakened.OnSuccess(ItemDropRule.Common(ItemType<MagmaCrystal>(), 3)); npcLoot.Add(Awakened);
                Expert.OnSuccess(ItemDropRule.Common(ItemType<MagmaCrystal>(), 4)); npcLoot.Add(Expert);
                Normal.OnSuccess(ItemDropRule.Common(ItemType<MagmaCrystal>(), 5)); npcLoot.Add(Normal);
            }
            if (npc.type == NPCID.Hellbat)
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<HellbatWing>(), 79)); npcLoot.Add(FromStatue);
            }
            if (npc.type == NPCID.Lavabat)
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<HellbatWing>(), 39)); npcLoot.Add(FromStatue);
            }
            if (npc.type == NPCID.Harpy)
            {
                FromStatue.OnSuccess(ItemDropRule.Common(ItemType<LightningCloud>(), 99)); npcLoot.Add(FromStatue);
            }
            if (npc.type == NPCID.Zombie)
            {
                conds = [new NPCSpawnedFromStatue(), new NoHardMode()];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<SteakShield>(), 12)); npcLoot.Add(Normal);
            }
            if (DeserNPC.Contains(npc.type))
            {
                conds = [new NPCSpawnedFromStatue(), new BiomeConditions(BiomeID.Desert)];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<DiscordantAmber>(), 1000)); npcLoot.Add(Normal);
            }
            if (HellNPC.Contains(npc.type))
            {
                conds = [new NPCSpawnedFromStatue(), new BiomeConditions(BiomeID.Underworld)];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<FieryJar>(), 1000)); npcLoot.Add(Normal);
            }
            if (SkyNPC.Contains(npc.type))
            {
                conds = [new NPCSpawnedFromStatue(), new BiomeConditions(BiomeID.Sky)];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<StrangeTotem>(), 800)); npcLoot.Add(Normal);
            }
            if (FrostNPC.Contains(npc.type) || FrostNPC_2.Contains(npc.type))
            {
                conds = [new NPCSpawnedFromStatue(), new BiomeConditions(BiomeID.Frost)];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<GlowingSlush>(), 1000)); npcLoot.Add(Normal);
            }
            if (BeachNPC.Contains(npc.type))
            {
                conds = [new NPCSpawnedFromStatue(), new BiomeConditions(BiomeID.InBeach)];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<OddWater>(), 1000)); npcLoot.Add(Normal);
            }
            if (VoidNPC.Contains(npc.type))
            {
                conds = [new BiomeConditions(BiomeID.InVoid, INVoid: true, VoidText: GetInstance<EALocalization>().InVoid)];
                Normal = new(new InMultipleConditionByMode(Mode.All, conds));
                Normal.OnSuccess(ItemDropRule.Common(ItemType<SoulOfPlight>(), 1000)); npcLoot.Add(Normal);
            }
        }
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
            if (npc.boss)
            {
                int dropChance = Main.expertMode ? MyWorld.awakenedMode ? 150 : 200 : 250;
                if (Main.rand.Next(dropChance) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Awakener>());
                }
            }
            if (Main.hardMode && Main.rand.Next(1499) == 0 && !npc.SpawnedFromStatue)
            {
                Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<InfinityCrys>());
            }
            if (Main.bloodMoon && Main.rand.Next(19) == 0 && !npc.SpawnedFromStatue)
            {
                Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<FleshClump>(), Main.rand.Next(1, 2));
            }
            if (player.ZoneRockLayerHeight && !player.ZoneDungeon && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(400) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<AssassinsKnife>(), 1);
                }
            }
            if (player.ZoneRockLayerHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(3999) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Tomato>(), 1);
                }
            }
            if (player.ZoneDesert && !player.ZoneBeach && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1250) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DiscordantAmber>(), 1);
                }
            }
            if (player.ZoneUnderworldHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1250) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<FieryJar>(), 1);
                }
            }
            if (player.ZoneSkyHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(800) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<StrangeTotem>(), 1);
                }
            }
            if (player.ZoneSnow && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1250) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<GlowingSlush>(), 1);
                }
            }
            if (player.ZoneBeach && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1250) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<OddWater>(), 1);
                }
            }
            #region Enemy Spawns
            if (NPC.downedMoonlord) //EoC
            {
                if (player.ZoneDungeon && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(9) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC(npc.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, NPCType<InfernoSpirit>());
                    }
                }
            }
            if (NPC.downedMoonlord) //EoC
            {
                if (npc.type == NPCID.Bunny)
                {
                    if (Main.rand.Next(29) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC(npc.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, NPCType<GiantTick>());
                    }
                }
                if (npc.type == NPCID.Squirrel)
                {
                    if (Main.rand.Next(29) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC(npc.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, NPCType<GiantTick>());
                    }
                }
                if (npc.type == NPCID.SquirrelRed)
                {
                    if (Main.rand.Next(29) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC(npc.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, NPCType<GiantTick>());
                    }
                }
            }
            #endregion

            if (player.GetModPlayer<PlayerEnergy>().soulConverter)
            {
                if (Main.rand.Next(12) == 0)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<EnergyPickup>(), 1);
                }
            }
            if (Main.slimeRain && Main.slimeRainNPC[npc.type] && !NPC.AnyNPCs(NPCType<ToySlime>()) && NPC.downedBoss3)
            {
                int killsRequired = 75;
                if (NPC.downedSlimeKing)
                {
                    killsRequired /= 2;
                }
                if (Main.slimeRainKillCount == killsRequired)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, NPCType<ToySlime>());
                }
            }
            if (npc.type == NPCID.MoonLordCore && !ElementsAwoken.ancientsAwakenedEnabled && MyWorld.moonlordKills < 5)
            {
                MyWorld.moonlordKills++;
                MyWorld.genLuminite = true;
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }
        }  
    }
}