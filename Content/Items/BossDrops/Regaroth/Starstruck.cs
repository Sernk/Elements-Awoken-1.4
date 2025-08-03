using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class Starstruck : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 64;
            Item.damage = 50;
            Item.knockBack = 5;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<StarstruckArrow>();
            Item.shootSpeed = 20f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<StarstruckArrow>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}