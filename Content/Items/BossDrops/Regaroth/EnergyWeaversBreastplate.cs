using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    [AutoloadEquip(EquipType.Body)]
    public class EnergyWeaversBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.defense = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.manaCost *= 0.8f;
            player.wingTimeMax = (int)(player.wingTimeMax * 1.1f);
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed *= 1.1f;
            acceleration *= 1.1f;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling *= 1.1f;
            ascentWhenRising *= 1.5f;
            maxCanAscendMultiplier *= 1.5f;
            maxAscentMultiplier *= 1.1f;
            constantAscend *= 1.1f;
        }
    }
}