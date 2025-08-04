using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{  
    public class EyeballStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 52;
            Item.knockBack = 3;
            Item.mana = 10;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<EyeballMinion>();
            Item.shootSpeed = 7f; 
        }
    }
}