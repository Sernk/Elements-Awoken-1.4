using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Summon
{
    public class Exliture : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;      
            Item.damage = 12;
            Item.knockBack = 5;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Summon;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<ExlitureEye>();
            Item.shootSpeed = 4f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Lens, 6);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddRecipeGroup(EARecipeGroups.SilverSword);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
