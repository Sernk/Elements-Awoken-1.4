using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.SpawnrateBanners
{
    public class HavocBannerItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 3;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<HavocBanner>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ModContent.ItemType<HavocPotion>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DeathwishFlame>(), 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}