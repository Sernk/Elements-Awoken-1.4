using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class ObsidiousBag : ModItem
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.ObsiLoot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SacredCrystal>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousRobes>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousPants>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiousTrophy>(), 10));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}