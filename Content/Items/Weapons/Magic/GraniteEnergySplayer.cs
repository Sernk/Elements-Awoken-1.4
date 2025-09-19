using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class GraniteEnergySplayer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 8;
            Item.mana = 4;
            Item.knockBack = 5;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item42;
            Item.shoot = ModContent.ProjectileType<GraniteEnergyShot>();
            Item.shootSpeed = 5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(3,7);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(35));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GraniteBlock, 20);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}