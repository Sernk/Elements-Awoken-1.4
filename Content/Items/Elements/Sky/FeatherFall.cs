using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class FeatherFall : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 48;
            Item.height = 64;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<Feather1>();
            }
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
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