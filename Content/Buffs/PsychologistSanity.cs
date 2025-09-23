using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class PsychologistSanity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            modPlayer.sanityRegen += 2;
            modPlayer.AddSanityRegen(2, "Counselling Session");
        }
    }
}   