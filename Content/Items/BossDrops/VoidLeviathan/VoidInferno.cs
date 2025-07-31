using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class VoidInferno : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.damage = 267;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.useStyle = 5;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<VoidInfernoP>();
        }
    }
}