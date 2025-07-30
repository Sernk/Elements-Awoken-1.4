using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Ancient.Krecheus
{
    public class AtaxiaII : ModItem
    {
        private int attackNum = 0;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 51;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<AtaxiaBall>();
            Item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            attackNum++;
            if (attackNum >= 4)
            {
                int orbitalCount = 3;
                for (int l = 0; l < orbitalCount; l++)
                {
                    int distance = Main.rand.Next(360);
                    Projectile orbital = Main.projectile[Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<AtaxiaCrystal>(), damage, 0f, 0, l * distance, Main.rand.Next(3))];
                    orbital.localAI[0] = 50;
                }
                attackNum = 0;
            }
            if (Main.rand.Next(3) == 0)
            {
                int numberProjectiles = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AtaxiaBlade>(), damage, knockback, player.whoAmI)];
                    proj.scale *= 1.3f;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AtaxiaI>(), 1);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}