using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Thrown
{
    public class Dictionary : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8f;
            Item.damage = 36;
            Item.maxStack = 9999;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 0, 8, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<DictionaryP>();
            Item.shootSpeed = 10f;
        }
    }
}