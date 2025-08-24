using ElementsAwoken.Content.Events.RadiantRain.Enemies;
using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2.ShadeWyrm;
using ElementsAwoken.Content.Items.BossSummons;
using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.Content.NPCs.Bosses.Aqueous;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.Content.NPCs.Bosses.CosmicObserver;
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
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.EASystem.ModSupport
{
    public class BossChecklistSuport : ModSystem
    {
        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
        }
        private void DoBossChecklistIntegration()
        {
            /* 
            Boss Progress
            SlimeKing = 1f;
            EyeOfCthulhu = 2f;
            EaterOfWorlds = 3f;
            QueenBee = 4f;
            Skeletron = 5f;
            1.4_1 = 6f  
            WallOfFlesh = 7f;
            1.4_2 = 8f  
            TheTwins = 9f;
            TheDestroyer = 10f;
            SkeletronPrime = 11f;
            Plantera = 12f;
            Golem = 13f;
            DukeFishron = 14f;
            1.4_3 = 15f  
            LunaticCultist = 16f;
            Moonlord = 17f;*/

            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalName = "WastelandBoss";
            float weight = 2.5f;
            Func<bool> downed = () => MyWorld.downedWasteland;
            int bossType = NPCType<Wasteland>();
            int spawnItem;
            List<int> collectibles = [];

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName,
               weight,
               downed,
               bossType,
               new Dictionary<string, object>()
               {
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "InfernaceBoss",
               weight = 6.5f,
               downed = () => MyWorld.downedInfernace,
               bossType = NPCType<Infernace>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<InfernaceSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogMiniBoss",
               Mod,
               internalName = "CosmicObserverBoss",
               weight = 8.1f,
               downed = () => MyWorld.downedCosmicObserver,
               bossType = NPCType<CosmicObserver>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<CosmicObserverSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "ScourgeFighterBoss",
               weight = 10.3f,
               downed = () => MyWorld.downedScourgeFighter,
               bossType = NPCType<ScourgeFighter>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<ScourgeFighterSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "RegarothBoss",
               weight = 11.4f,
               downed = () => MyWorld.downedRegaroth,
               bossType = NPCType<RegarothHead>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<RegarothSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "PermafrostBoss",
               weight = 12.4f,
               downed = () => MyWorld.downedPermafrost,
               bossType = NPCType<Permafrost>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<PermafrostSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "ObsidiousBoss",
               weight = 12.5f,
               downed = () => MyWorld.downedObsidious,
               bossType = NPCType<Obsidious>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<ObsidiousSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "AqueousBoss",
               weight = 13.1f,
               downed = () => MyWorld.downedAqueous,
               bossType = NPCType<Aqueous>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<AqueousSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "KeepersBoss",
               weight = 18.1f,
               downed = () => (MyWorld.downedAncientWyrm && MyWorld.downedEye),
               bossType = NPCType<TheEye>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<AncientDragonSummon>()
               }
            );

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "GuardianBoss",
               weight = 18.2f,
               downed = () => MyWorld.downedGuardian,
               bossType = NPCType<TheGuardian>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<GuardianSummon>()
               }
            );
            // EVENT 
            bossChecklistMod.Call(
               "LogEvent",
               Mod,
               internalName = "DotvEvent",
               weight = 18.4f,
               downed = () => MyWorld.downedVoidEvent,
               bossType = NPCType<ShadeWyrmTail>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<VoidEventSummon>()
               }
            );
            bossChecklistMod.Call(
               "LogEvent",
               Mod,
               internalName = "radiantRainEvent",
               weight = 18.75f,
               downed = () => MyWorld.downedRadiantMaster,
               bossType = NPCType<RadiantMaster>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<RadiantRainSummon>()
               }
            );
            bossChecklistMod.Call(
               "LogMiniBoss",
               Mod,
               internalName = "RadiantMasterBoss",
               weight = 18.76f,
               downed = () => MyWorld.downedRadiantMaster,
               bossType = NPCType<RadiantMaster>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<RadiantRainSummon>()
               }
            );
            // END EVENT
            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "ShadeWyrmEvent",
               weight = 18.5f,
               downed = () => MyWorld.downedShadeWyrm,
               bossType = NPCType<ShadeWyrmHead>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<VoidEventSummon>()
               }
            );
            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "VolcanoxEvent",
               weight = 18.5f,
               downed = () => MyWorld.downedVolcanox,
               bossType = NPCType<Volcanox>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<VolcanoxSummon>()
               }
            );
            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "VleviEvent",
               weight = 18.6f,
               downed = () => MyWorld.downedVoidLeviathan,
               bossType = NPCType<VoidLeviathanHead>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<VoidLeviathanSummon>()
               }
            );
            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               internalName = "AzanaBoss",
               weight = 18.7f,
               downed = () => MyWorld.downedAzana,
               bossType = NPCType<Azana>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<AzanaSummon>()
               }
            );
            bossChecklistMod.Call(
            "LogBoss",
               Mod,
               internalName = "ancientsBoss",
               weight = 18.8f,
               downed = () => MyWorld.downedAncients,
               bossType = NPCType<AncientAmalgam>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = spawnItem = ItemType<AncientsSummon>()
               }
            );
        }
    }
}