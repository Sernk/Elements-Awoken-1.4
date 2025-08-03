using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class VioletEdge : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 62;
            Item.mana = 6;
            Item.knockBack = 2.25f;
            Item.reuseDelay = 16;
            Item.useAnimation = 18;
            Item.useTime = 6;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item66;
            Item.shoot = ModContent.ProjectileType<VioletEdgeBall>();
            Item.shootSpeed = 12f;
        }
    }
}
