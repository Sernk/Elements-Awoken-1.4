using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class EndlessAbyssBlaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 28;
            Item.damage = 300;
            Item.knockBack = 2;
            Item.useTime = 90;
            Item.useAnimation = 90;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item92;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shoot = ModContent.ProjectileType<BlackholeCreator>();
            Item.shootSpeed = 20f;
        }
    }
}