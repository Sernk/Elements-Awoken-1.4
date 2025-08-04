using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class GreekFire : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 45;
            Item.knockBack = 2;
            Item.mana = 5;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item13;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
            Item.shoot = ModContent.ProjectileType<Projectiles.GreekFire>();
            Item.shootSpeed = 14f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Greek Fire");
            // Tooltip.SetDefault("Fires bouncing greek flames");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyWood, 30);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(2, 3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(5));
                perturbedSpeed *= Main.rand.NextFloat(0.8f, 1.1f);
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
