using ElementsAwoken.EASystem;
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.TCelLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCelestialTrophy>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CelestialsMask>(), 10));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CelestialCrown>(), 10));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.TCelItemId1, chanceDenominator: IDropSettings.TCelChanceDenominator1, minimumDropped: IDropSettings.TCelMinimumDropped1, maximumDropped: IDropSettings.TCelMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TCelItemId2, chanceDenominator: IDropSettings.TCelChanceDenominator2, minimumDropped: IDropSettings.TCelMinimumDropped2, maximumDropped: IDropSettings.TCelMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TCelItemId3, chanceDenominator: IDropSettings.TCelChanceDenominator3, minimumDropped: IDropSettings.TCelMinimumDropped3, maximumDropped: IDropSettings.TCelMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TCelItemId4, chanceDenominator: IDropSettings.TCelChanceDenominator4, minimumDropped: IDropSettings.TCelMinimumDropped4, maximumDropped: IDropSettings.TCelMaximumDropped4));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CelestialFlame>()));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}