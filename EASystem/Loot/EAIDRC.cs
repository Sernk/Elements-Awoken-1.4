using ElementsAwoken.Content.NPCs.Bosses.TheTempleKeepers;
using ElementsAwoken.Utilities;
using MonoMod.RuntimeDetour;
using System.Reflection;
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
        public class Hooks : ModSystem
        {
            private Hook _getDropMasterItemInAwakenedMod;
            private Hook _getShowMasterItemInAwakenedMod;
            private Hook _getDescriptionMasterItemInAwakenedMod;

            public override void Load()
            {
                MethodInfo target = typeof(Conditions.IsMasterMode).GetMethod(nameof(Conditions.IsMasterMode.CanDrop), BindingFlags.Instance | BindingFlags.Public);
                _getDropMasterItemInAwakenedMod = new Hook(target, (GetDropDetour)Drop);

                MethodInfo target2 = typeof(Conditions.IsMasterMode).GetMethod(nameof(Conditions.IsMasterMode.CanShowItemDropInUI), BindingFlags.Instance | BindingFlags.Public);
                _getShowMasterItemInAwakenedMod = new Hook(target2, (GetShowDetour)Show);

                MethodInfo target3 = typeof(Conditions.IsMasterMode).GetMethod(nameof(Conditions.IsMasterMode.GetConditionDescription), BindingFlags.Instance | BindingFlags.Public);
                _getDescriptionMasterItemInAwakenedMod = new Hook(target3, (GetDescriptionDetour)Description);
            }

            public override void Unload()
            {
                _getDropMasterItemInAwakenedMod?.Dispose();
                _getDropMasterItemInAwakenedMod = null;
                _getShowMasterItemInAwakenedMod?.Dispose();
                _getShowMasterItemInAwakenedMod = null;
                _getDescriptionMasterItemInAwakenedMod?.Dispose();
                _getDescriptionMasterItemInAwakenedMod = null;
            }

            private delegate bool Orig_GetDropSettings(Conditions.IsMasterMode self, DropAttemptInfo info);
            private delegate bool GetDropDetour(Orig_GetDropSettings orig, Conditions.IsMasterMode self, DropAttemptInfo info);

            private delegate bool Orig_GetShowSettings(Conditions.IsMasterMode self);
            private delegate bool GetShowDetour(Orig_GetShowSettings orig, Conditions.IsMasterMode self);

            private delegate string Orig_GetDescriptionSettings(Conditions.IsMasterMode self);
            private delegate string GetDescriptionDetour(Orig_GetDescriptionSettings orig, Conditions.IsMasterMode self);

            private bool Drop(Orig_GetDropSettings orig, Conditions.IsMasterMode self, DropAttemptInfo info)
            {
                bool originalResult = orig(self, info);
                if (Main.masterMode || MyWorld.awakenedMode)
                {
                    return true;
                }
                return originalResult;
            }
            private bool Show(Orig_GetShowSettings orig, Conditions.IsMasterMode self)
            {
                bool originalResult = orig(self);
                if (Main.masterMode || MyWorld.awakenedMode)
                {
                    return true;
                }
                return originalResult;
            }
            private string Description(Orig_GetDescriptionSettings orig, Conditions.IsMasterMode self)
            {
                string originalResult = orig(self);
                if (Main.masterMode || MyWorld.awakenedMode)
                {
                    return $"{originalResult} {ModContent.GetInstance<EALocalization>().BossString1} {ModContent.GetInstance<EALocalization>().AwakenedModeActive2}";
                }
                return originalResult;
            }
        }
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
        public class DropExpert : IItemDropRuleCondition
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
        public class DropAwakened : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (Main.expertMode || MyWorld.awakenedMode)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI()
            {
                if (Main.expertMode || MyWorld.awakenedMode)
                {
                    return true;
                }
                return false;
            }
            public string GetConditionDescription() => null;
        }
        public class DropNormal : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (Main.expertMode == false)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI()
            {
                if (Main.expertMode == false)
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
        public class DropRobe : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (Main.rand.Next(10) == 0)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        public class DropArmor : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (Main.rand.Next(5) == 0)
                {
                    return true;
                }
                return false;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
        //public class DropFruit : IItemDropRuleCondition
        //{
        //    //public bool CanDrop(DropAttemptInfo info)
        //    //{
        //    //    if (info.player.GetModPlayer<MyPlayer>().extraAccSlot == false && MyWorld.awakenedMode) return true;
        //    //    return false;
        //    //}
        //    //public bool CanShowItemDropInUI()
        //    //{
        //    //    if (Main.LocalPlayer.GetModPlayer<MyPlayer>().extraAccSlot && MyWorld.awakenedMode) return false;
        //    //    return true;
        //    //}
        //    //public string GetConditionDescription() => "Выпадает лишь один раз";
        //}
    }
}