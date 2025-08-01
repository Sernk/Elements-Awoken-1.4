using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class Shimmerspark : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 34;
            Item.damage = 1750;
            Item.knockBack = 12f;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 75, 0, 0);
            Item.rare = ModContent.RarityType<Rarity14>();
            Item.shoot = ModContent.ProjectileType<ShimmersparkHeld>();
            Item.shootSpeed = 20f;
        }
    }
}