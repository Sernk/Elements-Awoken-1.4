using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class StatMirror : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.rare = 2;       
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<Tiles.StatMirror>();
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mirror of Insight");
            // Tooltip.SetDefault("Mirror, mirror on the wall, who is the strongest of them all?\nShows the players stat boosts");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 15);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
