using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class TheDestroyer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 34;
            Item.damage = 60;
            Item.knockBack = 3.75f;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item15;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<TheDestroyerHeld>();
            Item.shootSpeed = 20f;
        }
    }
}