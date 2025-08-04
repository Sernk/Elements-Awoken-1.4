using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class Fireblast : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 86;
            Item.mana = 6;
            Item.reuseDelay = 16;
            Item.useAnimation = 15;
            Item.useTime = 5;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.knockBack = 2.25f;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item66;
            Item.shoot = ModContent.ProjectileType<Projectiles.Fireblast>();
            Item.shootSpeed = 12f;
        }
    }
}