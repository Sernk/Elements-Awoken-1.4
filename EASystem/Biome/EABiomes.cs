using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using ElementsAwoken.Content.NPCs.Bosses.Permafrost;
using ElementsAwoken.Content.NPCs.Bosses.TheGuardian;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Content.NPCs.Bosses.Volcanox;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EABiomes : ModBiome
    {
        public bool _useAncients = false;
        public bool _leviathan = false;
        public bool _volcanox = false;
        public bool _useGuardian = false;
        public bool _useInfernace = false;
        public bool _temple = false;
        public bool _usePermafrost = false;
        public bool _useAzana = false;
        public override bool IsBiomeActive(Player player)
        {
            _useAncients = NPC.AnyNPCs(ModContent.NPCType<Izaris>()) || NPC.AnyNPCs(ModContent.NPCType<Kirvein>()) || NPC.AnyNPCs(ModContent.NPCType<Krecheus>()) || NPC.AnyNPCs(ModContent.NPCType<Xernon>()) || NPC.AnyNPCs(ModContent.NPCType<AncientAmalgam>());
            _useAzana = NPC.AnyNPCs(ModContent.NPCType<Azana>());
            _leviathan = NPC.AnyNPCs(ModContent.NPCType<VoidLeviathanHead>());
            _volcanox = NPC.AnyNPCs(ModContent.NPCType<Volcanox>());
            _useGuardian = NPC.AnyNPCs(ModContent.NPCType<TheGuardianFly>());
            _useInfernace = NPC.AnyNPCs(ModContent.NPCType<Infernace>());
            _usePermafrost = NPC.AnyNPCs(ModContent.NPCType<Permafrost>());
            _temple = BiomeTileCounterSystem._lizardTiles >= 50;

            if (player.GetModPlayer<MyPlayer>().useAncients = _useAncients) return _useAncients;
            if (player.GetModPlayer<MyPlayer>().useAzana = _useAzana) return _useAzana;
            if (player.GetModPlayer<MyPlayer>().useLeviathan = _leviathan) return _leviathan;
            if (player.GetModPlayer<MyPlayer>().useVolcanox = _volcanox) return _volcanox;
            if (player.GetModPlayer<MyPlayer>().useGuardian = _useGuardian) return _useGuardian;
            if (player.GetModPlayer<MyPlayer>().useInfernace = _useInfernace) return _useInfernace;
            if (player.GetModPlayer<MyPlayer>().usePermafrost = _usePermafrost) return _usePermafrost;
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