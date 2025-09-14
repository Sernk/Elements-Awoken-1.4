using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    [AutoloadEquip(EquipType.Body)]
    public class ToyBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;
            Item.defense = 7;
        }
        public override void UpdateEquip(Player player) => player.endurance += 0.04f;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
