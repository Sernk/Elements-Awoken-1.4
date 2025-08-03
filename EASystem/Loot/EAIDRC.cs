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
    }
}