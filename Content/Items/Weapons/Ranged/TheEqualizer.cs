using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class TheEqualizer : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 39;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useAnimation = 4;
            Item.useTime = 4;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 8;
            Item.shootSpeed = 12f;
            Item.shoot = 10;
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 70f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(15));
    
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RoyalScale>(), 8);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddIngredient(ItemID.Gatligator, 1);
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(ItemID.ChainGun, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 40)
                return false;
            return true;
        }
    }
}