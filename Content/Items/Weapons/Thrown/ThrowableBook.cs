using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Thrown
{
    public class ThrowableBook : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.knockBack = 2f;
            Item.damage = 14;
            Item.maxStack = 9999;
            Item.useAnimation = 13;
            Item.useTime = 13;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 0, 1, 0);
            Item.shoot = ModContent.ProjectileType<ThrowableBookP>();
            Item.shootSpeed = 8f;
        }
    }
}