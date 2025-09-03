using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Floral
{
    public class FlowerPower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;  
            Item.damage = 8;
            Item.mana = 3;
            Item.knockBack = 1;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<FlowerPowerProj>();
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(2,5);
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
            recipe.AddIngredient(ModContent.ItemType<Petal>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
