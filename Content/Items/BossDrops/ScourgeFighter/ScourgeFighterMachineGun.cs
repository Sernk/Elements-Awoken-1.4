using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    public class ScourgeFighterMachineGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 28;
            Item.damage = 30;
            Item.knockBack = 3.75f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 2;
            Item.reuseDelay = 10;
            Item.useAnimation = 6;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item31;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = 97;
        }
    }
}