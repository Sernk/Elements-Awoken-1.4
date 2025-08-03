using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Permafrost
{
    public class Snowdrift : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 68;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 46, 0, 0);
            Item.rare = 7;
            Item.mana = 5;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SnowdriftBomb>();
            Item.shootSpeed = 14f;
        }
    }
}