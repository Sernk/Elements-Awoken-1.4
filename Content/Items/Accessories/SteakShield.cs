using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]

    public class SteakShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;    
            Item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 4;
            player.aggro += 250;
        }
    }
}