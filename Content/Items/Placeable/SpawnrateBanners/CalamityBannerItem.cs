using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.SpawnrateBanners
{
    public class CalamityBannerItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<CalamityBanner>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ModContent.ItemType<CalamityPotion>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DeathwishFlame>(), 30);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}