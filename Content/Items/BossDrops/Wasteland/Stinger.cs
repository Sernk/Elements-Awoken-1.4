using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Wasteland
{
    public class Stinger : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;             
            Item.damage = 14;
            Item.knockBack = 2;
            Item.mana = 16;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.reuseDelay = 24;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 2;
            Item.shoot = ModContent.ProjectileType<WastelandStingerFriendly>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item17, player.position);
            return true;
        }
    }
}
