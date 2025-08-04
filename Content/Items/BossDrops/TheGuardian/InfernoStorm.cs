using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheGuardian
{
    public class InfernoStorm : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.mana = 12;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<InfernoCloud>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(source, vector2.X, vector2.Y, 0, 0, ModContent.ProjectileType<InfernoCloud>(), damage, 3f, i, 0f, 0f);
            return false;
        }
    }
}