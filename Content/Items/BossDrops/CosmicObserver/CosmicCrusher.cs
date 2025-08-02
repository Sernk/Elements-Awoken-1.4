using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    public class CosmicCrusher : ModItem
    {
        public override void SetDefaults()
        {          
            Item.width = 60;   
            Item.height = 60;
            Item.damage = 43;
            Item.knockBack = 6;
            Item.useTime = 48;   
            Item.useAnimation = 48;
            Item.useStyle = 1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;  
            Item.shoot = ModContent.ProjectileType<CosmicCrusherSpawner>();
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, type, damage, knockBack, Main.myPlayer, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CosmicShard>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
