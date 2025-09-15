using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Tiles;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class ItemMagnetTile : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 5;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<ItemMagnet>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 16);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 6);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 12);
            recipe.AddIngredient(ModContent.ItemType<Transistor>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
