using ElementsAwoken.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Summon
{
    public class LightningRod : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 30;
            Item.mana = 15;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item20;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<EnergyStorm>();
            Item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<EnergyStorm>(), damage, knockback, Main.myPlayer, 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}