using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class Ghostbrand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.damage = 75;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.knockBack = 7;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 9;
            Item.UseSound = SoundID.Item103;
            Item.shoot = ModContent.ProjectileType<PoltercastP>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)  
        {
            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(ItemID.Keybrand, 1);
            recipe.AddIngredient(ModContent.ItemType<RoyalScale>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}