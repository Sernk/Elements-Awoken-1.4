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
    }
}