using ElementsAwoken.Content.Items.BossDrops.CosmicObserver;
using ElementsAwoken.Content.Items.BossDrops.Regaroth;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.Donator.Aegida;
using ElementsAwoken.Content.Items.Donator.Buildmonger;
using ElementsAwoken.Content.Items.Donator.Crow;
using ElementsAwoken.Content.Items.Donator.Eoite;
using ElementsAwoken.Content.Items.Donator.Lantard;
using ElementsAwoken.Content.Items.Donator.Superbaseball101;
using ElementsAwoken.Content.Items.Donator.YukkiKun;
using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Elemental;
using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips;

public class ArmorSetBonusToolTips : GlobalItem
{
    public bool IsHelmet;
    public List<int> Arid = [ModContent.ItemType<AridFalconHelm>(), ModContent.ItemType<AridHat>(), ModContent.ItemType<AridHeadgear>(), ModContent.ItemType<AridHood>(), ModContent.ItemType<AridWarriorMask>(),];
    public List<int> Empyrean = [ModContent.ItemType<EmpyreanMask>(), ModContent.ItemType<EmpyreanHat>(), ModContent.ItemType<EmpyreanHeadgear>(), ModContent.ItemType<EmpyreanHelmet>(), ModContent.ItemType<EmpyreanVisage>()];

    public ArmorSetBonusToolTips() {IsHelmet = false; }
    public override bool InstancePerEntity {get { return true; } }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        Color Gray = new(128, 128, 128);
        ArmorSetBonusPlayer ASBTPlayer = Main.LocalPlayer.GetModPlayer<ArmorSetBonusPlayer>();
        EALocalization LEA = ModContent.GetInstance<EALocalization>();
        if (IsHelmet)
        {
            List<string> keys = ElementsAwoken.ASBT.GetAssignedKeys();
            string keyName = keys.Count > 0 ? keys[0] : LEA.Unbound;
            string text = string.Format(LEA.SetBonusToolTips, keyName);
            if (!ASBTPlayer.SetBonus) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", text) { OverrideColor = Gray });
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
                if (item.type == ModContent.ItemType<AwokenWoodHelmet>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.AwokenWoodSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<FireDemonsHelm>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.FireDemonsSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<GelticConquerorHelmet>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.GelticConquerorSetBonus) { OverrideColor = Gray });
                if (Arid.Contains(item.type)) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.AridSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<ElementalMask>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.ElementalSetBonus) { OverrideColor = Gray });
                if (Empyrean.Contains(item.type)) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", EALocalization.EmpyreanSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<VoidHelmet>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", EALocalization.VoidSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<OceanicVisage>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.OceanicSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<DragonmailGreathelm>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.DragonmailGreathelmSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<DragonmailHood>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.DragonmailHoodSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<DragonmailMask>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.DragonmailMaskSetBonus) { OverrideColor = Gray });
                if (item.type == ModContent.ItemType<DragonmailVisage>()) tooltips.Add(new TooltipLine(Mod, "SetBonus:AwakenedTip", LEA.DragonmailVisageSetBonus) { OverrideColor = Gray });
            }
        }
    }
}