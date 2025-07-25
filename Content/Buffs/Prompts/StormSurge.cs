using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.NPCs;

namespace ElementsAwoken.Content.Buffs.Prompts
{
    public class StormSurge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Surge");
            // Description.SetDefault("Waternados sprout from the ground\nRain happens more frequently\nDefeat Aqueous to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}