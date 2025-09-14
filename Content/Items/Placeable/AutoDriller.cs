using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class AutoDriller : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.rare = 4;       
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<Tiles.AutoDriller>();
        }
        public override bool CanUseItem(Player player)
        {
            PlayerUtils modPlayer = player.GetModPlayer<PlayerUtils>();
            modPlayer.placingAutoDriller = 4;
            return base.CanUseItem(player);
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddRecipeGroup("ElementsAwoken:GoldBar", 15);
            recipe.AddIngredient(null, "Stardust", 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
