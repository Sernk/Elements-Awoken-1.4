using ElementsAwoken.Content.Items.Artifacts;
using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.BossDrops.Aqueous;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Utilities
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Masiv
    {
        public static readonly int[] Artifact = [ModContent.ItemType<ChaosFlameFlask>(), ModContent.ItemType<ElementalArcanum>(), ModContent.ItemType<EtherealShell>(), ModContent.ItemType<FrozenGauntlet>(), ModContent.ItemType<GreatThunderTotem>(), ModContent.ItemType<Nanocore>()];
        #region Boss Loot
        #region VoidLeviathan
        public static readonly int[] LeviLoot = [ModContent.ItemType<BladeOfTheNight>(), ModContent.ItemType<CosmicWrath>(), ModContent.ItemType<EndlessAbyssBlaster>(), ModContent.ItemType<ExtinctionBow>(), ModContent.ItemType<PikeOfEternalDespair>(), ModContent.ItemType<Reaperstorm>(), ModContent.ItemType<VoidInferno>(), ModContent.ItemType<VoidLeviathansAegis>(), ModContent.ItemType<BreathOfDarkness>(), ModContent.ItemType<LightsAffliction>()];
        #endregion
        #region Ancients
        public static readonly int[] AncLot = [ModContent.ItemType<Chromacast>(), ModContent.ItemType<Shimmerspark>(), ModContent.ItemType<TheFundamentals>()];
        #endregion
        #region AquLoot
        public static readonly int[] AquLoot = [ModContent.ItemType<BrinyBuster>(), ModContent.ItemType<BubblePopper>(), ModContent.ItemType<HighTide>(), ModContent.ItemType<OceansRazor>(), ModContent.ItemType<TheWave>(), ModContent.ItemType<Varee>(),];
        #endregion
        #region AzaLoot
        public static readonly int[] AzaLoot = [ModContent.ItemType<Anarchy>(), ModContent.ItemType<PurgeRifle>(), ModContent.ItemType<ChaoticImpaler>(), ModContent.ItemType<GleamOfAnnhialation>(), ModContent.ItemType<Pandemonium>(), ModContent.ItemType<AzanaMinionStaff>(),];
        #endregion
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