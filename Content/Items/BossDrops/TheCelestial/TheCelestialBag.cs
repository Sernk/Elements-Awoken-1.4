using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{
    public class TheCelestialBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 6;
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

            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.TCelLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCelestialTrophy>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CelestialsMask>(), 10));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CelestialCrown>(), 10));

            _NewItem.OnSuccess(ItemDropRule.Common(IDropSettings.TCelItemId1, chanceDenominator: IDropSettings.TCelChanceDenominator1, minimumDropped: IDropSettings.TCelMinimumDropped1, maximumDropped: IDropSettings.TCelMaximumDropped1));
            itemLoot.Add(_NewItem);
            _NewItem2.OnSuccess(ItemDropRule.Common(IDropSettings.TCelItemId2, chanceDenominator: IDropSettings.TCelChanceDenominator2, minimumDropped: IDropSettings.TCelMinimumDropped2, maximumDropped: IDropSettings.TCelMaximumDropped2));
            itemLoot.Add(_NewItem2);
            _NewItem3.OnSuccess(ItemDropRule.Common(IDropSettings.TCelItemId3, chanceDenominator: IDropSettings.TCelChanceDenominator3, minimumDropped: IDropSettings.TCelMinimumDropped3, maximumDropped: IDropSettings.TCelMaximumDropped3));
            itemLoot.Add(_NewItem3);
            _NewItem4.OnSuccess(ItemDropRule.Common(IDropSettings.TCelItemId4, chanceDenominator: IDropSettings.TCelChanceDenominator4, minimumDropped: IDropSettings.TCelMinimumDropped4, maximumDropped: IDropSettings.TCelMaximumDropped4));
            itemLoot.Add(_NewItem4);

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CelestialFlame>()));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}