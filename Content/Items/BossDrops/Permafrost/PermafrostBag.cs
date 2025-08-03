using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Permafrost
{
    public class PermafrostBag : ModItem
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
            var _AwakenedMode = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.PermLoot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfTheFrost>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PermafrostMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PermafrostTrophy>(), 10));

            _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IcyHeart>()));
            itemLoot.Add(_AwakenedMode);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}