using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.VoidEventItems
{
    public class CastersCurse : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 118;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.mana = 6;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item13;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CastersCurseBoltBase>();
            Item.shootSpeed = 12f;
        }
    }
}