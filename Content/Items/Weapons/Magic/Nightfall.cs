using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class Nightfall : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.damage = 80;
            Item.mana = 18;
            Item.knockBack = 5;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.UseSound = SoundID.Item20;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<Nightball>();
            Item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, Main.MouseWorld.X + Main.rand.Next(-60, 60), Main.MouseWorld.Y + Main.rand.Next(-60, 60), 0, 0, ModContent.ProjectileType<Nightball>(), damage, knockback, Main.myPlayer, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}