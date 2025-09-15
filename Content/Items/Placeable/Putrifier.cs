using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class Putrifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 8;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<Tiles.Crafting.Putrifier>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
            recipe.AddIngredient(ModContent.ItemType<SunFragment>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }   
    }
}