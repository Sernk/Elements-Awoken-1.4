using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Ancient.Izaris
{
    public class DisarrayI : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 24;
            Item.mana = 10;
            Item.knockBack = 2f;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<DisarrayEntity>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, type, damage, knockback, player.whoAmI, 0);
            return false;
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