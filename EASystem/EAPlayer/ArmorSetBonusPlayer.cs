using Terraria.GameInput;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EAPlayer;

public class ArmorSetBonusPlayer : ModPlayer
{
    public bool SetBonus;

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (ElementsAwoken.ASBT.Current) SetBonus = true;
        else SetBonus = false;
    }
}