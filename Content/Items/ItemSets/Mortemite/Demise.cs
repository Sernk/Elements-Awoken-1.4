using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Mortemite
{
    public class Demise : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 111;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
            Item.staff[Item.type] = true;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2.25f;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item66;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MortemiteBlade>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(6));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MortemiteDust>(), 50);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}