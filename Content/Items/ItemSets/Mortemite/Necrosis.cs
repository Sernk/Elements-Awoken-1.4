using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Mortemite
{
    public class Necrosis : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 75;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 6;
            Item.useStyle = 1;
            Item.useTime = 6;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.width = 18;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<NecrosisP>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(20));
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