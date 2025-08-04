using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Wasteland
{  
    public class ChitinStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;          
            Item.damage = 18;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<ScorpionMinion>();
            Item.shootSpeed = 7f; 
        }
    }
}