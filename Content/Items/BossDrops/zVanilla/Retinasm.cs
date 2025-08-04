using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class Retinasm : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Code2);
            Item.useStyle = 5;
            Item.damage = 58;
            Item.width = 16;
            Item.height = 16;
            Item.rare = 4;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = 541;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<RetinasmP>();
        }
    }
}