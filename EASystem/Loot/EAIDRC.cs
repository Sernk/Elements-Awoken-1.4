using ElementsAwoken.Content.NPCs.Bosses.TheTempleKeepers;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Loot
{
    /// <summary>
    /// EAIDRC = Elements Awoken Item Drop Rule Condition
    /// </summary>
    public class EAIDRC
    {
        public class ScourgeLootCondition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (info.player == null)
                {
                    return false;
                }
                if (Main.rand.Next(4) == 3)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => ModContent.GetInstance<EALocalization>().ScourgeLootCondition;
        }
        public class AwakenedModeActive : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {      
                if (MyWorld.awakenedMode)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => ModContent.GetInstance<EALocalization>().AwakenedModeActive;
        }
        public class DropSlot : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => true;
            public bool CanShowItemDropInUI()
            {
                if (IDropSettings.AncItemId1 == ModContent.ItemType<ModItem>())
                {
                    return false;
                }
                return true;
            }
            public string GetConditionDescription() => null;
        }
        public class DropSlot2 : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => true;
            public bool CanShowItemDropInUI()
            {
                if (IDropSettings.AncItemId2 == ModContent.ItemType<ModItem>())
                {
                    return false;
                }
                return true;
            }
            public string GetConditionDescription() => null;
        }
        public class DropSlot3 : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => true;
            public bool CanShowItemDropInUI()
            {
                if (IDropSettings.AncItemId3 == ModContent.ItemType<ModItem>())
                {
                    return false;
                }
                return true;
            }
            public string GetConditionDescription() => null;
        }
        public class DropSlot4 : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info) => true;
            public bool CanShowItemDropInUI()
            {
                if (IDropSettings.AncItemId4 == ModContent.ItemType<ModItem>())
                {
                    return false;
                }
                return true;
            }
            public string GetConditionDescription() => null;
        }
        public class DropVoidEssenceExpert : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (Main.expertMode && MyWorld.awakenedMode == false)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI()
            {
                if (Main.expertMode && MyWorld.awakenedMode == false)
                {
                    return true;
                }
                return false;
            }
            public string GetConditionDescription() => null;
        }
        public class DropVoidEssenceAwakened : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (MyWorld.awakenedMode)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI()
            {
                if (MyWorld.awakenedMode)
                {
                    return true;
                }
                return false;
            }
            public string GetConditionDescription() => null;
        }
        public class DropAncientWyrmHeadDeath : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<AncientWyrmHead>()))
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => ModContent.GetInstance<EALocalization>().AncientWyrmHeadDeath;
        }
        public class DropTheEyeDeath : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<TheEye>()))
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => ModContent.GetInstance<EALocalization>().TheEyeCondition;
        }
    }
}