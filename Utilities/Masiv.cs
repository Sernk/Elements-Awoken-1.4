using ElementsAwoken.Content.Items.Artifacts;
using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.BossDrops.Aqueous;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using Terraria.ModLoader;

namespace ElementsAwoken.Utilities
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Masiv
    {
        public static int[] Artifact = new int[] { ModContent.ItemType<ChaosFlameFlask>(), ModContent.ItemType<ElementalArcanum>(), ModContent.ItemType<EtherealShell>(), ModContent.ItemType<FrozenGauntlet>(), ModContent.ItemType<GreatThunderTotem>(), ModContent.ItemType<Nanocore>() };
        #region Boss Loot
        #region VoidLeviathan
        public static int[] LeviLoot = new int[] { ModContent.ItemType<BladeOfTheNight>(), ModContent.ItemType<CosmicWrath>(), ModContent.ItemType<EndlessAbyssBlaster>(), ModContent.ItemType<ExtinctionBow>(), ModContent.ItemType<PikeOfEternalDespair>(), ModContent.ItemType<Reaperstorm>(), ModContent.ItemType<VoidInferno>(), ModContent.ItemType<VoidLeviathansAegis>(), ModContent.ItemType<BreathOfDarkness>(), ModContent.ItemType<LightsAffliction>() };
        #endregion
        #region Ancients
        public static int[] AncLot = new int[] { ModContent.ItemType<Chromacast>(), ModContent.ItemType<Shimmerspark>(), ModContent.ItemType<TheFundamentals>() };
        #endregion
        #region AquLoot
        public static int[] AquLoot = new int[] { ModContent.ItemType<BrinyBuster>(), ModContent.ItemType<BubblePopper>(), ModContent.ItemType<HighTide>(), ModContent.ItemType<OceansRazor>(), ModContent.ItemType<TheWave>(), ModContent.ItemType<Varee>(), };
        #endregion
        #endregion
    }
}