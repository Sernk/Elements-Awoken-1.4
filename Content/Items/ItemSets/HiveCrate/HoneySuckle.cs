using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.HiveCrate
{
    public class Honeysuckle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
            player.manaRegen += 1;
            player.statLifeMax2 += 10;
            if (player.honeyWet)
            {
                player.AddBuff(BuffID.Honey, 2700);
            }
        }
    }
}