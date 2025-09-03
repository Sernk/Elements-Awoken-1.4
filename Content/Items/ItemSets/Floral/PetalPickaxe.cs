using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Floral
{
    public class PetalPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 60;
            Item.damage = 5;
            Item.knockBack = 4.5f;
            Item.pick = 70;
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Petal>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}