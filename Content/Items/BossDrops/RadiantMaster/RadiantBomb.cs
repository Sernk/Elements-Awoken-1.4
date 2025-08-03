using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.RadiantMaster
{
    public class RadiantBomb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.damage = 380;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<Rarity13>();
            Item.shoot = ProjectileType<RadiantBombP>();
            Item.shootSpeed = 14f;
        }
    }
}