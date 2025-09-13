using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class Manashard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ItemID.CrystalShard, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}