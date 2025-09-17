using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
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
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.batteryEnergy += 1750;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LunarEnergyHarnesser>(), 2);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<ChaoticFlare>(), 4);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}