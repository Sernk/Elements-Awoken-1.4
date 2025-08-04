using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{
    public class CelestialInferno : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 68;
            Item.knockBack = 2;
            Item.mana = 5;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item8;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<CelestialInfernoSpin>();
            Item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CelestialInfernoSpin>()] > 12)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockBack, player.whoAmI, 0.0f, Main.rand.Next(4));
            return false;
        }
    }
}