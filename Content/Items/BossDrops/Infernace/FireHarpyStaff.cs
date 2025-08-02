using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.Infernace
{  
    public class FireHarpyStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.damage = 34;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 3;
            Item.shoot = ProjectileType<HearthMinion>();
            Item.shootSpeed = 7f; 
        }
    }
}