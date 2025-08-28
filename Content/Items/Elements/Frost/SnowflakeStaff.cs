using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    public class SnowflakeStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 53;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 13;
            Item.width = 66;
            Item.height = 66;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;      
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item46;
            Item.shoot = ModContent.ProjectileType<Projectiles.Snowflake>();
            Item.shootSpeed = 3f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.MagicMissile);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}