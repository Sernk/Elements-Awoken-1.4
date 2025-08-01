using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Aqueous
{
    public class HighTide : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;  
            Item.height = 38;
            Item.damage = 70;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 7;
            Item.useStyle = 1;
            Item.useTime = 7;
            Item.knockBack = 7.5f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Throwing;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 8;
            Item.shoot = ModContent.ProjectileType<HighTideP>();
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 2f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<HighTideP>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}