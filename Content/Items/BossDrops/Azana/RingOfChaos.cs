using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class RingOfChaos : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.accessory = true;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.chaosRing = true;

            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;

            player.GetDamage(DamageClass.Magic) *= 1.05f;
            player.GetDamage(DamageClass.Melee) *= 1.05f;
            player.GetDamage(DamageClass.Ranged) *= 1.05f;
            player.GetDamage(DamageClass.Throwing) *= 1.05f;
            player.GetDamage(DamageClass.Summon) *= 1.05f;
        }
    }
}