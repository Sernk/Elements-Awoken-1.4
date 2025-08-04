using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers
{
    public class TemplesCrystal : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 86;
            Item.knockBack = 4;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item12;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<TempleBeam>();
            Item.shootSpeed = 24f;
        }
    }
}