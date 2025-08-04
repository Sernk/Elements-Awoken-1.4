using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class Shockstorm : ModItem
    {    
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 213;
            Item.knockBack = 2.25f;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.mana = 6;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<ShockstormPortal>();
            Item.shootSpeed = 9f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ShockstormPortal>()] >= 2)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, type, 0, knockback, player.whoAmI, 0f, damage);
            return false;
        }
    }
}