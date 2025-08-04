using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheGuardian
{
    public class GuardianBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 99;
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.TGuaLoot]));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGuadianMask>(), 10));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.TGuaItemId1, chanceDenominator: IDropSettings.TGuaChanceDenominator1, minimumDropped: IDropSettings.TGuaMinimumDropped1, maximumDropped: IDropSettings.TGuaMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TGuaItemId2, chanceDenominator: IDropSettings.TGuaChanceDenominator2, minimumDropped: IDropSettings.TGuaMinimumDropped2, maximumDropped: IDropSettings.TGuaMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TGuaItemId3, chanceDenominator: IDropSettings.TGuaChanceDenominator3, minimumDropped: IDropSettings.TGuaMinimumDropped3, maximumDropped: IDropSettings.TGuaMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.TGuaItemId4, chanceDenominator: IDropSettings.TGuaChanceDenominator4, minimumDropped: IDropSettings.TGuaMinimumDropped4, maximumDropped: IDropSettings.TGuaMaximumDropped4));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheGuardianTrophy>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FieryCore>()));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}