using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.EAUtilities;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Wasteland
{
    public class WastelandBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 2;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            var _NewItem = new LeadingConditionRule(new EAIDRC.DropSlot());
            var _NewItem2 = new LeadingConditionRule(new EAIDRC.DropSlot2());
            var _NewItem3 = new LeadingConditionRule(new EAIDRC.DropSlot3());
            var _NewItem4 = new LeadingConditionRule(new EAIDRC.DropSlot4());

            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.WasLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VenomSample>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WastelandMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WastelandTrophy>(), 10));

            _NewItem.OnSuccess(ItemDropRule.Common(IDropSettings.WasItemId1, chanceDenominator: IDropSettings.WasChanceDenominator1, minimumDropped: IDropSettings.WasMinimumDropped1, maximumDropped: IDropSettings.WasMaximumDropped1));
            itemLoot.Add(_NewItem);
            _NewItem2.OnSuccess(ItemDropRule.Common(IDropSettings.WasItemId2, chanceDenominator: IDropSettings.WasChanceDenominator2, minimumDropped: IDropSettings.WasMinimumDropped2, maximumDropped: IDropSettings.WasMaximumDropped2));
            itemLoot.Add(_NewItem2);
            _NewItem3.OnSuccess(ItemDropRule.Common(IDropSettings.WasItemId3, chanceDenominator: IDropSettings.WasChanceDenominator3, minimumDropped: IDropSettings.WasMinimumDropped3, maximumDropped: IDropSettings.WasMaximumDropped3));
            itemLoot.Add(_NewItem3);
            _NewItem4.OnSuccess(ItemDropRule.Common(IDropSettings.WasItemId4, chanceDenominator: IDropSettings.WasChanceDenominator4, minimumDropped: IDropSettings.WasMinimumDropped4, maximumDropped: IDropSettings.WasMaximumDropped4));
            itemLoot.Add(_NewItem4);
        }
    }
}