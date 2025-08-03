using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    public class SignalBooster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 40;
            Item.mana = 6;
            Item.knockBack = 6f;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<SignalBoost>();
            Item.shootSpeed = 12f;
        }
    }
}