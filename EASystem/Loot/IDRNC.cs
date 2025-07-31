using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace ElementsAwoken.EASystem.Loot
{
    /// <summary>
    /// A condition that checks if a specific boss has been defeated or not.
    /// </summary>
    public class IDRNC : IItemDropRuleCondition
    {
        public enum BossType
        {
            KingSlime,
            EyeOfCthulhu,
            EaterOfWorlds,
            BrainOfCthulhu,
            BothEvilBosses,
            QueenBee,
            Skeletron,
            WallOfFlesh,
            Destroyer,
            Twins,
            SkeletronPrime,
            Plantera,
            Golem,
            DukeFishron,
            LunaticCultist,
            MoonLord,
            QueenSlime,
            Deerclops,
            EmpressOfLight,
            Custom,
            AwakenedMode
        }

        private readonly BossType bossType;
        private readonly int customBossID;
        private readonly bool requireDefeated;
        private readonly string description;

        /// <summary>
        /// Creates a condition that checks if a specific boss has been defeated or not.
        /// </summary>
        /// <param name="bossType">The type of boss to check</param>
        /// <param name="requireDefeated">If true, the condition passes when the boss is defeated. If false, it passes when the boss is not defeated.</param>
        /// <param name="customBossID">Only used when bossType is Custom</param>
        public IDRNC(BossType bossType, bool requireDefeated = true, int customBossID = -1)
        {
            this.bossType = bossType;
            this.requireDefeated = requireDefeated;
            this.customBossID = customBossID;

            string bossName = GetBossName();
            this.description = requireDefeated
                ? $"Drops after {bossName} has been defeated"
                : $"Drops before {bossName} has been defeated";
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            bool isDefeated = IsBossDefeated();
            return requireDefeated ? isDefeated : !isDefeated;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return description;
        }

        private bool IsBossDefeated()
        {
            return bossType switch
            {
                BossType.KingSlime => NPC.downedSlimeKing,
                BossType.EyeOfCthulhu => NPC.downedBoss1,
                BossType.EaterOfWorlds => NPC.downedBoss2,
                BossType.BrainOfCthulhu => NPC.downedBoss2,
                BossType.BothEvilBosses => NPC.downedBoss2,
                BossType.QueenBee => NPC.downedQueenBee,
                BossType.Skeletron => NPC.downedBoss3,
                BossType.WallOfFlesh => Main.hardMode,
                BossType.Destroyer => NPC.downedMechBoss1,
                BossType.Twins => NPC.downedMechBoss2,
                BossType.SkeletronPrime => NPC.downedMechBoss3,
                BossType.Plantera => NPC.downedPlantBoss,
                BossType.Golem => NPC.downedGolemBoss,
                BossType.DukeFishron => NPC.downedFishron,
                BossType.LunaticCultist => NPC.downedAncientCultist,
                BossType.MoonLord => NPC.downedMoonlord,
                BossType.QueenSlime => NPC.downedQueenSlime,
                BossType.Deerclops => NPC.downedDeerclops,
                BossType.EmpressOfLight => NPC.downedEmpressOfLight,
                BossType.Custom => IsBossDefeatedByID(customBossID),
                BossType.AwakenedMode => MyWorld.awakenedMode,
                _ => false,
            };
        }

        private bool IsBossDefeatedByID(int bossID)
        {
            return bossID switch
            {
                NPCID.KingSlime => NPC.downedSlimeKing,
                NPCID.EyeofCthulhu => NPC.downedBoss1,
                NPCID.EaterofWorldsHead => NPC.downedBoss2,
                NPCID.BrainofCthulhu => NPC.downedBoss2,
                NPCID.QueenBee => NPC.downedQueenBee,
                NPCID.SkeletronHead => NPC.downedBoss3,
                NPCID.WallofFlesh => Main.hardMode,
                NPCID.TheDestroyer => NPC.downedMechBoss1,
                NPCID.Retinazer => NPC.downedMechBoss2,
                NPCID.Spazmatism => NPC.downedMechBoss2,
                NPCID.SkeletronPrime => NPC.downedMechBoss3,
                NPCID.Plantera => NPC.downedPlantBoss,
                NPCID.Golem => NPC.downedGolemBoss,
                NPCID.DukeFishron => NPC.downedFishron,
                NPCID.CultistBoss => NPC.downedAncientCultist,
                NPCID.MoonLordCore => NPC.downedMoonlord,
                NPCID.QueenSlimeBoss => NPC.downedQueenSlime,
                NPCID.Deerclops => NPC.downedDeerclops,
                NPCID.HallowBoss => NPC.downedEmpressOfLight,
                _ => false,
            };
        }

        private string GetBossName()
        {
            return bossType switch
            {
                BossType.KingSlime => "King Slime",
                BossType.EyeOfCthulhu => "Eye of Cthulhu",
                BossType.EaterOfWorlds => "Eater of Worlds",
                BossType.BrainOfCthulhu => "Brain of Cthulhu",
                BossType.QueenBee => "Queen Bee",
                BossType.Skeletron => "Skeletron",
                BossType.WallOfFlesh => "Wall of Flesh",
                BossType.Destroyer => "The Destroyer",
                BossType.Twins => "The Twins",
                BossType.SkeletronPrime => "Skeletron Prime",
                BossType.Plantera => "Plantera",
                BossType.Golem => "Golem",
                BossType.DukeFishron => "Duke Fishron",
                BossType.LunaticCultist => "Lunatic Cultist",
                BossType.MoonLord => "Moon Lord",
                BossType.QueenSlime => "Queen Slime",
                BossType.Deerclops => "Deerclops",
                BossType.EmpressOfLight => "Empress of Light",
                BossType.BothEvilBosses => "Both Evil Bosses",
                BossType.Custom => GetBossNameByID(customBossID),
                _ => "Unknown Boss",
            };
        }
        private string GetBossNameByID(int bossID)
        {
            return bossID switch
            {
                NPCID.KingSlime => "King Slime",
                NPCID.EyeofCthulhu => "Eye of Cthulhu",
                NPCID.EaterofWorldsHead => "Eater of Worlds",
                NPCID.BrainofCthulhu => "Brain of Cthulhu",
                NPCID.QueenBee => "Queen Bee",
                NPCID.SkeletronHead => "Skeletron",
                NPCID.WallofFlesh => "Wall of Flesh",
                NPCID.TheDestroyer => "The Destroyer",
                NPCID.Retinazer => "The Twins",
                NPCID.Spazmatism => "The Twins",
                NPCID.SkeletronPrime => "Skeletron Prime",
                NPCID.Plantera => "Plantera",
                NPCID.Golem => "Golem",
                NPCID.DukeFishron => "Duke Fishron",
                NPCID.CultistBoss => "Lunatic Cultist",
                NPCID.MoonLordCore => "Moon Lord",
                NPCID.QueenSlimeBoss => "Queen Slime",
                NPCID.Deerclops => "Deerclops",
                NPCID.HallowBoss => "Empress of Light",
                _ => $"Boss (ID: {bossID})",
            };
        }
    }
}