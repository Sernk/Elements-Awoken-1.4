using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    public class FatesFlame : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 38;
            Item.height = 18;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 11;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(2));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}