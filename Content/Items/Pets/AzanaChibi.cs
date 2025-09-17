using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Pets
{
    public class AzanaChibi : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.width = 18;
            Item.height = 20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = 1;
            Item.useTime = 12;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 0, 0, 5);
            Item.rare = 0;
            Item.shoot = ModContent.ProjectileType<ChaosTomatoP>();
            Item.shootSpeed = 4f;
        }
    }
}