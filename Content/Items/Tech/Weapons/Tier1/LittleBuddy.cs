using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier1
{
    public class LittleBuddy : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 26;        
            Item.damage = 9;
            Item.knockBack = 3.5f;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 5;
            Item.GetGlobalItem<ItemEnergy>().energy = 2;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item92;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<ElectricBolt>();
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<ElectricBolt>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 10);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}