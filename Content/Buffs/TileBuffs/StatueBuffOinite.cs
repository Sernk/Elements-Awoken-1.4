using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.TileBuffs
{
    public class StatueBuffOinite : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().oiniteStatue = true;
        }
    }
}