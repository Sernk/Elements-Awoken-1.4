using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Prompts
{
    public class ScorpionBreakout : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scorpion Breakout");
            // Description.SetDefault("Scorpions infest terraria\nDefeat Wasteland to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}