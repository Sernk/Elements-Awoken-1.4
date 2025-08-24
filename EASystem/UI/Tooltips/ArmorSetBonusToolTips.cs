using ElementsAwoken.Content.Items.BossDrops.CosmicObserver;
using ElementsAwoken.Content.Items.BossDrops.Regaroth;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.Donator.Aegida;
using ElementsAwoken.Content.Items.Donator.Buildmonger;
using ElementsAwoken.Content.Items.Donator.Crow;
using ElementsAwoken.Content.Items.Donator.Eoite;
using ElementsAwoken.Content.Items.Donator.Lantard;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips;
public class ArmorSetBonusToolTips : GlobalItem
{
    public bool IsHelmet;
    public ArmorSetBonusToolTips()
    {
        IsHelmet = false;
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
        Color Gray = new(128, 128, 128);
        var ASBTPlayer = Main.LocalPlayer.GetModPlayer<ArmorSetBonusPlayer>();
        var LEA = ModContent.GetInstance<EALocalization>();
        if (IsHelmet)
        {
            if (!ASBTPlayer.SetBonus)
            {
                tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.SetBonusToolTips) { OverrideColor = Gray });
            }
            else
            {
                if (item.type == ModContent.ItemType<CosmicalusVisor>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.CosmicalusVisor) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<VoidWalkersVisage>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersVisage) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<VoidWalkersHood>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersHood) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<VoidWalkersHelm>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersHelm) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<VoidWalkersGreatmask>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.VoidWalkersGreatmask) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<EnergyWeaversHelm>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.EnergyWeaversHelm) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<MechMask>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.MechSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<ForgedMask>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.ForgedSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<CrowsGreathelm>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.CrowsSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<EoitesHood>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.EoitesSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<AwokenWoodHelmet>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.AwokenWoodBonus) { OverrideColor = Gray });
            }
        }
    }
}