using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class Frosthail : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 75;
            Item.mana = 6;
            Item.knockBack = 2.25f;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item120;
            Item.shoot = ModContent.ProjectileType<IceMist>();
            Item.shootSpeed = 8f;
        }
    }
}