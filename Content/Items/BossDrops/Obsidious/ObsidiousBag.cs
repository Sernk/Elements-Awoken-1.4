using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class ObsidiousBag : ModItem, IDropSettings
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 7;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. ListItems.ObsiLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SacredCrystal>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousRobes>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousPants>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousTrophy>(), 10));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.ObsiItemId1, chanceDenominator: IDropSettings.ObsiChanceDenominator1, minimumDropped: IDropSettings.ObsiMinimumDropped1, maximumDropped: IDropSettings.ObsiMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.ObsiItemId2, chanceDenominator: IDropSettings.ObsiChanceDenominator2, minimumDropped: IDropSettings.ObsiMinimumDropped2, maximumDropped: IDropSettings.ObsiMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.ObsiItemId3, chanceDenominator: IDropSettings.ObsiChanceDenominator3, minimumDropped: IDropSettings.ObsiMinimumDropped3, maximumDropped: IDropSettings.ObsiMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.ObsiItemId4, chanceDenominator: IDropSettings.ObsiChanceDenominator4, minimumDropped: IDropSettings.ObsiMinimumDropped4, maximumDropped: IDropSettings.ObsiMaximumDropped4));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}