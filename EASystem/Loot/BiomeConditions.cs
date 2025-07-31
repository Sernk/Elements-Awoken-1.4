using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Loot
{
    /// <summary>
    /// Conditions for checking if the player is in specific biomes or zones
    /// </summary>
    public class BiomeConditions
    {

        /// <summary>
        /// Condition that checks if the player in the sky
        /// </summary>
        public class Sky : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneSkyHeight;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player in Underworld
        /// </summary>
        public class Underworld : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneUnderworldHeight;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in the Nebula Pillar zone
        /// </summary>
        public class InNebulaTowerZone : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneTowerNebula;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in the Blood moon
        /// </summary>
        public class InbloodMoon : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return Main.bloodMoon;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in the Solar Pillar zone
        /// </summary>
        public class InSolarTowerZone : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneTowerSolar;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in the Vortex Pillar zone
        /// </summary>
        public class InVortexTowerZone : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneTowerVortex;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in the Stardust Pillar zone
        /// </summary>
        public class InStardustTowerZone : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneTowerStardust;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is at the beach
        /// </summary>
        public class InBeach : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneBeach;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in the dungeon
        /// </summary>
        public class InDungeon : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.ZoneDungeon;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that checks if the player is in a custom mod biome
        /// </summary>
        /// <typeparam name="T">The type of the mod biome</typeparam>
        public class InModBiome<T> : IItemDropRuleCondition where T : ModBiome
        {
            private readonly string biomeName;

            public InModBiome()
            {
                // Try to get a nice display name for the biome
                T biome = ModContent.GetInstance<T>();
                biomeName = biome?.DisplayName.Value ?? typeof(T).Name;
            }

            public bool CanDrop(DropAttemptInfo info)
            {
                return info.player.InModBiome<T>();
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return "S";
            }
        }

        /// <summary>
        /// Condition that combines multiple biome conditions with AND logic
        /// </summary>
        public class InMultipleBiomes : IItemDropRuleCondition
        {
            private readonly IItemDropRuleCondition[] conditions;
            private readonly string description;

            public InMultipleBiomes(params IItemDropRuleCondition[] conditions)
            {
                this.conditions = conditions;
                this.description = "S";
            }

            public InMultipleBiomes(string customDescription, params IItemDropRuleCondition[] conditions)
            {
                this.conditions = conditions;
                this.description = customDescription;
            }

            public bool CanDrop(DropAttemptInfo info)
            {
                foreach (var condition in conditions)
                {
                    if (!condition.CanDrop(info))
                        return false;
                }
                return true;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return description;
            }
        }

        /// <summary>
        /// Condition that checks if the player is in any of the specified biomes (OR logic)
        /// </summary>
        public class InAnyBiome : IItemDropRuleCondition
        {
            private readonly IItemDropRuleCondition[] conditions;
            private readonly string description;

            public InAnyBiome(params IItemDropRuleCondition[] conditions)
            {
                this.conditions = conditions;
                this.description = "S";
            }

            public InAnyBiome(string customDescription, params IItemDropRuleCondition[] conditions)
            {
                this.conditions = conditions;
                this.description = customDescription;
            }

            public bool CanDrop(DropAttemptInfo info)
            {
                foreach (var condition in conditions)
                {
                    if (condition.CanDrop(info))
                        return true;
                }
                return false;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return description;
            }
        }
    }
}