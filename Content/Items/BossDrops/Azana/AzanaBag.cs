using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class AzanaBag : ModItem, IDropSettings
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, [..ListItems.AzaLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RingOfChaos>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AzanaMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AzanaTrophy>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EntropicCoating>(),5));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DiscordantOre>(), minimumDropped: 15, maximumDropped:20));

            itemLoot.Add(ItemDropRule.Common(IDropSettings.AzaItemId1, chanceDenominator: IDropSettings.AzaChanceDenominator1, minimumDropped: IDropSettings.AzaMinimumDropped1, maximumDropped: IDropSettings.AzaMaximumDropped1));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.AzaItemId2, chanceDenominator: IDropSettings.AzaChanceDenominator2, minimumDropped: IDropSettings.AzaMinimumDropped2, maximumDropped: IDropSettings.AzaMaximumDropped2));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.AzaItemId3, chanceDenominator: IDropSettings.AzaChanceDenominator3, minimumDropped: IDropSettings.AzaMinimumDropped3, maximumDropped: IDropSettings.AzaMaximumDropped3));
            itemLoot.Add(ItemDropRule.Common(IDropSettings.AzaItemId4, chanceDenominator: IDropSettings.AzaChanceDenominator4, minimumDropped: IDropSettings.AzaMinimumDropped4, maximumDropped: IDropSettings.AzaMaximumDropped4));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}