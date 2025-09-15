using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.SpawnrateBanners
{
    public class ChaosBannerItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 15, 0);
            Item.rare = 6;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<ChaosBanner>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ModContent.ItemType<ChaosPotion>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DeathwishFlame>(), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}