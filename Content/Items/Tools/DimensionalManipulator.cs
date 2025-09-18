using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tools
{
    public class DimensionalManipulator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item60;
            Item.consumable = false;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.dayTime = !Main.dayTime;
                    Main.time = 0; //16220 for midnight 9000 for 10pm
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                }
            }
            else
            {
                ModContent.GetInstance<ElementsAwoken>().VoidTimerChangerUI.SetState(new EASystem.UI.DimensionalManipulatorUI());
            }
            return true;
        }
    }
}
