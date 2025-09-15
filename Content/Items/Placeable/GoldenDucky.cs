using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class GoldenDucky : ModItem
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
            Item.createTile = ModContent.TileType<Tiles.GoldenDucky>();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Golden Ducky");
            // Tooltip.SetDefault("It's cute");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}