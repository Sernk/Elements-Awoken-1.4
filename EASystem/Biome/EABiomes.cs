using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.Content.NPCs.Bosses.Aqueous;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using ElementsAwoken.Content.NPCs.Bosses.Permafrost;
using ElementsAwoken.Content.NPCs.Bosses.Regaroth;
using ElementsAwoken.Content.NPCs.Bosses.TheGuardian;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Content.NPCs.Bosses.Volcanox;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Biome
{
    public class EABiomes : ModBiome
    {
        // Boss
        public bool _useAncients = false;        
        public bool _useAzana = false;
        public bool _leviathan = false;
        public bool _volcanox = false;
        public bool _useGuardian = false;                  
        public bool _useAqueous = false;
        public bool _useAqueousSky = false;
        public bool _usePermafrost = false;
        public bool _useInfernace = false;
        // Event
        public bool _useVoidEvent = false;
        public bool _useVoidEventDark = false;
        public bool _useRadRain = false;
        // Encounter
        public bool _useEncounter1 = false;
        public bool _useEncounter2 = false;
        public bool _useEncounter3 = false;
        // ??
        public bool _useDespair = false;
        public bool _useblizzard = false;
        public bool _useInfWrath = false;
        public bool _useInfWrath2 = false;
        // Biomes
        public bool _temple = false;
        public override bool IsBiomeActive(Player player)
        {
            #region Boss
            _useAncients = NPC.AnyNPCs(ModContent.NPCType<Izaris>()) || NPC.AnyNPCs(ModContent.NPCType<Kirvein>()) || NPC.AnyNPCs(ModContent.NPCType<Krecheus>()) || NPC.AnyNPCs(ModContent.NPCType<Xernon>()) || NPC.AnyNPCs(ModContent.NPCType<AncientAmalgam>());
            _useAzana = NPC.AnyNPCs(ModContent.NPCType<Azana>());
            _leviathan = NPC.AnyNPCs(ModContent.NPCType<VoidLeviathanHead>());
            _volcanox = NPC.AnyNPCs(ModContent.NPCType<Volcanox>());
            _useGuardian = NPC.AnyNPCs(ModContent.NPCType<TheGuardianFly>());
            _useAqueous = NPC.AnyNPCs(ModContent.NPCType<Aqueous>());
            _usePermafrost = NPC.AnyNPCs(ModContent.NPCType<Permafrost>());
            _useInfernace = NPC.AnyNPCs(ModContent.NPCType<Infernace>());      
            _temple = BiomeTileCounterSystem._lizardTiles >= 50;

            if (player.GetModPlayer<MyPlayer>().useAncients = _useAncients) return _useAncients;
            if (player.GetModPlayer<MyPlayer>().useAzana = _useAzana) return _useAzana;
            if (player.GetModPlayer<MyPlayer>().useLeviathan = _leviathan) return _leviathan;
            if (player.GetModPlayer<MyPlayer>().useVolcanox = _volcanox) return _volcanox;
            if (player.GetModPlayer<MyPlayer>().useGuardian = _useGuardian) return _useGuardian;
            if (player.GetModPlayer<MyPlayer>().useAqueous = _useAqueous) return _useAqueous;
            if (player.GetModPlayer<MyPlayer>().usePermafrost = _usePermafrost) return _usePermafrost;
            if (player.GetModPlayer<MyPlayer>().useInfernace = _useInfernace) return _useInfernace;
            if (MyPlayer.zoneTemple = _temple) return _temple;
            #endregion

            #region Event
            _useVoidEvent = MyWorld.voidInvasionUp && Main.time <= 16220 && !Main.dayTime;
            _useVoidEventDark = MyWorld.voidInvasionUp && Main.time > 16220 && !Main.dayTime;
            _useRadRain = MyWorld.radiantRain && player.position.Y / 16 < Main.worldSurface;

            if (player.GetModPlayer<MyPlayer>().useVoidEvent = _useVoidEvent) return _useVoidEvent;
            if (player.GetModPlayer<MyPlayer>().useVoidEventDark = _useVoidEventDark) return _useVoidEventDark;
            if (player.GetModPlayer<MyPlayer>().useRadRain = _useRadRain) return _useRadRain;
            #endregion

            #region Regaroth
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<RegarothHead>())
                {
                    if (Main.npc[i].life > Main.npc[i].lifeMax / 2)
                    {
                        player.GetModPlayer<MyPlayer>().useRegaroth = 1;
                        if (Main.npc[i].localAI[1] == 1)
                        {
                            player.GetModPlayer<MyPlayer>().useRegaroth = 3;
                        }
                    }
                    else
                    {
                        player.GetModPlayer<MyPlayer>().useRegaroth = 2;
                        if (Main.npc[i].localAI[1] == 1)
                        {
                            player.GetModPlayer<MyPlayer>().useRegaroth = 4;
                        }
                    }
                }
                if (Main.npc[i].active == false && Main.npc[i].type == ModContent.NPCType<RegarothHead>())
                {
                    player.GetModPlayer<MyPlayer>().useRegaroth = 0;
                }
            }
            #endregion

            #region Encounter
            _useEncounter1 = ElementsAwoken.encounter == 1;
            _useEncounter2 = ElementsAwoken.encounter == 2;
            _useEncounter3 = ElementsAwoken.encounter == 3;

            if (player.GetModPlayer<MyPlayer>().useEncounter1 = _useEncounter1) return _useEncounter1;
            if (player.GetModPlayer<MyPlayer>().useEncounter2 = _useEncounter2) return _useEncounter2;
            if (player.GetModPlayer<MyPlayer>().useEncounter3 = _useEncounter3) return _useEncounter3;
            #endregion

            #region ??
            _useDespair = player.GetModPlayer<MyPlayer>().voidEnergyTimer > 0 || player.GetModPlayer<MyPlayer>().voidWalkerAura > 0;
            _useblizzard = MyWorld.hailStormTime > 0 && player.ZoneOverworldHeight && !player.ZoneDesert && !player.GetModPlayer<MyPlayer>().ActiveBoss() && !ModContent.GetInstance<Config>().lowDust;
            _useInfWrath = MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && !player.GetModPlayer<MyPlayer>().ActiveBoss() && !ModContent.GetInstance<Config>().promptsDisabled;
            _useInfWrath2 = MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && !player.GetModPlayer<MyPlayer>().ActiveBoss() && !ModContent.GetInstance<Config>().promptsDisabled;

            if (player.GetModPlayer<MyPlayer>().useDespair = _useDespair) return _useDespair;
            if (player.GetModPlayer<MyPlayer>().useblizzard = _useblizzard) return _useblizzard;
            if (player.GetModPlayer<MyPlayer>().useInfWrath = _useInfWrath) return _useInfWrath;
            if (player.GetModPlayer<MyPlayer>().useInfWrath = _useInfWrath2 && player.position.Y / 16 < Main.worldSurface) return _useInfWrath2;
            #endregion

            return false;
        }
        public override string BestiaryIcon => "ElementsAwoken/Extra/Bestiary/icon_small";
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public class Emptiness : EABiomes
        {
            public override string BackgroundPath => "";
            public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        }
        public class RadiantRainBiome : ModBiome
        {
            public override string BestiaryIcon => "ElementsAwoken/Extra/Bestiary/RadiantRainIcon";
            public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        }
        public class DOTVBiome : ModBiome
        {
            public override string BestiaryIcon => "ElementsAwoken/Extra/Bestiary/DawnOfTheVoidBestiary";
            public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        }
    }
}