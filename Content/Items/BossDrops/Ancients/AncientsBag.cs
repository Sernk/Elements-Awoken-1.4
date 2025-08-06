using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class AncientsBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 11;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            var _NewItem = new LeadingConditionRule(new EAIDRC.DropSlot());
            var _NewItem2 = new LeadingConditionRule(new EAIDRC.DropSlot2());
            var _NewItem3 = new LeadingConditionRule(new EAIDRC.DropSlot3());
            var _NewItem4 = new LeadingConditionRule(new EAIDRC.DropSlot4());

            AncSettings.AncDropSlotOne(ItemID.Zenith);

            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.AncLot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystallineLocket>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalAmalgamate>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientShard>(), minimumDropped:7, maximumDropped:12));

            _NewItem.OnSuccess(ItemDropRule.Common(IDropSettings.AncItemId1, chanceDenominator: IDropSettings.AncChanceDenominator1, minimumDropped: IDropSettings.AncMinimumDropped1, maximumDropped: IDropSettings.AncMaximumDropped1));
            itemLoot.Add(_NewItem);
            _NewItem2.OnSuccess(ItemDropRule.Common(IDropSettings.AncItemId2, chanceDenominator: IDropSettings.AncChanceDenominator2, minimumDropped: IDropSettings.AncMinimumDropped2, maximumDropped: IDropSettings.AncMaximumDropped2));
            itemLoot.Add(_NewItem2);
            _NewItem3.OnSuccess(ItemDropRule.Common(IDropSettings.AncItemId3, chanceDenominator: IDropSettings.AncChanceDenominator3, minimumDropped: IDropSettings.AncMinimumDropped3, maximumDropped: IDropSettings.AncMaximumDropped3));
            itemLoot.Add(_NewItem3);
            _NewItem4.OnSuccess(ItemDropRule.Common(IDropSettings.AncItemId4, chanceDenominator: IDropSettings.AncChanceDenominator4, minimumDropped: IDropSettings.AncMinimumDropped4, maximumDropped: IDropSettings.AncMaximumDropped4));
            itemLoot.Add(_NewItem4);
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientsMask>(), 10));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientsTrophy>(), 10));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GiftOfTheArchaic>()));
            itemLoot.Add(_AwakenedMode);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}