using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Buildmonger
{
    public class ForgedIronBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.rare = 2;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forged Iron Bar");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 1);
            recipe.AddIngredient(ItemID.Chain, 15);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
