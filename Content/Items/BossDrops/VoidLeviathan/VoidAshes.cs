using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class VoidAshes : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(12);
            recipe.AddIngredient(ModContent.ItemType<VoidLeviathanHeart>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}