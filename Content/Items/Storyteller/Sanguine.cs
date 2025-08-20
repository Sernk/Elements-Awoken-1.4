using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class Sanguine : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 18;
            Item.knockBack = 2;
            Item.mana = 6;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<BloodshotP>();
            Item.shootSpeed = 3f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(12));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            return true;
        }
    }
}
