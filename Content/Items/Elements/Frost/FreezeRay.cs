using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    public class FreezeRay : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 20;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.damage = 60;
            Item.knockBack = 5f;
            Item.mana = 4;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item12;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 7;
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<FreezeBeam>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.HeatRay);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}