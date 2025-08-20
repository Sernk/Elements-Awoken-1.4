using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class Mjolnir : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 32;
            Item.width = 32;
            Item.damage = 46;
            Item.knockBack = 10f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Throwing;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<MjolnirP>();
            Item.shootSpeed = 28f;
        }
    }
}