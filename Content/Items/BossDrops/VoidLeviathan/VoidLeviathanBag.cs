using CalamityMod.Projectiles.DraedonsArsenal;
using ElementsAwoken.Utilities;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Loot;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class VoidLeviathanBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 99;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.expert = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("Right Click to open");
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            var _AwakenedMode = new LeadingConditionRule(new IDRNC(IDRNC.BossType.AwakenedMode, true));
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
