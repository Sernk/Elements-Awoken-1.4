using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Permafrost
{
    public class SoulOfTheFrost : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 46, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.10f;
            player.GetDamage(DamageClass.Throwing) *= 1.10f;
            player.GetDamage(DamageClass.Ranged) *= 1.10f;
            player.GetDamage(DamageClass.Magic) *= 1.10f;
            player.GetDamage(DamageClass.Summon) *= 1.10f;
            player.GetArmorPenetration(DamageClass.Generic) += 10;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Chilled] = true;
        }
    }
}