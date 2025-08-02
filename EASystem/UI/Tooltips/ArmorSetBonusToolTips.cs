using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips;
public class ArmorSetBonusToolTips : GlobalItem
{
    public bool IsHelmet;
    public bool IsCosmicalusVisor;
    public bool IsVoidWalkersVisage;
    public bool IsVoidWalkersHood;
    public bool IsVoidWalkersHelm;
    public bool IsVoidWalkersGreatmask;
    public ArmorSetBonusToolTips()
    {
        IsHelmet = false;
        IsCosmicalusVisor = false;
        IsVoidWalkersVisage = false;
        IsVoidWalkersHood = false;
        IsVoidWalkersHelm = false;
        IsVoidWalkersGreatmask = false;
    }
    public override bool InstancePerEntity
    {
        get
        {
            return true;
        }
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        var ASBTPlayer = Main.LocalPlayer.GetModPlayer<ArmorSetBonusPlayer>();
        var LEA = ModContent.GetInstance<EALocalization>();
        if (IsHelmet)
        {
            if (!ASBTPlayer.SetBonus)
            {
                tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.SetBonusToolTips) { OverrideColor = new Color(128, 128, 128) });
            }
            else
            {
                if (IsCosmicalusVisor)
                {
                    tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.CosmicalusVisor) { OverrideColor = new Color(128, 128, 128) });
                }
                if (IsVoidWalkersVisage)
                {
                    tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersVisage) { OverrideColor = new Color(128, 128, 128) });
                }
                if (IsVoidWalkersHood)
                {
                    tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersHood) { OverrideColor = new Color(128, 128, 128) });
                }
                if (IsVoidWalkersHelm)
                {
                    tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersHelm) { OverrideColor = new Color(128, 128, 128) });
                }
                if (IsVoidWalkersGreatmask)
                {
                    tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersGreatmask) { OverrideColor = new Color(128, 128, 128) });
                }
            }
        }
    }
}