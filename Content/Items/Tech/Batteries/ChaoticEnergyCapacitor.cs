using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Batteries
{
    public class ChaoticEnergyCapacitor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
        }

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Chaotic Energy Capacitor");
            //Tooltip.SetDefault("Increases the players maximum energy by 1750");
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.batteryEnergy += 1750;
        }
        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(null, "LunarEnergyHarnesser", 2);
        //    recipe.AddIngredient(null, "DiscordantBar", 8);
        //    recipe.AddIngredient(null, "ChaoticFlare", 4);
        //    recipe.AddTile(null, "ChaoticCrucible");
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //}
    }
}
