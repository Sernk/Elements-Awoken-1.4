using ElementsAwoken.Content.Items.Essence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class BlazeFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;           
            Item.damage = 42;
            Item.knockBack = 2f;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = false;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.rare = 4;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)  
        {
            if(type == ProjectileID.WoodenArrowFriendly) type = ProjectileID.FireArrow;
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f); 
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}