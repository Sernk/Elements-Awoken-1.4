using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Spears;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class SandySpear : ModItem
    {
        public override void SetDefaults()
        {       
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.useTime = 22;  
            Item.knockBack = 3.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.height = 60;
            Item.width = 60;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<SandySpearP>();
            Item.shootSpeed = 6f;
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