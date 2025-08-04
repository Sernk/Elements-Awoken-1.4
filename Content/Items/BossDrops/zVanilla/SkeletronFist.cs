using ElementsAwoken.Content.Projectiles.Flails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    class SkeletronFist : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.width = 30;
            Item.height = 10;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 2;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 7.5f;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<SkeletronFistP>();
            Item.shootSpeed = 15.1f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
        }
    }
}