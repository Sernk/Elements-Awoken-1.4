using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Prompts
{
    public class Hypothermia : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            EAU.CanBeCleared(Type);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 0.95f;
        }
    }
}