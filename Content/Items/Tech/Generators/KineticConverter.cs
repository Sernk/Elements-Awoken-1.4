using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class KineticConverter : ModItem
    {
        public int fuel = 0;
        public int producePowerCooldown = 0;

        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 4;
            Item.maxStack = 1;
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.kineticConverter = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFlight, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 12);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 12);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 4);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}