using ElementsAwoken.Utilities;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips
{
    public class EABossSummon : GlobalItem
    {
        public bool AwakenedSummonItem = false;
        public EABossSummon()
        {
            AwakenedSummonItem = false;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (ListItems.BossSummonList.Contains(item.type))
            {
                if (MyWorld.awakenedMode) { }
                else
                {
                    if (item.stack == 1)
                    {
                        item.consumable = true;
                    }
                    else
                    {
                        item.stack--;
                    }
                }
            }
            return true;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            if (MyWorld.awakenedMode && AwakenedSummonItem)
            {
                tooltips.Add(new TooltipLine(Mod, "Elements Awoken:AwakenedTip", EALocalization.AwakenedSummonItem) { OverrideColor = new Color?(new Color(220, 50, Main.DiscoB)) }); //OverrideColor = new Color?(new Color(225, 21, 170))
            }
            else if (!MyWorld.awakenedMode && AwakenedSummonItem)
            {
                tooltips.Add(new TooltipLine(Mod, "Elements Awoken:AwakenedTip", EALocalization.AwakenedSummonItem) { OverrideColor = new Color(128, 128, 128) });
            }
        }
    }
}