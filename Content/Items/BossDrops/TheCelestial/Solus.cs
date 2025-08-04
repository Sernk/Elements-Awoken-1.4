using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{
    public class Solus : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 70;
            Item.knockBack = 18;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<SolusP>();
            Item.shootSpeed = 5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(7);
            float speed = 5f;
            float xSpeed = player.direction == 1 ? speed : -speed;
            position += Vector2.Normalize(new Vector2(xSpeed, 3f)) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, position.X, position.Y, xSpeed, 3f, type, damage, knockback, player.whoAmI, -(i * 2));
            }
            return false;
        }
    }
}
