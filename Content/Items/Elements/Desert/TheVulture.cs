using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class TheVulture : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 44;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = 40;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}