using ElementsAwoken.EASystem;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class SanityRegenerationBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            // DisplayName.SetDefault("Sanity Regeneration");
            // Description.SetDefault("Increases sanity regeneration");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            modPlayer.sanityRegen += 3;
            modPlayer.AddSanityRegen(3, "Sanity Potion");
        }
    }
}   