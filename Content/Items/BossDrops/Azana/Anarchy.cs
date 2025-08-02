using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class Anarchy : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.damage = 200;
            Item.knockBack = 2.5f;
            Item.useStyle = 5;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<AnarchyP>();
        }
    }
}