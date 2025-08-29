using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Legs)]
    public class EmpyreanLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.defense = 12;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.2f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 7);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 18);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}