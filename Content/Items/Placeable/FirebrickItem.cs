using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class FirebrickItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 0;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Firebrick>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 1);
            recipe.AddIngredient(ItemID.GrayBrick, 50);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}