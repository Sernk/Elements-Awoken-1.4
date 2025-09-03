using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Carapace
{
    public class CarapaceCannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 10;
            Item.knockBack = 7;
            Item.UseSound = SoundID.Item61;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.rare = 0;
            Item.shoot = ProjectileType<CarapaceBall>();
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}