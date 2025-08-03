using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    public class ScourgeFighterRocketLauncher : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 19;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.shootSpeed = 6f;
            Item.shoot = ProjectileID.RocketI;
            Item.useAmmo = AmmoID.Rocket;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.RocketI)
            {
                type = ModContent.ProjectileType<ScourgeRocketFriendly>();
            }
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(8));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}