using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class BabyShark : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 16;          
            Item.damage = 10;
            Item.knockBack = 1.05f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item10;
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<BabySharkP>();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
