using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Blightfire
{
    public class Deteriorator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.damage = 60;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.reuseDelay = 16;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 11;
            Item.shoot = ModContent.ProjectileType<DeterioratorKnife>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Drip, player.position);

            float numberProjectiles = 6;
            float rotation = MathHelper.ToRadians(7);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 5f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Blightfire>(), 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}