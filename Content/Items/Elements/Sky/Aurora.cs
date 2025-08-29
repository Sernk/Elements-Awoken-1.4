using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class Aurora : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 38;
            Item.height = 18;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.shoot = 10;
            Item.useAmmo = 97;
            Item.shootSpeed = 18f;
            Item.UseSound = SoundID.Item41;
            Item.autoReuse = true;
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