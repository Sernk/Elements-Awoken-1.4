using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.NPCs;

namespace ElementsAwoken.Content.Buffs.Prompts
{
    public class DarkenedSkies : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkened Skies");
            // Description.SetDefault("Lightning strikes from the sky\nStorms happens more frequently\nDefeat Regaroth to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}