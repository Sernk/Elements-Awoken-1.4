using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Stellarium
{
    public class StellariumPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 85;
            Item.pick = 230;
            Item.knockBack = 4.5f;
            Item.useStyle = 1;
            Item.useTime = 8;
            Item.useAnimation = 11;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StellariumBar>(), 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}