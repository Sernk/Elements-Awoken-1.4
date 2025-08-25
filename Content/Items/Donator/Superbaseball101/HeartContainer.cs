using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Superbaseball101
{
    public class HeartContainer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 5;    
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.heartContainer = true;
            player.maxMinions += 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.PixieDust, 20);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.Register();
        }
    }
}