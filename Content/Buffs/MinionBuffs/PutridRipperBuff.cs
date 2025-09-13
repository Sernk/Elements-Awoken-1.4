using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class PutridRipperBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PutridRipper>()] > 0) modPlayer.putridRipper = true;
            if (!modPlayer.putridRipper) { player.DelBuff(buffIndex); buffIndex--; }
            else player.buffTime[buffIndex] = 18000;
        }
    }
}