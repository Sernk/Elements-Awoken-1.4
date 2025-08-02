using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    public class Demolecularizer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;
            Item.damage = 42;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            if (Main.rand.Next(6) == 0)
            {
                type = ModContent.ProjectileType<DemolecularizerLaser>();
                speed.X *= 2f;
                speed.Y *= 2f;
                damage = (int)(damage * 1.5f);
            }
            else
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<CosmicArrow>();
                    speed.X *= 1.5f;
                    speed.Y *= 1.5f;
                }
            }
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CosmicShard>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}