using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Buildmonger
{
    public class SonicArm : ModItem
    {
        public int shootTimer = 120;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.sonicArm = true;
        }
    }
}
