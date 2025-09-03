using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Carapace
{
    public class CarapaceSlicer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;  
            Item.height = 38;
            Item.damage = 11;
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.rare = 0;
            Item.shoot = ProjectileType<CarapaceSlicerP>();
            Item.shootSpeed = 7f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ProjectileType<CarapaceSlicerP>()] >= 2)
            {
                return false;
            }
            return true;
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