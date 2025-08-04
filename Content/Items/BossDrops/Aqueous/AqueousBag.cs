using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Aqueous
{
    public class AqueousBag : ModItem, IDropSettings
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.AquLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AqueousMask>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AqueousTrophy>(), 10));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.AquItemId1, chanceDenominator: IDropSettings.AquChanceDenominator1, minimumDropped: IDropSettings.AquMinimumDropped1, maximumDropped: IDropSettings.AquMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.AquItemId2, chanceDenominator: IDropSettings.AquChanceDenominator2, minimumDropped: IDropSettings.AquMinimumDropped2, maximumDropped: IDropSettings.AquMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.AquItemId3, chanceDenominator: IDropSettings.AquChanceDenominator3, minimumDropped: IDropSettings.AquMinimumDropped3, maximumDropped: IDropSettings.AquMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.AquItemId4, chanceDenominator: IDropSettings.AquChanceDenominator4, minimumDropped: IDropSettings.AquMinimumDropped4, maximumDropped: IDropSettings.AquMaximumDropped4));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}