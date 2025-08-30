using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    public class EyeofAnnihilation : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Code2);
            Item.useStyle = 5;
            Item.damage = 167;
            Item.width = 16;
            Item.height = 16;
            Item.rare = 11;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = 541;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<TheEyeP>();
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