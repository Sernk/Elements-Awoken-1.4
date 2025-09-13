using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    public class VelvetShot : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 44;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = 40;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}