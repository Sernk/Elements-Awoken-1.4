using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class VoidLeviathanBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.LeviLoot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidLeviathanHeart>(), 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AmuletOfDestruction>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidLeviathanMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidLeviathanTrophy>(), 10));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AbyssalMatter>()));
            itemLoot.Add(_AwakenedMode);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}
