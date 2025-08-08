using Luminance.Core.MenuInfoUI;
using System.Collections.Generic;

namespace ElementsAwoken.EASystem.UI.Tooltips
{
    public class WorldTooltips : InfoUIManager
    {
        public override IEnumerable<WorldInfoIcon> GetWorldInfoIcons()
        {
            //I used to love playing Dota2
            yield return new WorldInfoIcon("ElementsAwoken/icon_small", "Mods.ElementsAwoken.AwakenedMode", Dota2 =>
            {
                if (!Dota2.TryGetHeaderData<MyWorld>(out var tag))
                    return false;
                return tag.ContainsKey("awakenedMode");
            }, 1);
            yield return new WorldInfoIcon("ElementsAwoken/icon_small_grayscale", "Mods.ElementsAwoken.AwakenedModeNotActive", Dota =>
            {
                if (!Dota.TryGetHeaderData<MyWorld>(out var tag))
                    return false;
                return tag.ContainsKey("awakenedModeNoActive");
            }, 1);
        }
    }
}