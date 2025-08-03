using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{  
    public class EnergyStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 42;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<EnergySpirit>();
            Item.shootSpeed = 7f; 
        }
    }
}