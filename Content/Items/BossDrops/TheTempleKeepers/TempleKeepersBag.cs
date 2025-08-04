using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers
{
    public class TempleKeepersBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 5;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.TempLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Flare>()));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.TempItemId1, chanceDenominator: IDropSettings.TempChanceDenominator1, minimumDropped: IDropSettings.TempMinimumDropped1, maximumDropped: IDropSettings.TempMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TempItemId2, chanceDenominator: IDropSettings.TempChanceDenominator2, minimumDropped: IDropSettings.TempMinimumDropped2, maximumDropped: IDropSettings.TempMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TempItemId3, chanceDenominator: IDropSettings.TempChanceDenominator3, minimumDropped: IDropSettings.TempMinimumDropped3, maximumDropped: IDropSettings.TempMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TempItemId4, chanceDenominator: IDropSettings.TempChanceDenominator4, minimumDropped: IDropSettings.TempMinimumDropped4, maximumDropped: IDropSettings.TempMaximumDropped4));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WyrmHeart>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TempleFragment>()));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}