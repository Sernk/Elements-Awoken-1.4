using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Materials
{
    public class SiliconBoard : ModItem
    {
        // T4
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SandBlock, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 5);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}