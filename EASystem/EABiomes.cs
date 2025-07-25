using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EABiomes : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            bool _temple = BiomeTileCounterSystem._lizardTiles >= 50;
            bool _leviathan = NPC.AnyNPCs(ModContent.NPCType<VoidLeviathanHead>());
            if (MyPlayer.zoneTemple = _temple) return _leviathan;
            if (player.GetModPlayer<MyPlayer>().useLeviathan = _leviathan) return _leviathan;
            return false;
        }
    }
}
