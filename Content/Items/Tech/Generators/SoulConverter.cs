using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class SoulConverter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 1;
            Item.maxStack = 1;
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            energyPlayer.soulConverter = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 24);
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 12);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}