using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class TheLauncher : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Code2);
            Item.damage = 45;
            Item.useStyle = 5;
            Item.width = 16;
            Item.height = 16;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = 541;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<TheLauncherP>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}