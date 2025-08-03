using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Infernace
{
    public class FlareSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;    
            Item.damage = 26;
            Item.knockBack = 5;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<FireBlade>();
            Item.shootSpeed = 10f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}
