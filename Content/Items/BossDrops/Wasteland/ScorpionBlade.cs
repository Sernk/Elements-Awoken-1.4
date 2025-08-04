using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Wasteland
{
    public class ScorpionBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;           
            Item.damage = 28;
            Item.useTime = 32;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useAnimation = 32;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 200);
        }
    }
}