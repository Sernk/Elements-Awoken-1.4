using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class FistsOfFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 32;
            Item.width = 32;
            Item.damage = 32;
            Item.knockBack = 9f;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 9;
            Item.useTime = 9;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<FistsOfFuryP>();
            Item.shootSpeed = 7f;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(1, 2);
            for (int k = 0; k < numberProjectiles; k++)
            {
                position += new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-15, 15));
                Projectile.NewProjectile(source, position.X, position.Y, speed.X * 0.4f, speed.Y * 0.4f, ModContent.ProjectileType<FistsOfFuryFire>(), damage, knockback, player.whoAmI, 0f, 0f);
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Fireblossom, 2);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}