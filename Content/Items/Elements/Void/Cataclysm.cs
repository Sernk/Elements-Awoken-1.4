using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    public class Cataclysm : ModItem
    {
        public override void SetDefaults()
        {       
            Item.damage = 82;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = 5;
            Item.knockBack = 4.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 60;
            Item.width = 60;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.shoot = ModContent.ProjectileType<CataclysmP>();
            Item.shootSpeed = 11f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<CataclysmTip>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}