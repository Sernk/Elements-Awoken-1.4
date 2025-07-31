using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EABiomes : ModBiome
    {
        public bool _leviathan = false;
        public bool _temple = false;
        public override bool IsBiomeActive(Player player)
        {
            _leviathan = NPC.AnyNPCs(ModContent.NPCType<VoidLeviathanHead>());
            _temple = BiomeTileCounterSystem._lizardTiles >= 50;
            if (player.GetModPlayer<MyPlayer>().useLeviathan = _leviathan) return _leviathan;
            if (MyPlayer.zoneTemple = _temple) return _temple;
            return false;
        }
        public override string BestiaryIcon => "ElementsAwoken/Extra/Bestiary/icon_small";
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public class Emptiness : EABiomes
        {
            public override string BackgroundPath => "";
        }
    }
}