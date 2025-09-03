using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Floral
{
    public class PetalPopper : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 38;
            Item.height = 18;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.useAmmo = 97;
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = false;
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