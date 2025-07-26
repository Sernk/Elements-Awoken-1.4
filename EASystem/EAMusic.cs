using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EAMusic : ModSceneEffect
    {
        public override int Music => MusicID.OverworldDay;
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override bool IsSceneEffectActive(Player player)
        {
            return MyWorld.credits;
        }
        public class VoidInvasionNightEffect : ModSceneEffect
        {
            public override int Music => MusicID.LunarBoss;
            public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

            public override bool IsSceneEffectActive(Player player)
            {
                return Main.invasionX == Main.spawnTileX && MyWorld.voidInvasionUp && !Main.dayTime && Main.time > 16220;
            }
        }
        public class VoidInvasionDayEffect : ModSceneEffect
        {
            public override int Music => MusicID.TheTowers;
            public override SceneEffectPriority Priority => SceneEffectPriority.Event;

            public override bool IsSceneEffectActive(Player player)
            {
                return Main.invasionX == Main.spawnTileX && MyWorld.voidInvasionUp && (Main.dayTime || Main.time <= 16220);
            }
        }
        public class EncounterEffect : ModSceneEffect
        {
            public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Blank");
            public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

            public override bool IsSceneEffectActive(Player player)
            {
                return ElementsAwoken.encounterTimer > 0;
            }
        }
    }
}