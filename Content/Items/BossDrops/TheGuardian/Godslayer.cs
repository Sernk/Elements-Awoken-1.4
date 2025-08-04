using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheGuardian
{
    public class Godslayer : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Melee;
            Item.width = 70;
            Item.height = 70;
            Item.useTime = 16;
            Item.useTurn = true;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GodslayerStrike>();
            Item.shootSpeed = 12f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}