using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class AstralBarrage : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 65;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item12;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<AstralRound>();
            Item.useAmmo = ItemID.FallenStar;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(3, 6);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 0.8f, perturbedSpeed.Y * 0.8f, ModContent.ProjectileType<AstralStar>(), damage, knockback, player.whoAmI);
            }
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ProjectileID.FallingStar, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddIngredient(ItemID.StarCannon, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 75)
                return false;
            return true;
        }
    }
}
