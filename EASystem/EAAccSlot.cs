using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EAAccSlot : ModAccessorySlot
    {
        public override string Name => "EASlot";
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => checkItem.accessory;
        public override bool DrawDyeSlot => true;
        public override bool DrawFunctionalSlot => true;
        public override bool IsEnabled()
        {
            if (Player == null || !Player.active) return false;        
            MyPlayer modPlayer = Player.GetModPlayer<MyPlayer>();
            return modPlayer != null && modPlayer.extraAccSlot && MyWorld.awakenedMode;
        }
        public override bool IsVisibleWhenNotEnabled()
        {
            if (Player == null || !Player.active) return false;
            MyPlayer modPlayer = Player.GetModPlayer<MyPlayer>();
            return modPlayer != null && modPlayer.extraAccSlot;
        }
    }
}