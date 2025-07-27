using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class EldritchHorror : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eldritch Horror");
            // Description.SetDefault("Your mind cannot comprehend its power...\nSanity is drained");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            modPlayer.sanityRegen = -5;
        }
    }
}