using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    public class ChakramOfTheDuke : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;  
            Item.height = 38;
            Item.damage = 70;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 7;
            Item.useStyle = 1;
            Item.useTime = 7;
            Item.knockBack = 7.5f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Throwing;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8; 
            Item.shoot = ModContent.ProjectileType<DukeChakramProj>();
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}