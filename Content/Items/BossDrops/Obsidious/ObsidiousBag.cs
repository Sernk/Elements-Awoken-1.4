using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.EAUtilities;
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
            var _NewItem = new LeadingConditionRule(new EAIDRC.DropSlot());
            var _NewItem2 = new LeadingConditionRule(new EAIDRC.DropSlot2());
            var _NewItem3 = new LeadingConditionRule(new EAIDRC.DropSlot3());
            var _NewItem4 = new LeadingConditionRule(new EAIDRC.DropSlot4());

            itemLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.ObsiLoot]));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SacredCrystal>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousTrophy>(), 10));

            var RobeDrop = new LeadingConditionRule(new EAIDRC.DropRobe());
            RobeDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousMask>()));
            RobeDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousRobes>()));
            RobeDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ObsidiousPants>()));
            itemLoot.Add(RobeDrop);

            _NewItem.OnSuccess(ItemDropRule.Common(IDropSettings.ObsiItemId1, chanceDenominator: IDropSettings.ObsiChanceDenominator1, minimumDropped: IDropSettings.ObsiMinimumDropped1, maximumDropped: IDropSettings.ObsiMaximumDropped1));
            itemLoot.Add(_NewItem);
            _NewItem2.OnSuccess(ItemDropRule.Common(IDropSettings.ObsiItemId2, chanceDenominator: IDropSettings.ObsiChanceDenominator2, minimumDropped: IDropSettings.ObsiMinimumDropped2, maximumDropped: IDropSettings.ObsiMaximumDropped2));
            itemLoot.Add(_NewItem2);
            _NewItem3.OnSuccess(ItemDropRule.Common(IDropSettings.ObsiItemId3, chanceDenominator: IDropSettings.ObsiChanceDenominator3, minimumDropped: IDropSettings.ObsiMinimumDropped3, maximumDropped: IDropSettings.ObsiMaximumDropped3));
            itemLoot.Add(_NewItem3);
            _NewItem4.OnSuccess(ItemDropRule.Common(IDropSettings.ObsiItemId4, chanceDenominator: IDropSettings.ObsiChanceDenominator4, minimumDropped: IDropSettings.ObsiMinimumDropped4, maximumDropped: IDropSettings.ObsiMaximumDropped4));
            itemLoot.Add(_NewItem4);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}