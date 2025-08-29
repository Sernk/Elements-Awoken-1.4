using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class Stormsky : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 10;
            Item.useAnimation = 16;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.mana = 10;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Feather3>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}