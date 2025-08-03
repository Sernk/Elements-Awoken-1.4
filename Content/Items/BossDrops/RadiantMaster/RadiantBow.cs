using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.RadiantMaster
{
    public class RadiantBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 64;
            Item.damage = 360;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.UseSound = SoundID.Item5;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<Rarity13>();
            Item.shoot = 10;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ProjectileType<RadiantStarBow>();
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(15);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}