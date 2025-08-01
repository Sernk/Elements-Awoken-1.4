using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class AncientsBag : ModItem
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
            var _AwakenedMode = new LeadingConditionRule(new IDRNC(IDRNC.BossType.AwakenedMode, true));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.AncLot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystallineLocket>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalAmalgamate>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientShard>(), minimumDropped:7, maximumDropped:12));

            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientsMask>(), 10));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientsTrophy>(), 10));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GiftOfTheArchaic>()));
            itemLoot.Add(_AwakenedMode);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}