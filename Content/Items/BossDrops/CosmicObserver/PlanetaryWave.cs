using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    public class PlanetaryWave : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 35;
            Item.knockBack = 2;
            Item.mana = 6;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.staff[Item.type] = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item8;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<PlanetaryWavePortal>();
            Item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PlanetaryWavePortal>()] != 0)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            Projectile.NewProjectile(source, player.Center.X, player.Center.Y - 70, 0f, 0f, ModContent.ProjectileType<PlanetaryWavePortal>(), damage, knockBack, player.whoAmI, speed.X, speed.Y);
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
 