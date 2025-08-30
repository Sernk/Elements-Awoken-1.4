using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    public class InfinityTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 130;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 28;
            Item.crit = 10;
            Item.height = 30;
            Item.useTime = 5;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.UseSound = SoundID.Item103;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.VoidTentacle>();
            Item.shootSpeed = 17f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 velocity = new Vector2(speed.X, speed.Y).SafeNormalize(-Vector2.UnitY);
            Vector2 randomVel = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
            velocity = (velocity * 4f + randomVel).SafeNormalize(-Vector2.UnitY) * Item.shootSpeed;
            float randAi0 = Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                randAi0 *= -1f;
            }
            float randAi1 = Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                randAi1 *= -1f;
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, randAi0, randAi1);
            return false;
        }
    }
}