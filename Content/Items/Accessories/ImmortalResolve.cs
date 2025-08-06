using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class ImmortalResolve : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.immortalResolve = true;
            player.statLifeMax2 += 50;
            if (player.velocity.Y == 0f && player.velocity.X == 0f)
            {
                player.lifeRegen += 8;
            }
            else
            {
                player.lifeRegen += 4;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddIngredient(ItemID.LifeCrystal, 10);
            recipe.AddIngredient(ItemID.LifeFruit, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}