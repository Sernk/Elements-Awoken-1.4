using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class Ultramarine : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 67;
            Item.knockBack = 3.5f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item12;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<UltramarineBeam>();
        }
    }
}