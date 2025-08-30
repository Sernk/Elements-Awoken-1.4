using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    public class VoidBlaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 123;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 92;
            Item.height = 28;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1.75f;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<VoidBlast>();
            Item.shootSpeed = 24f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}