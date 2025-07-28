using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories.Emblems
{
    public class ThrowerEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) += 0.15f;
        }
    }
}