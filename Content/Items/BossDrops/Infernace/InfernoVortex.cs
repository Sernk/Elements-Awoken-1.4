using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Infernace
{
    public class InfernoVortex : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;      
            Item.damage = 32;
            Item.mana = 5;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<SpinningFlame>();
            Item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            int max = 6;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SpinningFlame>()] >= max)
            {
                return false;
            }
            else return true;
        }
    }
}