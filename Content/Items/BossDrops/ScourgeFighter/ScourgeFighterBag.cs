using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    /// <summary>
    /// No Item = ModContent.ItemType<ScourgeFighterMask>()
    /// fixes this
    /// </summary>
    public class ScourgeFighterBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
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

            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.ScoLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeDrive>()));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterTrophy>(), 10));

            _NewItem.OnSuccess(ItemDropRule.Common(IDropSettings.ScoItemId1, chanceDenominator: IDropSettings.ScoChanceDenominator1, minimumDropped: IDropSettings.ScoMinimumDropped1, maximumDropped: IDropSettings.ScoMaximumDropped1));
            itemLoot.Add(_NewItem);
            _NewItem2.OnSuccess(ItemDropRule.Common(IDropSettings.ScoItemId2, chanceDenominator: IDropSettings.ScoChanceDenominator2, minimumDropped: IDropSettings.ScoMinimumDropped2, maximumDropped: IDropSettings.ScoMaximumDropped2));
            itemLoot.Add(_NewItem2);
            _NewItem3.OnSuccess(ItemDropRule.Common(IDropSettings.ScoItemId3, chanceDenominator: IDropSettings.ScoChanceDenominator3, minimumDropped: IDropSettings.ScoMinimumDropped3, maximumDropped: IDropSettings.ScoMaximumDropped3));
            itemLoot.Add(_NewItem3);
            _NewItem4.OnSuccess(ItemDropRule.Common(IDropSettings.ScoItemId4, chanceDenominator: IDropSettings.ScoChanceDenominator4, minimumDropped: IDropSettings.ScoMinimumDropped4, maximumDropped: IDropSettings.ScoMaximumDropped4));
            itemLoot.Add(_NewItem4);

            IItemDropRule weaponDrop = new LeadingConditionRule(new EAIDRC.ScourgeLootCondition());
            weaponDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterRocketLauncher>()));
            weaponDrop.OnSuccess(ItemDropRule.Common(ItemID.RocketI, 1, 50, 150));
            itemLoot.Add(weaponDrop);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}