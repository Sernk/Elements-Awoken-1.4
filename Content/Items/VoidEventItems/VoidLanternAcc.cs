using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.VoidEventItems
{
    public class VoidLanternAcc : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 10;    
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual) => LightUp(player);
        public override void UpdateInventory(Player player) => LightUp(player);
        public static void LightUp(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.voidLantern = true;
            if (!MyWorld.voidInvasionUp) Lighting.AddLight(player.Center, 2f, 0.5f, 0.0f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 25);
            recipe.AddIngredient(ModContent.ItemType<Placeable.VoidStone.VoidStone>(), 50);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
