using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Yoyos;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    public class GlacialBlitz : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Code2);
            Item.damage = 50;
            Item.useStyle = 5;
            Item.width = 16;
            Item.height = 16;
            Item.rare = 7;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = 541;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<BlitzP>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}
