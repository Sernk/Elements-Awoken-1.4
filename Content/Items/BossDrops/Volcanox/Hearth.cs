using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    public class Hearth : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 170;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 11;
            Item.mana = 5;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SpinningFlame>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            int numberProjectiles = 3;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                Projectile.NewProjectile(source, vector2.X, vector2.Y, 0, 0, ModContent.ProjectileType<HearthP>(), damage, 3f, i, 0f, 0f);
            }
            return false;
        }
    }
}