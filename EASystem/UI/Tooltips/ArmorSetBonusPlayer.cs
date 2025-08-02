using Terraria.GameInput;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips;

public class ArmorSetBonusPlayer : ModPlayer
{
    public bool SetBonus;

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (ElementsAwoken.ASBT.JustPressed)
        {
            SetBonus = !SetBonus; 
        }
    }
}