using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.EASystem.UI
{
    public class HeartsPlayers : ModPlayer
    {
        public int chaosHeartLife;
        public int voidHeartLife;
        public int shieldLife;
        public override void SaveData(TagCompound tag)
        {
            tag["chaosHeartLife"] = chaosHeartLife;
            tag["voidHeartLife"] = voidHeartLife;
            tag["voidHeartLife"] = shieldLife;
        }
        public override void LoadData(TagCompound tag)
        {
            chaosHeartLife = tag.GetInt("chaosHeartLife");
            voidHeartLife = tag.GetInt("voidHeartLife");
            shieldLife = tag.GetInt("shieldLife");
        }
    }
}