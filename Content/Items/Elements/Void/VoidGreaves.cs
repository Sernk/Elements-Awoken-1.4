using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.defense = 22;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
            float speedBoost = 0f;
            speedBoost = (player.statLifeMax2 - player.statLifeMax2) / 5;
            player.moveSpeed += speedBoost;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 14);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}