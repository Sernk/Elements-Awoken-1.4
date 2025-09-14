using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    public class ToyBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;       
            Item.damage = 23;
            Item.knockBack = 2f;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item5;
            Item.shoot = 10;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ProjectileType<SuctionArrow>();
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}