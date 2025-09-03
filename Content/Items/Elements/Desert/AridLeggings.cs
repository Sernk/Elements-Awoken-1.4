using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Legs)]
    public class AridLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.2f;
            player.pickSpeed -= 0.5f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DesertEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 20);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 8);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}