using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Stellarium
{
    public class StellariumBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 64;
            Item.damage = 100;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.UseSound = SoundID.Item5;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == 1) type = ModContent.ProjectileType<StellarArrow>();
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 7;
            Vector2 vector14 = new(speed.X, speed.Y);
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                int num125 = Projectile.NewProjectile(source, vector2.X + vector15.X, vector2.Y + vector15.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num125].noDropItem = true;
            }
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