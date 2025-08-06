using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class LightningCloud : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.lightningCloud = true;
            if (hideVisual)
            {
                modPlayer.lightningCloudHidden = true;
            }
        }
    }
}