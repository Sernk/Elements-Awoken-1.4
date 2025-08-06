using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    public class VolcanoxBag : ModItem, IDropSettings
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
            var _NewItem = new LeadingConditionRule(new EAIDRC.DropSlot());
            var _NewItem2 = new LeadingConditionRule(new EAIDRC.DropSlot2());
            var _NewItem3 = new LeadingConditionRule(new EAIDRC.DropSlot3());
            var _NewItem4 = new LeadingConditionRule(new EAIDRC.DropSlot4());

            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.VolLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CharredInsignia>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VolcanoxMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VolcanoxTrophy>(), 10));

            _NewItem.OnSuccess(ItemDropRule.Common(IDropSettings.VolItemId1, chanceDenominator: IDropSettings.VolChanceDenominator1, minimumDropped: IDropSettings.VolMinimumDropped1, maximumDropped: IDropSettings.VolMaximumDropped1));
            itemLoot.Add(_NewItem);
            _NewItem2.OnSuccess(ItemDropRule.Common(IDropSettings.VolItemId2, chanceDenominator: IDropSettings.VolChanceDenominator2, minimumDropped: IDropSettings.VolMinimumDropped2, maximumDropped: IDropSettings.VolMaximumDropped2));
            itemLoot.Add(_NewItem2);
            _NewItem3.OnSuccess(ItemDropRule.Common(IDropSettings.VolItemId3, chanceDenominator: IDropSettings.VolChanceDenominator3, minimumDropped: IDropSettings.VolMinimumDropped3, maximumDropped: IDropSettings.VolMaximumDropped3));
            itemLoot.Add(_NewItem3);
            _NewItem4.OnSuccess(ItemDropRule.Common(IDropSettings.VolItemId4, chanceDenominator: IDropSettings.VolChanceDenominator4, minimumDropped: IDropSettings.VolMinimumDropped4, maximumDropped: IDropSettings.VolMaximumDropped4));
            itemLoot.Add(_NewItem4);

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pyroplasm>(), minimumDropped:10, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VolcanicStone>(), minimumDropped:10, maximumDropped: 25));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}