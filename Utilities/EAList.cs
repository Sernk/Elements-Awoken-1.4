using ElementsAwoken.Content.Items.Artifacts;
using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.BossDrops.Aqueous;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.BossDrops.Infernace;
using ElementsAwoken.Content.Items.BossDrops.Obsidious;
using ElementsAwoken.Content.Items.BossDrops.Permafrost;
using ElementsAwoken.Content.Items.BossDrops.Regaroth;
using ElementsAwoken.Content.Items.BossDrops.ScourgeFighter;
using ElementsAwoken.Content.Items.BossDrops.TheCelestial;
using ElementsAwoken.Content.Items.BossDrops.TheGuardian;
using ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.BossDrops.Wasteland;
using ElementsAwoken.Content.Items.BossSummons;
using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.Content.NPCs.Bosses.Aqueous;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using ElementsAwoken.Content.NPCs.Bosses.Obsidious;
using ElementsAwoken.Content.NPCs.Bosses.Permafrost;
using ElementsAwoken.Content.NPCs.Bosses.Regaroth;
using ElementsAwoken.Content.NPCs.Bosses.ScourgeFighter;
using ElementsAwoken.Content.NPCs.Bosses.TheGuardian;
using ElementsAwoken.Content.NPCs.Bosses.TheTempleKeepers;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Content.NPCs.Bosses.Volcanox;
using ElementsAwoken.Content.NPCs.Bosses.Wasteland;
using ElementsAwoken.Content.NPCs.ItemSets.ToySlime;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Utilities
{
    /// <summary>
    /// For moderators so that you can add an item. Сan be added as one of or as a new one
    /// <More details cref ="IDropSettings"></chec>
    /// You can only add 4 items that would be considered new
    /// </summary>
    public class EAList
    {
        public static readonly List<int> Artifact = [ModContent.ItemType<ChaosFlameFlask>(), ModContent.ItemType<ElementalArcanum>(), ModContent.ItemType<EtherealShell>(), ModContent.ItemType<FrozenGauntlet>(), ModContent.ItemType<GreatThunderTotem>(), ModContent.ItemType<Nanocore>()];
        #region Boss Loot      
        #region Ancients Loot
        public static readonly List<int> AncLot = [ModContent.ItemType<Chromacast>(), ModContent.ItemType<Shimmerspark>(), ModContent.ItemType<TheFundamentals>()];
        #endregion
        #region Aqueous Loot
        public static readonly List<int> AquLoot = [ModContent.ItemType<BrinyBuster>(), ModContent.ItemType<BubblePopper>(), ModContent.ItemType<HighTide>(), ModContent.ItemType<OceansRazor>(), ModContent.ItemType<TheWave>(), ModContent.ItemType<Varee>(),];
        #endregion
        #region Azana Loot
        public static readonly List<int> AzaLoot = [ModContent.ItemType<Anarchy>(), ModContent.ItemType<PurgeRifle>(), ModContent.ItemType<ChaoticImpaler>(), ModContent.ItemType<GleamOfAnnhialation>(), ModContent.ItemType<Pandemonium>(), ModContent.ItemType<AzanaMinionStaff>(),];
        #endregion
        #region Infernace Loot
        public static readonly List<int> InfeLoot = [ModContent.ItemType<FireBlaster>(), ModContent.ItemType<FlareSword>(), ModContent.ItemType<InfernoVortex>(), ModContent.ItemType<FireHarpyStaff>(),];
        #endregion
        #region Obsidious Loot
        public static readonly List<int> ObsiLoot = [ModContent.ItemType<Magmarox>(), ModContent.ItemType<TerreneScepter>(), ModContent.ItemType<Ultramarine>(), ModContent.ItemType<VioletEdge>()];
        #endregion
        #region Permafrost Loot
        public static readonly List<int> PermLoot = [ModContent.ItemType<IceReaver>(), ModContent.ItemType<Snowdrift>(), ModContent.ItemType<IceWrath>(), ModContent.ItemType<Flurry>()];
        #endregion
        #region Regaroth Loot
        public static readonly List<int> RegLoot = [ModContent.ItemType<EyeOfRegaroth>(), ModContent.ItemType<Starstruck>(), ModContent.ItemType<TheSilencer>(), ModContent.ItemType<EnergyStaff>(),];
        #endregion
        #region ScourgeFighter Loot
        public static readonly List<int> ScoLoot = [ModContent.ItemType<ScourgeSword>(), ModContent.ItemType<SignalBooster>(), ModContent.ItemType<ScourgeFighterMachineGun>(),];
        #endregion
        #region The Celestial Loot
        public static readonly List<int> TCelLoot = [ModContent.ItemType<CelestialInferno>(), ModContent.ItemType<Celestia>(), ModContent.ItemType<EyeballStaff>(), ModContent.ItemType<Solus>()];
        #endregion
        #region The Guardian Loot
        public static readonly List<int> TGuaLoot = [ModContent.ItemType<Godslayer>(), ModContent.ItemType<InfernoStorm>(), ModContent.ItemType<TemplesWrath>()];
        #endregion
        #region Temple Keepers Loot
        public static readonly List<int> TempLoot = [ModContent.ItemType<TemplesCrystal>(), ModContent.ItemType<GazeOfInferno>(), ModContent.ItemType<TheAllSeer>(), ModContent.ItemType<WyrmClaw>()];
        #endregion
        #region VoidLeviathan Loot
        public static readonly List<int> LeviLoot = [ModContent.ItemType<BladeOfTheNight>(), ModContent.ItemType<CosmicWrath>(), ModContent.ItemType<EndlessAbyssBlaster>(), ModContent.ItemType<ExtinctionBow>(), ModContent.ItemType<PikeOfEternalDespair>(), ModContent.ItemType<Reaperstorm>(), ModContent.ItemType<VoidInferno>(), ModContent.ItemType<VoidLeviathansAegis>(), ModContent.ItemType<BreathOfDarkness>(), ModContent.ItemType<LightsAffliction>()];
        #endregion
        #region Volcanox Loot
        public static readonly List<int> VolLoot = [ModContent.ItemType<Combustia>(), ModContent.ItemType<EmberBurst>(), ModContent.ItemType<FatesFlame>(), ModContent.ItemType<FirestarterStaff>(), ModContent.ItemType<Hearth>()];
        #endregion
        #region Wasteland Loot
        public static readonly List<int> WasLoot = [ModContent.ItemType<Pincer>(), ModContent.ItemType<ScorpionBlade>(), ModContent.ItemType<Stinger>(), ModContent.ItemType<ChitinStaff>()];
        #endregion
        #endregion
        #region Boss Summon
        public static readonly List<int> BossSummonList = [ModContent.ItemType<VoidLeviathanSummon>(), ModContent.ItemType<WastelandSummon>(), ModContent.ItemType<AncientDragonSummon>(), ModContent.ItemType<AqueousSummon>(), ModContent.ItemType<AzanaSummon>(), ModContent.ItemType<GuardianSummon>(), ModContent.ItemType<InfernaceSummon>(), ModContent.ItemType<ObsidiousSummon>(), ModContent.ItemType<PermafrostSummon>(), ModContent.ItemType<RadiantRainSummon>(), ModContent.ItemType<RegarothSummon>(), ModContent.ItemType<ScourgeFighterSummon>(), ModContent.ItemType<SlimeRainSummon>(), ModContent.ItemType<ToySlimeSummon>(), ModContent.ItemType<VoidEventSummon>(), ModContent.ItemType<VoidEventSummon2>(), ModContent.ItemType<VolcanoxSummon>(), ModContent.ItemType<AncientsSummon>()];
        #endregion
        #region Bosses
        public static readonly List<int> BossName = [ModContent.NPCType<AncientWyrmHead>(), ModContent.NPCType<TheEye>(), ModContent.NPCType<Aqueous>(), ModContent.NPCType<AzanaEye>(), ModContent.NPCType<Azana>(), ModContent.NPCType<Azana>(), ModContent.NPCType<Content.NPCs.Bosses.CosmicObserver.CosmicObserver>(), ModContent.NPCType<TheGuardian>(), ModContent.NPCType<TheGuardianFly>(), ModContent.NPCType<TheGuardianFly>(), ModContent.NPCType<Infernace>(), ModContent.NPCType<VoidLeviathanHead>(), ModContent.NPCType<Wasteland>(), ModContent.NPCType<ObsidiousHuman>(), ModContent.NPCType<Obsidious>(), ModContent.NPCType<Permafrost>(), ModContent.NPCType<RegarothHead>(), ModContent.NPCType<ScourgeFighter>(), ModContent.NPCType<ToySlime>(), ModContent.NPCType<Volcanox>(), ModContent.NPCType<Izaris>(), ModContent.NPCType<Kirvein>(), ModContent.NPCType<Krecheus>(), ModContent.NPCType<Xernon>(), ModContent.NPCType<ShardBase>()];
        #endregion;
        #region UndownerTerraria
        public static List<Func<bool>> BossFlagsGet = new()
        {
            () => MyWorld.downedAncients,
            () => MyWorld.downedAncientWyrm,
            () => MyWorld.downedAqueous,
            () => MyWorld.downedAzana,
            () => MyWorld.downedCosmicObserver,
            () => MyWorld.downedEye,
            () => MyWorld.downedGuardian,
            () => MyWorld.downedInfernace,
            () => MyWorld.downedObsidious,
            () => MyWorld.downedPermafrost,
            () => MyWorld.downedRadiantMaster,
            () => MyWorld.downedRegaroth,
            () => MyWorld.downedScourgeFighter,
            () => MyWorld.downedShadeWyrm,
            () => MyWorld.downedVoidLeviathan,
            () => MyWorld.downedVolcanox,
            () => MyWorld.downedWasteland
        };

        public static List<Action<bool>> BossFlagsSet = new()
        {
            v => MyWorld.downedAncients = v,
            v => MyWorld.downedAncientWyrm = v,
            v => MyWorld.downedAqueous = v,
            v => MyWorld.downedAzana = v,
            v => MyWorld.downedCosmicObserver = v,
            v => MyWorld.downedEye = v,
            v => MyWorld.downedGuardian = v,
            v => MyWorld.downedInfernace = v,
            v => MyWorld.downedObsidious = v,
            v => MyWorld.downedPermafrost = v,
            v => MyWorld.downedRadiantMaster = v,
            v => MyWorld.downedRegaroth = v,
            v => MyWorld.downedScourgeFighter = v,
            v => MyWorld.downedShadeWyrm = v,
            v => MyWorld.downedVoidLeviathan = v,
            v => MyWorld.downedVolcanox = v,
            v => MyWorld.downedWasteland = v
        };
        #endregion
        public static readonly Dictionary<string, DamageClass> damageTypes = new()
        {
            ["melee"] = DamageClass.Melee,
            ["ranged"] = DamageClass.Ranged,
            ["thrown"] = DamageClass.Throwing,
            ["magic"] = DamageClass.Magic,
            ["summon"] = DamageClass.Summon,
        };
    }
}