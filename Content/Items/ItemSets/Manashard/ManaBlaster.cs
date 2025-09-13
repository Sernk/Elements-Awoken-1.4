using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Projectiles;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class ManaBlaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 34;        
            Item.damage = 48;
            Item.knockBack = 3.75f;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item11;
            Item.shoot = 10;
            Item.shootSpeed = 12f;
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<ManaBolt>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}