using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Permafrost
{  
    public class Flurry : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 46, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<IcicleMinion>();
            Item.shootSpeed = 7f; 
        }
    }
}