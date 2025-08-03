using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class Magmarox : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 50;
            Item.knockBack = 2;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<MagmaroxRock>();
            Item.shootSpeed = 12f;
        }
    }
}