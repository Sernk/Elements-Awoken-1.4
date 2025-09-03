using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Floral
{
    public class PetalHamaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 60;
            Item.damage = 10;
            Item.axe = 30;
            Item.hammer = 65;
            Item.knockBack = 4.5f;
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;       
            Item.useTurn = true;
            Item.autoReuse = true;
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