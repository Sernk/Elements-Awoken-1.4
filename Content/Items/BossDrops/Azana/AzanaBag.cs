using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class AzanaBag : ModItem
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
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.AzaLoot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RingOfChaos>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AzanaMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AzanaTrophy>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EntropicCoating>(),5));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DiscordantOre>(), minimumDropped: 15, maximumDropped:20));
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}