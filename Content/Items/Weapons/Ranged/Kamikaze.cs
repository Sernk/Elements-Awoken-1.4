using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class Kamikaze : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 62;
            Item.knockBack = 3.5f;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item61;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = 7;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.RocketI;
            Item.useAmmo = AmmoID.Rocket;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new Vector2(speed.X, speed.Y);
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
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, -2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteShotbow, 1);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
