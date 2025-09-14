using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Stellarium
{
    public class StellariumScepter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 257;
            Item.knockBack = 4f;
            Item.mana = 9;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<StellariumBolt>();
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, (int)(damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StellariumBar>(), 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
