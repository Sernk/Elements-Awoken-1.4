using ElementsAwoken.Content.Items.BossDrops.Azana;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Materials
{
    public class LRM : ModItem
    {
        // T9
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = RarityType<Rarity13>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemType<DiscordantBar>(), 8);
            recipe.AddIngredient(ItemType<GoldWire>(), 6);
            recipe.AddTile(TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
        }
    }
}