using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class TheEater : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.channel = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 54;
            Item.height = 28;
            Item.scale = 1.1f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1.05f;
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<MiniEaterOfSouls>();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}