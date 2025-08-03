using ElementsAwoken.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class TerreneScepter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 40;
            Item.knockBack = 7.5f;
            Item.mana = 15;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 1;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<TerreneMortar>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int num154 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            Projectile.NewProjectile(source, (float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, type, damage, knockback, Main.myPlayer, 0f, 0f);
            player.UpdateMaxTurrets();
            return false;
        }
    }
}