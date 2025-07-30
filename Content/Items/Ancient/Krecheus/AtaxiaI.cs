using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Ancient.Krecheus
{
    public class AtaxiaI : ModItem
    {
        private int attackNum = 0;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 36;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<AtaxiaBall>();
            Item.shootSpeed = 6f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            attackNum++;
            if (attackNum >= 5)
            {
                int numberProjectiles = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AtaxiaBlade>(), damage, knockback, player.whoAmI);
                }
                attackNum = 0;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MysticGemstone>(), 1);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}