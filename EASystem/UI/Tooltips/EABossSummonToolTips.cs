using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips
{
    public class EABossSummonToolTips : GlobalItem
    {
        public bool AwakenedSummonItem = false;
        public bool IsSummonBoss = true;
        public static bool TimeToSummon = false;
        public static int TimeToSummonNextUses = 0;

        public EABossSummonToolTips()
        {
            IsSummonBoss = true;
            AwakenedSummonItem = false;
            TimeToSummon = false;
            TimeToSummonNextUses = 0;
        }
        public override bool InstancePerEntity {  get { return true; } }
        public override bool CanUseItem(Item item, Player player)
        {
            if (EAList.BossSummonList.Contains(item.type))
            {
                foreach (NPC npc in Main.npc) { if (npc.active && EAList.BossName.Contains(npc.type)) return false; }
                if (MyWorld.awakenedMode) { }
                else item.consumable = true;
            }
            return true;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            string Use = "";
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && EAList.BossName.Contains(npc.type))
                {
                    Use = $"{EALocalization.IsSummon} {Lang.GetNPCNameValue(npc.type)}";
                    break;
                }
            }
            if (MyWorld.awakenedMode && AwakenedSummonItem)
            {
                tooltips.Add(new TooltipLine(Mod, "Elements Awoken:AwakenedTip", EALocalization.AwakenedSummonItem) { OverrideColor = new Color?(new Color(220, 50, Main.DiscoB)) }); //OverrideColor = new Color?(new Color(225, 21, 170))
                if (IsSummonBoss) { tooltips.Add(new TooltipLine(Mod, "Elements Awoken:AwakenedTip", Use) { OverrideColor = new Color?(new Color(220, 50, Main.DiscoB)) }); }
            }
            else if (!MyWorld.awakenedMode && AwakenedSummonItem)
            {
                tooltips.Add(new TooltipLine(Mod, "Elements Awoken:AwakenedTip", EALocalization.AwakenedSummonItem) { OverrideColor = new Color(128, 128, 128) });
                if (IsSummonBoss) { tooltips.Add(new TooltipLine(Mod, "Elements Awoken:AwakenedTip", Use) { OverrideColor = new Color(128, 128, 128) }); }
            }
        }
    }
}