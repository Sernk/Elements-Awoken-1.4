using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class Katagina : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60;
            Item.height = 26;
            Item.useAnimation = 8;
            Item.useTime = 5;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.shoot = 10;
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                int num1 = Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage , knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddIngredient(ItemID.SDMG, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 50)
                return false;
            return true;
        }
    }
}
