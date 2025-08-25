using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class Sandball : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.damage = 14;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item39;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.Sandball>();
            Item.shootSpeed = 13f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(450);
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}