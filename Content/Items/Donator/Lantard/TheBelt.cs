using ElementsAwoken.Content.Projectiles.Whips;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    public class TheBelt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 11;
            Item.damage = 10;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 4;
            Item.useStyle = 5;
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<TheBeltP>();
            Item.shootSpeed = 15f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddRecipeGroup("IronBar", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, ai3);
            return false;
        }
    }
}
