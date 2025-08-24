using ElementsAwoken.Content.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Mounts
{
    public class CrowBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<CrowMount>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
