using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class VoidLeviathanBag : ModItem, IDropSettings
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.LeviLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidLeviathanHeart>(), 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AmuletOfDestruction>()));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.LeviItemId1, chanceDenominator: IDropSettings.LeviChanceDenominator1, minimumDropped: IDropSettings.LeviMinimumDropped1, maximumDropped: IDropSettings.LeviMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.LeviItemId2, chanceDenominator: IDropSettings.LeviChanceDenominator2, minimumDropped: IDropSettings.LeviMinimumDropped2, maximumDropped: IDropSettings.LeviMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.LeviItemId3, chanceDenominator: IDropSettings.LeviChanceDenominator3, minimumDropped: IDropSettings.LeviMinimumDropped3, maximumDropped: IDropSettings.LeviMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.LeviItemId4, chanceDenominator: IDropSettings.LeviChanceDenominator4, minimumDropped: IDropSettings.LeviMinimumDropped4, maximumDropped: IDropSettings.LeviMaximumDropped4));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidLeviathanMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidLeviathanTrophy>(), 10));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}