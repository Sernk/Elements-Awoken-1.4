using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Developer
{
    public class TimeGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;      
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.damage = 121;
            Item.knockBack = 3.5f;
            Item.useAnimation = 2;
            Item.useTime = 2;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.shootSpeed = 16f;
            Item.shoot = 10;
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (Main.rand.Next(3) == 0)
            {
                int numberProjectiles1 = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<TimeRocket>(), damage, knockback, player.whoAmI);
                }
            }
            int numberProjectiles2 = Main.rand.Next(3,5);
            for (int i = 0; i < numberProjectiles2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 1.1f, perturbedSpeed.Y * 1.1f, ProjectileType<TimecleaverRound>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.VortexBeater, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
