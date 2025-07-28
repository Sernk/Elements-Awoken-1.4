using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Materials
{
    public class VoiditeBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ModContent.RarityType<Rarity12>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoiditeOre>(), 4);
            recipe.AddIngredient(ModContent.ItemType<VoidAshes>(), 1);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}