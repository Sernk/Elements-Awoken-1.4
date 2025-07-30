using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Ancient.Xernon
{
    public class LamentI : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 38;
            Item.mana = 9;
            Item.knockBack = 2;
            Item.useStyle = 5;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<LamentBall>();
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MysticGemstone>(), 1);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}