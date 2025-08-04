using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class RegarothBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.expert = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 5;
        }
        public override bool CanRightClick()
        {
            return true;
        }
		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.RegLoot]));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StoneOfHope>()));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RegarothMask>(), 10));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RegarothTrophy>(), 10));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyWeaversHelm>(), 2));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyWeaversBreastplate>(), 2));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyWeaversLeggings>(), 2));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.RegItemId1, chanceDenominator: IDropSettings.RegChanceDenominator1, minimumDropped: IDropSettings.RegMinimumDropped1, maximumDropped: IDropSettings.RegMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.RegItemId2, chanceDenominator: IDropSettings.RegChanceDenominator2, minimumDropped: IDropSettings.RegMinimumDropped2, maximumDropped: IDropSettings.RegMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.RegItemId3, chanceDenominator: IDropSettings.RegChanceDenominator3, minimumDropped: IDropSettings.RegMinimumDropped3, maximumDropped: IDropSettings.RegMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.RegItemId4, chanceDenominator: IDropSettings.RegChanceDenominator4, minimumDropped: IDropSettings.RegMinimumDropped4, maximumDropped: IDropSettings.RegMaximumDropped4));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}