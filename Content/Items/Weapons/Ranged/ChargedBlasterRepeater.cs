using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class ChargedBlasterRepeater : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;
            Item.damage = 28;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item61;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 3;
            Item.shoot = 10;
            Item.shootSpeed = 14f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ModContent.ProjectileType<ExplosiveBolt>();
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(3));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 14);
            recipe.AddIngredient(ItemID.Obsidian, 4);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
