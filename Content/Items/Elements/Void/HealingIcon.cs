using ElementsAwoken.Content.Items.Consumable.Potions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    public class HealingIcon : ModItem
    {
        public float healTimer = 7f;

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item4;
            Item.maxStack = 1;
            Item.width = 26;
            Item.height = 44;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            return;
        }
        public override void UpdateInventory(Player player)
        {
            if (healTimer > 0f)
            {
                healTimer -= 1f;
            }
            if (healTimer == 0f && player.statLife <= 250)
            {
                player.statLife += 2;
                player.HealEffect(2);
                healTimer = 15f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.RegenerationPotion, 1);
            recipe.AddIngredient(ItemID.LesserHealingPotion, 1);
            recipe.AddIngredient(ItemID.HealingPotion, 1);
            recipe.AddIngredient(ItemID.GreaterHealingPotion, 1);
            recipe.AddIngredient(ItemID.SuperHealingPotion, 1);
            recipe.AddIngredient(ModContent.ItemType<EpicHealingPotion>(), 1);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}