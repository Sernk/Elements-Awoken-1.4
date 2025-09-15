using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.TileBuffs
{
    public class StatueBuffBurst : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.15f;
            player.moveSpeed *= 0.85f;
        }
    }
}