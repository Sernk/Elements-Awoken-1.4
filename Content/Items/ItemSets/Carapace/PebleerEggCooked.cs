using ElementsAwoken.Content.Buffs.PotionBuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Carapace
{
    public class PebleerEggCooked : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.UseSound = SoundID.Item2;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = 0;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 36000;
            return;
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffType<HeartyMeal>(), 600);
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PebleerEgg>(), 1);
            recipe.AddTile(TileID.Campfire);
            recipe.Register();
        }
    }
}