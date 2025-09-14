using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Stellarium
{
    public class StellariumHamaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 64;
            Item.damage = 78;
            Item.axe = 36;
            Item.hammer = 150;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useStyle = 1;
            Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
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