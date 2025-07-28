using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Materials
{
    public class Microcontroller : ModItem
    {
        // T6
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 80, 0);
            Item.rare = 7;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 2);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 3);
            recipe.AddIngredient(ModContent.ItemType<RoyalScale>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
