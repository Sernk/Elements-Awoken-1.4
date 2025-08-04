using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Infernace
{
    public class InfernaceBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 4;
            Item.expert = true;
        }
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Treasure Bag");
            //Tooltip.SetDefault("Right Click to open");
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [..ListItems.InfeLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireHeart>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernaceMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernaceTrophy>(), 10));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.InfItemId1, chanceDenominator: IDropSettings.InfChanceDenominator1, minimumDropped: IDropSettings.InfMinimumDropped1, maximumDropped: IDropSettings.InfMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.InfItemId2, chanceDenominator: IDropSettings.InfChanceDenominator2, minimumDropped: IDropSettings.InfMinimumDropped2, maximumDropped: IDropSettings.InfMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.InfItemId3, chanceDenominator: IDropSettings.InfChanceDenominator3, minimumDropped: IDropSettings.InfMinimumDropped3, maximumDropped: IDropSettings.InfMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.InfItemId4, chanceDenominator: IDropSettings.InfChanceDenominator4, minimumDropped: IDropSettings.InfMinimumDropped4, maximumDropped: IDropSettings.InfMaximumDropped4));
        }
    }
}