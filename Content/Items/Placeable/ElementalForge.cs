using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class ElementalForge : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 2;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Crafting.ElementalForge>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Furnace, 1);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddIngredient(ItemID.Cloud, 4);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 4);
            recipe.AddIngredient(ItemID.WaterBucket, 4);
            recipe.AddIngredient(ItemID.EbonstoneBlock, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.Furnace, 1);
            recipe1.AddRecipeGroup(EARecipeGroups.SandGroup, 4);
            recipe1.AddIngredient(ItemID.Torch, 4);
            recipe1.AddIngredient(ItemID.Cloud, 4);
            recipe1.AddRecipeGroup(EARecipeGroups.IceGroup, 4);
            recipe1.AddIngredient(ItemID.WaterBucket, 4);
            recipe1.AddIngredient(ItemID.CrimstoneBlock, 4);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
    }
}