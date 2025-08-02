using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class PurgeRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 550;
            Item.knockBack = 4;
            Item.useTime = 12;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.crit = 25;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item33;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<PurgeBeam>(), Item.damage, 0, player.whoAmI, 0f, 0f);
            return false;
        }
    }
}
