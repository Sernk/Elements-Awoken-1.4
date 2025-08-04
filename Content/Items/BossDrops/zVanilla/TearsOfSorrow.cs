using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class TearsOfSorrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;
            Item.knockBack = 2f;
            Item.damage = 15;
            Item.UseSound = SoundID.Item5;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<TearArrow>();
            }
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
