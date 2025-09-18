using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.VoidEventItems
{
    public class CrimsonShade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;    
            Item.damage = 69;
            Item.mana = 6;
            Item.knockBack = 2;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item13;
            Item.shoot = ModContent.ProjectileType<CrimsonShadeP>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadeScale>(), 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
