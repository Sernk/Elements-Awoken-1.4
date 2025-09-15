using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.TileBuffs
{
    public class StatueBuffRanipla : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.25f;
            player.GetDamage(DamageClass.Summon) *= 1.15f;

            Player.jumpHeight = (int)(Player.jumpHeight * 0.75f);
            player.wingTimeMax = (int)(player.wingTimeMax * 0.75f);
        }
    }
}