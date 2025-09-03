using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Spears;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    public class GlacierBreaker : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 60;
            Item.width = 60;
            Item.damage = 42;
            Item.knockBack = 4.75f;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 7;
            Item.shoot = ModContent.ProjectileType<GlacierBreakerP>();
            Item.shootSpeed = 14f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<IcicleProj>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}