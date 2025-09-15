using ElementsAwoken.Content.Tiles.Statues;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.Statues
{
    public class ThatOneJuicyOrangeStatue : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ArmorStatue);
            Item.createTile = ModContent.TileType<ThatOneJuicyOrange>();
			Item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}