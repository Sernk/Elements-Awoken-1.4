using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    class Starthrower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 75;
            Item.mana = 6;
            Item.knockBack = 2.25f;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item43;
            Item.shoot = ModContent.ProjectileType<Projectiles.Star>();
            Item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}