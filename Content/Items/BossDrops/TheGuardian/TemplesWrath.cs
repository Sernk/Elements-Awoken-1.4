using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheGuardian
{
    public class TemplesWrath : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 48;
            Item.height = 64;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TemplesWrathArrow>();
            Item.shootSpeed = 26f;
            Item.useAmmo = 40;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .60f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            int addPosition = Main.rand.Next(-30, 8);
            Projectile.NewProjectile(source, position.X + addPosition, position.Y + addPosition, speed.X, speed.Y, ModContent.ProjectileType<TemplesWrathArrow>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            if (Main.rand.Next(5) == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<TemplesWrathSword>(), 70, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
    }
}