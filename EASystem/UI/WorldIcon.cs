using Luminance.Core.Hooking;
using Luminance.Core.MenuInfoUI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Collections.Generic;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI
{
    public class WorldIcon : InfoUIManager
    {
        public override IEnumerable<WorldInfoIcon> GetWorldInfoIcons()
        {
            //I used to love playing Dota2
            yield return new WorldInfoIcon("ElementsAwoken/icon_small", "Mods.ElementsAwoken.AwakenedMode", Dota2 => { if (!Dota2.TryGetHeaderData<MyWorld>(out var tag)) { return false; } return tag.ContainsKey("awakenedMode"); }, 1);
            yield return new WorldInfoIcon("ElementsAwoken/Extra/icon_small_grayscale", "Mods.ElementsAwoken.AwakenedModeNotActive", Dota => { if (!Dota.TryGetHeaderData<MyWorld>(out var tag)) { return false; } return tag.ContainsKey("awakenedModeNoActive"); }, 1);
        }
    }
    public class MasterMode : ILEditProvider
    {
        public override void Subscribe(ManagedILEdit edit) => IL_UIWorldCreation.AddWorldDifficultyOptions += edit.SubscriptionWrapper;

        public override void Unsubscribe(ManagedILEdit edit) => IL_UIWorldCreation.AddWorldDifficultyOptions -= edit.SubscriptionWrapper;

        public override void PerformEdit(ILContext il, ManagedILEdit edit)
        {
            var c = new ILCursor(il);

            if (!c.TryGotoNext(MoveType.After, x => x.MatchLdstr("UI.WorldDescriptionMaster"))) return;
               
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(() => DifficultyManagementSystem.DisableDifficultyModes ? "Mods.ElementsAwoken.IsHard" : "UI.WorldDescriptionMaster");
        }
        public class DifficultyManagementSystem : ModSystem
        {
            public static bool DisableDifficultyModes
            {
                get;
                set;
            } = true;
        }
    }
}