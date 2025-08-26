using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class InfernoLance : ModItem
    {
        public override void SetDefaults()
        {       
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = 5;
            Item.knockBack = 4.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.height = 60;
            Item.width = 60;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<InfernoLanceP>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X/2, speed.Y/2, ModContent.ProjectileType<FirelashFlames>(), damage/2, knockback, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.FireEssence, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}