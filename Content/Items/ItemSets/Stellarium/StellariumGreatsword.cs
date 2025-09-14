using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Stellarium
{
    public class StellariumGreatsword : ModItem
    {
        public override void SetDefaults()
        {          
            Item.width = 60;   
            Item.height = 60;
            Item.damage = 160;
            Item.knockBack = 6;
            Item.useTime = 32;   
            Item.useAnimation = 32;
            Item.useStyle = 1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;    
            Item.UseSound = SoundID.Item1;  
            Item.shoot = ModContent.ProjectileType<StellarPortal>();
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y - 500, 0f, 0f, ModContent.ProjectileType<StellarPortal>(), damage, knockback, Main.myPlayer, 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StellariumBar>(), 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}