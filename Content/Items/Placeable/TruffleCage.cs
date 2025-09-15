using ElementsAwoken.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class TruffleCage : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<TruffleCageTile>();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Truffle Worm Cage");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TruffleWorm);
            recipe.AddIngredient(ItemID.Terrarium);
            recipe.Register();
        }   
    }
}