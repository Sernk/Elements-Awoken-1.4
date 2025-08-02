using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class GleamOfAnnhialation : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 360;
            Item.knockBack = 2;
            Item.mana = 5;
            Item.useStyle = 5;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<AzanaNanoBolt>();
            Item.shootSpeed = 14f;
        }
    }
}