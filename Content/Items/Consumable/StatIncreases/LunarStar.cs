using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.StatIncreases
{
    public class LunarStar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.consumable = true;    
            Item.useStyle = 4;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item4;
            Item.rare = 10;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void UpdateInventory(Player player)
        {
            HeartsPlayers hearts = player.GetModPlayer<HeartsPlayers>();
            if (Item.favorited)
            {
                hearts.ManaBonus = true;
            }
            else
            {
                hearts.ManaBonus = false;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 25);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.LunarBar, 40);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            //if (ModLoader.GetMod("CalamityMod") == null) recipe.Register();
        }
    }
}
