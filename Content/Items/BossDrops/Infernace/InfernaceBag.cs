using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Infernace
{
    public class InfernaceBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 4;
            Item.expert = true;
        }
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Treasure Bag");
            //Tooltip.SetDefault("Right Click to open");
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.InfeLoot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireHeart>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernaceMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernaceTrophy>(), 10));
        }
    }
}