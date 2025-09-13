using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    [AutoloadEquip(EquipType.Body)]
    public class ComfyShirt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 2, 0);
            Item.rare = 1;
            Item.defense = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}