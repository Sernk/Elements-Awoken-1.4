using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Floral
{
    public class BlossomBoomer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.damage = 15;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<BlossomBoomerP>();
            Item.shootSpeed = 6.5f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ModContent.ItemType<Petal>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}