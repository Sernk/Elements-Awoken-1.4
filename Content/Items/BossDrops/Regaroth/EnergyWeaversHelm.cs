using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    [AutoloadEquip(EquipType.Head)]
    public class EnergyWeaversHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.defense = 21;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsEnergyWeaversHelm = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 7;
            player.GetCritChance(DamageClass.Melee) += 7;
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.GetCritChance(DamageClass.Throwing) += 7;

            float damageBonus = 1f;
            if (Main.dayTime)
            {
                damageBonus = MathHelper.Lerp(1.3f, 1f, MathHelper.Distance((float)Main.time, 27000) / 27000);
            }

            player.GetDamage(DamageClass.Melee) *= damageBonus;
            player.GetDamage(DamageClass.Magic) *= damageBonus;
            player.GetDamage(DamageClass.Ranged) *= damageBonus;
            player.GetDamage(DamageClass.Summon) *= damageBonus;
            player.GetDamage(DamageClass.Throwing) *= damageBonus;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<EnergyWeaversBreastplate>() && legs.type == ModContent.ItemType<EnergyWeaversLeggings>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().EnergyWeaversHelm;
            player.GetModPlayer<MyPlayer>().energyWeaverArmor = true;
        }
    }
}
