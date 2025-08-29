using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class ThorsHammer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.damage = 50;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Throwing;
            Item.height = 68;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<ThorsHammerP>();
            Item.shootSpeed = 10f;
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