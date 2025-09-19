using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class NeptunesRay : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;          
            Item.damage = 24;
            Item.knockBack = 2;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = 1;
            Item.mana = 5;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<NeptuneRay>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Seashell, 6);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ItemID.Starfish, 3);
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}